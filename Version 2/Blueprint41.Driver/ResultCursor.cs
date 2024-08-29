using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blueprint41.Driver
{
    public class ResultCursor
    {
        internal ResultCursor(object value)
        {
            Value = value;
        }
        internal object Value { get; private set; }

        public Record Current => Driver.I_RESULT_CURSOR.Current(Value);
        internal object CurrentInternal => Driver.I_RESULT_CURSOR.CurrentInternal(Value);

        public bool IsOpen => Driver.I_RESULT_CURSOR.IsOpen(Value);
        public Task<string[]> KeysAsync() => Driver.I_RESULT_CURSOR.KeysAsync(Value);
        public Task<ResultSummary> ConsumeAsync() => Driver.I_RESULT_CURSOR.ConsumeAsync(Value);
        public Task<Record> PeekAsync() => Driver.I_RESULT_CURSOR.PeekAsync(Value);
        internal Task<object> PeekAsyncInternal() => Driver.I_RESULT_CURSOR.PeekAsyncInternal(Value);

        public Task<bool> FetchAsync() => Driver.I_RESULT_CURSOR.FetchAsync(Value);

        public async Task<List<Record>> ToListAsync()
        {
            List<Record> records = new List<Record>(64);

            if (Value is not null)
            {
                while (await FetchAsync())
                    records.Add(Current);
            }

            return records;
        }
        internal async Task<List<object>> ToListAsyncInternal()
        {
            List<object> records = new List<object>(64);

            if (Value is not null)
            {
                while (await FetchAsync())
                    records.Add(CurrentInternal);
            }

            return records;
        }
    }
}
