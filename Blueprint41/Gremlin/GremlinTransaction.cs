using Blueprint41.Core;
using Blueprint41.Response;
using Gremlin.Net.Driver;
using Gremlin.Net.Driver.Exceptions;
using Gremlin.Net.Structure.IO.GraphSON;
using Neo4j.Driver.V1;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.Gremlin
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
                    client = new GremlinClient(server, new GraphSONJTokenReader(), mimeType: GremlinClient.GraphSON2MimeType);

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

        protected override IGraphResponse RunPrivate(string cypher)
        {
            if ((RunningTransaction is GremlinTransaction grem && this == grem) == false)
                throw new InvalidOperationException("The current transaction is not a Gremlin transaction.");

            string gremlinQuery = Translate.ToGremlin(cypher, PersistenceProvider.TargetFeatures.GremlinFlavor);
            return Client.QueryAsync(gremlinQuery).Result;
        }

        protected override IGraphResponse RunPrivate(string cypher, Dictionary<string, object> parameters)
        {
            if ((RunningTransaction is GremlinTransaction grem && this == grem) == false)
                throw new InvalidOperationException("The current transaction is not a Gremlin transaction.");

            string gremlinQuery = Translate.ToGremlin(cypher.ToCypherString(parameters), PersistenceProvider.TargetFeatures.GremlinFlavor);
            return Client.QueryAsync(gremlinQuery).Result;
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
