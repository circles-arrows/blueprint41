using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blueprint41.Driver
{
    public class ResultCursor
    {
        internal ResultCursor() 
        {
            _instance = null;
        }
        internal ResultCursor(object instance)
        {
            _instance = instance;
        }
        internal object? _instance;
        internal object Instance
        {
            get
            {
                if (_instance is null)
                    throw new NotSupportedException("Cannot use void cursor.");

                return _instance;
            }
        }

        public Record Current => Driver.I_RESULT_CURSOR.Current(Instance);
        internal object CurrentInternal => Driver.I_RESULT_CURSOR.CurrentInternal(Instance);
        public Task<string[]> KeysAsync() => Driver.I_RESULT_CURSOR.KeysAsync(Instance);
        public Task<ResultSummary> ConsumeAsync() => Driver.I_RESULT_CURSOR.ConsumeAsync(Instance);
        public Task<Record> PeekAsync() => Driver.I_RESULT_CURSOR.PeekAsync(Instance);
        internal Task<object> PeekAsyncInternal() => Driver.I_RESULT_CURSOR.PeekAsyncInternal(Instance);

        public async Task<Counters> Statistics()
        {
            ResultSummary resultSummary = await ConsumeAsync();
            return resultSummary.Counters;
        }

        public Task<bool> FetchAsync() => Driver.I_RESULT_CURSOR.FetchAsync(Instance);
        public async Task<Record> First()
        {
            Record? result = await FirstOrDefault();
            if (result is null)
                throw new InvalidOperationException("Sequence contains no elements");

            return result;
        }
        public async Task<Record?> FirstOrDefault()
        {
            if (_instance is not null && await FetchAsync())
                return Current;

            return null;
        }

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
        public async Task<List<T>> ToListAsync<T>(Func<Record, T> selector)
        {
            List<T> records = new List<T>(64);

            if (_instance is not null)
            {
                while (await FetchAsync())
                    records.Add(selector.Invoke(Current));
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
        internal async Task<List<T>> ToListAsyncInternal<T>(Func<object, T> selector)
        {
            List<T> records = new List<T>(64);

            if (_instance is not null)
            {
                while (await FetchAsync())
                    records.Add(selector.Invoke(CurrentInternal));
            }

            return records;
        }
    }
}
