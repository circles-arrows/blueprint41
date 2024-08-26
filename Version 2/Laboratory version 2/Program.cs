using System;

using DataStore;
using Blueprint41.Persistence;

namespace Laboratory
{
    internal class Program
    {
        private const int MAX_CONNECTION_POOL_SIZE = 400;
        private const int DEFAULT_READWRITESIZE = 65536;
        private const int DEFAULT_READWRITESIZE_MAX = 655360;

        static void Main(string[] args)
        {
            Driver.Configure<Neo4j.Driver.IDriver>();

            Driver driver = Driver.Get(new Uri(@"bolt://localhost:7876"), AuthToken.Basic("neo4j", "neoneoneo"), delegate (ConfigBuilder o)
            {
                o.WithFetchSize(ConfigBuilder.Infinite);
                o.WithMaxConnectionPoolSize(MAX_CONNECTION_POOL_SIZE);
                o.WithDefaultReadBufferSize(DEFAULT_READWRITESIZE);
                o.WithDefaultWriteBufferSize(DEFAULT_READWRITESIZE);
                o.WithMaxReadBufferSize(DEFAULT_READWRITESIZE_MAX);
                o.WithMaxWriteBufferSize(DEFAULT_READWRITESIZE_MAX);
                o.WithMaxTransactionRetryTime(TimeSpan.Zero);
            });


            //MockModel model = MockModel.Connect(uri, AuthToken.Basic(username, password), database, config);
            //model.Execute(true);

            // Generate documentation
        }
    }
}
