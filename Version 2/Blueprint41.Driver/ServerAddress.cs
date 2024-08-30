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

        public static ServerAddress From(string host, int port) => null!;
        public static ServerAddress From(Uri uri) => null!;

        public override bool Equals(object obj) => false;
        public bool Equals(ServerAddress other) => false!;

        public override int GetHashCode() => 0!;
        public override string ToString() => null!;
    }
}
