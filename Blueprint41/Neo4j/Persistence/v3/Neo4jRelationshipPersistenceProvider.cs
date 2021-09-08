using System;
using System.Collections.Generic;
using System.Linq;

using Blueprint41.Core;
using Blueprint41.Dynamic;
using Blueprint41.Neo4j.Refactoring;

namespace Blueprint41.Neo4j.Persistence.v3
{
    internal class Neo4jRelationshipPersistenceProvider : Void.Neo4jRelationshipPersistenceProvider
    {
        public Neo4jRelationshipPersistenceProvider(PersistenceProvider factory) : base(factory)
        {
        }
    }
}
