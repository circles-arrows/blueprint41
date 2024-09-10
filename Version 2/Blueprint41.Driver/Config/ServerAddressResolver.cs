using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Blueprint41.Config
{
    public class ServerAddressResolver
    {
        protected ServerAddressResolver() { }
        public ServerAddressResolver(IDictionary<ServerAddress, ISet<ServerAddress>>? mappings, ISet<ServerAddress>? defaultSet = null!)
        {
            _mappings = mappings;
            _default = defaultSet;
        }

        public virtual ISet<ServerAddress>? Resolve(ServerAddress address)
        {
            if (_mappings?.TryGetValue(address, out var mappings) ?? false)
                return mappings;

            return _default;
        }

        private readonly IDictionary<ServerAddress, ISet<ServerAddress>>? _mappings;
        private readonly ISet<ServerAddress>? _default;
    }
}
