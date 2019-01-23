using System;
using System.Collections.Generic;
using System.Text;
using Blueprint41.Core;

namespace Blueprint41.Gremlin.Persistence
{
    internal class GremlinNodePersistenceProvider : Neo4j.Persistence.Neo4JNodePersistenceProvider
    {
        public GremlinNodePersistenceProvider(PersistenceProvider factory) : base(factory)
        {
        }

        public override void Delete(OGM item)
        {
            ForceDelete(item);
        }
    }
}
