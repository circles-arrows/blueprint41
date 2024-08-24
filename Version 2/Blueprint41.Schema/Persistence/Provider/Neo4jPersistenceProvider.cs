using System;
using System.Collections.Generic;

using Blueprint41.Core;
using Blueprint41.Persistence.Translator;
using Blueprint41.Refactoring;
using Blueprint41.Refactoring.Schema;
using Blueprint41.Refactoring.Schema.Memgraph;

namespace Blueprint41.Persistence.Provider
{
    internal class Neo4jPersistenceProvider : PersistenceProvider
    {
        internal Neo4jPersistenceProvider(DatastoreModel model, string? uri, string? username, string? password, string? database, AdvancedConfig? advancedConfig = null)
            : base(model, uri, username, password, database, advancedConfig ) 
        {
        }

        public override bool IsNeo4j => true;
        public override bool IsMemgraph => false;

        internal override NodePersistenceProvider NodePersistenceProvider => new NodePersistenceProvider(DatastoreModel);
        internal override RelationshipPersistenceProvider RelationshipPersistenceProvider => new RelationshipPersistenceProvider(DatastoreModel);

        internal override RefactorTemplates Templates => new RefactorTemplates_Memgraph(DatastoreModel);
        internal override SchemaInfo SchemaInfo => new SchemaInfo_Memgraph(DatastoreModel);
        internal override QueryTranslator Translator => new MemgraphQueryTranslatorV1(DatastoreModel);

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
