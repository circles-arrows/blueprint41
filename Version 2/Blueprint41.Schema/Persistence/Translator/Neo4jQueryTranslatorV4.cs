using System;
using System.Collections.Generic;
using Blueprint41.Core;
using Blueprint41.Persistence.Provider;
using Blueprint41.Refactoring.Schema;

namespace Blueprint41.Persistence.Translator
{
    internal class Neo4jQueryTranslatorV4 : QueryTranslator
    {
        internal Neo4jQueryTranslatorV4(DatastoreModel datastoreModel) : base(datastoreModel)
        {
        }

        internal override void ApplyFullTextSearchIndexes(IEnumerable<Entity> entities)
        {
            throw new NotImplementedException();
        }
        internal override NodePersistenceProvider GetNodePersistenceProvider()
        {
            throw new NotImplementedException();
        }
        internal override RelationshipPersistenceProvider GetRelationshipPersistenceProvider()
        {
            throw new NotImplementedException();
        }
        internal override SchemaInfo GetSchemaInfo()
        {
            throw new NotImplementedException();
        }
        internal override RefactorTemplates GetTemplates()
        {
            throw new NotImplementedException();
        }
    }
}