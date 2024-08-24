using System;
using System.Collections.Generic;

using Blueprint41.Core;

namespace Blueprint41.Persistence.Provider
{
    internal class Neo4jPersistenceProvider : PersistenceProvider
    {
        internal Neo4jPersistenceProvider(DatastoreModel model, string? uri, string? username, string? password, string? database, AdvancedConfig? advancedConfig = null)
            : base(model, uri, username, password, database, advancedConfig ) 
        {
        }

        public override Session NewSession(ReadWriteMode mode, OptimizeFor optimize = OptimizeFor.PartialSubGraphAccess)
        {
            throw new NotImplementedException();
        }
        public override Transaction NewTransaction(ReadWriteMode mode, OptimizeFor optimize = OptimizeFor.PartialSubGraphAccess)
        {
            throw new NotImplementedException();
        }

        protected internal override List<TypeMapping> GetSupportedTypeMappings()
        {
            throw new NotImplementedException();
        }
    }
}
