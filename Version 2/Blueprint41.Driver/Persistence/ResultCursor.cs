using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blueprint41.Persistence
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
        public string[] Keys() => Driver.RunBlocking(KeysAsync, "ResultCursor.Keys()");
        public Task<string[]> KeysAsync() => IsVoid(Driver.I_RESULT_CURSOR.KeysAsync, voidKeysAsync);
        public ResultSummary Consume() => Driver.RunBlocking(ConsumeAsync, "ResultCursor.Consume()");
        public Task<ResultSummary> ConsumeAsync() => IsVoid(Driver.I_RESULT_CURSOR.ConsumeAsync, voidResultSummaryAsync);
        public Record Peek() => Driver.RunBlocking(PeekAsync, "ResultCursor.Peek()");
        public Task<Record> PeekAsync() => IsVoid(Driver.I_RESULT_CURSOR.PeekAsync, voidRecord);
        internal object PeekInternal() => Driver.RunBlocking(PeekAsyncInternal, "ResultCursor.PeekInternal()");
        internal Task<object> PeekAsyncInternal() => IsVoid(Driver.I_RESULT_CURSOR.PeekAsyncInternal, voidObject);

        public Counters Statistics() => Driver.RunBlocking(StatisticsAsync, "ResultCursor.Statistics()");
        public async Task<Counters> StatisticsAsync()
        {
            if (_instance is null)
                return voidStatistics;

            ResultSummary resultSummary = await ConsumeAsync();
            return resultSummary.Counters;
        }

        public bool Fetch() => Driver.RunBlocking(FetchAsync, "ResultCursor.Fetch()");
        public Task<bool> FetchAsync() => IsVoid(Driver.I_RESULT_CURSOR.FetchAsync, voidFalse);

        public Record First() => Driver.RunBlocking(FirstAsync, "ResultCursor.First()");
        public async Task<Record> FirstAsync()
        {
            Record? result = await FirstOrDefaultAsync();
            if (result is null)
                throw new InvalidOperationException("Sequence contains no elements");

            return result;
        }
        public Record? FirstOrDefault() => Driver.RunBlocking(FirstOrDefaultAsync, "ResultCursor.FirstOrDefault()");
        public async Task<Record?> FirstOrDefaultAsync()
        {
            if (_instance is not null && await FetchAsync())
                return Current;

            return null;
        }

        public List<Record> ToList() => Driver.RunBlocking(ToListAsync, "ResultCursor.ToList()");
        public List<T> ToList<T>(Func<Record, T> selector) => Driver.RunBlocking(() => ToListAsync(selector), "ResultCursor.ToList<T>(Func<Record, T> selector)");
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

        internal List<object> ToListInternal() => Driver.RunBlocking(ToListAsyncInternal, "ResultCursor.ToListInternal()");
        internal List<T> ToListInternal<T>(Func<object, T> selector) => Driver.RunBlocking(() => ToListAsyncInternal(selector), "ResultCursor.ToListInternal<T>(Func<object, T> selector)");

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
        private static readonly Task<string[]> voidKeysAsync = Task.FromResult(new string[0]);
        private static readonly Counters voidStatistics = new Counters();
        private static readonly Task<ResultSummary> voidResultSummaryAsync = Task.FromResult(new ResultSummary());
    }
}
