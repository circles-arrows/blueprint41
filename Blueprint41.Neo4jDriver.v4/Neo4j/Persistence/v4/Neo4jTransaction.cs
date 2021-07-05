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
            Task<neo4j.IResultCursor> results = StatementRunner.RunAsync(cypher);

#if DEBUG
            if (Logger is not null)
            {
                results.GetTaskResult().PeekAsync().Wait();
                Logger.Stop(cypher, callerInfo: new List<string>() { memberName, sourceFilePath, sourceLineNumber.ToString() });
            }
#endif

            return new Neo4jRawResult(results);
        }
        public override RawResult Run(string cypher, Dictionary<string, object?>? parameters, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (StatementRunner is null)
                throw new InvalidOperationException("The current transaction was already committed or rolled back.");

#if DEBUG
            Logger?.Start();
#endif
            Task<neo4j.IResultCursor> results = StatementRunner.RunAsync(cypher, parameters);

#if DEBUG
            if (Logger is not null)
            {
                results.GetTaskResult().PeekAsync().Wait();
                Logger.Stop(cypher, parameters: parameters, callerInfo: new List<string>() { memberName, sourceFilePath, sourceLineNumber.ToString() });
            }
#endif

            return new Neo4jRawResult(results);
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
                if(Provider.Database is not null)
                    c.WithDatabase(Provider.Database);

                c.WithFetchSize(neo4j.Config.Infinite);
                c.WithDefaultAccessMode(accessMode); 
            });

            Transaction = Session.BeginTransactionAsync().GetTaskResult();
            StatementRunner = Transaction;
            base.Initialize();
        }

        protected override void OnCommit()
        {
            if (Session is null)
                throw new InvalidOperationException("The current transaction was already committed or rolled back.");

            if (Transaction is not null)
                Transaction.CommitAsync().Wait();

            CloseSession();
        }
        protected override void OnRollback()
        {
            if (Session is null)
                throw new InvalidOperationException("The current transaction was already committed or rolled back.");

            if (Transaction is not null)
                Transaction?.RollbackAsync().Wait();

            CloseSession();
        }
        protected override void OnRetry()
        {
            OnRollback();
            Initialize();
        }
        private void CloseSession()
        {
            if (Session is not null)
                Session.CloseAsync().Wait();

            Transaction = null;
            StatementRunner = null;
            Session = null;
        }

        protected override void FlushPrivate()
        {
            if (Session is null)
                throw new InvalidOperationException("The current transaction was already committed or rolled back.");

            if (!ReadWriteMode)
            {
                ReadWriteMode = true;
                OnCommit();
                Initialize();
            }

            base.FlushPrivate();
        }
    }
}
