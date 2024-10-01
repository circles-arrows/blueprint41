using System;
using System.Collections.Generic;
using System.Linq;

namespace Blueprint41.Persistence
{
    public class Record
    {
        internal Record(object instance)
        {
            _instance = instance;
        }
        internal object _instance { get; private set; }

        public IReadOnlyList<string> Keys => Driver.I_RECORD.Keys(_instance);

        public object this[int index] => WrapIEntity(Driver.I_RECORD.Item(_instance, index));
        public object this[string key] => WrapIEntity(Driver.I_RECORD.Item(_instance, key));

        public bool TryGetValue(string key, out object? value)
        {
            if (RawValues.TryGetValue(key, out object? output))
            {
                value = WrapIEntity(output);
                return true;
            }

            value = null;
            return false;
        }
        internal IReadOnlyDictionary<string, object> RawValues => Driver.I_RECORD.Values(_instance);


        internal static object ItemInternal(object neo4jRecord, int index) => Driver.I_RECORD.Item(neo4jRecord, index);
        internal static object ItemInternal(object neo4jRecord, string key) => Driver.I_RECORD.Item(neo4jRecord, key);
        internal static IReadOnlyDictionary<string, object> ValuesInternal(object neo4jRecord) => Driver.I_RECORD.Values(neo4jRecord);
        internal static IReadOnlyList<string> KeysInternal(object neo4jRecord) => Driver.I_RECORD.Keys(neo4jRecord);

        private static object WrapIEntity(object? value)
        {
            if (value is null)
                return null!;
            else if (NodeResult.IsINode(value))
                return new NodeResult(value);
            else if (RelationshipResult.IsIRelationship(value))
                return new RelationshipResult(value);
            else
                return value;
        }
        private static bool IsWrapped(object value) => NodeResult.IsINode(value) || RelationshipResult.IsIRelationship(value);
    }
}
