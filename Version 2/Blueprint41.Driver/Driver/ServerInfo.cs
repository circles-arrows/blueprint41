using System;
using System.Collections.Generic;
using System.Text;

namespace Blueprint41.Driver
{
    public class ServerInfo
    {
        public ServerInfo(object instance)
        {
            _instance = instance;
        }
        internal object _instance { get; private set; }

        public string Address => Driver.SERVER_INFO.Address(_instance);
        public string ProtocolVersion => Driver.SERVER_INFO.ProtocolVersion(_instance);
        public string Agent => Driver.SERVER_INFO.Agent(_instance);
    }
}
