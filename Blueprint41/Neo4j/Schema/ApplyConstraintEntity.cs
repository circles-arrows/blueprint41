using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Blueprint41.Neo4j.Schema
{
    public class ApplyConstraintEntity
    {
        internal ApplyConstraintEntity(SchemaInfo parent, Entity entity)
        {
            Parent = parent;
            Entity = entity;

            Actions = Initialize();
        }

        internal virtual List<ApplyConstraintProperty> Initialize()
        {
            // TODO: Only applies indexes and constraints on a single field, composite indexes and constraints are ignored.

            List<ApplyConstraintProperty> actions = new List<ApplyConstraintProperty>();
            IEnumerable<ConstraintInfo> entityConstraints = Parent.Constraints.Where(item => item.Entity.Count == 1 && item.Entity[0] == Entity.Label.Name);
            IEnumerable<IndexInfo> entityIndexes = Parent.Indexes.Where(item => item.Entity.Count == 1 && item.Entity[0] == Entity.Label.Name);
            IReadOnlyList<Property> properties = Entity.GetPropertiesOfBaseTypesAndSelf();

            foreach (Property property in properties)
            {
                if (property.SystemReturnType is null)
                    continue;

                IEnumerable<ConstraintInfo> constraints = entityConstraints.Where(item => item.Field.Count == 1 && item.Field[0] == property.Name);
                IEnumerable<IndexInfo> indexes = entityIndexes.Where(item => item.Field.Count == 1 && item.Field[0] == property.Name);

                List<(ApplyConstraintAction, string?)> commands = GetCommands(property.IndexType, property.Nullable, constraints, indexes);

                if (commands.Count > 0)
                    actions.Add(Parent.NewApplyConstraintProperty(this, property, commands));
            }

            List<string> propertiesWithIndexOrConstraint = entityIndexes.SelectMany(item => item.Field).Union(entityConstraints.SelectMany(item => item.Field)).Distinct().ToList();
            List<string> propertiesThatAreAllowedToHaveIndexOrConstraint = properties.Where(property => property.SystemReturnType is not null).Select(item => item.Name).ToList();

            foreach (string property in propertiesWithIndexOrConstraint.Where(item => !propertiesThatAreAllowedToHaveIndexOrConstraint.Contains(item)))
            {
                IEnumerable<ConstraintInfo> constraints = entityConstraints.Where(item => item.Field.Count == 1 && item.Field[0] == property);
                IEnumerable<IndexInfo> indexes = entityIndexes.Where(item => item.Field.Count == 1 && item.Field[0] == property);

                List<(ApplyConstraintAction, string?)> commands = GetCommands(IndexType.None, true, constraints, indexes);

                if (commands.Count > 0)
                    actions.Add(Parent.NewApplyConstraintProperty(this, property, commands));
            }

            return actions;
        }
        internal List<(ApplyConstraintAction, string?)> GetCommands(IndexType indexType, bool nullable, IEnumerable<ConstraintInfo> constraints, IEnumerable<IndexInfo> indexes)
        {
            bool IsUnique = Entity.IsVirtual ? false : constraints.Any(item => item.IsUnique);
            bool IsIndexed = Entity.IsVirtual ? false : indexes.Any(item => item.IsIndexed);
            bool IsMandatory = Entity.IsVirtual ? false : constraints.Any(item => item.IsMandatory);

            string? uniqueConstraintName = constraints.FirstOrDefault(item => item.IsUnique)?.Name;
            string? existsConstraintName = constraints.FirstOrDefault(item => item.IsMandatory)?.Name;
            string? indexName = indexes.FirstOrDefault(item => item.IsIndexed)?.Name;

            List<(ApplyConstraintAction, string?)> commands = new List<(ApplyConstraintAction, string?)>();

            if (Entity.IsAbstract && indexType == IndexType.Unique)
                indexType = IndexType.Indexed;

            switch (indexType)
            {
                case IndexType.None:
                    if (IsUnique)
                    {
                        // Database has an unique index, but we want no index at all
                        commands.Add((ApplyConstraintAction.DeleteUniqueConstraint, uniqueConstraintName));
                    }
                    else if (IsIndexed)
                    {
                        // Database has an index, but we want no index at all
                        commands.Add((ApplyConstraintAction.DeleteIndex, indexName));
                    }
                    break;
                case IndexType.Indexed:
                    if (IsUnique)
                    {
                        // Database has an unique index, but we want a normal index instead
                        commands.Add((ApplyConstraintAction.DeleteUniqueConstraint, uniqueConstraintName));
                        commands.Add((ApplyConstraintAction.CreateIndex, null));
                    }
                    else if (!IsIndexed)
                    {
                        // Database has no index, but we want a normal index instead
                        commands.Add((ApplyConstraintAction.CreateIndex, null));
                    }
                    break;
                case IndexType.Unique:
                    if (!IsUnique)
                    {
                        if (IsIndexed)
                        {
                            // Database has a normal index, but we want a unique index instead
                            commands.Add((ApplyConstraintAction.DeleteIndex, indexName));
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

            if (IsMandatory && nullable)
            {
                // Database has has a exists constraint, but we want a nullable field instead
                commands.Add((ApplyConstraintAction.DeleteExistsConstraint, existsConstraintName));
            }
            else if (!IsMandatory && !nullable)
            {
                // Database has has no exists constraint, but we want a non-nullable field instead
                commands.Add((ApplyConstraintAction.CreateExistsConstraint, null));
            }

            return commands;
        }

        protected SchemaInfo Parent { get; set; }
        public Entity Entity { get; protected set; }
        public IReadOnlyList<ApplyConstraintProperty> Actions { get; protected set; }

        public override string ToString()
        {
            return $"Differences for {Entity.Name}";
        }
    }
}
