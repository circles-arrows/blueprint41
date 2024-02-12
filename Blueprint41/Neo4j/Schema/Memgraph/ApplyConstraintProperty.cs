using System.Collections.Generic;
using Blueprint41.Neo4j.Persistence.Void;
using Blueprint41.Neo4j.Refactoring;
using Blueprint41.Neo4j.Schema.v5;

namespace Blueprint41.Neo4j.Schema.Memgraph
{
    public class ApplyConstraintProperty_Memgraph : ApplyConstraintProperty_v5
    {
        internal ApplyConstraintProperty_Memgraph(ApplyConstraintEntity parent, Property property, List<(ApplyConstraintAction actionEnum, string? constraintOrIndexName)> commands) : base(parent, property, commands) { }
        internal ApplyConstraintProperty_Memgraph(ApplyConstraintEntity parent, string property, List<(ApplyConstraintAction actionEnum, string? constraintOrIndexName)> commands) : base(parent, property, commands) { }

        internal override List<string> ToCypher()
        {
            IEntity entity = Parent.Entity;
            List<string> commands = new();

            foreach ((ApplyConstraintAction action, string? constraintOrIndexName) in Commands)
            {
                if (ShouldAddConstraint(action, entity))
                {
                    string command = GenerateConstraintCommand(action, entity, constraintOrIndexName);
                    if (!string.IsNullOrEmpty(command))
                    {
                        commands.Add(command);
                        Parser.Log(command);
                    }
                }
            }

            return commands;
        }

        private bool ShouldAddConstraint(ApplyConstraintAction action, IEntity entity)
        {
            FeatureSupport features = (entity is Entity) ? PersistenceProvider.NodePropertyFeatures : PersistenceProvider.RelationshipPropertyFeatures;

            return action switch
            {
                ApplyConstraintAction.CreateIndex => features.Index,
                ApplyConstraintAction.CreateUniqueConstraint => features.Unique,
                ApplyConstraintAction.CreateExistsConstraint => features.Exists,
                ApplyConstraintAction.CreateKeyConstraint => features.Key,
                ApplyConstraintAction.DeleteIndex => features.Index,
                ApplyConstraintAction.DeleteUniqueConstraint => features.Unique,
                ApplyConstraintAction.DeleteKeyConstraint => features.Key,
                ApplyConstraintAction.DeleteExistsConstraint => features.Exists,
                _ => false,
            };
        }

        private string GenerateConstraintCommand(ApplyConstraintAction action, IEntity entity, string? constraintOrIndexName)
        {
            string propertyType = (entity is Entity) ? "node" : "rel";
            string entityType = (entity is Entity) ? entity.Neo4jName : $"()-[rel:{entity.Neo4jName}]-()";
            string forEntityType = (entity is Entity) ? $"(node:{entity.Neo4jName})" : $"()-[rel:{entity.Neo4jName}]-()";

            string constraintType = (entity is Entity ? "NODE" : "RELATIONSHIP");
            string propertyName = Property;
            string commandTemplate = string.Empty;

            switch (action)
            {
                case ApplyConstraintAction.CreateIndex:
                    commandTemplate = $"CREATE INDEX {entityType}_{propertyName}_RangeIndex FOR {forEntityType} ON ({propertyType}.{propertyName})";
                    break;
                case ApplyConstraintAction.CreateUniqueConstraint:
                    commandTemplate = $"CREATE CONSTRAINT {entityType}_{propertyName}_UniqueConstraint FOR {forEntityType} REQUIRE {propertyType}.{propertyName} IS UNIQUE";
                    break;
                case ApplyConstraintAction.CreateExistsConstraint:
                    commandTemplate = $"CREATE CONSTRAINT {entityType}_{propertyName}_ExistsConstraint FOR {forEntityType} REQUIRE {propertyType}.{propertyName} IS NOT NULL";
                    break;
                case ApplyConstraintAction.CreateKeyConstraint:
                    commandTemplate = $"CREATE CONSTRAINT {entityType}_{propertyName}_KeyConstraint FOR {forEntityType} REQUIRE {propertyType}.{propertyName} IS {constraintType} KEY";
                    break;
                case ApplyConstraintAction.DeleteIndex:
                case ApplyConstraintAction.DeleteUniqueConstraint:
                case ApplyConstraintAction.DeleteKeyConstraint:
                case ApplyConstraintAction.DeleteExistsConstraint:
                    commandTemplate = $"DROP CONSTRAINT {constraintOrIndexName}";
                    break;
                default:
                    break;
            }

            return commandTemplate;
        }

    }
}
