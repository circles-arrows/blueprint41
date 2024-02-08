
using Neo4j.Driver;

using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.UnitTest.Memgraph.Mocks
{
    internal class MockTransaction : IAsyncTransaction/*,ITransaction*/
    {
        public MockTransaction(IAsyncTransaction neoTransaction)
        {
            NeoTransaction = neoTransaction;
        }

        public IAsyncTransaction NeoTransaction { get; private set; }

        public TransactionConfig TransactionConfig => NeoTransaction.TransactionConfig;

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

        public ValueTask DisposeAsync()
        {
            Console.WriteLine("DisposeAsync");
            return NeoTransaction.DisposeAsync();
        }

        public Task RollbackAsync()
        {
            Console.WriteLine("RollbackAsync");
            return NeoTransaction.RollbackAsync();
        }

        public Task<IResultCursor> RunAsync(string query)
        {
            Console.WriteLine(query);
            return NeoTransaction.RunAsync(query);
        }

        public Task<IResultCursor> RunAsync(string query, object parameters)
        {
            if (parameters is IDictionary<string, object> par)
                return RunAsync(query, par);

            Console.WriteLine(query);
            Console.WriteLine("params: {0}:{1}", parameters.ToString());
            return NeoTransaction.RunAsync(query, parameters);
        }

        public Task<IResultCursor> RunAsync(string query, IDictionary<string, object> parameters)
        {
            string st = query;

            if (parameters != null)
                foreach (var par in parameters)
                    st = st.Replace("{" + par.Key + "}", JsonConvert.SerializeObject(par.Value));

            Console.WriteLine(st);
            return NeoTransaction.RunAsync(query, parameters);
        }

        public Task<IResultCursor> RunAsync(global::Neo4j.Driver.Query query)
        {
            Console.WriteLine(query);
            return NeoTransaction.RunAsync(query);
        }
    }
}
