using Neo4j.Driver.V1;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.UnitTest.Mocks
{
    internal class MockTransaction : ITransaction
    {
        public MockTransaction(ITransaction neoTransaction)
        {
            NeoTransaction = neoTransaction;
        }

        public ITransaction NeoTransaction { get; private set; }

        public Task CommitAsync()
        {
            Console.WriteLine("CommitAsync");
            return NeoTransaction.CommitAsync();
        }

        public void Dispose()
        {
            Console.WriteLine("Transaction: Dispose");
            NeoTransaction.Dispose();
        }

        public void Failure()
        {
            Console.WriteLine("Transaction: Failure");
            NeoTransaction.Failure();
        }

        public Task RollbackAsync()
        {
            Console.WriteLine("RollbackAsync");
            return NeoTransaction.RollbackAsync();
        }

        public IStatementResult Run(string statement)
        {
            Console.WriteLine(statement);
            return NeoTransaction.Run(statement);
        }

        public IStatementResult Run(string statement, object parameters)
        {
            if (parameters is IDictionary<string, object> par)
                return Run(statement, par);

            Console.WriteLine(statement);
            Console.WriteLine("params: {0}:{1}", parameters.ToString());

            return NeoTransaction.Run(statement, parameters);
        }

        public IStatementResult Run(string statement, IDictionary<string, object> parameters)
        {
            string st = statement;

            if (parameters != null)
                foreach (var par in parameters)
                    st = st.Replace("{" + par.Key + "}", JsonConvert.SerializeObject(par.Value));

            Console.WriteLine(st);
            return NeoTransaction.Run(statement, parameters);
        }

        public IStatementResult Run(Statement statement)
        {
            return Run(statement.Text, statement.Parameters);
        }

        public Task<IStatementResultCursor> RunAsync(string statement)
        {
            Console.WriteLine(statement);
            return NeoTransaction.RunAsync(statement);
        }

        public Task<IStatementResultCursor> RunAsync(string statement, object parameters)
        {
            Console.WriteLine(statement);
            Console.WriteLine("params: {0}:{1}", parameters?.ToString());
            return NeoTransaction.RunAsync(statement, parameters);
        }

        public Task<IStatementResultCursor> RunAsync(string statement, IDictionary<string, object> parameters)
        {
            string st = statement;

            if (parameters != null)
                foreach (var par in parameters)
                    st = st.Replace("{" + par.Key + "}", JsonConvert.SerializeObject(par.Value));

            Console.WriteLine(st);
            return NeoTransaction.RunAsync(statement, parameters);
        }

        public Task<IStatementResultCursor> RunAsync(Statement statement)
        {
            return RunAsync(statement.Text, statement.Parameters);
        }

        public void Success()
        {
            Console.WriteLine("Transaction: Success");
            NeoTransaction.Success();
        }

        ~MockTransaction()
        {
            NeoTransaction = null;
        }
    }
}
