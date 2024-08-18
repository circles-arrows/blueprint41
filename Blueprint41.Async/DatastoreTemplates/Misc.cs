using System;
using System.Collections.Generic;
using System.Text;

namespace Blueprint41.Async.DatastoreTemplates
{
    public delegate void EventHandler<TSender, TEventArgs>(TSender sender, TEventArgs args);
}
