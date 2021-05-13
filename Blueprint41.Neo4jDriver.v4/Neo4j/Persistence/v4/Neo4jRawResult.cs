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

        private IResultCursor? Result
        {
            get
            {
                ConsumeResult();
                return m_Result!;
            }
        }
        private IResultCursor? m_Result;

        private RawRecordEnumerator<IRecord> Enumerator
        {
            get
            {
                ConsumeResult();
                m_Enumerator = new RawRecordEnumerator<IRecord>(Result.ToListAsync().GetTaskResult().GetEnumerator(), item => new Neo4jRawRecord(item));
                return m_Enumerator!;
            }
        }
        private RawRecordEnumerator<IRecord>? m_Enumerator;

        private IResultSummary ResultSummary
        {
            get
            {
                ConsumeResult();
                m_ResultSummary = Result!.ConsumeAsync().GetTaskResult();
                return m_ResultSummary!;
            }
        }
        private IResultSummary? m_ResultSummary;

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
            return Enumerator.Current;
        }
        public override IEnumerator<RawRecord> GetEnumerator()
        {
            return Enumerator;
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
            if (m_Result is null)
                m_Result = TaskResult.GetTaskResult();
        }

        //protected class RawRecordCursorEnumerator : IEnumerator<RawRecord>
        //{
        //    public RawRecordCursorEnumerator(IResultCursor cursor, Func<IRecord, RawRecord> converter)
        //    {
        //        Enumerator = cursor;
        //        Converter = converter;
        //        ResultSummary = null;
        //    }
        //    private IResultCursor Enumerator;
        //    private Func<IRecord, RawRecord> Converter;
        //    public IResultSummary? ResultSummary { get; private set; }

        //    public RawRecord Current => Converter.Invoke(Enumerator.Current);
        //    object IEnumerator.Current => Converter.Invoke(Enumerator.Current);
        //    public void Dispose()
        //    {
        //        ResultSummary = Enumerator.ConsumeAsync().GetTaskResult();
        //    }
        //    public bool MoveNext() => Enumerator.FetchAsync().GetTaskResult();
        //    public void Reset() => throw new NotImplementedException();
        //}
    }
}
