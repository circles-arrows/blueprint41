using System;
using System.Collections.Generic;
using System.Linq;
using Blueprint41.Core;
using Blueprint41.Persistence.Provider;

namespace Blueprint41.Refactoring.Schema
{
    public class SchemaInfo_MemgraphV1 : SchemaInfo_Neo4jV5
    {
        internal SchemaInfo_MemgraphV1(DatastoreModel model) : base(model) { }

        protected override void Initialize()
        {
            using (DatastoreModel.PersistenceProvider.NewSession(ReadWriteMode.ReadWrite))
            {
                Indexes = LoadData("SHOW INDEX INFO", record => NewIndexInfo(record, DatastoreModel.PersistenceProvider)).Where(item => item.Entity is not null && item.Field is not null).ToArray();
                Constraints = LoadData("SHOW CONSTRAINT INFO", record => NewConstraintInfo(record, DatastoreModel.PersistenceProvider)).Where(item => item.Entity is not null && item.Field is not null).ToArray();
                Labels = LoadData("SHOW NODE_LABELS INFO", record => record["node labels"].As<string>());
                RelationshipTypes = LoadData("SHOW EDGE_TYPES INFO", record => record["edge types"].As<string>());
            }
            FunctionalIds = new List<FunctionalIdInfo>(0);
        }
        protected override long FindMaxId(FunctionalId functionalId) => throw new NotSupportedException("FunctionalIds are not supported on Memgraph.");

        protected override ConstraintInfo NewConstraintInfo(IDictionary<string, object> rawRecord, PersistenceProvider neo4JPersistenceProvider) => new ConstraintInfo_MemgraphV1(rawRecord, neo4JPersistenceProvider);
        protected override IndexInfo NewIndexInfo(IDictionary<string, object> rawRecord, PersistenceProvider persistenceProvider) => new IndexInfo_MemgraphV1(rawRecord, persistenceProvider);
        internal override ApplyConstraintProperty NewApplyConstraintProperty(ApplyConstraintEntity parent, Property property, List<(ApplyConstraintAction, string?)> commands) => new ApplyConstraintProperty_MemgraphV1(parent, property, commands);
        internal override ApplyConstraintProperty NewApplyConstraintProperty(ApplyConstraintEntity parent, string property, List<(ApplyConstraintAction, string?)> commands) => new ApplyConstraintProperty_MemgraphV1(parent, property, commands);

        internal override IReadOnlyList<ApplyFunctionalId> GetFunctionalIdDifferences()
        {
            return new List<Schema.ApplyFunctionalId>();
        }
        internal override void UpdateFunctionalIds()
        {
            using (DatastoreModel.PersistenceProvider.NewSession(ReadWriteMode.ReadWrite))
            {
                foreach (var diff in GetFunctionalIdDifferences())
                {
                    Parser.Log(diff.ToString());
                    foreach (var query in diff.ToCypher())
                    {
                        Session.RunningSession.Run(query);
                    }
                }
            }
        }
        internal override void UpdateConstraints()
        {
            using (DatastoreModel.PersistenceProvider.NewSession(ReadWriteMode.ReadWrite))
            {
                foreach (var diff in GetConstraintDifferences())
                {
                    foreach (var action in diff.Actions)
                    {
                        foreach (var cql in action.ToCypher())
                        {
                            Parser.Log(cql);
                            Session.RunningSession.Run(cql);
                        }
                    }
                }
            }
        }

        //internal override void RemoveIndexesAndContraints(Property property)
        //{
        //    if (!property.Nullable || property.IndexType != IndexType.None)
        //    {
        //        property.Nullable = true;
        //        property.IndexType = IndexType.None;

        //        foreach (var entity in property.Parent.GetSubclassesOrSelf())
        //        {
        //            ApplyConstraintEntity applyConstraint = NewApplyConstraintEntity(entity);
        //            foreach (var action in applyConstraint.Actions.Where(c => c.Property == property.Name))
        //            {
        //                foreach (string query in action.ToCypher())
        //                {
        //                    Parser.Execute(query, null, !PersistenceProvider.IsMemgraph);
        //                }
        //            }
        //        }
        //    }
        //}
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
            HandleMandatoryConstraints(commands, (!isKey && nullable), constraintsAndIndex.Mandatory);

            return commands;
        }

        private IndexType AdjustIndexTypeBasedOnContext(IEntity entity, IndexType indexType, bool isKey)
        {
            if (entity.IsAbstract && indexType == IndexType.Unique)
                return IndexType.Indexed;

            return isKey ? IndexType.Unique : indexType;
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
                commands.Add((ApplyConstraintAction.CreateIndex, null));
            }
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