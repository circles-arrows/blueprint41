using System;
using System.Collections.Generic;
using System.Linq;

namespace Blueprint41.Persistence
{
    internal class Neo4jQueryTranslatorV5 : Neo4jQueryTranslatorV4
    {
        internal Neo4jQueryTranslatorV5(DatastoreModel datastoreModel) : base(datastoreModel)
        {
        }

        #region Compile Functions

        public override string FnExists => "{base} IS NOT NULL";
        public override string FnNotExists => "{base} is NULL";
        public override string FnIsNaN => "isNaN({base})";
        public override string FnApocCreateUuid => "randomUUID()";
        public override string CallApocCreateUuid => "WITH randomUUID() as key";
        public override string TestCompressedString(string alias, string field) => $"toStringOrNull({alias}.`{field}`) = {alias}.`{field}`";

        #endregion

        #region Full Text Indexes

        public override string FtiCreate => "CREATE FULLTEXT INDEX fts FOR (n:{0}) ON EACH [ {1} ]";
        public override string FtiEntity => "{0}";
        public override string FtiProperty => "n.{0}";
        public virtual string FtiEntitySeparator => "|";
        public virtual string FtiPropertySeparator => ", ";
        public override string FtiRemove => "DROP INDEX fts";

        internal override void ApplyFullTextSearchIndexes(IEnumerable<Entity> entities)
        {
            if (HasFullTextSearchIndexes())
            {
                try
                {
                    using (DatastoreModel.PersistenceProvider.NewTransaction(ReadWriteMode.ReadWrite))
                    {
                        Transaction.RunningTransaction.Run(FtiRemove);
                        Transaction.Commit();
                    }
                }
                catch { }
            }

            using (DatastoreModel.PersistenceProvider.NewTransaction(ReadWriteMode.ReadWrite))
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