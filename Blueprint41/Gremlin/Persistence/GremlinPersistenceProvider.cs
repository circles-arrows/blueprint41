using Blueprint41.Core;
using Blueprint41.Gremlin.Persistence;
using Blueprint41.Neo4j.Persistence;
using Gremlin.Net.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blueprint41.Gremlin.Persistence
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
                            driver = new GremlinServer(hostname, port, enableSsl: ssl,
                                                    username: "/dbs/" + database + "/colls/" + collection,
                                                    password: authKey);
                    }
                }
                return driver;
            }
        }


        readonly string hostname;
        readonly int port;
        readonly string authKey;
        readonly string database;
        readonly string collection;
        readonly bool ssl = false;

        public GremlinPersistenceProvider(string hostname, int port = 8182, string authKey = null, string database = null, string collection = null)
        {
            this.hostname = hostname;
            this.port = port;
            this.authKey = authKey;
            this.database = database;
            this.collection = collection;
            this.ssl = !(port == 8182) && hostname.Contains("localhost") == false;
        }

        public override IEnumerable<TypeMapping> SupportedTypeMappings => Neo4JPersistenceProvider.supportedTypeMappings;

        public override Transaction NewTransaction(bool withTransaction)
        {
            return new GremlinTransaction(Server);
        }

        internal override NodePersistenceProvider GetNodePersistenceProvider()
        {
            return new GremlinNodePersistenceProvider(this);
        }

        internal override RelationshipPersistenceProvider GetRelationshipPersistenceProvider()
        {
            return new Neo4JRelationshipPersistenceProvider(this);
        }
    }
}
