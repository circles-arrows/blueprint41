using Blueprint41.Core;
using Blueprint41.UnitTest.DataStore;
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
            //MockNeo4jPersistenceProvider persistenceProvider = new MockNeo4jPersistenceProvider(DatabaseConnectionSettings.URI, DatabaseConnectionSettings.USER_NAME, DatabaseConnectionSettings.PASSWORD);
            //PersistenceProvider.CurrentPersistenceProvider = persistenceProvider;
            throw new NotImplementedException();

            TearDown();
        }


        [TearDown]
        public void TearDown()
        {
            using (MockModel.BeginTransaction())
            {
                string reset = "Match (n) detach delete n";
                Transaction.Run(reset);

                Transaction.Commit();
            }
#if NEO4J
            using (MockModel.BeginTransaction())
            {
                string clearSchema = "CALL apoc.schema.assert({},{},true) YIELD label, key RETURN *";
                Transaction.Run(clearSchema);
                Transaction.Commit();
            }
#elif MEMGRAPH
            using (MockModel.BeginSession())
            {
                string clearSchema = "CALL schema.assert({},{}, {}, true) YIELD label, key RETURN *";
                Session.Run(clearSchema);
            }
#endif
        }
    }
}
