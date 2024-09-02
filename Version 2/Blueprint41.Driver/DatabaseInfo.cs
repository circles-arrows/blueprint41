using System;
using System.Collections.Generic;
using System.Text;

namespace Blueprint41.Driver
{
    public class DatabaseInfo
    {
        public DatabaseInfo(object instance)
        {
            _instance = instance;
        }
        internal object _instance { get; private set; }

        //
        // Summary:
        //     The name of the database where the query is processed.
        //
        // Remarks:
        //     Returns
        //
        //     null
        //
        //     if the source server does not support multiple databases.
        public string Name => Driver.DATABASE_INFO.Name(_instance);
    }
}
