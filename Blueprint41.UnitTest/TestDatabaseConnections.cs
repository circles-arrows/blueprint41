using Blueprint41.Core;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blueprint41.UnitTest
{
    [TestFixture]
    internal class TestDatabaseConnections
    {
        class Neo4jDatastoremodel : DatastoreModel<Neo4jDatastoremodel>
        {
            public override GraphFeatures TargetFeatures => GraphFeatures.Neo4j;

            protected override void SubscribeEventHandlers() { }
        }

        class GremlinDatastoremodel : DatastoreModel<Neo4jDatastoremodel>
        {
            public override GraphFeatures TargetFeatures => GraphFeatures.Gremlin;

            protected override void SubscribeEventHandlers() { }
        }


        [Test]
        public void EnsureConnectionIsNeo4jConnection()
        {
            string uri = "bolt://localhost:7687";
            string username = "neo4j";
            string pass = "neo";

            PersistenceProvider.Initialize(typeof(Neo4jDatastoremodel), uri, username, pass);
            Assert.IsTrue(PersistenceProvider.CurrentPersistenceProvider.GetType() == typeof(Neo4j.Persistence.Neo4JPersistenceProvider));
            Assert.Throws<MissingMethodException>(() => { PersistenceProvider.Initialize(typeof(GremlinDatastoremodel), uri, username, pass); });
        }

        [Test]
        public void EnsureConnectionIsGremlinConnection()
        {
            // Connection details for cosmos
            string hostname = "*.gremlin.cosmosdb.azure.com";
            int port = 443;
            string authKey = "authKey";
            string database = "sample-database";
            string collection = "sample-graph";

            // Connection details for a local gremlin server
            hostname = "localhost";
            port = 8182;
            authKey = database = collection = null;

            PersistenceProvider.Initialize(typeof(GremlinDatastoremodel), hostname, port, authKey, database, collection);
            Assert.IsTrue(PersistenceProvider.CurrentPersistenceProvider.GetType() == typeof(Gremlin.GremlinPersistenceProvider));
            Assert.Throws<MissingMethodException>(() => { PersistenceProvider.Initialize(typeof(Neo4jDatastoremodel), hostname, port, authKey, database, collection); });
        }
    }
}
