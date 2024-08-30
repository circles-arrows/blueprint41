using System;
using System.Collections.Generic;
using System.Text;

namespace Blueprint41.Driver
{
    public class Counters
    {
        internal Counters(object instance)
        {
            _instance = instance;
        }
        internal object _instance { get; private set; }

        public bool ContainsUpdates => Driver.I_COUNTERS.ContainsUpdates(_instance);
        public int NodesCreated => Driver.I_COUNTERS.NodesCreated(_instance);
        public int NodesDeleted => Driver.I_COUNTERS.NodesDeleted(_instance);
        public int RelationshipsCreated => Driver.I_COUNTERS.RelationshipsCreated(_instance);
        public int RelationshipsDeleted => Driver.I_COUNTERS.RelationshipsDeleted(_instance);
        public int PropertiesSet => Driver.I_COUNTERS.PropertiesSet(_instance);
        public int LabelsAdded => Driver.I_COUNTERS.LabelsAdded(_instance);
        public int LabelsRemoved => Driver.I_COUNTERS.LabelsRemoved(_instance);
        public int IndexesAdded => Driver.I_COUNTERS.IndexesAdded(_instance);
        public int IndexesRemoved => Driver.I_COUNTERS.IndexesRemoved(_instance);
        public int ConstraintsAdded => Driver.I_COUNTERS.ConstraintsAdded(_instance);
        public int ConstraintsRemoved => Driver.I_COUNTERS.ConstraintsRemoved(_instance);
        public int SystemUpdates => Driver.I_COUNTERS.SystemUpdates(_instance);
        public bool ContainsSystemUpdates => Driver.I_COUNTERS.ContainsSystemUpdates(_instance);
    }
}
