using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Blueprint41.Core;
using Blueprint41.Neo4j.Model;
using Blueprint41.Neo4j.Schema;
using Blueprint41.Query;

namespace Blueprint41.Neo4j.Persistence.Void
{
    internal class Neo4jQueryTranslator : QueryTranslator
    {
        internal Neo4jQueryTranslator(PersistenceProvider persistenceProvider) : base(persistenceProvider) { }

        #region

        internal override NodePersistenceProvider GetNodePersistenceProvider() => new Persistence.Void.Neo4jNodePersistenceProvider(PersistenceProvider);
        internal override RelationshipPersistenceProvider GetRelationshipPersistenceProvider() => new Persistence.Void.Neo4jRelationshipPersistenceProvider(PersistenceProvider);
        internal override SchemaInfo GetSchemaInfo(DatastoreModel datastoreModel) => new SchemaInfo(datastoreModel);
        internal override RefactorTemplates GetTemplates() => new RefactorTemplates();

        #endregion
    }
}
