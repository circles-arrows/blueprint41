using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using System.Threading.Tasks;

using Neo4j.Driver;
using Blueprint41.Core;
using Blueprint41.Neo4j.Persistence;
using System.Linq;

namespace Blueprint41.Neo4j.Persistence.Driver.v4
{
    internal class Neo4jRawResult : RawResult
    {
        internal Neo4jRawResult(Task<IResultCursor> taskResult)
        {
            TaskResult = taskResult;
        }
        private Task<IResultCursor> TaskResult;
        private IResultCursor? Result;
        private RawRecordCursorEnumerator? Enumerator;

        public override IReadOnlyList<string> Keys
        {
            get
            {
                ConsumeResult();
                return Result!.Current.Keys;
            }
        }
        public override RawRecord Peek()
        {
            ConsumeResult();
            return Enumerator!.Current;
        }
        public override IEnumerator<RawRecord> GetEnumerator()
        {
            ConsumeResult();
            return Enumerator!;
        }

        public IResultSummary ResultSummary
        {
            get 
            {
                ConsumeResult();
                return Enumerator!.ResultSummary ?? Result!.ConsumeAsync().Result;
            }
        }

        public override RawResultStatistics Statistics()
        {
            return new Neo4jRawResultStatistics(ResultSummary.Counters);
        }
        public override List<RawResultNotification> Notifications()
        {
            
            return ResultSummary.Notifications.Select(item => (RawResultNotification)new Neo4jRawResultNotifications(item)).ToList();
        }
        public override void Consume() { }

        private void ConsumeResult()
        {
            if (Result is null)
            {
                Result = TaskResult.Result;
                Enumerator = new RawRecordCursorEnumerator(Result, item => new Neo4jRawRecord(item));
            }
        }

        protected class RawRecordCursorEnumerator : IEnumerator<RawRecord>
        {
            public RawRecordCursorEnumerator(IResultCursor cursor, Func<IRecord, RawRecord> converter)
            {
                Enumerator = cursor;
                Converter = converter;
                ResultSummary = null;
            }
            private IResultCursor Enumerator;
            private Func<IRecord, RawRecord> Converter;
            public IResultSummary? ResultSummary { get; private set; }

            public RawRecord Current => Converter.Invoke(Enumerator.Current);
            object IEnumerator.Current => Converter.Invoke(Enumerator.Current);
            public void Dispose()
            {
                ResultSummary = Enumerator.ConsumeAsync().Result;
            }
            public bool MoveNext() => Enumerator.FetchAsync().Result;
            public void Reset() => throw new NotImplementedException();
        }
    }
}
