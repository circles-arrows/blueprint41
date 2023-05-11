using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Blueprint41.Core;
using Blueprint41.Neo4j.Model;
using Blueprint41.Neo4j.Schema;
using Blueprint41.Query;

using q = Blueprint41.Query;

namespace Blueprint41.Neo4j.Persistence.v5
{
    internal class Neo4jQueryTranslator : v4.Neo4jQueryTranslator
    {
        internal Neo4jQueryTranslator(PersistenceProvider persistenceProvider) : base(persistenceProvider) { }

        #region

        internal override NodePersistenceProvider GetNodePersistenceProvider() => new v3.Neo4jNodePersistenceProvider(PersistenceProvider);
        internal override RelationshipPersistenceProvider GetRelationshipPersistenceProvider() => new v3.Neo4jRelationshipPersistenceProvider(PersistenceProvider);
        internal override RefactorTemplates GetTemplates() => new RefactorTemplates_v5();
        internal override SchemaInfo GetSchemaInfo(DatastoreModel datastoreModel) => new Schema.v5.SchemaInfo_v5(datastoreModel);

        #endregion

        #region Compile Functions

        public override string FnExists    => "{base} IS NOT NULL";
        public override string FnNotExists => "{base} is NULL";
        public override string FnIsNaN     => "isNaN({base})";

        #endregion

        #region Full Text Indexes

        public override string FtiCreate => "CREATE FULLTEXT INDEX fts FOR (n:{0}) ON EACH [ {1} ])";
        public override string FtiEntity => "{0}";
        public override string FtiProperty => "{0}";
        public virtual string FtiEntitySeparator => "|";
        public virtual string FtiPropertySeparator => ", ";
        internal override void ApplyFullTextSearchIndexes(IEnumerable<Entity> entities)
        {
            if (HasFullTextSearchIndexes())
            {
                try
                {
                    using (Transaction.Begin(true))
                    {
                        Transaction.RunningTransaction.Run(FtiRemove);
                        Transaction.Commit();
                    }
                }
                catch { }
            }

            using (Transaction.Begin(true))
            {
                string e = string.Join(
                        FtiEntitySeparator,
                        entities.Where(entity => entity.FullTextIndexProperties.Count > 0).Select(entity =>
                            string.Format(
                                FtiEntity,
                                entity.Label.Name
                            )
                        )
                    );
                string p = string.Join(
                        FtiPropertySeparator,
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
    }
}
