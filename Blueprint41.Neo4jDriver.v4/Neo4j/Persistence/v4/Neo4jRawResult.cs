using System;
using System.Collections.Generic;
using System.Text;

using Neo4j.Driver;
using Blueprint41.Core;
using Blueprint41.Neo4j.Persistence;
using System.Linq;

namespace Blueprint41.Neo4j.Persistence.Driver.v4
{
    internal class Neo4jRawResult : RawResult
    {
        internal Neo4jRawResult(IResult result)
        {
            Result = result;
            Records = result.ToList();
            Enumerator = new RawRecordEnumerator<IRecord>(Records.GetEnumerator(), item => new Neo4jRawRecord(item));
        }
        private IResult Result;
        private List<IRecord> Records;
        private RawRecordEnumerator<IRecord> Enumerator;

        public override IReadOnlyList<string> Keys                  => Result.Keys;
        public override RawRecord Peek()                            => Enumerator.Current;
        public override IEnumerator<RawRecord> GetEnumerator()      => Enumerator;
        
        public override RawResultStatistics Statistics()            => new Neo4jRawResultStatistics(Result.Consume().Counters);
        public override List<RawResultNotification> Notifications() => Result.Consume().Notifications.Select(item => (RawResultNotification)new Neo4jRawResultNotifications(item)).ToList();
        public override void Consume() { }
    }
}
