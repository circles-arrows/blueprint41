using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.UnitTest
{
    internal static class DatabaseConnectionSettings
    {
#if NEO4J
        public const string URI = "bolt://localhost:7687";

        public const string USER_NAME = "neo4j";
        public const string PASSWORD = "neoneoneo";
        public const string? DATA_BASE = "unittest";
#elif MEMGRAPH
        public const string URI = "bolt://localhost:7690";

        public const string USER_NAME = "";
        public const string PASSWORD = "";
        public const string? DATA_BASE = null;
#endif

#if NEO4J
        public const GDMS DatastoreTechnology = GDMS.Neo4j;
#elif MEMGRAPH
        public const GDMS DatastoreTechnology = GDMS.Memgraph;
#endif
    }
}
