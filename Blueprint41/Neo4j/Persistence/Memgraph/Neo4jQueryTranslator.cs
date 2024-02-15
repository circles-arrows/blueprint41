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

        // Element ID is not a string on Memgraph, this might cause performance issues with updating relationship properties...
        public override string FnElementId => "toString(Id({0}))";
        public override string TestCompressedString(string alias, string field) => throw new NotSupportedException("CompressedString is not supported on Memgraph, since ByteArray is not supported on Memgraph. See: https://memgraph.com/docs/client-libraries/java");

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

        internal override IEnumerable<TypeMapping> FilterSupportedTypeMappings(IEnumerable<TypeMapping> mappings) => mappings
            .Where(item => !item.ShortReturnType.Contains(nameof(CompressedString)));
    }
}