using System;
using System.Collections.Generic;
using System.Text;

namespace Blueprint41.Persistence
{
    public class Query
    {
        public Query(object instance)
        {
            _instance = instance;
        }
        internal object _instance { get; private set; }

        public string Text => Driver.QUERY.Text(_instance);
        public IDictionary<string, object> Parameters => Driver.QUERY.Parameters(_instance);
    }
}
