using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;

using neo4j = Neo4j.Driver;

using Blueprint41.Core;
using Blueprint41.Log;
using System.Threading.Tasks;

namespace Blueprint41.Neo4j.Persistence.Driver.v4
{

    public class Neo4jTransaction : Void.Neo4jTransaction
    {
        internal Neo4jTransaction(Neo4jPersistenceProvider provider, bool readWriteMode, TransactionLogger? logger) : base(readWriteMode, logger)
        {
            Provider = provider;
            Initialize();
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
                Logger.Stop(cypher, callerInfo: new List<string>() { memberName, sourceFilePath, sourceLineNumber.ToString() });
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
                Logger.Stop(cypher, parameters: parameters, callerInfo: new List<string>() { memberName, sourceFilePath, sourceLineNumber.ToString() });
            }
#endif

            //DebugQueryString(cypher, parameters);
            return new Neo4jRawResult(Provider.TaskScheduler, results);
        }

        public Neo4jPersistenceProvider Provider { get; set; }
        public neo4j.IAsyncSession? Session { get; set; }
        public neo4j.IAsyncTransaction? Transaction { get; set; }
        public neo4j.IAsyncQueryRunner? StatementRunner { get; set; }

        protected override void Initialize()
        {
            neo4j.AccessMode accessMode = (ReadWriteMode) ? neo4j.AccessMode.Write : neo4j.AccessMode.Read;

            Session = Provider.Driver.AsyncSession(c =>
            {
                if (Provider.Database is not null)
                    c.WithDatabase(Provider.Database);

                c.WithFetchSize(neo4j.Config.Infinite);
                c.WithDefaultAccessMode(accessMode);
            });

            Transaction = Provider.TaskScheduler.RunBlocking(() => Session.BeginTransactionAsync(), "Begin Transaction");

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
            RaiseOnCommit(this);

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
