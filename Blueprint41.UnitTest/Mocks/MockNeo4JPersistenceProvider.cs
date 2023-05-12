using Blueprint41.Core;
using Blueprint41.Neo4j.Persistence.Driver.v5;

using Neo4j.Driver;

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Blueprint41.UnitTest.Mocks
{
    public class MockNeo4JPersistenceProvider : Neo4jPersistenceProvider
    {
        public bool NotNeo4jTransaction { get; set; }

        public MockNeo4JPersistenceProvider(string uri, string username, string pass) : base(uri, username, pass)
        {

        }

        public override Transaction NewTransaction(bool readWriteMode)
        {
            if (NotNeo4jTransaction)
                return new NotNeo4jTransaction();

            Neo4jTransaction transaction = base.NewTransaction(readWriteMode) as Neo4jTransaction;
            AccessMode accessMode = (readWriteMode) ? AccessMode.Write : AccessMode.Read;

            transaction.Session = this.Driver.AsyncSession(c =>
            {
                if (this.Database is not null)
                    c.WithDatabase(this.Database);

                c.WithFetchSize(Config.Infinite);
                c.WithDefaultAccessMode(accessMode);
            });
            IAsyncSession session = transaction.Session;
            transaction.Session = new MockSession(session);
            transaction.StatementRunner = transaction.Session;
            transaction.Transaction?.Dispose();
            transaction.Transaction = null;

            if (readWriteMode)
            {
                transaction.Transaction = this.TaskScheduler.RunBlocking(() => transaction.Session.BeginTransactionAsync(), "Begin Transaction");
                transaction.StatementRunner = transaction.Transaction;
            }

            return transaction;
        }
        internal CustomTaskScheduler TaskScheduler
        {
            get
            {
                if (taskScheduler is null)
                {
                    lock (sync)
                    {
                        if (taskScheduler is null)
                        {
                            CustomTaskQueueOptions main = new CustomTaskQueueOptions(10, 50);
                            CustomTaskQueueOptions sub = new CustomTaskQueueOptions(4, 20);
                            taskScheduler = new CustomThreadSafeTaskScheduler(main, sub);
                        }
                    }
                }

                return taskScheduler;
            }
        }
        private CustomTaskScheduler? taskScheduler = null;
        private static object sync = new object();
    }

    public class NotNeo4jTransaction : Transaction
    {
        public override RawResult Run(string cypher, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
            throw new NotImplementedException();
        }

        public override RawResult Run(string cypher, Dictionary<string, object> parameters, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
            throw new NotImplementedException();
        }

        protected override void ApplyFunctionalId(FunctionalId functionalId)
        {

        }

        protected override void CommitInternal()
        {
        }

        protected override void Initialize()
        {
        }

        protected override void RetryInternal()
        {
        }

        protected override void RollbackInternal()
        {
        }
    }
}
