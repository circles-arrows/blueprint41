using Blueprint41.Core;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.GremlinUnitTest
{
    [TestFixture]
    public abstract class CosmosBase
    {
        /// <summary>
        /// The datastore model will be initialize every test
        /// </summary>
        public abstract Type Datastoremodel { get; }

        [OneTimeSetUp, Order(1)]
        public void InitConnection()
        {
            string hostname = "e16e30fe-0ee0-4-231-b9ee.gremlin.cosmosdb.azure.com";
            int port = 443;
            string authKey = "4vPcOM5cjxjAsgD7UQgIVhx48HkvriT5NmgQ44ug4vQlSUUFghOmseoA9szI44CR2M4vsvfBKe4lQYiDLJyb0g==";
            string database = "sample-database";
            string collection = "sample-graphtest";

            PersistenceProvider.Initialize(Datastoremodel, hostname, port, authKey, database, collection);

            using (Transaction.Begin())
                Transaction.Run("Match (n) detach delete n");
        }
    }
}
