using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using Blueprint41.Core;
using Blueprint41.Neo4j.Persistence.Void;
using Blueprint41.Neo4j.Refactoring;

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
            FunctionalIds = new List<FunctionalIdInfo>(0);
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

                    RawResult result = PersistenceProvider.Run(procedure);
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

        internal override void UpdateConstraints()
        {
            foreach (var diff in GetConstraintDifferences())
            {
                foreach (var action in diff.Actions)
                {
                    foreach (var cql in action.ToCypher())
                    {
                        Parser.Log(cql);
                        PersistenceProvider.Run(cql, !PersistenceProvider.IsMemgraph);
                    }
                }
            }
            //Transaction.Commit();
        }
        internal override List<(ApplyConstraintAction, string?)> ComputeCommands(
            IEntity entity,
            IndexType indexType,
            bool nullable,
            bool isKey,
            IEnumerable<ConstraintInfo> constraints,
            IEnumerable<IndexInfo> indexes)
        {
            var commands = new List<(ApplyConstraintAction, string?)>();
            var constraintsAndIndex = GetRelevantConstraintsAndIndex(entity, constraints, indexes);

            indexType = AdjustIndexTypeBasedOnContext(entity, indexType, isKey);
            ManageIndexConstraints(commands, indexType, constraintsAndIndex.Unique, constraintsAndIndex.Index);
            HandleKeyConstraints(commands, isKey, constraintsAndIndex.Key);
            HandleMandatoryConstraints(commands, nullable, constraintsAndIndex.Mandatory);

            return commands;
        }

        private IndexType AdjustIndexTypeBasedOnContext(IEntity entity, IndexType indexType, bool isKey)
        {
            if (entity.IsAbstract && indexType == IndexType.Unique)
                return IndexType.Indexed;

            return isKey ? IndexType.None : indexType;
        }

        private void ManageIndexConstraints(
            List<(ApplyConstraintAction, string?)> commands,
            IndexType indexType,
            ConstraintInfo? uniqueConstraint,
            IndexInfo? indexInfo)
        {
            switch (indexType)
            {
                case IndexType.None:
                    DeleteUniqueConstraintIfPresent(commands, uniqueConstraint);
                    DeleteIndexIfNoOwningConstraint(commands, indexInfo);
                    break;
                case IndexType.Indexed:
                    TransitionToIndexed(commands, uniqueConstraint, indexInfo);
                    break;
                case IndexType.Unique:
                    TransitionToUnique(commands, uniqueConstraint, indexInfo);
                    break;
            }
        }

        private void DeleteUniqueConstraintIfPresent(List<(ApplyConstraintAction, string?)> commands, ConstraintInfo? constraint)
        {
            if (constraint != null)
                commands.Add((ApplyConstraintAction.DeleteUniqueConstraint, constraint.Name));
        }

        private void DeleteIndexIfNoOwningConstraint(List<(ApplyConstraintAction, string?)> commands, IndexInfo? indexInfo)
        {
            if (indexInfo != null && indexInfo.OwningConstraint == null)
                commands.Add((ApplyConstraintAction.DeleteIndex, indexInfo.Name));
        }

        private void TransitionToIndexed(List<(ApplyConstraintAction, string?)> commands, ConstraintInfo? uniqueConstraint, IndexInfo? indexInfo)
        {
            DeleteUniqueConstraintIfPresent(commands, uniqueConstraint);
            if (uniqueConstraint != null || indexInfo == null)
                commands.Add((ApplyConstraintAction.CreateIndex, null));
        }

        private void TransitionToUnique(List<(ApplyConstraintAction, string?)> commands, ConstraintInfo? uniqueConstraint, IndexInfo? indexInfo)
        {
            if (uniqueConstraint == null)
            {
                DeleteIndexIfNoOwningConstraint(commands, indexInfo);
                commands.Add((ApplyConstraintAction.CreateUniqueConstraint, null));
            }
        }

        private void HandleKeyConstraints(List<(ApplyConstraintAction, string?)> commands, bool isKey, ConstraintInfo? keyConstraint)
        {
            if (!isKey && keyConstraint != null)
                commands.Add((ApplyConstraintAction.DeleteKeyConstraint, keyConstraint.Name));
            else if (isKey && keyConstraint == null)
                commands.Add((ApplyConstraintAction.CreateKeyConstraint, null));
        }

        private void HandleMandatoryConstraints(List<(ApplyConstraintAction, string?)> commands, bool nullable, ConstraintInfo? mandatoryConstraint)
        {
            if (nullable && mandatoryConstraint != null)
                commands.Add((ApplyConstraintAction.DeleteExistsConstraint, mandatoryConstraint.Name));
            else if (!nullable && mandatoryConstraint == null)
                commands.Add((ApplyConstraintAction.CreateExistsConstraint, null));
        }

        private (ConstraintInfo? Unique, ConstraintInfo? Mandatory, ConstraintInfo? Key, IndexInfo? Index) GetRelevantConstraintsAndIndex(
            IEntity entity,
            IEnumerable<ConstraintInfo> constraints,
            IEnumerable<IndexInfo> indexes)
        {
            if (entity.IsVirtual)
                return (null, null, null, null);

            return (
                constraints.FirstOrDefault(c => c.IsUnique),
                constraints.FirstOrDefault(c => c.IsMandatory),
                constraints.FirstOrDefault(c => c.IsKey),
                indexes.FirstOrDefault(i => i.IsIndexed)
            );
        }
    }
}