using Gremlin.Net.Driver;
using Gremlin.Net.Driver.Exceptions;
using Gremlin.Net.Structure.IO.GraphSON;
using Neo4j.Driver.V1;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.Gremlin.Cosmos
{
    public class GremlinTransaction : Transaction
    {
        private GremlinServer server;
        private GremlinClient client;

        public GremlinClient Client
        {
            get
            {
                if (client == null)
                    client = new GremlinClient(server, new GraphSON2Reader(), new GraphSON2Writer(), GremlinClient.GraphSON2MimeType);

                return client;
            }
        }

        internal GremlinTransaction(GremlinServer server)
        {
            this.server = server;
        }

        protected override void ApplyFunctionalId(FunctionalId functionalId)
        {

        }

        protected override void OnCommit()
        {
            client?.Dispose();
            client = null;
            server = null;
        }

        protected override void OnRollback()
        {
            client?.Dispose();
            client = null;
        }

        protected override void FlushPrivate()
        {
            base.FlushPrivate();
        }

        protected override IStatementResult RunPrivate(string cypher)
        {
            if ((RunningTransaction is GremlinTransaction grem && this == grem) == false)
                throw new InvalidOperationException("The current transaction is not a Gremlin transaction.");
            
            string gremlinQuery = Translate.ToCosmos(cypher);

            // TODO: change the ResultSet to IStatementResult
            SubmitRequest(client, gremlinQuery);

            return new CustomIStatementResult();
        }

        protected override IStatementResult RunPrivate(string cypher, Dictionary<string, object> parameters)
        {
            if ((RunningTransaction is GremlinTransaction grem && this == grem) == false)
                throw new InvalidOperationException("The current transaction is not a Gremlin transaction.");

            string gremlinQuery = Translate.ToCosmos(cypher.ToCypherString(parameters));

            // TODO: change the ResultSet to IStatementResult
            SubmitRequest(client, gremlinQuery);

            return new CustomIStatementResult();
        }
        private Task<ResultSet<dynamic>> SubmitRequest(GremlinClient gremlinClient, string query)
        {
            try
            {
                return gremlinClient.SubmitAsync<dynamic>(query);
            }
            catch (ResponseException e)
            {
                Debug.WriteLine("\tRequest Error!");
                Debug.WriteLine($"\tStatusCode: {e.StatusCode}");

                // On error, ResponseException.StatusAttributes will include the common StatusAttributes for successful requests, as well as
                // additional attributes for retry handling and diagnostics.
                // These include:
                //  x-ms-retry-after-ms         : The number of milliseconds to wait to retry the operation after an initial operation was throttled. This will be populated when
                //                              : attribute 'x-ms-status-code' returns 429.
                //  x-ms-activity-id            : Represents a unique identifier for the operation. Commonly used for troubleshooting purposes.
                PrintStatusAttributes(e.StatusAttributes);
                Debug.WriteLine($"\t[\"x-ms-retry-after-ms\"] : { GetValueAsString(e.StatusAttributes, "x-ms-retry-after-ms")}");
                Debug.WriteLine($"\t[\"x-ms-activity-id\"] : { GetValueAsString(e.StatusAttributes, "x-ms-activity-id")}");

                throw;
            }
        }

        private void PrintStatusAttributes(IReadOnlyDictionary<string, object> attributes)
        {
            Debug.WriteLine($"\tStatusAttributes:");
            Debug.WriteLine($"\t[\"x-ms-status-code\"] : { GetValueAsString(attributes, "x-ms-status-code")}");
            Debug.WriteLine($"\t[\"x-ms-total-request-charge\"] : { GetValueAsString(attributes, "x-ms-total-request-charge")}");
        }

        public string GetValueAsString(IReadOnlyDictionary<string, object> dictionary, string key)
        {
            return JsonConvert.SerializeObject(GetValueOrDefault(dictionary, key));
        }

        public object GetValueOrDefault(IReadOnlyDictionary<string, object> dictionary, string key)
        {
            if (dictionary.ContainsKey(key))
            {
                return dictionary[key];
            }

            return null;
        }

        /// <summary>
        /// Temp class to mimic IStatementResult from neo4j
        /// </summary>
        public class CustomIStatementResult : IStatementResult
        {
            private Dictionary<string, IRecord> records = new Dictionary<string, IRecord>();

            public IReadOnlyList<string> Keys => records.Keys.Select(x => x).ToList();

            public IResultSummary Summary => null;

            public IResultSummary Consume() => null;

            public IEnumerator<IRecord> GetEnumerator() => records.Select(x => x.Value).ToList().GetEnumerator();

            public IRecord Peek() => null;

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
