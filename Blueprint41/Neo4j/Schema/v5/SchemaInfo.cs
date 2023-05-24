using System.Collections.Generic;
using System.Linq;
using System.Text;

using Blueprint41.Core;

namespace Blueprint41.Neo4j.Schema.v5
{
    public class SchemaInfo_v5 : v4.SchemaInfo_v4
    {
        internal SchemaInfo_v5(DatastoreModel model) : base(model) { }

        protected override void Initialize()
        {
            using (Transaction.Begin())
            {
                bool hasPlugin = Model.PersistenceProvider.Translator.HasBlueprint41FunctionalidFnNext.Value;
                FunctionalIds = hasPlugin ? LoadData("CALL blueprint41.functionalid.list()", record => NewFunctionalIdInfo(record)) : new List<FunctionalIdInfo>(0);
                Constraints = LoadData("show constraints", record => NewConstraintInfo(record)).Where(item => item.Entity is not null && item.Field is not null).ToArray();
                Indexes = LoadData("show indexes", record => NewIndexInfo(record)).Where(item => item.Entity is not null && item.Field is not null).ToArray();
                Labels = LoadSimpleData("CALL db.labels()", "label");
                PropertyKeys = LoadSimpleData("CALL db.propertyKeys()", "propertyKey");
                RelationshipTypes = LoadSimpleData("CALL db.relationshipTypes()", "relationshipType");
            }
        }
        protected override long FindMaxId(FunctionalId functionalId)
        {
            bool first = true;
            string templateNumeric = "MATCH (node:{0}) WHERE toInteger(node.Uid) IS NOT NULL WITH toInteger(node.Uid) AS decoded RETURN CASE WHEN Max(decoded) IS NULL THEN 0 ELSE Max(decoded) END as MaxId";
            string templateHash = "MATCH (node:{0}) where node.Uid STARTS WITH '{1}' AND SIZE(node.Uid) = {2} CALL blueprint41.hashing.decode(replace(node.Uid, '{1}', '')) YIELD value as decoded RETURN CASE WHEN Max(decoded) IS NULL THEN 0 ELSE Max(decoded) END as MaxId";
            string actualFidValue = "CALL blueprint41.functionalid.current('{0}') YIELD Sequence as sequence RETURN sequence";
            StringBuilder queryBuilder = new();
            foreach (var entity in Model.Entities.Where(entity => entity.FunctionalId?.Label == functionalId.Label))
            {
                if (first)
                    first = false;
                else
                    queryBuilder.AppendLine("UNION");

                if (functionalId.Format == IdFormat.Hash)
                    queryBuilder.AppendFormat(templateHash, entity.Label.Name, functionalId.Prefix, functionalId.Prefix.Length + 6);
                else
                    queryBuilder.AppendFormat(templateNumeric, entity.Label.Name);
                queryBuilder.AppendLine();
            }

            if (queryBuilder.Length != 0)
            {
                var ids = LoadData(queryBuilder.ToString(), record => record.Values["MaxId"].As<int>());
                return ids.Count == 0 ? 0 : ids.Max() + 1;
            }
            else
            {
                return LoadData(string.Format(actualFidValue, functionalId.Label), record => record.Values["sequence"].As<int?>()).FirstOrDefault() ?? 0;
            }
        }

        protected override ConstraintInfo NewConstraintInfo(RawRecord rawRecord) => new ConstraintInfo_v5(rawRecord);
        protected override IndexInfo NewIndexInfo(RawRecord rawRecord) => new IndexInfo_v5(rawRecord);
        internal override ApplyConstraintProperty NewApplyConstraintProperty(ApplyConstraintEntity parent, Property property, List<(ApplyConstraintAction, string?)> commands) => new ApplyConstraintProperty_v5(parent, property, commands);
        internal override ApplyConstraintProperty NewApplyConstraintProperty(ApplyConstraintEntity parent, string property, List<(ApplyConstraintAction, string?)> commands) => new ApplyConstraintProperty_v5(parent, property, commands);

    }
}