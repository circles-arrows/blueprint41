﻿using System;
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
#elif MEMGRAPH
        public const string URI = "bolt://localhost:7690";
#endif    	

        public const string USER_NAME = "neo4j";
        public const string PASSWORD = "neoneoneo";
    }
}
