using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Blueprint41.Neo4j.Schema
{
    public class ApplyConstraintProperty
    {
        internal ApplyConstraintProperty(ApplyConstraintEntity parent, Property property, params ApplyConstraintAction[] commands)
        {
            Parent = parent;
            Property = property.Name;
            Commands = commands;
        }

        internal ApplyConstraintProperty(ApplyConstraintEntity parent, string property, params ApplyConstraintAction[] commands)
        {
            Parent = parent;
            Property = property;
            Commands = commands;
        }

        protected ApplyConstraintEntity Parent { get; set; }

        public string Property { get; protected set; }
        public IReadOnlyList<ApplyConstraintAction> Commands { get; protected set; }

        /// <summary>
        /// Turns actions into cypher queries.
        /// </summary>
        /// <returns></returns>
        internal virtual List<string> ToCypher()
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
                Debug.WriteLine(command);
            }

            return commands;
        }

        public override string ToString()
        {
            string diff = string.Join(", ", Commands.Select(item => item.ToString()).ToArray());
            return $"Differences for {Parent.Entity.Name}.{Property} -> {diff}";
        }
    }
}
