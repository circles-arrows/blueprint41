using Blueprint41.Core;
using Blueprint41.Neo4j.Persistence;
using Blueprint41.UnitTest.Mocks;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blueprint41.UnitTest.Tests
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
            using (Transaction.Begin())
            {
                string clearSchema = "CALL apoc.schema.assert({},{},true) YIELD label, key RETURN *";
                Transaction.RunningTransaction.Run(clearSchema);
                Transaction.Commit();
            }
        }
    }
}
