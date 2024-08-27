using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blueprint41.Persistence
{
    public class DriverTransaction : IDriverQueryRunner, IDisposable, IAsyncDisposable
    {
        internal DriverTransaction(object value)
        {
            Value = value;
        }
        internal object Value { get; private set; }

        public Task CommitAsync() => Driver.I_ASYNC_TRANSACTION.CommitAsync(Value);
        public Task RollbackAsync() => Driver.I_ASYNC_TRANSACTION.RollbackAsync(Value);

        public Task<DriverResultSet> RunAsync(string query) => Driver.I_ASYNC_QUERY_RUNNER.RunAsync(Value, query);
        public Task<DriverResultSet> RunAsync(string query, Dictionary<string, object?> parameters) => Driver.I_ASYNC_QUERY_RUNNER.RunAsync(Value, query, parameters);

        public void Dispose() => ((IDisposable)Value).Dispose();
        public ValueTask DisposeAsync() => ((IAsyncDisposable)Value).DisposeAsync();
    }
}
