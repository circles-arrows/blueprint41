using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;

using neo4j = Neo4j.Driver;

using Blueprint41.Persistence;
using Blueprint41.UnitTest.DataStore;
using System.Diagnostics;


namespace Blueprint41.UnitTest.Tests
{
    [TestFixture]
    public abstract class TestBase
    {
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Driver.Configure<neo4j.IDriver>();
            Connect<MockModel>(true);
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
            using (MockModel.BeginSession())
            {
                string reset = "Match (n) detach delete n";
                Session.Run(reset);
            }

            using (MockModel.BeginSession())
            {
#if NEO4J
                string clearSchema = "CALL apoc.schema.assert({},{},true) YIELD label, key RETURN *";
#elif MEMGRAPH
                string clearSchema = "CALL schema.assert({},{}, {}, true) YIELD label, key RETURN *";
#endif
                Session.Run(clearSchema);
            }
        }

        protected T Connect<T>(bool execute)
            where T : DatastoreModel<T>, new()
        {
            return DatastoreModel<T>.Connect(new Uri(DatabaseConnectionSettings.URI), AuthToken.Basic(DatabaseConnectionSettings.USER_NAME, DatabaseConnectionSettings.PASSWORD), DatabaseConnectionSettings.DATA_BASE, new AdvancedConfig()
            {
                CustomCypherLogging = delegate (string cypher, Dictionary<string, object?>? parameters, long elapsedMilliseconds, string? memberName, string? sourceFilePath, int sourceLineNumber)
                {
                    Debug.WriteLine(cypher);
                }
            });
        }
    }
}
