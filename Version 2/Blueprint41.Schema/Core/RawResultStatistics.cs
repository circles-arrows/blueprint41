using System;
using System.Collections.Generic;

namespace Blueprint41.Core
{
    public class RawResultStatistics
    {
        internal RawResultStatistics()
        {
            throw new NotImplementedException();
        }

        public bool ContainsUpdates { get; }
        public int NodesCreated { get; }
        public int NodesDeleted { get; }
        public int RelationshipsCreated { get; }
        public int RelationshipsDeleted { get; }
        public int PropertiesSet { get; }
        public int LabelsAdded { get; }
        public int LabelsRemoved { get; }
        public int IndexesAdded { get; }
        public int IndexesRemoved { get; }
        public int ConstraintsAdded { get; }
        public int ConstraintsRemoved { get; }
        public int SystemUpdates { get; }
        public bool ContainsSystemUpdates { get; }
    }
}
