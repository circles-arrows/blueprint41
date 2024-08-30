using System;
using System.Collections.Generic;
using System.Text;

namespace Blueprint41.Driver
{
    public interface IServerAddressResolver
    {
        ISet<ServerAddress> Resolve(ServerAddress address);
    }
}
