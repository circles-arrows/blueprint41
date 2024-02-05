﻿using System;
using System.Collections.Generic;
using Blueprint41.Neo4j.Persistence.Void;
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
            // TODO: Fix case where constraint is for a property on a relationship
            //       https://neo4j.com/docs/cypher-manual/current/constraints/
            IEntity entity = Parent.Entity;

            List<string> commands = new();
            foreach ((ApplyConstraintAction actionEnum, string? constraintOrIndexName) in Commands)
            {
                switch (actionEnum)
                {
                    case ApplyConstraintAction.CreateIndex:
                        if (Parent.Entity is Entity)
                        {
                            if (PersistenceProvider.NodePropertyFeatures.Index)
                                commands.Add($"CREATE INDEX {entity.Neo4jName}_{Property}_RangeIndex FOR (node:{entity.Neo4jName}) ON (node.{Property})");
                        }
                        else if (Parent.Entity is Relationship)
                        {
                            if (PersistenceProvider.RelationshipPropertyFeatures.Index)
                                commands.Add($"CREATE INDEX {entity.Neo4jName}_{Property}_RangeIndex FOR ()-[rel:{entity.Neo4jName}]-() ON (rel.{Property})");
                        }
                        break;
                    case ApplyConstraintAction.CreateUniqueConstraint:
                        if (Parent.Entity is Entity)
                        {
                            if (PersistenceProvider.NodePropertyFeatures.Unique)
                                commands.Add($"CREATE CONSTRAINT {entity.Neo4jName}_{Property}_UniqueConstraint FOR (node:{entity.Neo4jName}) REQUIRE node.{Property} IS UNIQUE");
                        }
                        else if (Parent.Entity is Relationship)
                        {
                            if (PersistenceProvider.RelationshipPropertyFeatures.Unique)
                                commands.Add($"CREATE CONSTRAINT {entity.Neo4jName}_{Property}_UniqueConstraint FOR ()-[rel:{entity.Neo4jName}]-() REQUIRE rel.{Property} IS UNIQUE");
                        }
                        break;
                    case ApplyConstraintAction.CreateExistsConstraint:
                        if (Parent.Entity is Entity)
                        {
                            if (PersistenceProvider.NodePropertyFeatures.Exists)
                                commands.Add($"CREATE CONSTRAINT {entity.Neo4jName}_{Property}_ExistsConstraint FOR (node:{entity.Neo4jName}) REQUIRE node.{Property} IS NOT NULL");
                        }
                        else if (Parent.Entity is Relationship)
                        {
                            if (PersistenceProvider.RelationshipPropertyFeatures.Exists)
                                commands.Add($"CREATE CONSTRAINT {entity.Neo4jName}_{Property}_ExistsConstraint FOR ()-[rel:{entity.Neo4jName}]-() REQUIRE rel.{Property} IS NOT NULL");
                        }
                        break;
                    case ApplyConstraintAction.CreateKeyConstraint:
                        if (Parent.Entity is Entity)
                        {
                            if (PersistenceProvider.NodePropertyFeatures.Key)
                                commands.Add($"CREATE CONSTRAINT {entity.Neo4jName}_{Property}_KeyConstraint FOR (node:{entity.Neo4jName}) REQUIRE node.{Property} IS NODE KEY");
                        }
                        else if (Parent.Entity is Relationship)
                        {
                            if (PersistenceProvider.RelationshipPropertyFeatures.Key)
                                commands.Add($"CREATE CONSTRAINT {entity.Neo4jName}_{Property}_KeyConstraint FOR ()-[rel:{entity.Neo4jName}]-() REQUIRE rel.{Property} IS RELATIONSHIP KEY");
                        }
                        break;
                    case ApplyConstraintAction.DeleteIndex:
                        if (Parent.Entity is Entity)
                        {
                            if (PersistenceProvider.NodePropertyFeatures.Index)
                                commands.Add($"DROP INDEX {constraintOrIndexName}");
                        }
                        else if (Parent.Entity is Relationship)
                        {
                            if (PersistenceProvider.RelationshipPropertyFeatures.Index)
                                commands.Add($"DROP INDEX {constraintOrIndexName}");
                        }
                        break;
                    case ApplyConstraintAction.DeleteUniqueConstraint:
                        if (Parent.Entity is Entity)
                        {
                            if (PersistenceProvider.NodePropertyFeatures.Unique)
                                commands.Add($"DROP CONSTRAINT {constraintOrIndexName}");
                        }
                        else if (Parent.Entity is Relationship)
                        {
                            if (PersistenceProvider.RelationshipPropertyFeatures.Unique)
                                commands.Add($"DROP CONSTRAINT {constraintOrIndexName}");
                        }
                        break;
                    case ApplyConstraintAction.DeleteKeyConstraint:
                        if (Parent.Entity is Entity)
                        {
                            if (PersistenceProvider.NodePropertyFeatures.Key)
                                commands.Add($"DROP CONSTRAINT {constraintOrIndexName}");
                        }
                        else if (Parent.Entity is Relationship)
                        {
                            if (PersistenceProvider.RelationshipPropertyFeatures.Key)
                                commands.Add($"DROP CONSTRAINT {constraintOrIndexName}");
                        }
                        break;
                    case ApplyConstraintAction.DeleteExistsConstraint:
                        if (Parent.Entity is Entity)
                        {
                            if (PersistenceProvider.NodePropertyFeatures.Exists)
                                commands.Add($"DROP CONSTRAINT {constraintOrIndexName}");
                        }
                        else if (Parent.Entity is Relationship)
                        {
                            if (PersistenceProvider.RelationshipPropertyFeatures.Exists)
                                commands.Add($"DROP CONSTRAINT {constraintOrIndexName}");
                        }
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
