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

        public object this[int index] => WrapIEntity(Driver.I_RECORD.Item(_instance, index));
        public object this[string key] => WrapIEntity(Driver.I_RECORD.Item(_instance, key));

        [Obsolete("Using record[name] or record[index] performs better than using record.Values.", false)]
        public IReadOnlyDictionary<string, object> Values => WrapIEntity(Driver.I_RECORD.Values(_instance));
        public IReadOnlyList<string> Keys => Driver.I_RECORD.Keys(_instance);

        internal static object ItemInternal(object neo4jRecord, int index) => Driver.I_RECORD.Item(neo4jRecord, index);
        internal static object ItemInternal(object neo4jRecord, string key) => Driver.I_RECORD.Item(neo4jRecord, key);
        internal static IReadOnlyDictionary<string, object> ValuesInternal(object neo4jRecord) => Driver.I_RECORD.Values(neo4jRecord);
        internal static IReadOnlyList<string> KeysInternal(object neo4jRecord) => Driver.I_RECORD.Keys(neo4jRecord);


        private IReadOnlyDictionary<string, object> WrapIEntity(IReadOnlyDictionary<string, object> values)
        {
            if (values.Values.Any(IsWrapped))
                return values.ToDictionary(item => item.Key, item => WrapIEntity(item.Value));

            return values;

            // or would it be quicker to just always return: values.ToDictionary(item => item.Key, item => WrapIEntity(item.Value)); ???
        }
        private object WrapIEntity(object? value)
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
        private bool IsWrapped(object value) => NodeResult.IsINode(value) || RelationshipResult.IsIRelationship(value);
    }
}
