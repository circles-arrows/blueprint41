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
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            MockModel model = MockModel.Connect(new Uri(DatabaseConnectionSettings.URI), AuthToken.Basic(DatabaseConnectionSettings.USER_NAME, DatabaseConnectionSettings.PASSWORD), DatabaseConnectionSettings.DATA_BASE);
            model.Execute(true);
        }

        [SetUp]
        public void Setup()
        {
            TearDown();

            // Run mock model every time because the FunctionalId is wiped out by cleanup and needs to be recreated!
            MockModel model = new MockModel()
            {
                LogToConsole = true
            };
            model.Execute(true);
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
