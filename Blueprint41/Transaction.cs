using Blueprint41.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
        }

        #region Transaction Logic

        public static Transaction Begin()
        {
            return Begin(false, OptimizeFor.PartialSubGraphAccess);
        }

        public static Transaction Begin(bool withTransaction)
        {
            return Begin(withTransaction, OptimizeFor.PartialSubGraphAccess);
        }

        public static Transaction Begin(OptimizeFor mode)
        {
            return Begin(false, mode);
        }

        public static Transaction Begin(bool withTransaction, OptimizeFor mode)
        {
            if (PersistenceProvider.CurrentPersistenceProvider == null)
                throw new InvalidOperationException("PersistenceProviderFactory should be set before you start doing transactions.");

            PersistenceProvider factory = PersistenceProvider.CurrentPersistenceProvider;

            Transaction trans = factory.NewTransaction(withTransaction);
            trans.TransactionDate = DateTime.UtcNow;
            trans.PersistenceProviderFactory = factory;
            trans.NodePersistenceProvider = factory.GetNodePersistenceProvider();
            trans.RelationshipPersistenceProvider = factory.GetRelationshipPersistenceProvider();
            trans.Mode = mode;
            trans.FireEvents = EventOptions.AllEvents;

            if (transactions == null)
                transactions = new Stack<Transaction>();

            transactions.Push(trans);
            transaction = trans;

            return trans;
        }

        protected abstract void ApplyFunctionalId(FunctionalId functionalId);
        // Flush is private for now, until RelationshipActions will have their own persistence state. 
        protected virtual void FlushPrivate()
        {
            List<OGM> entities = registeredEntities.Values.SelectMany(item => item).Where(item => item is OGMImpl).ToList();
            foreach (OGMImpl entity in entities)
            {
                if (entity.PersistenceState == PersistenceState.Persisted)
                    continue;

                if (entity.PersistenceState != PersistenceState.New && entity.PersistenceState != PersistenceState.Deleted && entity.PersistenceState != PersistenceState.HasUid && entity.PersistenceState != PersistenceState.ForceDeleted && entity.PersistenceState != PersistenceState.Loaded)
                {
                    entity.GetEntity().RaiseOnSave((OGMImpl)entity, this);
                    foreach (EntityEventArgs item in entity.EventHistory)
                        item.Flush();
                }
            }

            if (!DisableForeignKeyChecks)
            {
                foreach (var entitySet in registeredEntities.Values)
                {
                    foreach (OGM entity in entitySet)
                    {
                        if (entity.PersistenceState == PersistenceState.Persisted)
                            continue;


                        if (entity.PersistenceState != PersistenceState.New && entity.PersistenceState != PersistenceState.Deleted && entity.PersistenceState != PersistenceState.HasUid && entity.PersistenceState != PersistenceState.ForceDeleted && entity.PersistenceState != PersistenceState.Loaded)
                            entity.ValidateSave();
                    }
                }

            }

            foreach (var entitySet in registeredEntities.Values)
            {
                foreach (OGM entity in entitySet)
                {
                    if (entity.PersistenceState == PersistenceState.Persisted)
                        continue;

                    if (entity.PersistenceState != PersistenceState.New && entity.PersistenceState != PersistenceState.Deleted && entity.PersistenceState != PersistenceState.HasUid && entity.PersistenceState != PersistenceState.ForceDeleted && entity.PersistenceState != PersistenceState.Loaded)
                        entity.Save();
                }
            }

            if (actions != null)
            {
                foreach (var action in actions)
                {
                    action.ExecuteInDatastore();
                }
            }

            foreach (var entitySet in registeredEntities.Values)
            {
                foreach (OGM entity in entitySet)
                {
                    if (entity.PersistenceState == PersistenceState.Persisted)
                        continue;

                    if (entity.PersistenceState == PersistenceState.Deleted || entity.PersistenceState == PersistenceState.ForceDeleted)
                        entity.ValidateDelete();
                }
            }

            foreach (var entitySet in registeredEntities.Values)
            {
                foreach (OGM entity in entitySet)
                {
                    if (entity.PersistenceState == PersistenceState.Persisted)
                        continue;

                    if (entity.PersistenceState == PersistenceState.Deleted || entity.PersistenceState == PersistenceState.ForceDeleted)
                        entity.Save();
                }
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

            trans.FlushPrivate();
            trans.ApplyFunctionalIds();
            trans.OnCommit();
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
        public static Transaction Current
        {
            get
            {
                if (transaction == null)
                {
                    if (transactions == null || transactions.Count == 0)
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
                Transaction trans = Current;

                if (trans == null)
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
            foreach (FunctionalId functionalId in DatastoreModel.RegisteredModels.SelectMany(model => model.FunctionalIds).Where(item=> item != null))
            {
                ApplyFunctionalId(functionalId);
            }
        }
        protected virtual void OnCommit() { }
        protected virtual void OnRollback() { }

        #endregion

        #region Storage

        [ThreadStatic]
        private static Stack<Transaction> transactions = null;

        [ThreadStatic]
        private static Transaction transaction = null;

        #endregion

        #region IDisposable Support

        private bool isDisposed = false;

        public void Dispose()
        {
            if (!isDisposed)
            {
                if (InTransaction)
                    Rollback();

                if (transactions != null)
                {
                    if (transactions.Count > 0)
                        transactions.Pop();

                    if (transactions.Count == 0)
                        transactions = null;
                }

                transaction = null;

                GC.SuppressFinalize(this);
                isDisposed = true;
            }
        }

        ~Transaction()
        {
            if (!isDisposed)
                throw new InvalidOperationException("The transaction should be used with the using command: using (Transaction.Begin()) { ... Transaction.Commit(); }");
        }

        #endregion

        #region Registration

        private Dictionary<string, HashSet<OGM>> registeredEntities = new Dictionary<string, HashSet<OGM>>(50);
        private Dictionary<string, Dictionary<string, HashSet<Core.EntityCollectionBase>>> registeredCollections = new Dictionary<string, Dictionary<string, HashSet<Core.EntityCollectionBase>>>(100);

        internal void Register(OGM item)
        {
            if (item == null)
                return;

            item.Transaction = this;

            string entityName = item.GetEntity().Name;

            HashSet<OGM> values;
            if (!registeredEntities.TryGetValue(entityName, out values))
            {
                values = new HashSet<OGM>();
                values.SetCapacity(1000);
                registeredEntities.Add(entityName, values);
            }

            if (values.Contains(item))
                throw new InvalidOperationException("You cannot register an already loaded object.");

            values.Add(item);
        }

        internal void Register(string type, OGM item)
        {
            object key = item.GetKey();
            if (key == null)
                return;

            Dictionary<object, OGM> values;
            if (!entitiesByKey.TryGetValue(type, out values))
            {
                values = new Dictionary<object, OGM>(1000);
                entitiesByKey.Add(type, values);
            }

            if (values.ContainsKey(key))
            {
                OGM similarItem = values[key];
                if (similarItem.PersistenceState != PersistenceState.HasUid)
                    throw new InvalidOperationException("You cannot register an already loaded object.");
                else
                    values[key] = item;
            }
            else
                values.Add(key, item);
        }

        internal void Register(Core.EntityCollectionBase item)
        {
            if (item == null)
                return;

            item.Transaction = this;

            string relationshipName = item.Relationship.Name;

            Dictionary<string, HashSet<Core.EntityCollectionBase>> properties;
            if (!registeredCollections.TryGetValue(relationshipName, out properties))
            {
                properties = new Dictionary<string, HashSet<Core.EntityCollectionBase>>();
                registeredCollections.Add(relationshipName, properties);
            }

            string propertyName = string.Concat(item.Parent.GetEntity().Name, ".", item.ParentProperty.Name);

            HashSet<Core.EntityCollectionBase> values;
            if (!properties.TryGetValue(propertyName, out values))
            {
                values = new HashSet<Core.EntityCollectionBase>();
                values.SetCapacity(1000);
                properties.Add(propertyName, values);
            }

            if (values.Contains(item))
                throw new InvalidOperationException("You cannot register an already loaded collection.");

            values.Add(item);
        }

        private void Invalidate()
        {
            foreach (OGM item in registeredEntities.Values.SelectMany(item => item))
            {
                item.PersistenceState = PersistenceState.OutOfScope;
                item.Transaction = null;
            }
            registeredEntities.Clear();

            foreach (Core.EntityCollectionBase item in registeredCollections.Values.SelectMany(item => item.Values).SelectMany(item => item))
                item.Transaction = null;

            registeredCollections.Clear();
        }


        private Dictionary<string, Dictionary<object, OGM>> entitiesByKey = new Dictionary<string, Dictionary<object, OGM>>(50);

        public OGM GetEntityByKey(string type, object key)
        {
            if (key == null)
                return null;

            Dictionary<object, OGM> values;
            if (!entitiesByKey.TryGetValue(type, out values))
                return null;

            OGM item;
            if (!values.TryGetValue(key, out item))
                return null;

            return item;
        }

        #endregion

        #region Action Distribution

        private LinkedList<RelationshipAction> actions = new LinkedList<RelationshipAction>();

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
            foreach (Core.EntityCollectionBase collection in registeredCollections.SelectMany(item => item.Value).SelectMany(item => item.Value))
                action.ExecuteInMemory(collection);
        }

        internal void LoadAll(Core.EntityCollectionBase collection)
        {
            if (collection == null)
                return;

            string relationshipName = collection.Relationship.Name;

            Dictionary<string, HashSet<Core.EntityCollectionBase>> properties;
            if (!registeredCollections.TryGetValue(relationshipName, out properties))
            {
                properties = new Dictionary<string, HashSet<Core.EntityCollectionBase>>();
                registeredCollections.Add(relationshipName, properties);
            }

            string propertyName = string.Concat(collection.Parent.GetEntity().Name, ".", collection.ParentProperty.Name);

            HashSet<Core.EntityCollectionBase> values;
            if (!properties.TryGetValue(propertyName, out values))
                return;

            List<Core.EntityCollectionBase> collections = values.Where(item => !item.IsLoaded).ToList();

            const int chunkSize = 10000;
            int initialSize = Math.Min(chunkSize, collections.Count);
            foreach (var chunk in collections.Chunks(chunkSize))
            {
                List<OGM> parents = new List<OGM>(initialSize);
                foreach (Core.EntityCollectionBase item in chunk)
                    parents.Add(item.Parent);

                Dictionary<OGM, CollectionItemList> allItems = RelationshipPersistenceProvider.Load(parents, collection);

                foreach (Core.EntityCollectionBase item in chunk)
                {
                    CollectionItemList items = null;
                    if(allItems.TryGetValue(item.Parent, out items))
                        item.InitialLoad(items.Items);
                    else
                        item.InitialLoad(new List<CollectionItem>());
                }
            }
        }

        #endregion

        #region PersistenceProviderFactory

        public PersistenceProvider PersistenceProviderFactory { get; private set; }
        internal NodePersistenceProvider NodePersistenceProvider { get; private set; }
        internal RelationshipPersistenceProvider RelationshipPersistenceProvider { get; private set; }

        #endregion

        #region Query

        public static Query.IBlankQuery CompiledQuery
        {
            get
            {
                return new Query.Query(PersistenceProvider.CurrentPersistenceProvider);
            }
        }

        #endregion

        #region Conversion

        public object ConvertToStoredType<TValue>(TValue value)
        {
            return ConvertToStoredTypeCache<TValue>.Convert(PersistenceProviderFactory, value);
        }
        public object ConvertToStoredType(Type returnType, object value)
        {
            Conversion converter;
            if (!PersistenceProviderFactory.ConvertToStoredTypeCache.TryGetValue(returnType, out converter))
                return value;

            if ((object)converter == null)
                return value;

            return converter.Convert(value);
        }
        public TReturnType ConvertFromStoredType<TReturnType>(object value)
        {
            return (TReturnType)ConvertFromStoredTypeCache<TReturnType>.Convert(PersistenceProviderFactory, value);
        }
        public object ConvertFromStoredType(Type returnType, object value)
        {
            Conversion converter;
            if (!PersistenceProviderFactory.ConvertFromStoredTypeCache.TryGetValue(returnType, out converter))
                return value;

            if ((object)converter == null)
                return value;

            return converter.Convert(value);
        }

        public Type GetStoredType<TReturnType>()
        {
            ConvertFromStoredTypeCache<TReturnType>.Initialize(PersistenceProviderFactory);
            return ConvertFromStoredTypeCache<TReturnType>.Converter?.FromType ?? typeof(TReturnType);
        }
        public Type GetStoredType(Type returnType)
        {
            Conversion converter;
            if (!PersistenceProviderFactory.ConvertFromStoredTypeCache.TryGetValue(returnType, out converter))
                return null;

            return converter.FromType;
        }

        private class ConvertToStoredTypeCache<TReturnType>
        {
            public static Conversion Converter = null;
            public static bool IsInitialized = false;

            public static object Convert(PersistenceProvider factory, object value)
            {
                Initialize(factory);

                if (Converter == null)
                    return value;

                return Converter.Convert(value);
            }

            internal static void Initialize(PersistenceProvider factory)
            {
                if (!IsInitialized)
                {
                    lock (typeof(ConvertFromStoredTypeCache<TReturnType>))
                    {
                        if (!IsInitialized)
                        {
                            factory.ConvertToStoredTypeCache.TryGetValue(typeof(TReturnType), out Converter);
                            IsInitialized = true;
                        }
                    }
                }
            }
        }
        private class ConvertFromStoredTypeCache<TReturnType>
        {
            public static Conversion Converter = null;
            public static bool IsInitialized = false;

            public static object Convert(PersistenceProvider factory, object value)
            {
                Initialize(factory);

                if (Converter == null)
                    return value;

                return Converter.Convert(value);
            }

            internal static void Initialize(PersistenceProvider factory)
            {
                if (!IsInitialized)
                {
                    lock (typeof(ConvertFromStoredTypeCache<TReturnType>))
                    {
                        if (!IsInitialized)
                        {
                            factory.ConvertFromStoredTypeCache.TryGetValue(typeof(TReturnType), out Converter);
                            IsInitialized = true;
                        }
                    }
                }
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
            if (action == null)
                return default(T);

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
