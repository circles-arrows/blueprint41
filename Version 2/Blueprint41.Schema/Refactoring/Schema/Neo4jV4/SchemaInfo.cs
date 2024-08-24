using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Blueprint41.Core;
using Blueprint41.Persistence.Provider;


namespace Blueprint41.Refactoring.Schema
{
    public class SchemaInfo_Neo4jV4 : SchemaInfo
    {
        internal SchemaInfo_Neo4jV4(DatastoreModel model) : base(model)
        {
        }

        protected override void Initialize()
        {
            using (DatastoreModel.PersistenceProvider.NewTransaction(ReadWriteMode.ReadWrite))
            {
                bool hasPlugin = DatastoreModel.PersistenceProvider.Translator.HasBlueprint41FunctionalidFnNext.Value;
                FunctionalIds     = hasPlugin ? LoadData("CALL blueprint41.functionalid.list()", record => NewFunctionalIdInfo(record)) : new List<FunctionalIdInfo>(0);
                Constraints       = LoadData("CALL db.constraints()", record => NewConstraintInfo(record, DatastoreModel.PersistenceProvider));
                Indexes           = LoadData("CALL db.indexes()", record => NewIndexInfo(record, DatastoreModel.PersistenceProvider));
                Labels            = LoadSimpleData("CALL db.labels()", "label");
                RelationshipTypes = LoadSimpleData("CALL db.relationshipTypes()", "relationshipType");
            }
        }

        protected override long FindMaxId(FunctionalId functionalId)
        {
            bool first = true;
            string templateNumeric = "MATCH (node:{0}) WHERE toInteger(node.{1}) IS NOT NULL WITH toInteger(node.{1}) AS decoded RETURN case Max(decoded) WHEN NULL THEN 0 ELSE Max(decoded) END as MaxId";
            string templateHash = "MATCH (node:{0}) where node.{1} STARTS WITH '{2}' AND SIZE(node.{1}) = {3} CALL blueprint41.hashing.decode(replace(node.{1}, '{2}', '')) YIELD value as decoded RETURN case Max(decoded) WHEN NULL THEN 0 ELSE Max(decoded) END as MaxId";
            string actualFidValue = "CALL blueprint41.functionalid.current('{0}') YIELD Sequence as sequence RETURN sequence";
            StringBuilder queryBuilder = new StringBuilder();
            foreach (var entity in DatastoreModel.Entities.Where(entity => entity.FunctionalId?.Label == functionalId.Label))
            {
                if (entity.Key is null)
                    continue;

                if (first)
                    first = false;
                else
                    queryBuilder.AppendLine("UNION");

                if (functionalId.Format == IdFormat.Hash)
                    queryBuilder.AppendFormat(templateHash, entity.Label.Name, entity.Key.Name, functionalId.Prefix, functionalId.Prefix.Length + 6);
                else
                    queryBuilder.AppendFormat(templateNumeric, entity.Label.Name, entity.Key.Name);
                queryBuilder.AppendLine();
            }

            if (queryBuilder.Length != 0)
            {
                var ids = LoadData(queryBuilder.ToString(), record => record["MaxId"].As<long>());
                return ids.Count == 0 ? 0 : ids.Max() + 1;
            }
            else
            {
                return LoadData(string.Format(actualFidValue, functionalId.Label), record => record["sequence"].As<int?>()).FirstOrDefault() ?? 0;
            }
        }

        protected override ConstraintInfo   NewConstraintInfo(IDictionary<string, object> rawRecord, PersistenceProvider persistenceProvider)   => new ConstraintInfo(rawRecord, persistenceProvider);
        protected override IndexInfo        NewIndexInfo(IDictionary<string, object> rawRecord, PersistenceProvider persistenceProvider)        => new IndexInfo_Neo4jV4(rawRecord, persistenceProvider);

        internal  override ApplyConstraintProperty NewApplyConstraintProperty(ApplyConstraintEntity parent, Property property, List<(ApplyConstraintAction, string?)> commands) => new ApplyConstraintProperty_Neo4jV4(parent, property, commands);
        internal  override ApplyConstraintProperty NewApplyConstraintProperty(ApplyConstraintEntity parent, string property, List<(ApplyConstraintAction, string?)> commands)   => new ApplyConstraintProperty_Neo4jV4(parent, property, commands);
    }
}