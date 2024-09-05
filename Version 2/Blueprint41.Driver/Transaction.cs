using System;
using System.Collections.Generic;
using System.Linq;
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

        public void Commit() => Driver.RunBlocking(() => Driver.I_ASYNC_TRANSACTION.CommitAsync(_instance), "Transaction.Commit()");
        public Task CommitAsync() => Driver.I_ASYNC_TRANSACTION.CommitAsync(_instance);
        public void Rollback() => Driver.RunBlocking(() => Driver.I_ASYNC_TRANSACTION.RollbackAsync(_instance), "Transaction.Rollback()");
        public Task RollbackAsync() => Driver.I_ASYNC_TRANSACTION.RollbackAsync(_instance);

        public ResultCursor Run(string query) => Driver.RunBlocking(() => Driver.I_ASYNC_TRANSACTION.RunAsync(_instance, query), $"Transaction.Run({query})");
        public ResultCursor Run(string query, Dictionary<string, object?> parameters) => Driver.RunBlocking(() => Driver.I_ASYNC_TRANSACTION.RunAsync(_instance, query, parameters), $"Transaction.Run({query}, $\"Session.Run({query}, {{ {string.Join(", ", parameters.Select(parameter => $"{parameter.Key}: ({parameter.Value?.GetType().Name ?? "object"}){parameter.Value}"))} }})");
        public Task<ResultCursor> RunAsync(string query) => Driver.I_ASYNC_TRANSACTION.RunAsync(_instance, query);
        public Task<ResultCursor> RunAsync(string query, Dictionary<string, object?> parameters) => Driver.I_ASYNC_TRANSACTION.RunAsync(_instance, query, parameters);

        public void Dispose() => ((IDisposable)_instance).Dispose();
        public ValueTask DisposeAsync() => ((IAsyncDisposable)_instance).DisposeAsync();
    }
}
