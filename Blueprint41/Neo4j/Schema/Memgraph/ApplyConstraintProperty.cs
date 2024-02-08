using System.Collections.Generic;
using Blueprint41.Neo4j.Persistence.Void;
using Blueprint41.Neo4j.Refactoring;
using Blueprint41.Neo4j.Schema.v4;

namespace Blueprint41.Neo4j.Schema.Memgraph
{
    public class ApplyConstraintProperty_Memgraph : ApplyConstraintProperty_v4
    {
        internal ApplyConstraintProperty_Memgraph(ApplyConstraintEntity parent, Property property, List<(ApplyConstraintAction actionEnum, string? constraintOrIndexName)> commands) : base(parent, property, commands) { }
        internal ApplyConstraintProperty_Memgraph(ApplyConstraintEntity parent, string property, List<(ApplyConstraintAction actionEnum, string? constraintOrIndexName)> commands) : base(parent, property, commands) { }

        internal override List<string> ToCypher()
        {
            Entity entity = (Entity)Parent.Entity;
            List<string> commands = new();
            bool isEnterpriseEdition = Parent.Entity.Parent.PersistenceProvider is Neo4jPersistenceProvider neo4j && neo4j.IsEnterpriseEdition;
            foreach ((ApplyConstraintAction actionEnum, string? constraintOrIndexName) in Commands)
            {
                switch (actionEnum)
                {
                    case ApplyConstraintAction.CreateIndex:
                        commands.Add($"CREATE INDEX {entity.Label.Name}_{Property}_RangeIndex FOR (node:{entity.Label.Name}) ON (node.{Property})");
                        break;
                    case ApplyConstraintAction.CreateUniqueConstraint:
                        commands.Add($"CREATE CONSTRAINT {entity.Label.Name}_{Property}_UniqueConstraint FOR (node:{entity.Label.Name}) REQUIRE node.{Property} IS UNIQUE");
                        break;
                    case ApplyConstraintAction.CreateExistsConstraint:
                        if(isEnterpriseEdition)
                            commands.Add($"CREATE CONSTRAINT {entity.Label.Name}_{Property}_ExistsConstraint FOR (node:{entity.Label.Name}) REQUIRE node.{Property} IS NOT NULL");
                        break;
                    case ApplyConstraintAction.CreateKeyConstraint:
                        if (isEnterpriseEdition)
                            commands.Add($"CREATE CONSTRAINT {entity.Label.Name}_{Property}_KeyConstraint FOR (node:{entity.Label.Name}) REQUIRE node.{Property} IS NODE KEY");
                        break;
                    case ApplyConstraintAction.DeleteIndex:
                        commands.Add($"DROP INDEX {constraintOrIndexName}");
                        break;
                    case ApplyConstraintAction.DeleteUniqueConstraint:
                    case ApplyConstraintAction.DeleteKeyConstraint:
                    case ApplyConstraintAction.DeleteExistsConstraint:
                        commands.Add($"DROP CONSTRAINT {constraintOrIndexName}");
                        break;
                    default:
                        break;
                }
            }

            foreach (var command in commands)
            {
                Parser.Log(command);
            }

            return commands;
        }
    }
}
