using System;
using System.Collections.Generic;
using System.Text;

namespace Blueprint41.Driver
{
    public class Record
    {
        internal Record(object value)
        {
            Value = value;
        }
        internal object Value { get; private set; }

        public object this[int index] => Driver.I_RECORD.Item(Value, index);
        public object this[string key] => Driver.I_RECORD.Item(Value, key);
        public IReadOnlyDictionary<string, object?> Values => Driver.I_RECORD.Values(Value);
        public IReadOnlyList<string> Keys => Driver.I_RECORD.Keys(Value);

        internal static object ItemInternal(object neo4jRecord, int index) => Driver.I_RECORD.Item(neo4jRecord, index);
        internal static object ItemInternal(object neo4jRecord, string key) => Driver.I_RECORD.Item(neo4jRecord, key);
        internal static IReadOnlyDictionary<string, object?> ValuesInternal(object neo4jRecord) => Driver.I_RECORD.Values(neo4jRecord);
        internal static IReadOnlyList<string> KeysInternal(object neo4jRecord) => Driver.I_RECORD.Keys(neo4jRecord);
    }
}
