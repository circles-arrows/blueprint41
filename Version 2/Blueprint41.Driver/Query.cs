using System;
using System.Collections.Generic;
using System.Text;

namespace Blueprint41.Driver
{
    public class Query
    {
        public Query(object value)
        {
            Value = value;
        }
        internal object Value { get; private set; }

        public string Text => Driver.QUERY.Text(Value);
        public IDictionary<string, object> Parameters => Driver.QUERY.Parameters(Value);
    }
}
