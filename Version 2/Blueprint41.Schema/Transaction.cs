using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

using Blueprint41.Config;
using Blueprint41.Core;
using Blueprint41.Persistence;
using Blueprint41.Events;
using System.Collections.ObjectModel;

namespace Blueprint41
{
    public class Transaction : DisposableScope<Transaction>, IStatementRunner, IStatementRunnerAsync
    {
        public DriverSession? DriverSession { get; set; }
        
        
        public virtual DriverTransaction? GetDriverTransaction()
        {
            if (!InTransaction)
                return null;

            if (_driverTransaction is null && DriverSession is not null)
                _driverTransaction = DriverSession.BeginTransaction();

            return _driverTransaction;
        }
        public virtual async Task<DriverTransaction?> GetDriverTransactionAsync()
        {
            if (!InTransaction)
                return null;

            if (_driverTransaction is null && DriverSession is not null)
                _driverTransaction = await DriverSession.BeginTransactionAsync();

            return _driverTransaction;
        }
        protected DriverTransaction? _driverTransaction = null;

        public IQueryRunner? GetStatementRunner() => GetDriverTransaction() as IQueryRunner ?? DriverSession;
        public async Task<IQueryRunner?> GetStatementRunnerAsync() => (await GetDriverTransactionAsync()) as IQueryRunner ?? DriverSession;

        static internal Transaction Get(DatastoreModel model, ReadWriteMode readwrite, OptimizeFor optimize, TransactionLogger? logger)
        {
            Transaction transaction = new Transaction(model, readwrite, optimize, logger);
            transaction.Attach();
            transaction.TransactionDate = DateTime.UtcNow;
            transaction.FireEvents = EventOptions.AllEvents;

            return transaction;
        }
        protected Transaction(DatastoreModel model, ReadWriteMode readwrite, OptimizeFor optimize, TransactionLogger? logger)
        {
            Logger = logger;
            OptimizeFor = optimize;
            ReadWriteMode = readwrite;
            InTransaction = true;
            DisableForeignKeyChecks = false;

            Model = model;

            RaiseOnBegin();
        }

        protected override void Initialize()
        {
            AccessMode accessMode = (ReadWriteMode == ReadWriteMode.ReadWrite) ? AccessMode.Write : AccessMode.Read;

            DriverSession = PersistenceProvider.Driver.Session(c =>
            {
                if (PersistenceProvider.Database is not null)
                    c.WithDatabase(PersistenceProvider.Database);

                c.WithFetchSize(ConfigBuilder.Infinite);
                c.WithDefaultAccessMode(accessMode);

                if (Consistency is not null)
                    c.WithBookmarks(Consistency);
            });
            
            //DriverTransaction = DriverSession!.BeginTransaction();
        }

        private protected TransactionLogger? Logger { get; private set; }
        public static void Log(string message) => RunningTransaction.Logger?.Log(message);

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


        public Transaction WithConsistency(params Bookmarks[] consistency)
        {
            if (consistency.Length == 0)
                return this;

            if (Consistency is null && consistency.Length == 1)
            {
                Consistency = consistency[0];
            }
            else
            {
                HashSet<string> hashset = new HashSet<string>();

                if (Consistency is not null)
                {
                    string[]? tokens = PersistenceProvider.ToToken(Consistency);
                    if (tokens is not null)
                    {
                        foreach (string token in tokens)
                        {
                            if (!hashset.Contains(token))
                                hashset.Add(token);
                        }
                    }
                }

                foreach (Bookmarks bookmark in consistency)
                {
                    string[]? tokens = PersistenceProvider.ToToken(bookmark);
                    if (tokens is not null)
                    {
                        foreach (string token in tokens)
                        {
                            if (!hashset.Contains(token))
                                hashset.Add(token);
                        }
                    }
                }

                Consistency = PersistenceProvider.FromToken(hashset.ToArray());
            }

            return this;
        }
        public Transaction WithConsistency(params string[] consistencyTokens)
        {
            if (consistencyTokens.Length == 0)
                return this;

            HashSet<string> hashset = new HashSet<string>();

            if (Consistency is not null)
            {
                string[]? tokens = PersistenceProvider.ToToken(Consistency);
                if (tokens is not null)
                {
                    foreach (string token in tokens)
                    {
                        if (!hashset.Contains(token))
                            hashset.Add(token);
                    }
                }
            }

            if (consistencyTokens is not null && consistencyTokens.Length > 0)
            {
                foreach (string token in consistencyTokens)
                {
                    if (!hashset.Contains(token))
                        hashset.Add(token);
                }
            }

            Consistency = PersistenceProvider.FromToken(hashset.ToArray());

            return this;
        }
        protected internal Bookmarks? Consistency;

        static public Task<ResultCursor> RunAsync(string cypher, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0) => ((IStatementRunnerAsync)RunningTransaction).RunAsync(cypher, memberName, sourceFilePath, sourceLineNumber);
        static public Task<ResultCursor> RunAsync(string cypher, Dictionary<string, object?>? parameters, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0) => ((IStatementRunnerAsync)RunningTransaction).RunAsync(cypher, parameters, memberName, sourceFilePath, sourceLineNumber);
        static public ResultCursor Run(string cypher, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0) => ((IStatementRunner)RunningTransaction).Run(cypher, memberName, sourceFilePath, sourceLineNumber);
        static public ResultCursor Run(string cypher, Dictionary<string, object?>? parameters, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0) => ((IStatementRunner)RunningTransaction).Run(cypher, parameters, memberName, sourceFilePath, sourceLineNumber);

        async Task<ResultCursor> IStatementRunnerAsync.RunAsync(string cypher, string memberName, string sourceFilePath, int sourceLineNumber)
        {
            if (PersistenceProvider.IsVoidProvider)
            {
#if DEBUG
                Logger?.Start();
                if (Logger is not null)
                    Logger.Stop(cypher, null, memberName, sourceFilePath, sourceLineNumber);
#endif
                return new ResultCursor();
            }
            else
            {
                IQueryRunner? runner = await GetStatementRunnerAsync();

                if (runner is null)
                    throw new InvalidOperationException("The current transaction was already committed or rolled back.");

                return await runner.RunAsync(cypher);
            }
        }
        async Task<ResultCursor> IStatementRunnerAsync.RunAsync(string cypher, Dictionary<string, object?>? parameters, string memberName, string sourceFilePath, int sourceLineNumber)
        {
            if (PersistenceProvider.IsVoidProvider)
            {
#if DEBUG
                Logger?.Start();

                if (Logger is not null)
                    Logger.Stop(cypher, null, memberName, sourceFilePath, sourceLineNumber);
#endif
                return new ResultCursor();
            }
            else
            {
                IQueryRunner? runner = await GetStatementRunnerAsync();

                if (runner is null)
                    throw new InvalidOperationException("The current transaction was already committed or rolled back.");

                if (parameters is null)
                    return await runner.RunAsync(cypher);
                else
                    return await runner.RunAsync(cypher, parameters);
            }
        }
        ResultCursor IStatementRunner.Run(string cypher, string memberName, string sourceFilePath, int sourceLineNumber)
        {
            if (PersistenceProvider.IsVoidProvider)
            {
#if DEBUG
                Logger?.Start();
                if (Logger is not null)
                    Logger.Stop(cypher, null, memberName, sourceFilePath, sourceLineNumber);
#endif
                return new ResultCursor();
            }
            else
            {
                IQueryRunner? runner = GetStatementRunner();

                if (runner is null)
                    throw new InvalidOperationException("The current transaction was already committed or rolled back.");

                return runner.Run(cypher);
            }
        }
        ResultCursor IStatementRunner.Run(string cypher, Dictionary<string, object?>? parameters, string memberName, string sourceFilePath, int sourceLineNumber)
        {
            if (PersistenceProvider.IsVoidProvider)
            {
#if DEBUG
                Logger?.Start();
                if (Logger is not null)
                    Logger.Stop(cypher, null, memberName, sourceFilePath, sourceLineNumber);
#endif
                return new ResultCursor();
            }
            else
            {
                IQueryRunner? runner = GetStatementRunner();

                if (runner is null)
                    throw new InvalidOperationException("The current transaction was already committed or rolled back.");

                if (parameters is null)
                    return runner.Run(cypher);
                else
                    return runner.Run(cypher, parameters);
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
                ResultCursor result = Run(getFidQuery);
                Record? record = result.FirstOrDefault();
                long? currentFid = record?["Sequence"].As<long?>();
                if (currentFid.HasValue)
                    functionalId.SeenUid(currentFid.Value);

                string setFidQuery = $"CALL blueprint41.functionalid.setSequenceNumber('{functionalId.Label}', {functionalId.highestSeenId}, {(functionalId.Format == IdFormat.Numeric).ToString().ToLowerInvariant()})";
                Run(setFidQuery);
                functionalId.wasApplied = true;
                functionalId.highestSeenId = -1;
            }
        }
        protected virtual async Task ApplyFunctionalIdAsync(FunctionalId functionalId)
        {
            if (functionalId is null)
                return;

            if (functionalId.wasApplied || functionalId.highestSeenId == -1)
                return;

            //TODO: Fix Lock Issue
            //lock (functionalId)
            {
                string getFidQuery = $"CALL blueprint41.functionalid.current('{functionalId.Label}')";
                ResultCursor result = await RunAsync(getFidQuery);
                Record? record = await result.FirstOrDefaultAsync();
                long? currentFid = record?["Sequence"].As<long?>();
                if (currentFid.HasValue)
                    functionalId.SeenUid(currentFid.Value);

                string setFidQuery = $"CALL blueprint41.functionalid.setSequenceNumber('{functionalId.Label}', {functionalId.highestSeenId}, {(functionalId.Format == IdFormat.Numeric).ToString().ToLowerInvariant()})";
                await RunAsync(setFidQuery);
                functionalId.wasApplied = true;
                functionalId.highestSeenId = -1;
            }
        }

        // Flush is private for now, until RelationshipActions will have their own persistence state.
        protected virtual void FlushInternal()
        {
            List<OgmClass> entities = registeredEntities.Values.SelectMany(item => item.Values).Where(item => item is OgmClass).Cast<OgmClass>().ToList();
            foreach (OgmClass entity in entities)
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

            List<KeyValuePair<(string name, EntityFlavor flavor), Dictionary<OGM, OGM>>> sortedItems = registeredEntities.OrderBy(item => item.Key.name).ThenBy(item => item.Key.flavor).ToList(); // key is entity name
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
                        if (!(key is null) && entitiesByKey.TryGetValue((entity.GetEntity().Name, entity.Flavor), out cache))
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

            foreach (OgmClass entity in entities)
            {
                if (entity.PersistenceState == PersistenceState.Persisted || entity.PersistenceState == PersistenceState.Deleted)
                {
                    entity.GetEntity().RaiseOnAfterSave(entity, this);
                    foreach (EntityEventArgs item in entity.EventHistory)
                        item.Flush();
                }
            }
        }
        protected virtual async Task FlushAsyncInternal()
        {
            List<OgmClass> entities = registeredEntities.Values.SelectMany(item => item.Values).Where(item => item is OgmClass).Cast<OgmClass>().ToList();
            foreach (OgmClass entity in entities)
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

            List<KeyValuePair<(string name, EntityFlavor flavor), Dictionary<OGM, OGM>>> sortedItems = registeredEntities.OrderBy(item => item.Key.name).ThenBy(item => item.Key.flavor).ToList(); // key is entity name
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
                        await entity.SaveAsync();
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
                        await entity.SaveAsync();
                        object? key = entity.GetKey();
                        Dictionary<object, OGM>? cache;
                        if (!(key is null) && entitiesByKey.TryGetValue((entity.GetEntity().Name, entity.Flavor), out cache))
                            cache.Remove(key);
                        //entitySet.Remove(entity);
                    }
                }
            }

            static bool HasChanges(OGM entity)
            {
                return entity.PersistenceState != PersistenceState.New && entity.PersistenceState != PersistenceState.Delete && entity.PersistenceState != PersistenceState.HasUid && entity.PersistenceState != PersistenceState.DoesntExist && entity.PersistenceState != PersistenceState.ForceDelete && entity.PersistenceState != PersistenceState.Loaded;
            }

            foreach (Core.EntityCollectionAsyncBase collection in registeredCollections.Values.SelectMany(item => item.Values).SelectMany(item => item))
            {
                collection.AfterFlush();
            }

            foreach (OgmClass entity in entities)
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
        public static Task FlushAsync()
        {
            Transaction trans = RunningTransaction;

            return trans.FlushAsyncInternal();
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

                        foreach (OgmClass entity in trans.registeredEntities.Values.SelectMany(item => item.Values).OfType<OgmClass>().ToList())
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
        public static async Task CommitAsync()
        {
            Transaction trans = RunningTransaction;
            bool repeat = false;
            do
            {
                try
                {
                    repeat = false;
                    await trans.FlushAsyncInternal();
                    await trans.ApplyFunctionalIdsAsync();
                    await trans.CommitAsyncInternal();
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

                        foreach (OgmClass entity in trans.registeredEntities.Values.SelectMany(item => item.Values).OfType<OgmClass>().ToList())
                        {
                            if (trans.beforeCommitEntityState.TryGetValue(entity, out var state))
                                entity.PersistenceState = state;
                        }
                        trans.beforeCommitEntityState.Clear();

                        await trans.RetryAsyncInternal();
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
        public static async Task RollbackAsync()
        {
            Transaction trans = RunningTransaction;

            await trans.RollbackAsyncInternal();
            trans.Invalidate();
            trans.InTransaction = false;
        }

        public static Transaction RunningTransaction
        {
            get
            {
                Transaction? trans = Current;
                
                if (trans is null)
                    throw new InvalidOperationException("There is no transaction, you should create one first -> using (DatastoreModel.BeginTransaction()) { ... Transaction.Commit(); }");

                if (!trans.InTransaction)
                    throw new InvalidOperationException("The transaction was already committed or rolled back.");

                return trans;
            }
        }

        public bool InTransaction { get; private set; }
        public DateTime TransactionDate { get; protected set; }
        public OptimizeFor OptimizeFor { get; private set; }
        public ReadWriteMode ReadWriteMode { get; private set; }

        public Bookmarks? GetConsistency() => (DriverSession is not null) ? Driver.I_ASYNC_SESSION.LastBookmarks(DriverSession._instance) : null;

        public bool DisableForeignKeyChecks { get; set; }

        protected void ApplyFunctionalIds()
        {
            if (PersistenceProvider.IsNeo4j && PersistenceProvider.HasProcedure("blueprint41.functionalid.current"))
            {
                foreach (FunctionalId functionalId in DatastoreModel.RegisteredModels.SelectMany(model => model.FunctionalIds).Where(item => item is not null))
                {
                    ApplyFunctionalId(functionalId);
                }
            }
        }
        protected async Task ApplyFunctionalIdsAsync()
        {
            if (PersistenceProvider.IsNeo4j && PersistenceProvider.HasProcedure("blueprint41.functionalid.current"))
            {
                foreach (FunctionalId functionalId in DatastoreModel.RegisteredModels.SelectMany(model => model.FunctionalIds).Where(item => item is not null))
                {
                    await ApplyFunctionalIdAsync(functionalId);
                }
            }
        }

        protected void CommitInternal()
        {
            if (DriverSession is null)
                throw new InvalidOperationException("The current transaction was already committed or rolled back.");

            DriverTransaction? t = GetDriverTransaction();
            if (t is not null)
                t.Commit();

            RaiseOnCommit();
        }
        protected async Task CommitAsyncInternal()
        {
            if (DriverSession is null)
                throw new InvalidOperationException("The current transaction was already committed or rolled back.");

            DriverTransaction? t = await GetDriverTransactionAsync();
            if (t is not null)
                await t.CommitAsync();

            RaiseOnCommit();
        }
        protected void RollbackInternal()
        {
            if (DriverSession is null)
                throw new InvalidOperationException("The current transaction was already committed or rolled back.");

            DriverTransaction? t = GetDriverTransaction();
            if (t is not null)
                t.Rollback();
        }
        protected async Task RollbackAsyncInternal()
        {
            if (DriverSession is null)
                throw new InvalidOperationException("The current transaction was already committed or rolled back.");

            DriverTransaction? t = await GetDriverTransactionAsync();
            if (t is not null)
                await t.RollbackAsync();
        }
        protected void RetryInternal()
        {
            RollbackInternal();
            Initialize();
        }
        protected async Task RetryAsyncInternal()
        {
            await RollbackAsyncInternal();
            Initialize();
            _driverTransaction = null;
        }

        protected override void Cleanup()
        {
            if (InTransaction)
                Rollback();

            DriverTransaction? t = GetDriverTransaction();
            if (t is not null)
                t.Dispose();

            DriverSession? s = DriverSession;
            if (s is not null)
                s.Dispose();
        }
        protected override async Task CleanupAsync()
        {
            if (InTransaction)
            {
                await RollbackAsyncInternal();
                Invalidate();
                InTransaction = false;
            }

            DriverTransaction? t = await GetDriverTransactionAsync();
            if (t is not null)
                await t.DisposeAsync();

            DriverSession? s = DriverSession;
            if (s is not null)
                await s.DisposeAsync();
        }

        #endregion

        #region Registration

        private Dictionary<OGM, PersistenceState> beforeCommitEntityState = new Dictionary<OGM, PersistenceState>();
        private Dictionary<(string name, EntityFlavor flavor), Dictionary<OGM, OGM>> registeredEntities = new Dictionary<(string, EntityFlavor), Dictionary<OGM, OGM>>(50);
        private Dictionary<(string name, EntityFlavor flavor), Dictionary<string, HashSet<IReplayableCollection>>> registeredCollections = new Dictionary<(string, EntityFlavor), Dictionary<string, HashSet<IReplayableCollection>>>(100);

        internal void Register(OGM item)
        {
            if (item is null)
                return;

            item.Transaction = this;

            string entityName = item.GetEntity().Name;
            EntityFlavor flavor = item.Flavor;

            Dictionary<OGM, OGM>? values;
            if (!registeredEntities.TryGetValue((entityName, flavor), out values))
            {
                values = new Dictionary<OGM, OGM>(1000);
                registeredEntities.Add((entityName, flavor), values);
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

        internal void Register(string type, EntityFlavor flavor, OGM item, bool noError = false)
        {
            object? key = item.GetKey();
            if (key is null)
                return;

            Dictionary<object, OGM>? values;
            if (!entitiesByKey.TryGetValue((type, flavor), out values))
            {
                values = new Dictionary<object, OGM>(1000);
                entitiesByKey.Add((type, flavor), values);
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

        internal void Register(IReplayableCollection item)
        {
            if (item is null)
                return;

            item.Transaction = this;

            string relationshipName = item.Relationship.Name;
            EntityFlavor flavor = item.Parent.Flavor;

            Dictionary<string, HashSet<IReplayableCollection>>? properties;
            if (!registeredCollections.TryGetValue((relationshipName, flavor), out properties))
            {
                properties = new Dictionary<string, HashSet<IReplayableCollection>>();
                registeredCollections.Add((relationshipName, flavor), properties);
            }

            string propertyName = string.Concat(item.Parent.GetEntity().Name, ".", item.ParentProperty?.Name ?? "NonExisting");

            HashSet<IReplayableCollection>? values;
            if (!properties.TryGetValue(propertyName, out values))
            {
                values = new HashSet<IReplayableCollection>();
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

            foreach (IReplayableCollection item in registeredCollections.Values.SelectMany(item => item.Values).SelectMany(item => item))
                item.Transaction = null;

            registeredCollections.Clear();
        }


        private Dictionary<(string name, EntityFlavor flavor), Dictionary<object, OGM>> entitiesByKey = new Dictionary<(string, EntityFlavor), Dictionary<object, OGM>>(50);

        public OGM? GetEntityByKey(string type, object key, EntityFlavor flavor)
        {
            if (key is null)
                return null;

            Dictionary<object, OGM>? values;
            if (!entitiesByKey.TryGetValue((type, flavor), out values))
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

        internal void Replay(IReplayableCollection collection)
        {
            foreach (RelationshipAction action in actions)
                action.ExecuteInMemory(collection);
        }
        private void Distribute(RelationshipAction action)
        {
            List<IReplayableCollection>? collections = registeredCollections.SelectMany(item => item.Value).SelectMany(item => item.Value).ToList();
            foreach (IReplayableCollection collection in collections)
                action.ExecuteInMemory(collection);
        }

        internal void LoadAll(Core.EntityCollectionBase collection)
        {
            if (collection is null)
                return;

            string relationshipName = collection.Relationship.Name;
            EntityFlavor flavor = collection.Parent.Flavor;

            Dictionary<string, HashSet<IReplayableCollection>>? properties;
            if (!registeredCollections.TryGetValue((relationshipName, flavor), out properties))
            {
                properties = new Dictionary<string, HashSet<IReplayableCollection>>();
                registeredCollections.Add((relationshipName, flavor), properties);
            }

            string propertyName = string.Concat(collection.Parent.GetEntity().Name, ".", collection.ParentProperty?.Name ?? "NonExisting");

            HashSet<IReplayableCollection>? values;
            if (properties.TryGetValue(propertyName, out values))
            {
                List<IReplayableCollection> collections = values.Where(item => !item.IsLoaded).ToList();

                const int chunkSize = 10000;
                int initialSize = Math.Min(chunkSize, collections.Count);
                foreach (ReadOnlyCollection<IReplayableCollection> chunk in collections.Chunks(chunkSize))
                {
                    List<OGM> parents = new List<OGM>(initialSize);
                    foreach (IReplayableCollection item in chunk)
                        if (item.Parent.PersistenceState != PersistenceState.New && item.Parent.PersistenceState != PersistenceState.NewAndChanged)
                            parents.Add(item.Parent);

                    Dictionary<OGM, RelationshipPersistenceProvider.CollectionItemList> allItems = RelationshipPersistenceProvider.Load(parents, collection);

                    foreach (IReplayableCollection item in chunk)
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
        internal async Task LoadAllAsync(Core.EntityCollectionAsyncBase collection)
        {
            if (collection is null)
                return;

            string relationshipName = collection.Relationship.Name;
            EntityFlavor flavor = collection.Parent.Flavor;

            Dictionary<string, HashSet<IReplayableCollection>>? properties;
            if (!registeredCollections.TryGetValue((relationshipName, flavor), out properties))
            {
                properties = new Dictionary<string, HashSet<IReplayableCollection>>();
                registeredCollections.Add((relationshipName, flavor), properties);
            }

            string propertyName = string.Concat(collection.Parent.GetEntity().Name, ".", collection.ParentProperty?.Name ?? "NonExisting");

            HashSet<IReplayableCollection>? values;
            if (properties.TryGetValue(propertyName, out values))
            {
                List<IReplayableCollection> collections = values.Where(item => !item.IsLoaded).ToList();

                const int chunkSize = 10000;
                int initialSize = Math.Min(chunkSize, collections.Count);
                foreach (ReadOnlyCollection<IReplayableCollection> chunk in collections.Chunks(chunkSize))
                {
                    List<OGM> parents = new List<OGM>(initialSize);
                    foreach (IReplayableCollection item in chunk)
                        if (item.Parent.PersistenceState != PersistenceState.New && item.Parent.PersistenceState != PersistenceState.NewAndChanged)
                            parents.Add(item.Parent);

                    Dictionary<OGM, RelationshipPersistenceProvider.CollectionItemList> allItems = await RelationshipPersistenceProvider.LoadAsync(parents, collection);

                    foreach (IReplayableCollection item in chunk)
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

        public DatastoreModel Model { get; private set; }
        public PersistenceProvider PersistenceProvider => Model.PersistenceProvider;
        internal NodePersistenceProvider NodePersistenceProvider => PersistenceProvider.NodePersistenceProvider;
        internal RelationshipPersistenceProvider RelationshipPersistenceProvider => PersistenceProvider.RelationshipPersistenceProvider;

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
        public EventOptions FireEvents { get; protected set; }

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
