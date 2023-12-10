using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

using neo4j = Neo4j.Driver;

using Blueprint41.Core;
using Blueprint41.Log;

using driver = Blueprint41.Neo4j.Persistence.Driver.v5;
using System.Linq;

namespace Blueprint41.UnitTest.Mocks
{
    public class MockNeo4jPersistenceProvider : driver.Neo4jPersistenceProvider
    {
        public MockNeo4jPersistenceProvider(string uri, string username, string pass) : base(uri, username, pass, "unittest") { }

        public override Transaction NewTransaction(bool readWriteMode) => new MockNeo4jTransaction(this, readWriteMode, TransactionLogger);
    }

    public class MockNeo4jTransaction : driver.Neo4jTransaction
    {
        internal MockNeo4jTransaction(MockNeo4jPersistenceProvider provider, bool readWriteMode, TransactionLogger? logger) : base(provider, readWriteMode, logger)
        {
        }

        protected override void Initialize()
        {
            neo4j.AccessMode accessMode = (ReadWriteMode) ? neo4j.AccessMode.Write : neo4j.AccessMode.Read;

            if ((Provider ?? PersistenceProvider.CurrentPersistenceProvider) is not MockNeo4jPersistenceProvider provider)
                throw new InvalidOperationException("CurrentPersistenceProvider is null");

            Session = new MockSession(provider.Driver.AsyncSession(c =>
            {
                if (provider.Database is not null)
                    c.WithDatabase(provider.Database);

                c.WithFetchSize(neo4j.Config.Infinite);
                c.WithDefaultAccessMode(accessMode);

                if (Consistency is not null)
                    c.WithBookmarks(Consistency.Select(item => item.ToBookmark()).ToArray());
            }));

            Transaction = provider.TaskScheduler.RunBlocking(() => Session.BeginTransactionAsync(), "Begin Transaction");

            StatementRunner = Transaction;
        }
    }
}
