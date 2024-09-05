using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Blueprint41.Core;
using Blueprint41.Driver;
using Blueprint41.Events;
using Blueprint41.Persistence;
using driver = Blueprint41.Driver;

namespace Blueprint41
{
    public class Transaction : DisposableScope<Transaction>, IStatementRunner
    {
        public driver.Session? DriverSession { get; set; }
        public driver.Transaction? DriverTransaction { get; set; }
        public driver.IQueryRunner? StatementRunner { get; set; }

        internal Transaction(PersistenceProvider provider, ReadWriteMode readwrite, OptimizeFor optimize, TransactionLogger? logger)
        {
            Logger = logger;
            OptimizeFor = optimize;
            ReadWriteMode = readwrite;
            InTransaction = true;
            DisableForeignKeyChecks = false;

            PersistenceProvider = provider;

            RaiseOnBegin();
            Attach();
            TransactionDate = DateTime.UtcNow;
            FireEvents = EventOptions.AllEvents;
        }
        private protected TransactionLogger? Logger { get; private set; }
        public static void Log(string message) => RunningTransaction.Logger?.Log(message);

        protected override void Initialize()
        {
            driver.AccessMode accessMode = (ReadWriteMode == ReadWriteMode.ReadWrite) ? driver.AccessMode.Write : driver.AccessMode.Read;

            DriverSession = PersistenceProvider.Driver.Session(c =>
            {
                if (PersistenceProvider.Database is not null)
                    c.WithDatabase(PersistenceProvider.Database);

                c.WithFetchSize(ConfigBuilder.Infinite);
                c.WithDefaultAccessMode(accessMode);

                if (Consistency is not null)
                    c.WithBookmarks(Consistency);
            });

            DriverTransaction = DriverSession.BeginTransaction();

            StatementRunner = DriverTransaction;
            base.Initialize();
        }

        #region Transaction Logic

        //public static Transaction Begin()
        //{
        //    return Begin(true, OptimizeFor.PartialSubGraphAccess);
        //}
        //public static Transaction Begin(bool readWriteMode)
        //{
        //    return Begin(readWriteMode, OptimizeFor.PartialSubGraphAccess);
        //}
        //public static Transaction Begin(OptimizeFor mode)
        //{
        //    return Begin(true, mode);
        //}
        //public static Transaction Begin(bool readWriteMode, OptimizeFor mode)
        //{
        //    if (PersistenceProvider.CurrentPersistenceProvider is null)
        //        throw new InvalidOperationException("PersistenceProviderFactory should be set before you start doing transactions.");

        //    Transaction trans = PersistenceProvider.CurrentPersistenceProvider.NewTransaction(readWriteMode);
        //    trans.RaiseOnBegin();
        //    trans.Attach();
        //    trans.TransactionDate = DateTime.UtcNow;
        //    trans.Mode = mode;
        //    trans.FireEvents = EventOptions.AllEvents;

        //    return trans;
        //}

        public virtual Transaction WithConsistency(Bookmark consistency)
        {
            return this;
        }
        public virtual Transaction WithConsistency(string consistencyToken)
        {
            return this;
        }
        public Transaction WithConsistency(params Bookmark[] consistency)
        {
            if (consistency is not null)
            {
                foreach (Bookmark item in consistency)
                    WithConsistency(item);
            }

            return this;
        }
        public Transaction WithConsistency(params string[] consistencyTokens)
        {
            if (consistencyTokens is not null)
            {
                foreach (string item in consistencyTokens)
                    WithConsistency(item);
            }

            return this;
        }
        protected internal driver.Bookmarks[]? Consistency;

        static public Task<driver.ResultCursor> RunAsync(string cypher, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0) => ((IStatementRunner)RunningTransaction).RunAsync(cypher, memberName, sourceFilePath, sourceLineNumber);
        static public Task<driver.ResultCursor> RunAsync(string cypher, Dictionary<string, object?>? parameters, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0) => ((IStatementRunner)RunningTransaction).RunAsync(cypher, parameters, memberName, sourceFilePath, sourceLineNumber);
        static public driver.ResultCursor Run(string cypher, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0) => ((IStatementRunner)RunningTransaction).Run(cypher, memberName, sourceFilePath, sourceLineNumber);
        static public driver.ResultCursor Run(string cypher, Dictionary<string, object?>? parameters, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0) => ((IStatementRunner)RunningTransaction).Run(cypher, parameters, memberName, sourceFilePath, sourceLineNumber);

        Task<driver.ResultCursor> IStatementRunner.RunAsync(string cypher, string memberName, string sourceFilePath, int sourceLineNumber)
        {
            if (PersistenceProvider.IsVoidProvider)
            {
#if DEBUG
                Logger?.Start();
                if (Logger is not null)
                    Logger.Stop(cypher, null, memberName, sourceFilePath, sourceLineNumber);
#endif
                return Task.FromResult(new driver.ResultCursor());
            }
            else
            {
                if (StatementRunner is null)
                    throw new InvalidOperationException("The current transaction was already committed or rolled back.");

                return StatementRunner.RunAsync(cypher);
            }
        }
        Task<driver.ResultCursor> IStatementRunner.RunAsync(string cypher, Dictionary<string, object?>? parameters, string memberName, string sourceFilePath, int sourceLineNumber)
        {
            if (PersistenceProvider.IsVoidProvider)
            {
#if DEBUG
                Logger?.Start();
                if (Logger is not null)
                    Logger.Stop(cypher, null, memberName, sourceFilePath, sourceLineNumber);
#endif
                return Task.FromResult(new driver.ResultCursor());
            }
            else
            {

                if (StatementRunner is null)
                    throw new InvalidOperationException("The current transaction was already committed or rolled back.");

                if (parameters is null)
                    return StatementRunner.RunAsync(cypher);
                else
                    return StatementRunner.RunAsync(cypher, parameters);
            }
        }
        driver.ResultCursor IStatementRunner.Run(string cypher, string memberName, string sourceFilePath, int sourceLineNumber)
        {
            if (PersistenceProvider.IsVoidProvider)
            {
#if DEBUG
                Logger?.Start();
                if (Logger is not null)
                    Logger.Stop(cypher, null, memberName, sourceFilePath, sourceLineNumber);
#endif
                return new driver.ResultCursor();
            }
            else
            {
                if (StatementRunner is null)
                    throw new InvalidOperationException("The current transaction was already committed or rolled back.");

                return StatementRunner.Run(cypher);
            }
        }
        driver.ResultCursor IStatementRunner.Run(string cypher, Dictionary<string, object?>? parameters, string memberName, string sourceFilePath, int sourceLineNumber)
        {
            if (PersistenceProvider.IsVoidProvider)
            {
#if DEBUG
                Logger?.Start();
                if (Logger is not null)
                    Logger.Stop(cypher, null, memberName, sourceFilePath, sourceLineNumber);
#endif
                return new driver.ResultCursor();
            }
            else
            {

                if (StatementRunner is null)
                    throw new InvalidOperationException("The current transaction was already committed or rolled back.");

                if (parameters is null)
                    return StatementRunner.Run(cypher);
                else
                    return StatementRunner.Run(cypher, parameters);
            }
        }

        protected virtual void ApplyFunctionalId(FunctionalId functionalId)
        {
            if (functionalId is null)
                return;

            if (functionalId.wasApplied || functionalId.highestSeenId == -1)
                return;

            lock (functionalId)
            {
                string getFidQuery = $"CALL blueprint41.functionalid.current('{functionalId.Label}')";
                driver.ResultCursor result = Run(getFidQuery);
                driver.Record? record = result.FirstOrDefault();
                long? currentFid = record?["Sequence"].As<long?>();
                if (currentFid.HasValue)
                    functionalId.SeenUid(currentFid.Value);

                string setFidQuery = $"CALL blueprint41.functionalid.setSequenceNumber('{functionalId.Label}', {functionalId.highestSeenId}, {(functionalId.Format == IdFormat.Numeric).ToString().ToLowerInvariant()})";
                Run(setFidQuery);
                functionalId.wasApplied = true;
                functionalId.highestSeenId = -1;
            }
        }

        // Flush is private for now, until RelationshipActions will have their own persistence state.
        protected virtual void FlushInternal()
        {
            List<OGMImpl> entities = registeredEntities.Values.SelectMany(item => item.Values).Where(item => item is OGMImpl).Cast<OGMImpl>().ToList();
            foreach (OGMImpl entity in entities)
            {
                if (entity.PersistenceState == PersistenceState.Persisted || entity.PersistenceState == PersistenceState.Deleted)
                    continue;

                if (HasChanges(entity))
                {
                    if (!beforeCommitEntityState.ContainsKey(entity))
                        beforeCommitEntityState.Add(entity, entity.PersistenceState);

                    entity.GetEntity().RaiseOnSave(entity, this);
                    foreach (EntityEventArgs item in entity.EventHistory)
                        item.Flush();
                }
            }

            List<KeyValuePair<string, Dictionary<OGM, OGM>>> sortedItems = registeredEntities.OrderBy(item => item.Key).ToList(); // key is entity name
            if (!DisableForeignKeyChecks)
            {
                foreach (var entitySet in sortedItems)
                {
                    foreach (OGM entity in entitySet.Value.Values.OrderBy(item => item.GetKey()))
                    {
                        if (entity.PersistenceState == PersistenceState.Persisted || entity.PersistenceState == PersistenceState.Deleted)
                            continue;


                        if (HasChanges(entity))
                            entity.ValidateSave();
                    }
                }
            }

            foreach (var entitySet in sortedItems)
            {
                foreach (OGM entity in entitySet.Value.Values.OrderBy(item => item.GetKey()))
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

            foreach (var entitySet in sortedItems)
            {
                foreach (OGM entity in entitySet.Value.Values.OrderBy(item => item.GetKey()))
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

            foreach (var entitySet in sortedItems)
            {
                foreach (OGM entity in entitySet.Value.Values.OrderBy(item => item.GetKey()))
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

            foreach (Core.EntityCollectionBase collection in registeredCollections.Values.SelectMany(item => item.Values).SelectMany(item => item))
            {
                collection.AfterFlush();
            }

            foreach (OGMImpl entity in entities)
            {
                if (entity.PersistenceState == PersistenceState.Persisted || entity.PersistenceState == PersistenceState.Deleted)
                {
                    entity.GetEntity().RaiseOnAfterSave(entity, this);
                    foreach (EntityEventArgs item in entity.EventHistory)
                        item.Flush();
                }
            }
        }
        public static void Flush()
        {
            Transaction trans = RunningTransaction;

            trans.FlushInternal();
        }
        public static void Commit()
        {
            Transaction trans = RunningTransaction;
            bool repeat = false;
            do
            {
                try
                {
                    repeat = false;
                    trans.FlushInternal();
                    trans.ApplyFunctionalIds();
                    trans.CommitInternal();
                }
                catch (Exception e)
                {
                    if (e.Message.ToLowerInvariant().Contains("can't acquire ExclusiveLock".ToLowerInvariant()) || e.Message.ToLowerInvariant().Contains("can't acquire UpdateLock".ToLowerInvariant()))
                    {
                        repeat = true;

                        trans.actions.Clear();
                        foreach (var item in trans.forRetry)
                        {
                            trans.actions.AddLast(item);
                        }
                        trans.forRetry.Clear();

                        foreach (OGMImpl entity in trans.registeredEntities.Values.SelectMany(item => item.Values).OfType<OGMImpl>().ToList())
                        {
                            if (trans.beforeCommitEntityState.TryGetValue(entity, out var state))
                                entity.PersistenceState = state;
                        }
                        trans.beforeCommitEntityState.Clear();

                        trans.RetryInternal();
                    }
                    else
                        throw;
                }
            }
            while (repeat);

            trans.Invalidate();
            trans.InTransaction = false;
        }
        public static void Rollback()
        {
            Transaction trans = RunningTransaction;

            trans.RollbackInternal();
            trans.Invalidate();
            trans.InTransaction = false;
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
        public OptimizeFor OptimizeFor { get; private set; }
        public ReadWriteMode ReadWriteMode { get; private set; }

        public virtual Bookmark GetConsistency() => Bookmark.NullBookmark;

        public bool DisableForeignKeyChecks { get; set; }

        protected void ApplyFunctionalIds()
        {
            foreach (FunctionalId functionalId in DatastoreModel.RegisteredModels.SelectMany(model => model.FunctionalIds).Where(item => item is not null))
            {
                ApplyFunctionalId(functionalId);
            }
        }

        protected void CommitInternal()
        {
            if (DriverSession is null)
                throw new InvalidOperationException("The current transaction was already committed or rolled back.");

            driver.Transaction? t = DriverTransaction;
            if (t is not null)
                t.Commit();

            RaiseOnCommit();

            CloseSession();
        }
        protected void RollbackInternal()
        {
            if (DriverSession is null)
                throw new InvalidOperationException("The current transaction was already committed or rolled back.");

            driver.Transaction? t = DriverTransaction;
            if (t is not null)
                t.Rollback();

            CloseSession();
        }
        protected void RetryInternal()
        {
            RollbackInternal();
            Initialize();
        }
        private void CloseSession()
        {
            driver.Session? s = DriverSession;
            if (s is not null)
                s.Close();

            driver.Driver.TaskScheduler.ClearHistory();

            DriverTransaction = null;
            StatementRunner = null;
            DriverSession = null;
        }

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

            item.Transaction = this;

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
                item.Transaction = null;

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

                    Dictionary<OGM, RelationshipPersistenceProvider.CollectionItemList> allItems = RelationshipPersistenceProvider.Load(parents, collection);

                    foreach (Core.EntityCollectionBase item in chunk)
                    {
                        RelationshipPersistenceProvider.CollectionItemList? items = null;
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

        public PersistenceProvider PersistenceProvider { get; private set; }
        internal NodePersistenceProvider NodePersistenceProvider => PersistenceProvider.NodePersistenceProvider;
        internal RelationshipPersistenceProvider RelationshipPersistenceProvider => PersistenceProvider.RelationshipPersistenceProvider;

        #endregion

        //#region Query

        //public static Query.IBlankQuery CompiledQuery
        //{
        //    get
        //    {
        //        return new Query.Query(Current?.PersistenceProviderFactory ?? PersistenceProvider.CurrentPersistenceProvider);
        //    }
        //}

        //public static Query.ICallSubquery CompiledSubQuery
        //{
        //    get
        //    {
        //        return new Query.Query(Current?.PersistenceProviderFactory ?? PersistenceProvider.CurrentPersistenceProvider);
        //    }
        //}

        //#endregion

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

        internal void RaiseOnBegin()
        {
            TransactionEventArgs args = TransactionEventArgs.CreateInstance(EventTypeEnum.OnBegin, this);
            onBegin?.Invoke(this, args);
        }
        public static bool HasRegisteredOnBeginHandlers { get { return onBegin is not null; } }
        private static EventHandler<TransactionEventArgs>? onBegin;
        public static event EventHandler<TransactionEventArgs> OnBegin
        {
            add { onBegin += value; }
            remove { onBegin -= value; }
        }


        internal void RaiseOnCommit()
        {
            if (!FireEntityEvents)
                return;

            TransactionEventArgs args = TransactionEventArgs.CreateInstance(EventTypeEnum.OnCommit, this);
            onCommit?.Invoke(this, args);

            // Wipe custom-state so the garbage collection can collect it. If anyone thinks this causes a bug for them, feel free to remove this line of code :o)
            customState = null;
        }
        public bool HasRegisteredOnCommitHandlers { get { return onCommit is not null; } }
        private EventHandler<TransactionEventArgs>? onCommit;
        public event EventHandler<TransactionEventArgs> OnCommit
        {
            add { onCommit += value; }
            remove { onCommit -= value; }
        }

        private Dictionary<string, object?>? customState = null;
        public IDictionary<string, object?> CustomState
        {
            get
            {
                if (customState is null)
                {
                    lock (this)
                    {
                        if (customState is null)
                            customState = new Dictionary<string, object?>();
                    }
                }
                return customState;
            }
        }

        #endregion
    }
}
