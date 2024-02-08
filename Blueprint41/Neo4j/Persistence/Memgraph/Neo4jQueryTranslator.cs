using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Blueprint41.Core;
using Blueprint41.Neo4j.Model;
using Blueprint41.Neo4j.Persistence.Void;
using Blueprint41.Neo4j.Schema;
using Blueprint41.Query;

namespace Blueprint41.Neo4j.Persistence.Memgraph
{
    internal class Neo4jQueryTranslator : v5.Neo4jQueryTranslator
    {
        internal Neo4jQueryTranslator(Neo4jPersistenceProvider persistenceProvider) : base(persistenceProvider)
        {
        }

        #region

        internal override RefactorTemplates GetTemplates() => new RefactorTemplates_v5();

        internal override SchemaInfo GetSchemaInfo(DatastoreModel datastoreModel) => new Schema.Memgraph.SchemaInfo_Memgraph(datastoreModel, PersistenceProvider);

        #endregion

        #region Compile Functions

        #endregion

        #region Full Text Indexes
        internal override bool HasFullTextSearchIndexes()
        {
            //TODO: memgraph does not support Full-Text search indexes
            return false;
        }

        internal override void ApplyFullTextSearchIndexes(IEnumerable<Entity> entities)
        {
            //TODO: DO NOTHING memgraph does not support Full-Text search indexes
        }

        #endregion
    }
}