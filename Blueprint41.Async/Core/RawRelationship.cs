using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Blueprint41.Async.Core
{
    public abstract class RawRelationship : IEquatable<RawRelationship>
    {
        public abstract long Id { get; }
        public abstract string Type { get; }
        public abstract long StartNodeId { get; }
        public abstract long EndNodeId { get; }

        public abstract object this[string key] { get; }
        public abstract IReadOnlyDictionary<string, object> Properties { get; }

        public abstract bool Equals(RawRelationship other);
    }
}
