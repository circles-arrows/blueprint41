using System;
using System.Collections.Generic;
using System.Text;

using Blueprint41.Core;

namespace Blueprint41.Neo4j.Persistence.Void
{
    internal class Neo4jRawResult : RawResult
    {
        internal Neo4jRawResult()
        {
        }

        public override IReadOnlyList<string> Keys => new List<string>(0);
        public override RawRecord? Peek() => null;
        public override void Consume() { }
        public override IEnumerator<RawRecord> GetEnumerator() => new List<RawRecord>(0).GetEnumerator();
        public override RawResultStatistics Statistics() => new Neo4jRawResultStatistics();
        public override List<RawResultNotification> Notifications() => new List<RawResultNotification>(0);
    }
}
