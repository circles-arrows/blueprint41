using Blueprint41.Core;
using NUnit.Framework;
using System;

namespace Blueprint41.GremlinUnitTest
{
    [TestFixture]
    public abstract class GremlinBase
    {
        /// <summary>
        /// The datastore model will be initialize every test
        /// </summary>
        public abstract Type Datastoremodel { get; }

        [OneTimeSetUp, Order(1)]
        public void InitConnection()
        {
            string hostname = "localhost";
            int port = 8182;
            string authKey = null;
            string database = null;
            string collection = null;

            PersistenceProvider.Initialize(Datastoremodel, hostname, port, authKey, database, collection);

            using (Transaction.Begin())
                Transaction.Run("Match (n) detach delete n");
        }
    }
}
