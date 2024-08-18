using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Data;
using System.Text;

using Blueprint41.Sync.Core;
using Blueprint41.Sync.Query;
using Blueprint41.Sync.Neo4j.Model;

namespace Blueprint41.Sync.Neo4j.Persistence.v3
{
    internal class Neo4jNodePersistenceProvider : Void.Neo4jNodePersistenceProvider
    {
        public Neo4jNodePersistenceProvider(PersistenceProvider factory) : base(factory)
        {
        }
    }
}
