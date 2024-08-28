using System;
using System.Collections.Generic;
using System.Text;

namespace Blueprint41.Persistence
{
    public class DriverQuery
    {
        public DriverQuery(object value)
        {
            Value = value;
        }
        internal object Value { get; private set; }

        public string Text => Driver.QUERY.Text(Value);
        public IReadOnlyDictionary<string, object?> Parameters => Driver.QUERY.Parameters(Value);
    }
}
