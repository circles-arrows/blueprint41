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

        public Record Current => IsVoid(Driver.I_RESULT_CURSOR.Current, null!);
        internal object CurrentInternal => IsVoid(Driver.I_RESULT_CURSOR.CurrentInternal, null!);
        public Task<string[]> KeysAsync() => IsVoid(Driver.I_RESULT_CURSOR.KeysAsync, voidKeys);
        public Task<ResultSummary> ConsumeAsync() => IsVoid(Driver.I_RESULT_CURSOR.ConsumeAsync, voidResultSummary);
        public Task<Record> PeekAsync() => IsVoid(Driver.I_RESULT_CURSOR.PeekAsync, voidRecord);
        internal Task<object> PeekAsyncInternal() => IsVoid(Driver.I_RESULT_CURSOR.PeekAsyncInternal, voidObject);

        public async Task<Counters> Statistics()
        {
            if (_instance is null)
                return voidStatistics;

            ResultSummary resultSummary = await ConsumeAsync();
            return resultSummary.Counters;
        }

        public Task<bool> FetchAsync() => IsVoid(Driver.I_RESULT_CURSOR.FetchAsync, voidFalse);
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

        private T IsVoid<T>(Func<object, T> value, T defaultValue)
        {
            if (_instance is null)
                return defaultValue;

            return value.Invoke(_instance);
        }

        private static readonly Task<bool> voidFalse = Task.FromResult(false);
        private static readonly Task<object> voidObject = Task.FromResult<object>(default!);
        private static readonly Task<Record> voidRecord = Task.FromResult<Record>(default!);
        private static readonly Task<string[]> voidKeys = Task.FromResult(new string[0]);
        private static readonly Counters voidStatistics = new Counters();
        private static readonly Task<ResultSummary> voidResultSummary = Task.FromResult(new ResultSummary());
    }
}
