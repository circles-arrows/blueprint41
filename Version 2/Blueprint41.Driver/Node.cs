using System;
using System.Collections.Generic;
using System.Text;

namespace Blueprint41.Driver
{
    public class Node
    {
        internal Node(object value)
        {
            Value = value;
        }
        internal object Value { get; private set; }
    }
}
