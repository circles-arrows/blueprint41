using System;
using System.Collections.Generic;
using System.Text;

namespace Blueprint41.Events
{
    [Flags]
    public enum EventOptions
    {
        SupressEvents = 0,
        EntityEvents = 1,
        GraphEvents = 2,
        AllEvents = 3,
    }
}
