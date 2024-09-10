#pragma warning disable S3881

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Blueprint41.Config;

namespace Blueprint41.Persistence
{
    public class DriverSession : IQueryRunner, IDisposable, IAsyncDisposable
    {
        internal DriverSession(object instance)
        {
            _instance = instance;
        }
        internal object _instance { get; private set; }

        public Bookmarks LastBookmarks => Driver.I_ASYNC_SESSION.LastBookmarks(_instance);
        public DriverTransaction BeginTransaction() => Driver.RunBlocking(() => Driver.I_ASYNC_SESSION.BeginTransactionAsync(_instance), "Session.BeginTransaction()");
        public DriverTransaction BeginTransaction(Action<TransactionConfigBuilder> configBuilder) => Driver.RunBlocking(() => Driver.I_ASYNC_SESSION.BeginTransactionAsync(_instance, configBuilder), "Session.BeginTransaction(Action<TransactionConfigBuilder> configBuilder)");

        public Task<DriverTransaction> BeginTransactionAsync() => Driver.I_ASYNC_SESSION.BeginTransactionAsync(_instance);
        public Task<DriverTransaction> BeginTransactionAsync(Action<TransactionConfigBuilder> configBuilder) => Driver.I_ASYNC_SESSION.BeginTransactionAsync(_instance, configBuilder);

        public ResultCursor Run(string query) => Driver.RunBlocking(() => Driver.I_ASYNC_SESSION.RunAsync(_instance, query), $"Session.Run({query})");
        public ResultCursor Run(string query, Dictionary<string, object?> parameters) => Driver.RunBlocking(() => Driver.I_ASYNC_SESSION.RunAsync(_instance, query, parameters), $"Session.Run({query}, {{ {string.Join(", ", parameters.Select(parameter => $"{parameter.Key}: ({parameter.Value?.GetType().Name ?? "object"}){parameter.Value}"))} }})");

        public Task<ResultCursor> RunAsync(string query) => Driver.I_ASYNC_SESSION.RunAsync(_instance, query);
        public Task<ResultCursor> RunAsync(string query, Dictionary<string, object?> parameters) => Driver.I_ASYNC_SESSION.RunAsync(_instance, query, parameters);

        public void Dispose() => ((IDisposable)_instance).Dispose();
        public ValueTask DisposeAsync() => ((IAsyncDisposable)_instance).DisposeAsync();

        internal void Close() => Driver.RunBlocking(async () => await DisposeAsync().ConfigureAwait(false), "Close Session");
    }
}
