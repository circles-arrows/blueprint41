using System;
using System.Collections.Generic;
using System.Text;

namespace Blueprint41.Persistence
{
    public class DriverResultSummary
    {
        internal DriverResultSummary(object value)
        {
            Value = value;
        }
        internal object Value { get; private set; }
    }
}
