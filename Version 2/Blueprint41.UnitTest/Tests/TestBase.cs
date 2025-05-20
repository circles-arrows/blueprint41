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
        private static Lazy<Driver> _driver = new Lazy<Driver>(delegate()
        {
            Driver.Configure<neo4j.IDriver>();
            return Driver.Get(new Uri(DatabaseConnectionSettings.URI), AuthToken.Basic(DatabaseConnectionSettings.USER_NAME, DatabaseConnectionSettings.PASSWORD));

        }, true);
        private DriverSession GetSession()
        {
            return _driver.Value.Session(o => { o.WithDatabase(DatabaseConnectionSettings.DATA_BASE); });
        }

        [SetUp]
        public virtual void Setup()
        {
            // Run mock model every time because the FunctionalId is wiped out by cleanup and needs to be recreated!         
            var model = Connect<MockModel>();
            model.Execute(true);
        }

        [TearDown]
        public void TearDown()
        {
            using (DriverSession session = GetSession())
            {
                string reset = "Match (n) detach delete n";
                session.Run(reset);
            }

            using (DriverSession session = GetSession())
            {
#if NEO4J
                string clearSchema = "CALL apoc.schema.assert({},{},true) YIELD label, key RETURN *";
#elif MEMGRAPH
                string clearSchema = "CALL schema.assert({},{}, {}, true) YIELD label, key RETURN *";
#endif
                session.Run(clearSchema);
            }
        }

        protected T Connect<T>(bool logToConsole = false, bool teardown = true)
            where T : DatastoreModel<T>, new()
        {
            if (teardown)
                TearDown();
            var model = DatastoreModel<T>.Connect(new Uri(DatabaseConnectionSettings.URI), AuthToken.Basic(DatabaseConnectionSettings.USER_NAME, DatabaseConnectionSettings.PASSWORD), DatabaseConnectionSettings.DATA_BASE, new AdvancedConfig()
            {
                CustomCypherLogging = delegate (string cypher, Dictionary<string, object?>? parameters, long elapsedMilliseconds, string? memberName, string? sourceFilePath, int sourceLineNumber)
                {
                    Debug.WriteLine(cypher);
                }
            });

            model.LogToConsole = logToConsole;
            
            return model;
        }
    }

    public static class DataStoreModelEx
    {
        public static T ExecuteModel<T>(this T model, bool upgradeDatastore = false)
            where T : DatastoreModel, new()
        {
            model.Execute(upgradeDatastore);

            return model;
        }
    }
}
