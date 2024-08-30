#pragma warning disable S3881

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.Driver
{
    public class Session : IQueryRunner, IDisposable, IAsyncDisposable
    {
        internal Session(object instance)
        {
            _instance = instance;
        }
        internal object _instance { get; private set; }

        public Bookmarks LastBookmarks => Driver.I_ASYNC_SESSION.LastBookmarks(_instance);
        public Task<Transaction> BeginTransactionAsync() => Driver.I_ASYNC_SESSION.BeginTransactionAsync(_instance);
        public Task<Transaction> BeginTransactionAsync(Action<TransactionConfigBuilder> configBuilder) => Driver.I_ASYNC_SESSION.BeginTransactionAsync(_instance, configBuilder);

        public Task<ResultCursor> RunAsync(string query) => Driver.I_ASYNC_SESSION.RunAsync(_instance, query);
        public Task<ResultCursor> RunAsync(string query, Dictionary<string, object?> parameters) => Driver.I_ASYNC_SESSION.RunAsync(_instance, query, parameters);

        public void Dispose() => ((IDisposable)_instance).Dispose();
        public ValueTask DisposeAsync() => ((IAsyncDisposable)_instance).DisposeAsync();
    }
}
