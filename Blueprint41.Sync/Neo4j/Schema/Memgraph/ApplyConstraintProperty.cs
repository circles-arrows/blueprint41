using System.Collections.Generic;
using Blueprint41.Sync.Neo4j.Persistence.Void;
using Blueprint41.Sync.Neo4j.Refactoring;
using Blueprint41.Sync.Neo4j.Schema.v5;

namespace Blueprint41.Sync.Neo4j.Schema.Memgraph
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
            var features = entity is Entity ? PersistenceProvider.NodePropertyFeatures : PersistenceProvider.RelationshipPropertyFeatures;

            return action switch
            {
                ApplyConstraintAction.CreateIndex or ApplyConstraintAction.DeleteIndex => features.Index,
                ApplyConstraintAction.CreateUniqueConstraint or ApplyConstraintAction.DeleteUniqueConstraint => features.Unique,
                ApplyConstraintAction.CreateExistsConstraint or ApplyConstraintAction.DeleteExistsConstraint => features.Exists,
                ApplyConstraintAction.CreateKeyConstraint or ApplyConstraintAction.DeleteKeyConstraint => features.Key,
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
                ApplyConstraintAction.CreateIndex => CreateIndexCommand(entity.Neo4jName, propertyName),
                ApplyConstraintAction.CreateKeyConstraint => CreateKeyConstraintCommand(targetEntityType, alias, propertyName),
                ApplyConstraintAction.CreateUniqueConstraint => CreateUniqueConstraintCommand(targetEntityType, alias, propertyName),
                ApplyConstraintAction.CreateExistsConstraint => CreateExistsConstraintCommand(targetEntityType, alias, propertyName),
                ApplyConstraintAction.DeleteUniqueConstraint => DropUniqueConstraintCommand(targetEntityType, alias, propertyName),
                ApplyConstraintAction.DeleteExistsConstraint => DropExistsConstraintCommand(targetEntityType, alias, propertyName),
                ApplyConstraintAction.DeleteIndex => DropIndexCommand(entity.Neo4jName, propertyName),
                ApplyConstraintAction.DeleteKeyConstraint => DropKeyConstraintCommand(targetEntityType, alias, propertyName),
                _ => string.Empty,
            };
        }


        private string CreateIndexCommand(string neo4jName, string propertyName)
        {
            return $"CREATE INDEX ON :{neo4jName}({propertyName})";
        }
        private string CreateKeyConstraintCommand(string targetEntityType, string alias, string propertyName)
        {
            return  $"{CreateExistsConstraintCommand(targetEntityType, alias, propertyName)}; " +
                    $"{CreateUniqueConstraintCommand(targetEntityType, alias, propertyName)}";
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
        private string DropIndexCommand(string neo4jName, string propertyName)
        {
            return $"DROP INDEX ON :{neo4jName}({propertyName})";
        }
        private string DropKeyConstraintCommand(string targetEntityType, string alias, string propertyName)
        {
            return $"{DropExistsConstraintCommand(targetEntityType, alias, propertyName)}; " +
                    $"{DropUniqueConstraintCommand(targetEntityType, alias, propertyName)}";
        }
    }
}
