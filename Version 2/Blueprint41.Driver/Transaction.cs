using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blueprint41.Driver
{
    public class Transaction : IQueryRunner, IDisposable, IAsyncDisposable
    {
        internal Transaction(object instance)
        {
            _instance = instance;
        }
        internal object _instance { get; private set; }

        public Task CommitAsync() => Driver.I_ASYNC_TRANSACTION.CommitAsync(_instance);
        public Task RollbackAsync() => Driver.I_ASYNC_TRANSACTION.RollbackAsync(_instance);

        public Task<ResultCursor> RunAsync(string query) => Driver.I_ASYNC_TRANSACTION.RunAsync(_instance, query);
        public Task<ResultCursor> RunAsync(string query, Dictionary<string, object?> parameters) => Driver.I_ASYNC_TRANSACTION.RunAsync(_instance, query, parameters);

        public void Dispose() => ((IDisposable)_instance).Dispose();
        public ValueTask DisposeAsync() => ((IAsyncDisposable)_instance).DisposeAsync();
    }
}
