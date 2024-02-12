using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using Blueprint41.Core;
using Blueprint41.Neo4j.Persistence.Void;

namespace Blueprint41.Neo4j.Schema.Memgraph
{
    public class SchemaInfo_Memgraph : v5.SchemaInfo_v5
    {
        internal SchemaInfo_Memgraph(DatastoreModel model, Neo4jPersistenceProvider persistenceProvider) : base(model, persistenceProvider) { }

        protected override void Initialize()
        {
            Indexes = ImplicitLoadData("SHOW INDEX INFO", record => NewIndexInfo(record, PersistenceProvider)).Where(item => item.Entity is not null && item.Field is not null).ToArray();
            Constraints = ImplicitLoadData("SHOW CONSTRAINT INFO", record => NewConstraintInfo(record, PersistenceProvider)).Where(item => item.Entity is not null && item.Field is not null).ToArray();
            Labels = ImplicitLoadData("SHOW NODE_LABELS INFO", record => record.Values["node labels"].As<string>());
            RelationshipTypes = ImplicitLoadData("SHOW EDGE_TYPES INFO", record => record.Values["edge types"].As<string>());

            using (Transaction.Begin())
            {
                bool hasPlugin = Model.PersistenceProvider.Translator.HasBlueprint41FunctionalidFnNext.Value;
                FunctionalIds = hasPlugin ? LoadData("CALL blueprint41.functionalid.list()", record => NewFunctionalIdInfo(record)) : new List<FunctionalIdInfo>(0);
                PropertyKeys = LoadSimpleData("CALL schema.node_type_properties() YIELD propertyName as propertyKey", "propertyKey");
            }
        }
        protected IReadOnlyList<T> ImplicitLoadData<T>(string procedure, Func<RawRecord, T> processor)
        {
            bool retry;
            IReadOnlyList<T>? data = null;
            do
            {
                try
                {
                    retry = false;

                    RawResult result = PersistenceProvider.RunImplicit(procedure);
                    data = result.Select(processor).ToArray();
                }
                catch (Exception clientException) when (clientException.Message.Contains("is still populating"))
                {
                    retry = true;
                    Thread.Sleep(500);
                }
            } while (retry);

            return data ?? throw new NotSupportedException("Could not load the data.");
        }
        protected override long FindMaxId(FunctionalId functionalId)
        {
            bool first = true;
            const string templateNumeric = "MATCH (node:{0}) WHERE toInteger(node.Uid) IS NOT NULL WITH toInteger(node.Uid) AS decoded RETURN CASE WHEN Max(decoded) IS NULL THEN 0 ELSE Max(decoded) END as MaxId";
            const string templateHash = "MATCH (node:{0}) where node.Uid STARTS WITH '{1}' AND SIZE(node.Uid) = {2} CALL blueprint41.hashing.decode(replace(node.Uid, '{1}', '')) YIELD value as decoded RETURN CASE WHEN Max(decoded) IS NULL THEN 0 ELSE Max(decoded) END as MaxId";
            const string actualFidValue = "CALL blueprint41.functionalid.current('{0}') YIELD Sequence as sequence RETURN sequence";
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

        protected override ConstraintInfo NewConstraintInfo(RawRecord rawRecord, Neo4jPersistenceProvider neo4JPersistenceProvider) => new ConstraintInfo_Memgraph(rawRecord, neo4JPersistenceProvider);
        protected override IndexInfo NewIndexInfo(RawRecord rawRecord, Neo4jPersistenceProvider neo4JPersistenceProvider) => new IndexInfo_Memgraph(rawRecord, neo4JPersistenceProvider);
        internal override ApplyConstraintProperty NewApplyConstraintProperty(ApplyConstraintEntity parent, Property property, List<(ApplyConstraintAction, string?)> commands) => new ApplyConstraintProperty_Memgraph(parent, property, commands);
        internal override ApplyConstraintProperty NewApplyConstraintProperty(ApplyConstraintEntity parent, string property, List<(ApplyConstraintAction, string?)> commands) => new ApplyConstraintProperty_Memgraph(parent, property, commands);
        internal override List<(ApplyConstraintAction, string?)> ComputeCommands(IEntity entity, IndexType indexType, bool nullable, bool isKey, IEnumerable<ConstraintInfo> constraints, IEnumerable<IndexInfo> indexes)
        {
            ConstraintInfo? uniqueConstraint = entity.IsVirtual ? null : constraints.FirstOrDefault(item => item.IsUnique);
            ConstraintInfo? mandatoryConstraint = entity.IsVirtual ? null : constraints.FirstOrDefault(item => item.IsMandatory);
            ConstraintInfo? keyConstraint = entity.IsVirtual ? null : constraints.FirstOrDefault(item => item.IsKey);
            IndexInfo? indexInfo = entity.IsVirtual ? null : indexes.FirstOrDefault(item => item.IsIndexed);

            List<(ApplyConstraintAction, string?)> commands = new();

            if (entity.IsAbstract && indexType == IndexType.Unique)
                indexType = IndexType.Indexed;

            if (isKey)
                indexType = IndexType.None;

            switch (indexType)
            {
                case IndexType.None:
                    if (uniqueConstraint is not null)
                    {
                        // Database has an unique index, but we want no index at all
                        commands.Add((ApplyConstraintAction.DeleteUniqueConstraint, uniqueConstraint.Name));
                    }
                    else if (indexInfo is not null && indexInfo.OwningConstraint is null)
                    {
                        // Database has an index, but we want no index at all
                        commands.Add((ApplyConstraintAction.DeleteIndex, indexInfo.Name));
                    }
                    break;
                case IndexType.Indexed:
                    if (uniqueConstraint is not null)
                    {
                        // Database has an unique index, but we want a normal index instead
                        commands.Add((ApplyConstraintAction.DeleteUniqueConstraint, uniqueConstraint.Name));
                        commands.Add((ApplyConstraintAction.CreateIndex, null));
                    }
                    else if (indexInfo is null)
                    {
                        // Database has no index, but we want a normal index instead
                        commands.Add((ApplyConstraintAction.CreateIndex, null));
                    }
                    break;
                case IndexType.Unique:
                    if (uniqueConstraint is null)
                    {
                        if (indexInfo is not null && indexInfo.OwningConstraint is null)
                        {
                            // Database has a normal index, but we want a unique index instead
                            commands.Add((ApplyConstraintAction.DeleteIndex, indexInfo.Name));
                            commands.Add((ApplyConstraintAction.CreateUniqueConstraint, null));
                        }
                        else
                        {
                            // Database has no index, but we want a unique index instead
                            commands.Add((ApplyConstraintAction.CreateUniqueConstraint, null));
                        }
                    }
                    break;
            }

            if (!isKey && keyConstraint is not null)
            {
                // Database has has a exists constraint, but we want a nullable field instead
                commands.Add((ApplyConstraintAction.DeleteKeyConstraint, keyConstraint.Name));
            }
            else if (isKey && keyConstraint is null)
            {
                // Database has has no exists constraint, but we want a non-nullable field instead
                commands.Add((ApplyConstraintAction.CreateKeyConstraint, null));
            }
            if (nullable && mandatoryConstraint is not null)
            {
                // Database has has a exists constraint, but we want a nullable field instead
                commands.Add((ApplyConstraintAction.DeleteExistsConstraint, mandatoryConstraint.Name));
            }
            else if (!nullable && mandatoryConstraint is null)
            {
                // Database has has no exists constraint, but we want a non-nullable field instead
                commands.Add((ApplyConstraintAction.CreateExistsConstraint, null));
            }

            return commands;
        }
    }
}