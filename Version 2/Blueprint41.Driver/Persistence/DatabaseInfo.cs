using System;
using System.Collections.Generic;
using System.Text;

namespace Blueprint41.Persistence
{
    public class DatabaseInfo
    {
        public DatabaseInfo(object instance)
        {
            _instance = instance;
        }
        internal object _instance { get; private set; }

        public string Name => Driver.DATABASE_INFO.Name(_instance);
    }
}
