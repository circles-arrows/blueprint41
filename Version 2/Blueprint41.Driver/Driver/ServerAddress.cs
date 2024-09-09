using System;
using System.Collections.Generic;
using System.Text;

namespace Blueprint41.Driver
{
    public sealed class ServerAddress : IEquatable<ServerAddress>
    {
        internal ServerAddress(object instance)
        {
            _instance = instance;
        }
        internal object _instance { get; private set; }

        public static ServerAddress From(string host, int port) => Driver.SERVER_ADDRESS.From(host, port);
        public static ServerAddress From(Uri uri) => Driver.SERVER_ADDRESS.From(uri);

        public bool Equals(ServerAddress? other) => Driver.SERVER_ADDRESS.EqualsServerAddress(_instance, other);
        public override bool Equals(object? obj) => Driver.SERVER_ADDRESS.Equals(_instance, obj);
        public override int GetHashCode() => Driver.SERVER_ADDRESS.GetHashCode(_instance);

    }
}
