using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Blueprint41.Core;
using Blueprint41.Persistence;
using driver = Blueprint41.Driver;

namespace Blueprint41
{
    public class Session : DisposableScope<Session>, IStatementRunner, IStatementRunnerAsync
    {
        public driver.Session? DriverSession { get; set; }
        public driver.IQueryRunner? StatementRunner => DriverSession;


        static internal Session Get(PersistenceProvider provider, ReadWriteMode readwrite, OptimizeFor optimize, TransactionLogger? logger)
        {
            Session session = new Session(provider, readwrite, optimize, logger);
            session.InitializeDriverAsync();
            return session;
        }

        private Session(PersistenceProvider provider, ReadWriteMode readwrite, OptimizeFor optimize, TransactionLogger? logger)
        {
            Logger = logger;
            OptimizeFor = optimize;
            ReadWriteMode = readwrite;
            //DisableForeignKeyChecks = false;

            PersistenceProvider = provider;
            Attach();
        }
        private protected TransactionLogger? Logger { get; private set; }
        public static void Log(string message) => RunningSession.Logger?.Log(message);

        private void InitializeDriverAsync()
        {
            driver.AccessMode accessMode = (ReadWriteMode == ReadWriteMode.ReadWrite) ? driver.AccessMode.Write : driver.AccessMode.Read;

            DriverSession = PersistenceProvider.Driver.Session(c =>
            {
                if (PersistenceProvider.Database is not null)
                    c.WithDatabase(PersistenceProvider.Database);

                c.WithFetchSize(driver.ConfigBuilder.Infinite);
                c.WithDefaultAccessMode(accessMode);

                //if (Consistency is not null)
                //    c.WithBookmarks(Consistency);
            });
        }

        public DateTime TransactionDate { get; private set; }
        public OptimizeFor OptimizeFor { get; private set; }
        public ReadWriteMode ReadWriteMode { get; private set; }

        #region Session Logic

        public virtual Session WithConsistency(Bookmark consistency)
        {
            return this;
        }
        public virtual Session WithConsistency(string consistencyToken)
        {
            return this;
        }
        public Session WithConsistency(params Bookmark[] consistency)
        {
            if (consistency is not null)
            {
                foreach (Bookmark item in consistency)
                    WithConsistency(item);
            }

            return this;
        }
        public Session WithConsistency(params string[] consistencyTokens)
        {
            if (consistencyTokens is not null)
            {
                foreach (string item in consistencyTokens)
                    WithConsistency(item);
            }

            return this;
        }

        static public Task<driver.ResultCursor> RunAsync(string cypher, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0) => ((IStatementRunnerAsync)RunningSession).RunAsync(cypher, memberName, sourceFilePath,sourceLineNumber);
        static public Task<driver.ResultCursor> RunAsync(string cypher, Dictionary<string, object?>? parameters, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0) => ((IStatementRunnerAsync)RunningSession).RunAsync(cypher, parameters, memberName, sourceFilePath, sourceLineNumber);
        static public driver.ResultCursor Run(string cypher, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0) => ((IStatementRunner)RunningSession).Run(cypher, memberName, sourceFilePath, sourceLineNumber);
        static public driver.ResultCursor Run(string cypher, Dictionary<string, object?>? parameters, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0) => ((IStatementRunner)RunningSession).Run(cypher, parameters, memberName, sourceFilePath, sourceLineNumber);

        Task<driver.ResultCursor> IStatementRunnerAsync.RunAsync(string cypher, string memberName, string sourceFilePath, int sourceLineNumber)
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
        Task<driver.ResultCursor> IStatementRunnerAsync.RunAsync(string cypher, Dictionary<string, object?>? parameters, string memberName, string sourceFilePath, int sourceLineNumber)
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

        public static Session RunningSession
        {
            get
            {
                Session? session = Current;

                if (session is null)
                    throw new InvalidOperationException("There is no session, you should create one first -> using (Session.Begin()) { ... }");

                return session;
            }
        }

        public virtual Bookmark GetConsistency() => Bookmark.NullBookmark;

        #endregion

        #region PersistenceProviderFactory

        public PersistenceProvider PersistenceProvider { get; private set; }
        internal NodePersistenceProvider NodePersistenceProvider => PersistenceProvider.NodePersistenceProvider;
        internal RelationshipPersistenceProvider RelationshipPersistenceProvider => PersistenceProvider.RelationshipPersistenceProvider;

        #endregion

        protected override void Cleanup() => CloseSession();
        protected virtual void CloseSession() { }
    }
}
