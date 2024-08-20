using System;
using System.Collections.Generic;

namespace Blueprint41.Core
{
    public abstract class RawResultStatistics
    {
        public abstract bool ContainsUpdates { get; }
        public abstract int NodesCreated { get; }
        public abstract int NodesDeleted { get; }
        public abstract int RelationshipsCreated { get; }
        public abstract int RelationshipsDeleted { get; }
        public abstract int PropertiesSet { get; }
        public abstract int LabelsAdded { get; }
        public abstract int LabelsRemoved { get; }
        public abstract int IndexesAdded { get; }
        public abstract int IndexesRemoved { get; }
        public abstract int ConstraintsAdded { get; }
        public abstract int ConstraintsRemoved { get; }
        public abstract int SystemUpdates { get; }
        public abstract bool ContainsSystemUpdates { get; }
    }
}
