using Blueprint41.Core;
using Blueprint41.Neo4j.Persistence;
using Gremlin.Net.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blueprint41.Gremlin.Cosmos
{
    public class GremlinPersistenceProvider : PersistenceProvider
    {

        private GremlinServer driver;
        public GremlinServer Server
        {
            get
            {
                if (driver == null)
                {
                    lock (typeof(GremlinPersistenceProvider))
                    {
                        if (driver == null)
                            driver = new GremlinServer(hostname, port, enableSsl: true,
                                                    username: "/dbs/" + database + "/colls/" + collection,
                                                    password: authKey);
                    }
                }
                return driver;
            }
        }


        private string hostname;
        private int port;
        private string authKey;
        private string database;
        private string collection;

        public GremlinPersistenceProvider(string hostname, int port, string authKey, string database, string collection)
        {
            this.hostname = hostname;
            this.port = port;
            this.authKey = authKey;
            this.database = database;
            this.collection = collection;
        }

        public override IEnumerable<TypeMapping> SupportedTypeMappings => Neo4JPersistenceProvider.supportedTypeMappings;

        public override Transaction NewTransaction(bool withTransaction)
        {
            return new GremlinTransaction(Server);
        }

        internal override NodePersistenceProvider GetNodePersistenceProvider()
        {
            return new Neo4JNodePersistenceProvider(this);
        }

        internal override RelationshipPersistenceProvider GetRelationshipPersistenceProvider()
        {
            return new Neo4JRelationshipPersistenceProvider(this);
        }
    }
}
