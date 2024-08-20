using System;
using System.Collections.Generic;

namespace Blueprint41.Core
{
    public abstract class RawNode : IEquatable<RawNode>
    {
        public abstract long Id { get; }

        public abstract IReadOnlyList<string> Labels { get; }
        public abstract object? this[string key] { get; }
        public abstract IReadOnlyDictionary<string, object?> Properties { get; }

        public abstract bool Equals(RawNode other);
    }
 }
