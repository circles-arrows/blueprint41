using Blueprint41.Core;
using Blueprint41.Response;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blueprint41.UnitTest.Base
{
    [TestFixture]
    public abstract class Neo4jBase
    {
        /// <summary>
        /// The datastore model will be initialize every test
        /// </summary>
        public abstract Type Datastoremodel { get; }

        [SetUp]
        public void InitConnection()
        {
            string uri = "bolt://localhost:7687";
            string username = "neo4j";
            string pass = "neo";

            PersistenceProvider.Initialize(Datastoremodel, uri, username, pass);
        }

        [TearDown]
        public void TearDown()
        {
            using (Transaction.Begin())
            {
                Transaction.Run("CALL apoc.schema.assert({}, {})");
                Transaction.Run("Match (n) detach delete n");

                IGraphResponse response = Transaction.Run("CALL apoc.schema.nodes() yield name RETURN name");

                foreach (var record in response)
                {
                    string dropCons = $"DROP {record["name"]}";
                    Transaction.Run(dropCons);
                }
            }
        }
    }
}
