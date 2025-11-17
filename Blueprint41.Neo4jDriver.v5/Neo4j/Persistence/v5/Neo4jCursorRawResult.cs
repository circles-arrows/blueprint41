using Blueprint41.Core;
using Neo4j.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Blueprint41.Neo4j.Persistence.Driver.v5
{
    internal sealed class Neo4jCursorRawResult : RawResult
    {
        internal Neo4jCursorRawResult(IResultCursor cursor)
        {
            Cursor = cursor ?? throw new ArgumentNullException(nameof(cursor));
        }

        private readonly IResultCursor Cursor;
        private List<IRecord>? records;
        private IReadOnlyList<string>? keys;
        private IResultSummary? summary;
        private bool materialized;
        private bool cursorConsumed;

        private void EnsureMaterialized()
        {
            if (materialized)
                return;

            if (cursorConsumed)
            {
                records ??= new List<IRecord>();
                keys ??= Array.Empty<string>();
                materialized = true;
                return;
            }

            List<IRecord> buffer = new();
            while (Cursor.FetchAsync().ConfigureAwait(false).GetAwaiter().GetResult())
            {
                buffer.Add(Cursor.Current);
            }

            records = buffer;
            keys ??= buffer.Count > 0 ? buffer[0].Keys : Array.Empty<string>();

            summary ??= Cursor.ConsumeAsync().ConfigureAwait(false).GetAwaiter().GetResult();
            cursorConsumed = true;
            materialized = true;
        }

        private void EnsureSummary()
        {
            if (summary is not null)
                return;

            summary = Cursor.ConsumeAsync().ConfigureAwait(false).GetAwaiter().GetResult();
            cursorConsumed = true;

            if (!materialized)
            {
                records = new List<IRecord>();
                keys ??= Array.Empty<string>();
                materialized = true;
            }
        }

        public override IReadOnlyList<string> Keys
        {
            get
            {
                EnsureMaterialized();
                return keys!;
            }
        }

        public override RawRecord? Peek()
        {
            EnsureMaterialized();
            if (records is null || records.Count == 0)
                return null;

            return new Neo4jRawRecord(records[0]);
        }

        public override IEnumerator<RawRecord> GetEnumerator()
        {
            EnsureMaterialized();

            IEnumerable<IRecord> source = records ?? Enumerable.Empty<IRecord>();
            return source.Select(record => new Neo4jRawRecord(record)).GetEnumerator();
        }

        public override RawResultStatistics Statistics()
        {
            EnsureSummary();
            return new Neo4jRawResultStatistics(summary!.Counters);
        }

        public override List<RawResultNotification> Notifications()
        {
            EnsureSummary();

            if (summary!.Notifications is null)
                return new List<RawResultNotification>();

            return summary.Notifications.Select(notification => (RawResultNotification)new Neo4jRawResultNotifications(notification)).ToList();
        }

        public override void Consume()
        {
            EnsureSummary();
        }
    }
}
