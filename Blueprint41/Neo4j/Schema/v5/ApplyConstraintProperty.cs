using System.Collections.Generic;

using Blueprint41.Neo4j.Refactoring;
using Blueprint41.Neo4j.Schema.v4;

namespace Blueprint41.Neo4j.Schema.v5
{
    public class ApplyConstraintProperty_v5 : ApplyConstraintProperty_v4
    {
        internal ApplyConstraintProperty_v5(ApplyConstraintEntity parent, Property property, List<(ApplyConstraintAction actionEnum, string? constraintOrIndexName)> commands) : base(parent, property, commands) { }
        internal ApplyConstraintProperty_v5(ApplyConstraintEntity parent, string property, List<(ApplyConstraintAction actionEnum, string? constraintOrIndexName)> commands) : base(parent, property, commands) { }

        internal override List<string> ToCypher()
        {
            List<string> commands = new();
            foreach ((ApplyConstraintAction actionEnum, string? constraintOrIndexName) in Commands)
            {
                switch (actionEnum)
                {
                    case ApplyConstraintAction.CreateIndex:
                        commands.Add($"CREATE INDEX {Parent.Entity.Label.Name}_{Property}_RangeIndex FOR (node:{Parent.Entity.Label.Name}) ON (node.{Property})");
                        break;
                    case ApplyConstraintAction.CreateUniqueConstraint:
                        commands.Add($"CREATE CONSTRAINT {Parent.Entity.Label.Name}_{Property}_UniqueConstraint FOR (node:{Parent.Entity.Label.Name}) REQUIRE node.{Property} IS UNIQUE");
                        break;
                    case ApplyConstraintAction.CreateExistsConstraint:
                        commands.Add($"CREATE CONSTRAINT {Parent.Entity.Label.Name}_{Property}_ExistsConstraint FOR (node:{Parent.Entity.Label.Name}) REQUIRE node.{Property} IS NOT NULL");
                        break;
                    case ApplyConstraintAction.DeleteIndex:
                        commands.Add($"DROP INDEX {constraintOrIndexName}");
                        break;
                    case ApplyConstraintAction.DeleteUniqueConstraint:
                        commands.Add($"DROP CONSTRAINT {constraintOrIndexName}");
                        break;
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
