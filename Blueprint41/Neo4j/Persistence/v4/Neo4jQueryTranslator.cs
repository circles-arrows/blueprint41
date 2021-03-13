using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Blueprint41.Core;
using Blueprint41.Neo4j.Model;
using Blueprint41.Neo4j.Schema;
using Blueprint41.Query;

namespace Blueprint41.Neo4j.Persistence.v4
{
    internal class Neo4jQueryTranslator : QueryTranslator
    {
        #region Full Text Indexes

        public override string FtiSearch    => "CALL db.index.fulltext.queryNodes(\"fts\", \"{0}\") YIELD node AS {1}";
        public override string FtiWeight    => ", score AS {0}";
        public override string FtiCreate    => "CALL db.index.fulltext.createNodeIndex(\"fts\", [ {0} ], [ {1} ])";
        public override string FtiEntity    => "\"{0}\"";
        public override string FtiProperty  => "\"{0}\"";
        public override string FtiSeparator => ", ";
        public override string FtiRemove    => "CALL db.index.fulltext.drop(\"fts\")";

        internal override void ApplyFullTextSearchIndexes(IEnumerable<Entity> entities)
        {
            using (Transaction.Begin(true))
            {
                try
                {
                    Transaction.RunningTransaction.Run(FtiRemove);
                    Transaction.Commit();
                }
                catch (Exception ex)
                {
                    if (!ex.Message.StartsWith("Failed to invoke procedure `db.index.fulltext.drop`: Caused by: java.lang.IllegalArgumentException: No index found with the name"))
                        throw;
                    // there's no procedure to check for full text indexes
                }
            }

            using (Transaction.Begin(true))
            {
                string e = string.Join(
                        FtiSeparator,
                        entities.Where(entity => entity.FullTextIndexProperties.Count > 0).Select(entity =>
                            string.Format(
                                FtiEntity,
                                entity.Label.Name
                            )
                        )
                    );
                string p = string.Join(
                        FtiSeparator,
                        entities.Where(entity => entity.FullTextIndexProperties.Count > 0).SelectMany(entity => entity.FullTextIndexProperties).Select(property =>
                            string.Format(
                                FtiProperty,
                                property.Name
                            )
                        ).Distinct()
                    );
                string query = string.Format(FtiCreate, e, p);

                Transaction.RunningTransaction.Run(query);
                Transaction.Commit();
            }
        }

        #endregion

        #region

        internal override NodePersistenceProvider GetNodePersistenceProvider(PersistenceProvider persistenceProvider) => new v3.Neo4jNodePersistenceProvider(persistenceProvider);
        internal override RelationshipPersistenceProvider GetRelationshipPersistenceProvider(PersistenceProvider persistenceProvider) => new v3.Neo4jRelationshipPersistenceProvider(persistenceProvider);
        internal override RefactorTemplates GetTemplates() => new RefactorTemplates_v4();
        internal override SchemaInfo GetSchemaInfo(DatastoreModel datastoreModel) => new Schema.v4.SchemaInfo_v4(datastoreModel);

        #endregion
    }
}
