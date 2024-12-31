using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

using Blueprint41.Config;
using Blueprint41.Core;
using Blueprint41.Persistence;

namespace Blueprint41
{
    public class Session : DisposableScope<Session>, IStatementRunner, IStatementRunnerAsync
    {
        public DriverSession? DriverSession { get; set; }
        public IQueryRunner? StatementRunner => DriverSession;

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
        }

        private protected TransactionLogger? Logger { get; private set; }
        public static void Log(string message) => RunningSession.Logger?.Log(message);

        protected virtual void InitializeDriverAsync()
        {
            AccessMode accessMode = (ReadWriteMode == ReadWriteMode.ReadWrite) ? AccessMode.Write : AccessMode.Read;

            DriverSession = PersistenceProvider.Driver.Session(c =>
            {
                if (PersistenceProvider.Database is not null)
                    c.WithDatabase(PersistenceProvider.Database);

                c.WithFetchSize(ConfigBuilder.Infinite);
                c.WithDefaultAccessMode(accessMode);

                Bookmarks? consistency = GetConsistency();
                if (consistency is not null)
                    c.WithBookmarks(consistency);
            });
        }

        public DateTime TransactionDate { get; private set; }
        public OptimizeFor OptimizeFor { get; private set; }
        public ReadWriteMode ReadWriteMode { get; private set; }

        #region Session Logic

        public Session WithConsistency(params Bookmarks[] consistency)
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
        public Session WithConsistency(params string[] consistencyTokens)
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

        static public Task<ResultCursor> RunAsync(string cypher, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0) => ((IStatementRunnerAsync)RunningSession).RunAsync(cypher, memberName, sourceFilePath,sourceLineNumber);
        static public Task<ResultCursor> RunAsync(string cypher, Dictionary<string, object?>? parameters, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0) => ((IStatementRunnerAsync)RunningSession).RunAsync(cypher, parameters, memberName, sourceFilePath, sourceLineNumber);
        static public ResultCursor Run(string cypher, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0) => ((IStatementRunner)RunningSession).Run(cypher, memberName, sourceFilePath, sourceLineNumber);
        static public ResultCursor Run(string cypher, Dictionary<string, object?>? parameters, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0) => ((IStatementRunner)RunningSession).Run(cypher, parameters, memberName, sourceFilePath, sourceLineNumber);

        Task<ResultCursor> IStatementRunnerAsync.RunAsync(string cypher, string memberName, string sourceFilePath, int sourceLineNumber)
        {
            if (PersistenceProvider.IsVoidProvider)
            {
#if DEBUG
                Logger?.Start();
                if (Logger is not null)
                    Logger.Stop(cypher, null, memberName, sourceFilePath, sourceLineNumber);
#endif
                return Task.FromResult(new ResultCursor());
            }
            else
            {
                if (StatementRunner is null)
                    throw new InvalidOperationException("The current transaction was already committed or rolled back.");

                return StatementRunner.RunAsync(cypher);
            }
        }
        Task<ResultCursor> IStatementRunnerAsync.RunAsync(string cypher, Dictionary<string, object?>? parameters, string memberName, string sourceFilePath, int sourceLineNumber)
        {
            if (PersistenceProvider.IsVoidProvider)
            {
#if DEBUG
                Logger?.Start();
                if (Logger is not null)
                    Logger.Stop(cypher, null, memberName, sourceFilePath, sourceLineNumber);
#endif
                return Task.FromResult(new ResultCursor());
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
                if (StatementRunner is null)
                    throw new InvalidOperationException("The current transaction was already committed or rolled back.");

                return StatementRunner.Run(cypher);
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

        public Bookmarks? GetConsistency() => (DriverSession is not null) ? Driver.I_ASYNC_SESSION.LastBookmarks(DriverSession._instance) : null;

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
