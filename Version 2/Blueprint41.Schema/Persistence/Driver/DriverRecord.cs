using System;
using System.Collections.Generic;
using System.Text;

namespace Blueprint41.Persistence
{
    public class DriverRecord
    {
        public static object Item(object neo4jRecord, int index) => Driver.I_RECORD.Item(neo4jRecord, index);
        public static object Item(object neo4jRecord, string key) => Driver.I_RECORD.Item(neo4jRecord, key);
        public static IReadOnlyDictionary<string, object?> Values(object neo4jRecord) => Driver.I_RECORD.Values(neo4jRecord);
        public static IReadOnlyList<string> Keys(object neo4jRecord) => Driver.I_RECORD.Keys(neo4jRecord);
    }
}
