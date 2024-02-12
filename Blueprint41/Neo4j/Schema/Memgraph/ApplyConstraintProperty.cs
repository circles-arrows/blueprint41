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
            foreach ((ApplyConstraintAction action, _) in Commands)
            {
                if (ShouldAddConstraint(action, entity))
                {
                    string command = GenerateConstraintCommand(action, entity);
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

        private string GenerateConstraintCommand(ApplyConstraintAction action, IEntity entity)
        {
            string targetEntityType = (entity is Entity) ? $"(node:{entity.Neo4jName})" : $"()-[rel:{entity.Neo4jName}]-()";
            string alias = (entity is Entity) ? "node" : "rel";
            string propertyName = Property;

            return action switch
            {
                ApplyConstraintAction.CreateUniqueConstraint => CreateUniqueConstraintCommand(targetEntityType, alias, propertyName),
                ApplyConstraintAction.CreateExistsConstraint => CreateExistsConstraintCommand(targetEntityType, alias, propertyName),
                ApplyConstraintAction.DeleteUniqueConstraint => DropUniqueConstraintCommand(targetEntityType, alias, propertyName),
                ApplyConstraintAction.DeleteExistsConstraint => DropExistsConstraintCommand(targetEntityType, alias, propertyName),
                _ => string.Empty,
            };
        }


        private string CreateUniqueConstraintCommand(string targetEntityType, string alias, string propertyName)
        {
            return $"CREATE CONSTRAINT ON {targetEntityType} ASSERT {alias}.{propertyName} IS UNIQUE";
        }

        private string CreateExistsConstraintCommand(string targetEntityType, string alias, string propertyName)
        {
            return $"CREATE CONSTRAINT ON {targetEntityType} ASSERT EXISTS ({alias}.{propertyName})";
        }
        private string DropUniqueConstraintCommand(string targetEntityType, string alias, string propertyName)
        {
            return $"DROP CONSTRAINT ON {targetEntityType} ASSERT {alias}.{propertyName} IS UNIQUE";
        }
        private string DropExistsConstraintCommand(string targetEntityType, string alias, string propertyName)
        {
            return $"DROP CONSTRAINT ON {targetEntityType} ASSERT EXISTS ({alias}.{propertyName})";
        }

    }
}
