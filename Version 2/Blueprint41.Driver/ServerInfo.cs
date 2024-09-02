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

        //
        // Summary:
        //     Get the address of the server
        public string Address => Driver.SERVER_INFO.Address(_instance);

        //
        // Summary:
        //     Get the protocol version
        public string ProtocolVersion => Driver.SERVER_INFO.ProtocolVersion(_instance);

        //
        // Summary:
        //     Get the entire server agent string
        public string Agent => Driver.SERVER_INFO.Agent(_instance);

        //
        // Summary:
        //     Get the version of Neo4j running at the server.
        //
        // Remarks:
        //     Introduced since Neo4j 3.1. Default to null if not supported by server Deprecated
        //     since Neo4j 4.3. Will be removed in Neo4j 5.0. Please use IServerInfo.Agent,
        //     IServerInfo.ProtocolVersion or call the dbms.components procedure instead.
        public string Version => Driver.SERVER_INFO.Version(_instance);
    }
}
