using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Diagnostics;
using Blueprint41.Neo4j.Refactoring;
using Blueprint41.Neo4j.Persistence.Void;

namespace Blueprint41.Neo4j.Schema.v4
{
    public class ApplyConstraintProperty_v4 : ApplyConstraintProperty
    {
        internal ApplyConstraintProperty_v4(ApplyConstraintEntity parent, Property property, List<(ApplyConstraintAction actionEnum, string? constraintOrIndexName)> commands) : base(parent, property, commands) { }
        internal ApplyConstraintProperty_v4(ApplyConstraintEntity parent, string property, List<(ApplyConstraintAction actionEnum, string? constraintOrIndexName)> commands) : base(parent, property, commands) { }
    
        internal override List<string> ToCypher()
        {
            // TODO: What about if the constraint is for a property on a relationship
            Entity entity = (Entity)Parent.Entity;

            List<string> commands = new List<string>();
            foreach (var (actionEnum, constraintOrIndexName) in Commands)
            {
                switch (actionEnum)
                {
                    case ApplyConstraintAction.CreateIndex:
                        if (PersistenceProvider.NodePropertyFeatures.Index)
                            commands.Add($"CREATE INDEX ON :{entity.Label.Name}({Property})");
                        break;
                    case ApplyConstraintAction.CreateUniqueConstraint:
                        if (PersistenceProvider.NodePropertyFeatures.Unique)
                            commands.Add($"CREATE CONSTRAINT ON (node:{entity.Label.Name}) ASSERT node.{Property} IS UNIQUE");
                        break;
                    case ApplyConstraintAction.CreateExistsConstraint:
                        if (PersistenceProvider.NodePropertyFeatures.Exists)
                            commands.Add($"CREATE CONSTRAINT ON (node:{entity.Label.Name}) ASSERT exists(node.{Property})");
                        break;
                    case ApplyConstraintAction.DeleteIndex:
                        if (PersistenceProvider.NodePropertyFeatures.Index)
                            commands.Add($"DROP INDEX ON :{entity.Label.Name}({Property})");
                        break;
                    case ApplyConstraintAction.DeleteUniqueConstraint:
                        if (PersistenceProvider.NodePropertyFeatures.Unique)
                            commands.Add($"DROP CONSTRAINT ON (node:{entity.Label.Name}) ASSERT node.{Property} IS UNIQUE");
                        break;
                    case ApplyConstraintAction.DeleteExistsConstraint:
                        if (PersistenceProvider.NodePropertyFeatures.Exists)
                            commands.Add($"DROP CONSTRAINT ON (node:{entity.Label.Name}) ASSERT exists(node.{Property})");
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
