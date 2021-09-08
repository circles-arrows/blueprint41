using Blueprint41.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static Blueprint41.Core.RelationshipPersistenceProvider;
using persistence = Blueprint41.Neo4j.Persistence;

namespace Blueprint41
{
    public abstract class Transaction : IDisposable
    {
        protected Transaction()
        {
            InTransaction = true;
            DisableForeignKeyChecks = false;

            PersistenceProvider factory = PersistenceProvider.CurrentPersistenceProvider;
            PersistenceProviderFactory = factory;
        }

        #region Transaction Logic

        public static Transaction Begin()
        {
            return Begin(true, OptimizeFor.PartialSubGraphAccess);
        }
        public static Transaction Begin(bool readWriteMode)
        {
            return Begin(readWriteMode, OptimizeFor.PartialSubGraphAccess);
        }
        public static Transaction Begin(OptimizeFor mode)
        {
            return Begin(true, mode);
        }
        public static Transaction Begin(bool readWriteMode, OptimizeFor mode)
        {
            if (PersistenceProvider.CurrentPersistenceProvider is null)
                throw new InvalidOperationException("PersistenceProviderFactory should be set before you start doing transactions.");

            Transaction trans = PersistenceProvider.CurrentPersistenceProvider.NewTransaction(readWriteMode);
            trans.TransactionDate = DateTime.UtcNow;
            trans.Mode = mode;
            trans.FireEvents = EventOptions.AllEvents;

            if (transactions is null)
                transactions = new Stack<Transaction>();

            transactions.Push(trans);
            transaction = trans;

            return trans;
        }

        public abstract RawResult Run(string cypher, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0);
        public abstract RawResult Run(string cypher, Dictionary<string, object?>? parameters, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0);

        protected abstract void Initialize();
        protected abstract void ApplyFunctionalId(FunctionalId functionalId);
        // Flush is private for now, until RelationshipActions will have their own persistence state.
        protected virtual void FlushPrivate()
        {
            List<OGM> entities = registeredEntities.Values.SelectMany(item => item.Values).Where(item => item is OGMImpl).ToList();
            foreach (OGMImpl entity in entities)
            {
                if (entity.PersistenceState == PersistenceState.Persisted || entity.PersistenceState == PersistenceState.Deleted)
                    continue;

                if (HasChanges(entity))
                {
                    if(!beforeCommitEntityState.ContainsKey(entity))
                        beforeCommitEntityState.Add(entity, entity.PersistenceState);

                    entity.GetEntity().RaiseOnSave((OGMImpl)entity, this);
                    foreach (EntityEventArgs item in entity.EventHistory)
                        item.Flush();
                }
            }

            if (!DisableForeignKeyChecks)
            {
                // TODO: check why entities is missing entities when using new code
                foreach (var entitySet in registeredEntities.Values)
                {
                    foreach (OGM entity in entitySet.Values)
                    {
                        if (entity.PersistenceState == PersistenceState.Persisted || entity.PersistenceState == PersistenceState.Deleted)
                            continue;


                        if (HasChanges(entity))
                            entity.ValidateSave();
                    }
                }
            }

            foreach (var entitySet in registeredEntities.Values)
            {
                foreach (OGM entity in entitySet.Values)
                {
                    if (entity.PersistenceState == PersistenceState.Persisted || entity.PersistenceState == PersistenceState.Deleted)
                        continue;

                    if (HasChanges(entity))
                        entity.Save();
                }
            }

            if (actions is not null)
            {
                foreach (var action in actions)
                {
                    action.ExecuteInDatastore();
                    forRetry.AddLast(action);
                }
                actions.Clear();
            }

            foreach (var entitySet in registeredEntities.Values)
            {
                foreach (OGM entity in entitySet.Values)
                {
                    if (entity.PersistenceState == PersistenceState.Persisted || entity.PersistenceState == PersistenceState.Deleted)
                        continue;

                    if (entity.PersistenceState == PersistenceState.Delete || entity.PersistenceState == PersistenceState.ForceDelete)
                    {
                        if (!beforeCommitEntityState.ContainsKey(entity))
                            beforeCommitEntityState.Add(entity, entity.PersistenceState);

                        entity.ValidateDelete();
                    }
                }
            }

            foreach (var entitySet in registeredEntities.Values)
            {
                foreach (OGM entity in entitySet.Values.ToList())
                {
                    if (entity.PersistenceState == PersistenceState.Persisted || entity.PersistenceState == PersistenceState.Deleted)
                        continue;

                    if (entity.PersistenceState == PersistenceState.Delete || entity.PersistenceState == PersistenceState.ForceDelete)
                    {
                        entity.Save();
                        object? key = entity.GetKey();
                        Dictionary<object, OGM>? cache;
                        if (!(key is null) && entitiesByKey.TryGetValue(entity.GetEntity().Name, out cache))
                            cache.Remove(key);
                        //entitySet.Remove(entity);
                    }
                }
            }

            static bool HasChanges(OGM entity)
            {
                return entity.PersistenceState != PersistenceState.New && entity.PersistenceState != PersistenceState.Delete && entity.PersistenceState != PersistenceState.HasUid && entity.PersistenceState != PersistenceState.DoesntExist && entity.PersistenceState != PersistenceState.ForceDelete && entity.PersistenceState != PersistenceState.Loaded;
            }
        }
        public static void Flush()
        {
            Transaction trans = RunningTransaction;

            trans.FlushPrivate();
        }
        public static void Commit()
        {
            Transaction trans = RunningTransaction;
            bool repeat = false;
            do {
                try
                {
                    repeat = false;
                    trans.FlushPrivate();
                    trans.ApplyFunctionalIds();
                    trans.OnCommit();
                }
                catch (Exception e)
                {
                    if (e.Message.ToLowerInvariant().Contains("can't acquire ExclusiveLock".ToLowerInvariant()))
                    {
                        repeat = true;

                        trans.actions.Clear();
                        foreach (var item in trans.forRetry)
                        {
                            trans.actions.AddLast(item);
                        }
                        trans.forRetry.Clear();

                        foreach (OGMImpl entity in trans.registeredEntities.Values.SelectMany(item => item.Values).Where(item => item is OGMImpl).ToList())
                        {
                            if (trans.beforeCommitEntityState.TryGetValue(entity, out var state))
                                entity.PersistenceState = state;
                        }
                        trans.beforeCommitEntityState.Clear();

                        trans.OnRetry();
                    }
                    else
                        throw e;
                }
            }
            while (repeat);

            trans.Invalidate();
            trans.InTransaction = false;
        }
        public static void Rollback()
        {
            Transaction trans = RunningTransaction;

            trans.OnRollback();
            trans.Invalidate();
            trans.InTransaction = false;
        }
        public static Transaction? Current
        {
            get
            {
                if (transaction is null)
                {
                    if (transactions is null || transactions.Count == 0)
                        return null;

                    transaction = transactions.Peek();
                }
                return transaction;
            }
        }
        public static Transaction RunningTransaction
        {
            get
            {
                Transaction? trans = Current;

                if (trans is null)
                    throw new InvalidOperationException("There is no transaction, you should create one first -> using (Transaction.Begin()) { ... Transaction.Commit(); }");

                if (!trans.InTransaction)
                    throw new InvalidOperationException("The transaction was already committed or rolled back.");

                return trans;
            }
        }

        public bool InTransaction { get; private set; }
        public DateTime TransactionDate { get; private set; }
        public OptimizeFor Mode { get; set; }

        public bool DisableForeignKeyChecks { get; set; }

        protected void ApplyFunctionalIds()
        {
            foreach (FunctionalId functionalId in DatastoreModel.RegisteredModels.SelectMany(model => model.FunctionalIds).Where(item=> item is not null))
            {
                ApplyFunctionalId(functionalId);
            }
        }
        protected virtual void OnCommit() { }
        protected virtual void OnRollback() { }
        protected virtual void OnRetry() { }

        #endregion

        #region Storage

        [ThreadStatic]
        private static Stack<Transaction>? transactions = null;

        [ThreadStatic]
        private static Transaction? transaction = null;

        #endregion

        #region IDisposable Support

        protected bool isDisposed = true;

        public void Dispose()
        {
            if (!isDisposed)
            {
                try
                {
                    if (InTransaction)
                        Rollback();
                }
                finally
                {
                    if (transactions is not null)
                    {
                        if (transactions.Count > 0)
                            transactions.Pop();

                        if (transactions.Count == 0)
                            transactions = null;
                    }

                    transaction = null;

                    if (IsDebug.Value)
                        GC.SuppressFinalize(this);

                    isDisposed = true;
                }
            }
        }

        ~Transaction()
        {
            if (IsDebug.Value && !isDisposed)
                throw new InvalidOperationException("The transaction should be used with the using command: using (Transaction.Begin()) { ... Transaction.Commit(); }");
        }
        private readonly Lazy<bool> IsDebug = new Lazy<bool>(() => System.Diagnostics.Debugger.IsAttached, true);


        #endregion

        #region Registration
        private Dictionary<OGM, PersistenceState> beforeCommitEntityState = new Dictionary<OGM, PersistenceState>();
        private Dictionary<string, Dictionary<OGM, OGM>> registeredEntities = new Dictionary<string, Dictionary<OGM, OGM>>(50);
        private Dictionary<string, Dictionary<string, HashSet<Core.EntityCollectionBase>>> registeredCollections = new Dictionary<string, Dictionary<string, HashSet<Core.EntityCollectionBase>>>(100);

        internal void Register(OGM item)
        {
            if (item is null)
                return;

            item.Transaction = this;

            string entityName = item.GetEntity().Name;

            Dictionary<OGM, OGM>? values;
            if (!registeredEntities.TryGetValue(entityName, out values))
            {
                values = new Dictionary<OGM, OGM>(1000);
                registeredEntities.Add(entityName, values);
            }

            OGM? inSet;
            if (values.TryGetValue(item, out inSet))
            {
                if (inSet.PersistenceState != PersistenceState.DoesntExist && inSet == item)
                    throw new InvalidOperationException("You cannot register an already loaded object.");
            }
            else
            {
                values.Add(item, item);
            }
        }

        internal void Register(string type, OGM item, bool noError = false)
        {
            object? key = item.GetKey();
            if (key is null)
                return;

            Dictionary<object, OGM>? values;
            if (!entitiesByKey.TryGetValue(type, out values))
            {
                values = new Dictionary<object, OGM>(1000);
                entitiesByKey.Add(type, values);
            }

            if (values.ContainsKey(key))
            {
                OGM similarItem = values[key];
                if (!noError && similarItem.PersistenceState != PersistenceState.HasUid && similarItem.PersistenceState != PersistenceState.DoesntExist)
                    throw new InvalidOperationException("You cannot register an already loaded object.");
                else
                    values[key] = item;
            }
            else
                values.Add(key, item);
        }

        internal void Register(Core.EntityCollectionBase item)
        {
            if (item is null)
                return;

            item.DbTransaction = this;

            string relationshipName = item.Relationship.Name;

            Dictionary<string, HashSet<Core.EntityCollectionBase>>? properties;
            if (!registeredCollections.TryGetValue(relationshipName, out properties))
            {
                properties = new Dictionary<string, HashSet<Core.EntityCollectionBase>>();
                registeredCollections.Add(relationshipName, properties);
            }

            string propertyName = string.Concat(item.Parent.GetEntity().Name, ".", item.ParentProperty?.Name ?? "NonExisting");

            HashSet<Core.EntityCollectionBase>? values;
            if (!properties.TryGetValue(propertyName, out values))
            {
                values = new HashSet<Core.EntityCollectionBase>();
                properties.Add(propertyName, values);
            }

            if (values.Contains(item))
                throw new InvalidOperationException("You cannot register an already loaded collection.");

            values.Add(item);
        }

        private void Invalidate()
        {
            forRetry.Clear();

            foreach (OGM item in registeredEntities.Values.SelectMany(item => item.Values))
            {
                item.PersistenceState = PersistenceState.OutOfScope;
                item.Transaction = null;
            }
            registeredEntities.Clear();

            foreach (Core.EntityCollectionBase item in registeredCollections.Values.SelectMany(item => item.Values).SelectMany(item => item))
                item.DbTransaction = null;

            registeredCollections.Clear();
        }


        private Dictionary<string, Dictionary<object, OGM>> entitiesByKey = new Dictionary<string, Dictionary<object, OGM>>(50);

        public OGM? GetEntityByKey(string type, object key)
        {
            if (key is null)
                return null;

            Dictionary<object, OGM>? values;
            if (!entitiesByKey.TryGetValue(type, out values))
                return null;

            OGM? item;
            if (!values.TryGetValue(key, out item))
                return null;

            return item;
        }

        #endregion

        #region Action Distribution

        private LinkedList<RelationshipAction> actions = new LinkedList<RelationshipAction>();
        private LinkedList<RelationshipAction> forRetry = new LinkedList<RelationshipAction>();

        internal void Register(RelationshipAction action)
        {
            actions.AddLast(action);
            Distribute(action);
        }
        internal void Register(LinkedList<RelationshipAction> actions)
        {
            foreach (RelationshipAction action in actions)
                Register(action);
        }

        internal void Replay(Core.EntityCollectionBase collection)
        {
            foreach (RelationshipAction action in actions)
                action.ExecuteInMemory(collection);
        }
        private void Distribute(RelationshipAction action)
        {
            List<EntityCollectionBase>? collections = registeredCollections.SelectMany(item => item.Value).SelectMany(item => item.Value).ToList();
            foreach (Core.EntityCollectionBase collection in collections)
                action.ExecuteInMemory(collection);
        }

        internal void LoadAll(Core.EntityCollectionBase collection)
        {
            if (collection is null)
                return;

            string relationshipName = collection.Relationship.Name;

            Dictionary<string, HashSet<Core.EntityCollectionBase>>? properties;
            if (!registeredCollections.TryGetValue(relationshipName, out properties))
            {
                properties = new Dictionary<string, HashSet<Core.EntityCollectionBase>>();
                registeredCollections.Add(relationshipName, properties);
            }

            string propertyName = string.Concat(collection.Parent.GetEntity().Name, ".", collection.ParentProperty?.Name ?? "NonExisting");

            HashSet<Core.EntityCollectionBase>? values;
            if (properties.TryGetValue(propertyName, out values))
            {
                List<Core.EntityCollectionBase> collections = values.Where(item => !item.IsLoaded).ToList();

                const int chunkSize = 10000;
                int initialSize = Math.Min(chunkSize, collections.Count);
                foreach (var chunk in collections.Chunks(chunkSize))
                {
                    List<OGM> parents = new List<OGM>(initialSize);
                    foreach (Core.EntityCollectionBase item in chunk)
                        if (item.Parent.PersistenceState != PersistenceState.New && item.Parent.PersistenceState != PersistenceState.NewAndChanged)
                            parents.Add(item.Parent);

                    Dictionary<OGM, CollectionItemList> allItems = RelationshipPersistenceProvider.Load(parents, collection);

                    foreach (Core.EntityCollectionBase item in chunk)
                    {
                        CollectionItemList? items = null;
                        if (allItems.TryGetValue(item.Parent, out items))
                            item.InitialLoad(items.Items);
                        else
                            item.InitialLoad(new List<CollectionItem>());
                    }
                }
            }

            if (!collection.IsLoaded)
                collection.InitialLoad(new List<CollectionItem>());
        }

        #endregion

        #region PersistenceProviderFactory

        public PersistenceProvider PersistenceProviderFactory { get; private set; }
        internal NodePersistenceProvider NodePersistenceProvider => PersistenceProviderFactory.NodePersistenceProvider;
        internal RelationshipPersistenceProvider RelationshipPersistenceProvider => PersistenceProviderFactory.RelationshipPersistenceProvider;

        #endregion

        #region Query

        public static Query.IBlankQuery CompiledQuery
        {
            get
            {
                return new Query.Query(Current?.PersistenceProviderFactory ?? PersistenceProvider.CurrentPersistenceProvider);
            }
        }

        #endregion

        #region Events

        public static void Execute(Action action, EventOptions withEvents = EventOptions.GraphEvents)
        {
            Transaction trans = RunningTransaction;
            EventOptions oldValue = trans.FireEvents;
            trans.FireEvents = withEvents;

            try
            {
                action.Invoke();
            }
            finally
            {
                trans.FireEvents = oldValue;
            }
        }
        public static T Execute<T>(Func<T> action, EventOptions withEvents = EventOptions.GraphEvents)
        {
            if (action is null)
                return default!;

            T result;
            Transaction trans = RunningTransaction;
            EventOptions oldValue = trans.FireEvents;
            trans.FireEvents = withEvents;

            try
            {
                result = action.Invoke();
            }
            finally
            {
                trans.FireEvents = oldValue;
            }

            return result;
        }
        public EventOptions FireEvents { get; private set; }

        internal bool FireEntityEvents { get { return (FireEvents & EventOptions.EntityEvents) == EventOptions.EntityEvents; } }
        internal bool FireGraphEvents { get { return (FireEvents & EventOptions.GraphEvents) == EventOptions.GraphEvents; } }

        #endregion
    }

    [Flags]
    public enum EventOptions
    {
        SupressEvents = 0,
        EntityEvents = 1,
        GraphEvents = 2,
        AllEvents = 3,
    }
}
