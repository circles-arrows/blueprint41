using System;
using System.Collections.Generic;
using System.Text;

namespace Blueprint41.Persistence
{
    public class RelationshipResult
    {
        internal RelationshipResult(object instance)
        {
            _instance = instance;
        }
        internal object _instance { get; private set; }

        public string ElementId => Driver.I_RELATIONSHIP.ElementId(_instance);
        public string Type => Driver.I_RELATIONSHIP.RelationshipType(_instance);
        public string StartNodeElementId => Driver.I_RELATIONSHIP.StartNodeElementId(_instance);
        public string EndNodeElementId => Driver.I_RELATIONSHIP.EndNodeElementId(_instance);
        public object this[string key] => Driver.I_RELATIONSHIP.Item(_instance, key);
        public IReadOnlyDictionary<string, object?> Properties => Driver.I_RELATIONSHIP.Properties(_instance);
        public bool Equals(RelationshipResult relationship) => Driver.I_RELATIONSHIP.EqualsIRelationship(_instance, relationship._instance);
        public override bool Equals(object? obj) => Driver.I_RELATIONSHIP.Equals(_instance, obj);
        public override int GetHashCode() => Driver.I_RELATIONSHIP.GetHashCode(_instance);

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
