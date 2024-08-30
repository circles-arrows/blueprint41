using System;
using System.Collections.Generic;
using System.Text;

namespace Blueprint41.Driver
{
    public class Relationship
    {
        internal Relationship(object value)
        {
            Value = value;
        }
        internal object Value { get; private set; }

        public string ElementId => Driver.I_RELATIONSHIP.ElementId(Value);
        public string Type => Driver.I_RELATIONSHIP.RelationshipType(Value);
        public string StartNodeElementId => Driver.I_RELATIONSHIP.StartNodeElementId(Value);
        public string EndNodeElementId => Driver.I_RELATIONSHIP.EndNodeElementId(Value);
        public object this[string key] => Driver.I_RELATIONSHIP.Item(Value, key);
        public IReadOnlyDictionary<string, object?> Properties => Driver.I_RELATIONSHIP.Properties(Value);
        public bool Equals(Relationship relationship) => Driver.I_RELATIONSHIP.EqualsIRelationship(Value, relationship.Value);

        internal static string ElementIdInternal(object neo4jIRelationship) => Driver.I_RELATIONSHIP.ElementId(neo4jIRelationship);
        internal static string TypeInternal(object neo4jIRelationship) => Driver.I_RELATIONSHIP.RelationshipType(neo4jIRelationship);
        internal static string StartNodeElementIdInternal(object neo4jIRelationship) => Driver.I_RELATIONSHIP.StartNodeElementId(neo4jIRelationship);
        internal static string EndNodeElementIdInternal(object neo4jIRelationship) => Driver.I_RELATIONSHIP.EndNodeElementId(neo4jIRelationship);
        internal static object ItemInternal(object neo4jIRelationship,string key) => Driver.I_RELATIONSHIP.Item(neo4jIRelationship, key);
        internal static IReadOnlyDictionary<string, object?> PropertiesInternal(object neo4jIRelationship) => Driver.I_RELATIONSHIP.Properties(neo4jIRelationship);
        internal static bool EqualsInternal(object neo4jIRelationship, object value) => Driver.I_RELATIONSHIP.EqualsIRelationship(neo4jIRelationship, value);
        internal static bool IsIRelationship(object? neo4jIRelationship) => (neo4jIRelationship?.GetType() == Driver.I_RELATIONSHIP.Type);
    }
}
