using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Blueprint41.Async.Core;
using Blueprint41.Async.Neo4j.Model;
using Blueprint41.Async.Neo4j.Schema;
using Blueprint41.Async.Query;

namespace Blueprint41.Async.Neo4j.Persistence.Void
{
    internal class Neo4jQueryTranslator : QueryTranslator
    {
        internal Neo4jQueryTranslator(Neo4jPersistenceProvider persistenceProvider) : base(persistenceProvider) { }

        #region

        internal override NodePersistenceProvider GetNodePersistenceProvider() => new Neo4jNodePersistenceProvider(PersistenceProvider);
        internal override RelationshipPersistenceProvider GetRelationshipPersistenceProvider() => new Neo4jRelationshipPersistenceProvider(PersistenceProvider);
        internal override SchemaInfo GetSchemaInfo(DatastoreModel datastoreModel) => new SchemaInfo(datastoreModel, PersistenceProvider);
        internal override RefactorTemplates GetTemplates() => new RefactorTemplates();

        #endregion
    }
}
