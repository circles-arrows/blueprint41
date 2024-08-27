using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blueprint41.Persistence
{
    public class DriverRecordSet
    {
        internal DriverRecordSet(object value)
        {
            Value = value;
        }
        internal object Value { get; private set; }

        public object Current => Driver.I_RESULT_CURSOR.Current(Value);
        public bool IsOpen => Driver.I_RESULT_CURSOR.IsOpen(Value);
        public Task<string[]> KeysAsync() => Driver.I_RESULT_CURSOR.KeysAsync(Value);
        public Task<DriverResultSummary> ConsumeAsync() => Driver.I_RESULT_CURSOR.ConsumeAsync(Value);
        public Task<object> PeekAsync() => Driver.I_RESULT_CURSOR.PeekAsync(Value);
        public Task<bool> FetchAsync() => Driver.I_RESULT_CURSOR.FetchAsync(Value);

        public async Task<List<object>> ToList()
        {
            List<object> records = new List<object>(64);

            if (Value is not null)
            {
                while (await FetchAsync())
                    records.Add(Current);
            }

            return records;
        }
    }
}
