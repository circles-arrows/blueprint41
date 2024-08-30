using System;
using System.Collections.Generic;
using System.Text;

namespace Blueprint41.Driver
{
    public class Node
    {
        internal Node(object instance)
        {
            _instance = instance;
        }
        internal object _instance { get; private set; }

        public string ElementId => Driver.I_NODE.ElementId(_instance);
        public IReadOnlyList<string> Labels => Driver.I_NODE.Labels(_instance);
        public object this[string key] => Driver.I_NODE.Item(_instance, key);
        public IReadOnlyDictionary<string, object?> Properties => Driver.I_NODE.Properties(_instance);
        public bool Equals(Node node) => Driver.I_NODE.EqualsINode(_instance, node._instance);

        internal static string ElementIdInternal(object neo4jINode) => Driver.I_NODE.ElementId(neo4jINode);
        internal static IReadOnlyList<string> LabelsInternal(object neo4jINode) => Driver.I_NODE.Labels(neo4jINode);
        internal static object ItemInternal(object neo4jINode, string key) => Driver.I_NODE.Item(neo4jINode, key);
        internal static IReadOnlyDictionary<string, object?> PropertiesInternal(object neo4jINode) => Driver.I_NODE.Properties(neo4jINode);
        internal static bool EqualsInternal(object neo4jINode, object value) => Driver.I_NODE.EqualsINode(neo4jINode, value);
        internal static bool IsINode(object? neo4jINode) => (neo4jINode?.GetType() == Driver.I_NODE.Type);
    }
}
