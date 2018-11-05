using Blueprint41.Neo4j.Persistence;
using Neo4j.Driver.V1;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blueprint41.UnitTest.Mocks
{
    

    internal class MockSession : ISession
    {
        public string LastBookmark => NeoSession.LastBookmark;
        public ISession NeoSession { get; private set; }
        public ITransaction MockTransaction { get; set; }

        public MockSession(ISession neoSession)
        {
            NeoSession = neoSession;
        }

        public ITransaction BeginTransaction()
        {
            Console.WriteLine("Begin Transaction");
            ITransaction transaction = NeoSession.BeginTransaction();
            MockTransaction = new MockTransaction(transaction);
            return MockTransaction;
        }

        public ITransaction BeginTransaction(string bookmark)
        {
            Console.WriteLine("Begin Transaction");
            ITransaction transaction = NeoSession.BeginTransaction();
            MockTransaction = new MockTransaction(transaction);
            return MockTransaction;
        }

        public Task<ITransaction> BeginTransactionAsync()
        {
            Console.WriteLine("BeginTransactionAsync");
            return Task.Run(() =>
            {
                ITransaction transaction = NeoSession.BeginTransaction();
                MockTransaction = new MockTransaction(transaction);
                return MockTransaction;
            });
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

        ~MockSession()
        {
            NeoSession = null;
        }

        public T ReadTransaction<T>(Func<ITransaction, T> work)
        {
            return NeoSession.ReadTransaction(work);
        }

        public void ReadTransaction(Action<ITransaction> work)
        {
            NeoSession.ReadTransaction(work);
        }

        public Task<T> ReadTransactionAsync<T>(Func<ITransaction, Task<T>> work)
        {
            return NeoSession.ReadTransactionAsync(work);
        }

        public Task ReadTransactionAsync(Func<ITransaction, Task> work)
        {
            return NeoSession.ReadTransactionAsync(work);
        }


        public IStatementResult Run(string statement)
        {
            Console.WriteLine(statement);
            return NeoSession.Run(statement);
        }

        public IStatementResult Run(string statement, object parameters)
        {
            if (parameters is IDictionary<string, object> par)
                return Run(statement, par);

            Console.WriteLine(statement);
            Console.WriteLine("params: {0}:{1}", parameters.ToString());

            return NeoSession.Run(statement, parameters);
        }

        public IStatementResult Run(string statement, IDictionary<string, object> parameters)
        {
            string st = statement;

            if (parameters != null)
                foreach (var par in parameters)
                    st = st.Replace("{" + par.Key + "}", JsonConvert.SerializeObject(par.Value));

            Console.WriteLine(st);
            return NeoSession.Run(statement, parameters);
        }

        public IStatementResult Run(Statement statement)
        {
            return Run(statement.Text, statement.Parameters);
        }

        public Task<IStatementResultCursor> RunAsync(string statement)
        {
            Console.WriteLine(statement);
            return NeoSession.RunAsync(statement);
        }

        public Task<IStatementResultCursor> RunAsync(string statement, object parameters)
        {
            Console.WriteLine(statement);
            Console.WriteLine("params: {0}:{1}", parameters?.ToString());
            return NeoSession.RunAsync(statement, parameters);
        }

        public Task<IStatementResultCursor> RunAsync(string statement, IDictionary<string, object> parameters)
        {
            if (parameters != null)
                foreach (var par in parameters)
                    statement = statement.Replace("{" + par.Key + "}", JsonConvert.SerializeObject(par.Value));

            Console.WriteLine(statement);
            return NeoSession.RunAsync(statement, parameters);
        }

        public Task<IStatementResultCursor> RunAsync(Statement statement)
        {
            return RunAsync(statement.Text, statement.Parameters);
        }


        public T WriteTransaction<T>(Func<ITransaction, T> work)
        {
            Console.WriteLine("Write Transaction");
            return NeoSession.WriteTransaction(work);
        }

        public void WriteTransaction(Action<ITransaction> work)
        {
            Console.WriteLine("Write Transaction");
            NeoSession.WriteTransaction(work);
        }

        public Task<T> WriteTransactionAsync<T>(Func<ITransaction, Task<T>> work)
        {
            Console.WriteLine("WriteTransactionAsync");
            return NeoSession.WriteTransactionAsync(work);
        }

        public Task WriteTransactionAsync(Func<ITransaction, Task> work)
        {
            Console.WriteLine("WriteTransactionAsync");
            return NeoSession.WriteTransactionAsync(work);
        }

        public ITransaction BeginTransaction(TransactionConfig txConfig)
        {
            Console.WriteLine("BeginTransaction");
            return NeoSession.BeginTransaction(txConfig);
        }

        public Task<ITransaction> BeginTransactionAsync(TransactionConfig txConfig)
        {
            Console.WriteLine("BeginTransactionAsync");
            return NeoSession.BeginTransactionAsync(txConfig);
        }

        public T ReadTransaction<T>(Func<ITransaction, T> work, TransactionConfig txConfig)
        {
            Console.WriteLine("ReadTransaction");
            return NeoSession.ReadTransaction(work, txConfig);
        }

        public Task<T> ReadTransactionAsync<T>(Func<ITransaction, Task<T>> work, TransactionConfig txConfig)
        {
            Console.WriteLine("ReadTransactionAsync");
            return NeoSession.ReadTransactionAsync(work, txConfig);
        }

        public void ReadTransaction(Action<ITransaction> work, TransactionConfig txConfig)
        {
            Console.WriteLine("ReadTransaction");
            NeoSession.ReadTransaction(work, txConfig);
        }

        public Task ReadTransactionAsync(Func<ITransaction, Task> work, TransactionConfig txConfig)
        {
            Console.WriteLine("ReadTransactionAsync");
            return NeoSession.ReadTransactionAsync(work, txConfig);
        }

        public T WriteTransaction<T>(Func<ITransaction, T> work, TransactionConfig txConfig)
        {
            Console.WriteLine("WriteTransaction");
            return NeoSession.WriteTransaction(work, txConfig);
        }

        public Task<T> WriteTransactionAsync<T>(Func<ITransaction, Task<T>> work, TransactionConfig txConfig)
        {
            Console.WriteLine("WriteTransactionAsync");
            return NeoSession.WriteTransactionAsync(work, txConfig);
        }

        public void WriteTransaction(Action<ITransaction> work, TransactionConfig txConfig)
        {
            Console.WriteLine("WriteTransaction");
            NeoSession.WriteTransaction(work, txConfig);
        }

        public Task WriteTransactionAsync(Func<ITransaction, Task> work, TransactionConfig txConfig)
        {
            Console.WriteLine("WriteTransactionAsync");
            return NeoSession.WriteTransactionAsync(work, txConfig);
        }

        public IStatementResult Run(string statement, TransactionConfig txConfig)
        {
            Console.WriteLine("Run");
            return NeoSession.Run(statement, txConfig);
        }

        public Task<IStatementResultCursor> RunAsync(string statement, TransactionConfig txConfig)
        {
            Console.WriteLine("RunAsync");
            return NeoSession.RunAsync(statement, txConfig);
        }

        public IStatementResult Run(string statement, IDictionary<string, object> parameters, TransactionConfig txConfig)
        {
            Console.WriteLine("Run");
            return NeoSession.Run(statement, parameters, txConfig);
        }

        public Task<IStatementResultCursor> RunAsync(string statement, IDictionary<string, object> parameters, TransactionConfig txConfig)
        {
            Console.WriteLine("RunAsync");
            return NeoSession.RunAsync(statement, parameters, txConfig);
        }

        public IStatementResult Run(Statement statement, TransactionConfig txConfig)
        {
            Console.WriteLine("Run");
            return NeoSession.Run(statement, txConfig);
        }

        public Task<IStatementResultCursor> RunAsync(Statement statement, TransactionConfig txConfig)
        {
            Console.WriteLine("RunAsync");
            return NeoSession.RunAsync(statement, txConfig);
        }
    }
}
