using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blueprint41.Driver
{
    public class ResultCursor
    {
        internal ResultCursor(object instance)
        {
            _instance = instance;
        }
        internal object _instance { get; private set; }

        public Record Current => Driver.I_RESULT_CURSOR.Current(_instance);
        internal object CurrentInternal => Driver.I_RESULT_CURSOR.CurrentInternal(_instance);
        public Task<string[]> KeysAsync() => Driver.I_RESULT_CURSOR.KeysAsync(_instance);
        public Task<ResultSummary> ConsumeAsync() => Driver.I_RESULT_CURSOR.ConsumeAsync(_instance);
        public Task<Record> PeekAsync() => Driver.I_RESULT_CURSOR.PeekAsync(_instance);
        internal Task<object> PeekAsyncInternal() => Driver.I_RESULT_CURSOR.PeekAsyncInternal(_instance);

        public Task<bool> FetchAsync() => Driver.I_RESULT_CURSOR.FetchAsync(_instance);

        public async Task<List<Record>> ToListAsync()
        {
            List<Record> records = new List<Record>(64);

            if (_instance is not null)
            {
                while (await FetchAsync())
                    records.Add(Current);
            }

            return records;
        }
        internal async Task<List<object>> ToListAsyncInternal()
        {
            List<object> records = new List<object>(64);

            if (_instance is not null)
            {
                while (await FetchAsync())
                    records.Add(CurrentInternal);
            }

            return records;
        }
    }
}
