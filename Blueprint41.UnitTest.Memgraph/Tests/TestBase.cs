using Blueprint41.Core;
using Blueprint41.Neo4j.Persistence;
using Blueprint41.UnitTest.Memgraph.Mocks;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blueprint41.UnitTest.Memgraph.Tests
{
    [TestFixture]
    public abstract class TestBase
    {
        [SetUp]
        public void Setup()
        {
            TearDown();
            MockNeo4jPersistenceProvider persistenceProvider = new MockNeo4jPersistenceProvider(DatabaseConnectionSettings.URI, DatabaseConnectionSettings.USER_NAME, DatabaseConnectionSettings.PASSWORD);
            PersistenceProvider.CurrentPersistenceProvider = persistenceProvider;

            TearDown();
        }


        [TearDown]
        public void TearDown()
        {
            using (Transaction.Begin())
            {
                string reset = "Match (n) detach delete n";
                Transaction.RunningTransaction.Run(reset);

                Transaction.Commit();
            }

            string clearSchema = "CALL schema.assert({},{}) YIELD label, key RETURN *";
            PersistenceProvider.CurrentPersistenceProvider.Run(clearSchema);
        }
    }
}
