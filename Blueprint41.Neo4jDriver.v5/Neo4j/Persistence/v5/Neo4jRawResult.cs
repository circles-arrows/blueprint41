using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using System.Threading.Tasks;

using Neo4j.Driver;
using Blueprint41.Core;
using Blueprint41.Neo4j.Persistence;
using System.Linq;

namespace Blueprint41.Neo4j.Persistence.Driver.v5
{
    internal class Neo4jRawResult : RawResult
    {
        internal Neo4jRawResult(CustomTaskScheduler scheduler, IResultCursor taskResult)
        {
            Scheduler = scheduler;
            TaskResult = taskResult;
        }
        private readonly CustomTaskScheduler Scheduler;
        private readonly IResultCursor TaskResult;

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
                m_Enumerator = new RawRecordEnumerator<IRecord>(Scheduler.RunBlocking(() => ToList(Result), "Convert Result to Result List").GetEnumerator(), item => new Neo4jRawRecord(item));
                return m_Enumerator!;

                async Task<List<IRecord>> ToList(IResultCursor? cursor)
                {
                    List<IRecord> records = new List<IRecord>(64);

                    if (cursor is not null)
                    {
                        while (await cursor.FetchAsync())
                        {
                            var record = cursor.Current;
                            records.Add(record);
                        }
                    }

                    return records;
                }
            }
        }
        private RawRecordEnumerator<IRecord>? m_Enumerator;

        private IResultSummary ResultSummary
        {
            get
            {
                m_ResultSummary = Scheduler.RunBlocking(() => Result!.ConsumeAsync(), "Consume result to get statistics");
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
            if (ResultSummary.Notifications is not null)
                return ResultSummary.Notifications.Select(item => (RawResultNotification)new Neo4jRawResultNotifications(item)).ToList();

            return new List<RawResultNotification>();
        }
        public override void Consume() { }

        private void ConsumeResult()
        {
            m_Result ??= TaskResult;
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
