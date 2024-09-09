using System;
using System.Collections.Generic;
using System.Text;

namespace Blueprint41.Driver
{
    public class Counters
    {
        internal Counters()
        {
            _instance = null!;
        }
        internal Counters(object instance)
        {
            _instance = instance;
        }
        internal object _instance { get; private set; }

        public bool ContainsUpdates => IsVoid(Driver.I_COUNTERS.ContainsUpdates, false);
        public int NodesCreated => IsVoid(Driver.I_COUNTERS.NodesCreated);
        public int NodesDeleted => IsVoid(Driver.I_COUNTERS.NodesDeleted);
        public int RelationshipsCreated => IsVoid(Driver.I_COUNTERS.RelationshipsCreated);
        public int RelationshipsDeleted => IsVoid(Driver.I_COUNTERS.RelationshipsDeleted);
        public int PropertiesSet => IsVoid(Driver.I_COUNTERS.PropertiesSet);
        public int LabelsAdded => IsVoid(Driver.I_COUNTERS.LabelsAdded);
        public int LabelsRemoved => IsVoid(Driver.I_COUNTERS.LabelsRemoved);
        public int IndexesAdded => IsVoid(Driver.I_COUNTERS.IndexesAdded);
        public int IndexesRemoved => IsVoid(Driver.I_COUNTERS.IndexesRemoved);
        public int ConstraintsAdded => IsVoid(Driver.I_COUNTERS.ConstraintsAdded);
        public int ConstraintsRemoved => IsVoid(Driver.I_COUNTERS.ConstraintsRemoved);
        public int SystemUpdates => IsVoid(Driver.I_COUNTERS.SystemUpdates);
        public bool ContainsSystemUpdates => IsVoid(Driver.I_COUNTERS.ContainsSystemUpdates);

        private T IsVoid<T>(Func<object, T> value, T? defaultValue = null)
            where T : struct
        {
            if (_instance is null)
                return defaultValue ?? default;

            return value.Invoke(_instance);
        }
    }
}
