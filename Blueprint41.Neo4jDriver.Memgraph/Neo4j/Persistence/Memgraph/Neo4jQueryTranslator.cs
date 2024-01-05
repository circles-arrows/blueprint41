using System.Collections.Generic;
using System.Linq;

using Blueprint41.Core;
using Blueprint41.Neo4j.Schema;

namespace Blueprint41.Neo4j.Persistence.Memgraph
{
    internal class Neo4jQueryTranslator : v5.Neo4jQueryTranslator
    {
        internal Neo4jQueryTranslator(PersistenceProvider persistenceProvider) : base(persistenceProvider)
        {
        }

        #region

        internal override RefactorTemplates GetTemplates() => new RefactorTemplates_v5();

        internal override SchemaInfo GetSchemaInfo(DatastoreModel datastoreModel) => new Schema.Memgraph.SchemaInfo_Memgraph(datastoreModel);

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