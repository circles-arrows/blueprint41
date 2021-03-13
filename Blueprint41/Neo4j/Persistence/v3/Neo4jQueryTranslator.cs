using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Blueprint41.Core;
using Blueprint41.Neo4j.Model;
using Blueprint41.Neo4j.Schema;
using Blueprint41.Query;

namespace Blueprint41.Neo4j.Persistence.v3
{
    internal class Neo4jQueryTranslator : QueryTranslator
    {
        #region

        internal override NodePersistenceProvider GetNodePersistenceProvider(PersistenceProvider persistenceProvider) => new Neo4jNodePersistenceProvider(persistenceProvider);
        internal override RelationshipPersistenceProvider GetRelationshipPersistenceProvider(PersistenceProvider persistenceProvider) => new Neo4jRelationshipPersistenceProvider(persistenceProvider);
        internal override RefactorTemplates GetTemplates() => new RefactorTemplates_v4();
        internal override SchemaInfo GetSchemaInfo(DatastoreModel datastoreModel) => new SchemaInfo(datastoreModel);

        #endregion
    }
}
