#pragma warning disable S3881

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.Persistence
{
    public class DriverSession : IDriverQueryRunner, IDisposable, IAsyncDisposable
    {
        internal DriverSession(object value)
        {
            Value = value;
        }
        internal object Value { get; private set; }

        public Bookmark LastBookmarks => Driver.I_ASYNC_SESSION.LastBookmarks(Value);
        public Task<DriverTransaction> BeginTransactionAsync() => Driver.I_ASYNC_SESSION.BeginTransactionAsync(Value);
        public Task<DriverTransaction> BeginTransactionAsync(Action<TransactionConfigBuilder> configBuilder) => Driver.I_ASYNC_SESSION.BeginTransactionAsync(Value, configBuilder);

        public Task<DriverResultSet> RunAsync(string query) => Driver.I_ASYNC_QUERY_RUNNER.RunAsync(Value, query);
        public Task<DriverResultSet> RunAsync(string query, Dictionary<string, object?> parameters) => Driver.I_ASYNC_QUERY_RUNNER.RunAsync(Value, query, parameters);

        public void Dispose() => ((IDisposable)Value).Dispose();
        public ValueTask DisposeAsync() => ((IAsyncDisposable)Value).DisposeAsync();
    }
}
