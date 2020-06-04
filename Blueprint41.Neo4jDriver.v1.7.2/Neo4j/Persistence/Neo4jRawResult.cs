using System;
using System.Collections.Generic;
using System.Text;

using Neo4j.Driver.V1;
using Blueprint41.Core;
using Blueprint41.Neo4j.Persistence;
using System.Linq;

namespace Blueprint41.Neo4j.Persistence.Driver.v3
{
    internal class Neo4jRawResult : RawResult
    {
        internal Neo4jRawResult(IStatementResult result)
        {
            Result = result;
        }
        private IStatementResult Result;

        public override IReadOnlyList<string> Keys                  => Result.Keys;
        public override RawRecord Peek()                            => new Neo4jRawRecord(Result.Peek());
        public override void Consume()                              => Result.Consume();
        public override IEnumerator<RawRecord> GetEnumerator()      => new RawRecordEnumerator<IRecord>(Result.GetEnumerator(), item => new Neo4jRawRecord(item));
        public override RawResultStatistics Statistics()            => new Neo4jRawResultStatistics(Result.Consume().Counters);
        public override List<RawResultNotification> Notifications() => Result.Consume().Notifications.Select(item => (RawResultNotification)new Neo4jRawResultNotifications(item)).ToList();
    }
}
