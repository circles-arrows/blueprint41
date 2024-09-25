using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

using Newtonsoft.Json;

using Neo4j.Driver;

namespace Blueprint41.UnitTest.Mocks
{


    internal class MockSession : IAsyncSession
    {
        public MockSession(IAsyncSession neoSession)
        {
            NeoSession = neoSession;
        }
        public IAsyncSession NeoSession { get; private set; }
        public IAsyncTransaction MockTransaction { get; set; }

        [Obsolete]
        public global::Neo4j.Driver.Bookmark LastBookmark => NeoSession.LastBookmark;

        public Bookmarks LastBookmarks => NeoSession.LastBookmarks;

        public SessionConfig SessionConfig => NeoSession.SessionConfig;

        public async Task<IAsyncTransaction> BeginTransactionAsync()
        {
            Console.WriteLine("BeginTransactionAsync");
            IAsyncTransaction transaction = await NeoSession.BeginTransactionAsync();
            MockTransaction = new MockTransaction(transaction);
            return MockTransaction;
        }

        public async Task<IAsyncTransaction> BeginTransactionAsync(Action<TransactionConfigBuilder> action)
        {
            Console.WriteLine("BeginTransactionAsync");
            IAsyncTransaction transaction = await NeoSession.BeginTransactionAsync(action);
            MockTransaction = new MockTransaction(transaction);
            return MockTransaction;
        }

        public Task CloseAsync()
        {
            Console.WriteLine("CloseAsync");
            return NeoSession.CloseAsync();
        }

        public void Dispose()
        {
            Console.WriteLine("Session: Dispose");
            NeoSession.Dispose();
        }

        public ValueTask DisposeAsync()
        {
            Console.WriteLine("Session: DisposeAsync");
            return NeoSession.DisposeAsync();
        }

        public Task<TResult> ExecuteReadAsync<TResult>(Func<IAsyncQueryRunner, Task<TResult>> work, Action<TransactionConfigBuilder> action = null)
        {
            return NeoSession.ExecuteReadAsync(work, action);
        }

        public Task ExecuteReadAsync(Func<IAsyncQueryRunner, Task> work, Action<TransactionConfigBuilder> action = null)
        {
            return NeoSession.ExecuteReadAsync(work, action);
        }

        public Task<TResult> ExecuteWriteAsync<TResult>(Func<IAsyncQueryRunner, Task<TResult>> work, Action<TransactionConfigBuilder> action = null)
        {
            return NeoSession.ExecuteWriteAsync(work, action);
        }

        public Task ExecuteWriteAsync(Func<IAsyncQueryRunner, Task> work, Action<TransactionConfigBuilder> action = null)
        {
            return NeoSession.ExecuteWriteAsync(work, action);
        }

        [Obsolete]
        public Task<T> ReadTransactionAsync<T>(Func<IAsyncTransaction, Task<T>> work, Action<TransactionConfigBuilder> action = null)
        {
            return NeoSession.ReadTransactionAsync(work, action);
        }

        [Obsolete]
        public Task ReadTransactionAsync(Func<IAsyncTransaction, Task> work, Action<TransactionConfigBuilder> action = null)
        {
            return NeoSession.ReadTransactionAsync(work, action);
        }

        public Task<IResultCursor> RunAsync(string query, Action<TransactionConfigBuilder> action = null)
        {
            Console.WriteLine(query);
            return NeoSession.RunAsync(query, action);
        }

        public Task<IResultCursor> RunAsync(string query, IDictionary<string, object> parameters, Action<TransactionConfigBuilder> action = null)
        {
            string st = query;

            if (parameters != null)
                foreach (var par in parameters)
                    st = st.Replace("{" + par.Key + "}", JsonConvert.SerializeObject(par.Value));

            Console.WriteLine(st);
            Console.WriteLine("params: {0}:{1}", parameters.ToString());

            return NeoSession.RunAsync(query, parameters, action);
        }

        public Task<IResultCursor> RunAsync(global::Neo4j.Driver.Query query, Action<TransactionConfigBuilder> action = null)
        {
            Console.WriteLine(query);
            return NeoSession.RunAsync(query, action);
        }

        public Task<IResultCursor> RunAsync(string query)
        {
            Console.WriteLine(query);
            return ((IAsyncQueryRunner)NeoSession).RunAsync(query);
        }

        public Task<IResultCursor> RunAsync(string query, object parameters)
        {
            if (parameters is IDictionary<string, object> par)
                return RunAsync(query, par);

            Console.WriteLine(query);
            Console.WriteLine("params: {0}:{1}", parameters.ToString());

            return NeoSession.RunAsync(query, parameters);
        }

        public Task<IResultCursor> RunAsync(string query, IDictionary<string, object> parameters)
        {
            string st = query;

            if (parameters != null)
                foreach (var par in parameters)
                    st = st.Replace("{" + par.Key + "}", JsonConvert.SerializeObject(par.Value));

            Console.WriteLine(st);
            Console.WriteLine("params: {0}:{1}", parameters.ToString());
            return ((IAsyncQueryRunner)NeoSession).RunAsync(query, parameters);
        }

        public Task<IResultCursor> RunAsync(global::Neo4j.Driver.Query query)
        {
            Console.WriteLine(query);
            return ((IAsyncQueryRunner)NeoSession).RunAsync(query);
        }

        [Obsolete]
        public Task<T> WriteTransactionAsync<T>(Func<IAsyncTransaction, Task<T>> work, Action<TransactionConfigBuilder> action = null)
        {
            return NeoSession.WriteTransactionAsync(work, action);
        }

        [Obsolete]
        public Task WriteTransactionAsync(Func<IAsyncTransaction, Task> work, Action<TransactionConfigBuilder> action = null)
        {
            return NeoSession.WriteTransactionAsync(work, action);
        }
    }
}
