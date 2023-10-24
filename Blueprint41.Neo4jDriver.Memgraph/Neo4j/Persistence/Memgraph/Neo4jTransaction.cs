using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;

using neo4j = Neo4j.Driver;

using Blueprint41.Core;
using Blueprint41.Log;
using System.Threading.Tasks;

namespace Blueprint41.Neo4j.Persistence.Driver.Memgraph
{

    public class Neo4jTransaction : Void.Neo4jTransaction
    {
        internal Neo4jTransaction(Neo4jPersistenceProvider provider, bool readWriteMode, TransactionLogger? logger) : base(readWriteMode, logger)
        {
            Provider = provider;
        }

        public override Transaction WithConsistency(string consistencyToken)
        {
            if (string.IsNullOrEmpty(consistencyToken))
                return this;

            return WithConsistency(PersistenceProvider.CurrentPersistenceProvider.FromToken(consistencyToken));
        }
        public override Transaction WithConsistency(Bookmark consistency)
        {
            Neo4jBookmark? neo4JConsistency = consistency as Neo4jBookmark;
            if (neo4JConsistency is not null && consistency != Bookmark.NullBookmark)
            {
                Consistency ??= new List<Neo4jBookmark>();

                Consistency.Add(neo4JConsistency);
            }

            return this;
        }

        public override RawResult Run(string cypher, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (StatementRunner is null)
                throw new InvalidOperationException("The current transaction was already committed or rolled back.");

#if DEBUG
            Logger?.Start();
#endif
            neo4j.IResultCursor results = Provider.TaskScheduler.RunBlocking(() => StatementRunner.RunAsync(cypher), cypher);

#if DEBUG
            if (Logger is not null)
            {
                Provider.TaskScheduler.RunBlocking(() => results.PeekAsync(), "Peek the Result");
                Logger.Stop(cypher, null, memberName, sourceFilePath, sourceLineNumber);
            }
#endif

            //DebugQueryString(cypher, null);
            return new Neo4jRawResult(Provider.TaskScheduler, results);
        }
        public override RawResult Run(string cypher, Dictionary<string, object?>? parameters, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (StatementRunner is null)
                throw new InvalidOperationException("The current transaction was already committed or rolled back.");

#if DEBUG
            Logger?.Start();
#endif
            neo4j.IResultCursor results = Provider.TaskScheduler.RunBlocking(() => StatementRunner.RunAsync(cypher, parameters), cypher);

#if DEBUG
            if (Logger is not null)
            {
                Provider.TaskScheduler.RunBlocking(() => results.PeekAsync(), "Peek the Result");
                Logger.Stop(cypher, parameters, memberName, sourceFilePath, sourceLineNumber);
            }
#endif

            //DebugQueryString(cypher, parameters);
            return new Neo4jRawResult(Provider.TaskScheduler, results);
        }

        public Neo4jPersistenceProvider Provider { get; set; }
        public neo4j.IAsyncSession? Session { get; set; }
        public neo4j.IAsyncTransaction? Transaction { get; set; }
        public neo4j.IAsyncQueryRunner? StatementRunner { get; set; }

        private List<Neo4jBookmark>? Consistency;

        protected override void Initialize()
        {
            neo4j.AccessMode accessMode = (ReadWriteMode) ? neo4j.AccessMode.Write : neo4j.AccessMode.Read;

            if ((Provider ?? PersistenceProvider.CurrentPersistenceProvider) is not Neo4jPersistenceProvider provider)
                throw new NullReferenceException("CurrentPersistenceProvider is null");

            Session = provider.Driver.AsyncSession(c =>
            {
                if (provider.Database is not null)
                    c.WithDatabase(provider.Database);

                c.WithFetchSize(neo4j.Config.Infinite);
                c.WithDefaultAccessMode(accessMode);

                if (Consistency is not null)
                    c.WithBookmarks(Consistency.Select(item => item.ToBookmark()).ToArray());
            });

            Transaction = provider.TaskScheduler.RunBlocking(() => Session.BeginTransactionAsync(), "Begin Transaction");

            StatementRunner = Transaction;
            base.Initialize();
        }

        protected override void CommitInternal()
        {
            if (Session is null)
                throw new InvalidOperationException("The current transaction was already committed or rolled back.");

            neo4j.IAsyncTransaction? t = Transaction;
            if (t is not null)
                Provider.TaskScheduler.RunBlocking(() => t.CommitAsync(), "Commit Transaction");

            RaiseOnCommit();

            CloseSession();
        }
        protected override void RollbackInternal()
        {
            if (Session is null)
                throw new InvalidOperationException("The current transaction was already committed or rolled back.");

            neo4j.IAsyncTransaction? t = Transaction;
            if (t is not null)
                Provider.TaskScheduler.RunBlocking(() => t.RollbackAsync(), "Rollback Transaction");

            CloseSession();
        }
        protected override void RetryInternal()
        {
            RollbackInternal();
            Initialize();
        }
        private void CloseSession()
        {
            if (Session is not null)
                Provider.TaskScheduler.RunBlocking(() => Session.CloseAsync(), "Close Session");

            Provider.TaskScheduler.ClearHistory();

            Transaction = null;
            StatementRunner = null;
            Session = null;
        }

        public override Bookmark GetConsistency()
        {
            if (Session is null)
                throw new InvalidOperationException("The current transaction was already committed or rolled back.");

            return new Neo4jBookmark(Session.LastBookmark.Values);
        }

        protected override void FlushInternal()
        {
            if (Session is null)
                throw new InvalidOperationException("The current transaction was already committed or rolled back.");

            if (!ReadWriteMode)
            {
                ReadWriteMode = true;
                CommitInternal();
                Initialize();
            }

            base.FlushInternal();

            Provider.TaskScheduler.ClearHistory();
        }
        public void DebugQueryString(string cypherQuery, Dictionary<string, object?>? parameterValues = null)
        {
            if (parameterValues is not null)
            {
                foreach (var queryParam in parameterValues)
                {
                    object paramValue = queryParam.Value?.GetType() == typeof(string) ? string.Format("'{0}'", queryParam.Value.ToString()) : queryParam.Value?.ToString() ?? "NULL";
                    cypherQuery = cypherQuery.Replace(queryParam.Key, paramValue.ToString());
                }
            }
            System.Diagnostics.Debug.WriteLine(cypherQuery);
        }
    }
}
