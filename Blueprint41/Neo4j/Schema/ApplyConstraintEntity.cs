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

        private List<ApplyConstraintProperty> Initialize()
        {
            List<ApplyConstraintProperty> actions = new List<ApplyConstraintProperty>();
            IEnumerable<ConstraintInfo> entityConstraints = Parent.Constraints.Where(item => item.Entity == Entity.Label.Name);
            IEnumerable<IndexInfo> entityIndexes = Parent.Indexes.Where(item => item.Entity == Entity.Label.Name);
            IReadOnlyList<Property> properties = Entity.GetPropertiesOfBaseTypesAndSelf();

            foreach (Property property in properties)
            {
                if (property.SystemReturnType is null)
                    continue;

                IEnumerable<ConstraintInfo> constraints = entityConstraints.Where(item => item.Field == property.Name);
                IEnumerable<IndexInfo> indexes = entityIndexes.Where(item => item.Field == property.Name);

                bool IsUnique = Entity.IsVirtual ? false : constraints.Any(item => item.IsUnique);
                bool IsIndexed = Entity.IsVirtual ? false : indexes.Any(item => item.IsIndexed);
                bool IsMandatory = Entity.IsVirtual ? false : constraints.Any(item => item.IsMandatory);

                List<ApplyConstraintAction> commands = GetCommands(property.IndexType, property.Nullable, IsUnique, IsIndexed, IsMandatory);

                if (commands.Count > 0)
                    actions.Add(Parent.NewApplyConstraintProperty(this, property, commands.ToArray()));
            }

            List<string> propertiesWithIndexOrConstraint = entityIndexes.Select(item => item.Field).Union(entityConstraints.Select(item => item.Field)).Distinct().ToList();
            List<string> propertiesThatAreAllowedToHaveIndexOrConstraint = properties.Where(property => property.SystemReturnType is not null).Select(item => item.Name).ToList();

            foreach (string property in propertiesWithIndexOrConstraint.Where(item => !propertiesThatAreAllowedToHaveIndexOrConstraint.Contains(item)))
            {
                IEnumerable<ConstraintInfo> constraints = entityConstraints.Where(item => item.Field == property);
                IEnumerable<IndexInfo> indexes = entityIndexes.Where(item => item.Field == property);

                bool IsUnique = Entity.IsVirtual ? false : constraints.Any(item => item.IsUnique);
                bool IsIndexed = Entity.IsVirtual ? false : indexes.Any(item => item.IsIndexed);
                bool IsMandatory = Entity.IsVirtual ? false : constraints.Any(item => item.IsMandatory);

                List<ApplyConstraintAction> commands = GetCommands(IndexType.None, true, IsUnique, IsIndexed, IsMandatory);

                if (commands.Count > 0)
                    actions.Add(Parent.NewApplyConstraintProperty(this, property, commands.ToArray()));
            }

            return actions;
        }

        private List<ApplyConstraintAction> GetCommands(IndexType indexType, bool nullable, bool IsUnique, bool IsIndexed, bool IsMandatory)
        {
            List<ApplyConstraintAction> commands = new List<ApplyConstraintAction>();

            if (Entity.IsAbstract && indexType == IndexType.Unique)
                indexType = IndexType.Indexed;

            switch (indexType)
            {
                case IndexType.None:
                    if (IsUnique)
                    {
                        // Database has an unique index, but we want no index at all
                        commands.Add(ApplyConstraintAction.DeleteUniqueConstraint);
                    }
                    else if (IsIndexed)
                    {
                        // Database has an index, but we want no index at all
                        commands.Add(ApplyConstraintAction.DeleteIndex);
                    }
                    break;
                case IndexType.Indexed:
                    if (IsUnique)
                    {
                        // Database has an unique index, but we want a normal index instead
                        commands.Add(ApplyConstraintAction.DeleteUniqueConstraint);
                        commands.Add(ApplyConstraintAction.CreateIndex);
                    }
                    else if (!IsIndexed)
                    {
                        // Database has no index, but we want a normal index instead
                        commands.Add(ApplyConstraintAction.CreateIndex);
                    }
                    break;
                case IndexType.Unique:
                    if (!IsUnique)
                    {
                        if (IsIndexed)
                        {
                            // Database has a normal index, but we want a unique index instead
                            commands.Add(ApplyConstraintAction.DeleteIndex);
                            commands.Add(ApplyConstraintAction.CreateUniqueConstraint);
                        }
                        else
                        {
                            // Database has no index, but we want a unique index instead
                            commands.Add(ApplyConstraintAction.CreateUniqueConstraint);
                        }
                    }
                    break;
            }

            if (IsMandatory && nullable)
            {
                // Database has has a exists constraint, but we want a nullable field instead
                commands.Add(ApplyConstraintAction.DeleteExistsConstraint);
            }
            else if (!IsMandatory && !nullable)
            {
                // Database has has no exists constraint, but we want a non-nullable field instead
                commands.Add(ApplyConstraintAction.CreateExistsConstraint);
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
