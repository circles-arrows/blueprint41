using System;
using System.Collections.Generic;
using System.Text;

using Neo4j.Driver;
using Blueprint41.Core;
using Blueprint41.Neo4j.Persistence;
using System.Linq;

namespace Blueprint41.Neo4j.Persistence.Driver.Memgraph
{
    internal class Neo4jRawRecord : RawRecord
    {
        internal Neo4jRawRecord(IRecord record)
        {
            Record = record;
        }
        private readonly IRecord Record;

        public override IReadOnlyDictionary<string, object> Values => Record.Values;
        public override IReadOnlyList<string> Keys => Record.Keys;
        public override object this[string key] => Record[key];
        public override object this[int index] => Record[index];
    }
}
