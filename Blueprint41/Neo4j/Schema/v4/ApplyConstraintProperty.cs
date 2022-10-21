using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Diagnostics;
using Blueprint41.Neo4j.Refactoring;

namespace Blueprint41.Neo4j.Schema.v4
{
    public class ApplyConstraintProperty_v4 : ApplyConstraintProperty
    {
        internal ApplyConstraintProperty_v4(ApplyConstraintEntity parent, Property property, params ApplyConstraintAction[] commands) : base(parent, property, commands) { }
        internal ApplyConstraintProperty_v4(ApplyConstraintEntity parent, string property, params ApplyConstraintAction[] commands) : base(parent, property, commands) { }
    
        internal override List<string> ToCypher()
        {
            List<string> commands = new List<string>();
            foreach (var command in Commands)
            {
                switch (command)
                {
                    case ApplyConstraintAction.CreateIndex:
                        commands.Add($"CREATE INDEX ON :{Parent.Entity.Label.Name}({Property})");
                        break;
                    case ApplyConstraintAction.CreateUniqueConstraint:
                        commands.Add($"CREATE CONSTRAINT ON (node:{Parent.Entity.Label.Name}) ASSERT node.{Property} IS UNIQUE");
                        break;
                    case ApplyConstraintAction.CreateExistsConstraint:
                        commands.Add($"CREATE CONSTRAINT ON (node:{Parent.Entity.Label.Name}) ASSERT exists(node.{Property})");
                        break;
                    case ApplyConstraintAction.DeleteIndex:
                        commands.Add($"DROP INDEX ON :{Parent.Entity.Label.Name}({Property})");
                        break;
                    case ApplyConstraintAction.DeleteUniqueConstraint:
                        commands.Add($"DROP CONSTRAINT ON (node:{Parent.Entity.Label.Name}) ASSERT node.{Property} IS UNIQUE");
                        break;
                    case ApplyConstraintAction.DeleteExistsConstraint:
                        commands.Add($"DROP CONSTRAINT ON (node:{Parent.Entity.Label.Name}) ASSERT exists(node.{Property})");
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
