using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;

using neo4j = Neo4j.Driver;
using bp41 = Blueprint41.Driver;

namespace Blueprint41.Driver.RuntimeGeneration
{
    public class ServerAddressResolverProxy : neo4j.IServerAddressResolver
    {
        public static ServerAddressResolverProxy Get(bp41.ServerAddressResolver instance) => new ServerAddressResolverProxy() { _instance = instance };
        private bp41.ServerAddressResolver _instance;

        public ISet<neo4j.ServerAddress> Resolve(neo4j.ServerAddress address)
        {
            ISet<bp41.ServerAddress> result = _instance.Resolve(new ServerAddress(address));

            return new HashSet<neo4j.ServerAddress>(result.Select(item => (neo4j.ServerAddress)item._instance));
        }
    }
}