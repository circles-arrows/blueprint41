using System;
using System.Collections.Generic;
using System.Text;

namespace Blueprint41.Driver
{
    public class Counters
    {
        internal Counters(object value)
        {
            Value = value;
        }
        internal object Value { get; private set; }

        //
        // Summary:
        //     Gets whether there were any updates at all, eg. any of the counters are greater
        //     than 0.
        //
        // Value:
        //     Returns true if the query made any updates, false otherwise.
        public bool ContainsUpdates => Driver.I_COUNTERS.ContainsUpdates(Value);

        //
        // Summary:
        //     Gets the number of nodes created.
        public int NodesCreated => Driver.I_COUNTERS.NodesCreated(Value);

        //
        // Summary:
        //     Gets the number of nodes deleted.
        public int NodesDeleted => Driver.I_COUNTERS.NodesDeleted(Value);

        //
        // Summary:
        //     Gets the number of relationships created.
        public int RelationshipsCreated => Driver.I_COUNTERS.RelationshipsCreated(Value);

        //
        // Summary:
        //     Gets the number of relationships deleted.
        public int RelationshipsDeleted => Driver.I_COUNTERS.RelationshipsDeleted(Value);

        //
        // Summary:
        //     Gets the number of properties (on both nodes and relationships) set.
        public int PropertiesSet => Driver.I_COUNTERS.PropertiesSet(Value);

        //
        // Summary:
        //     Gets the number of labels added to nodes.
        public int LabelsAdded => Driver.I_COUNTERS.LabelsAdded(Value);

        //
        // Summary:
        //     Gets the number of labels removed from nodes.
        public int LabelsRemoved => Driver.I_COUNTERS.LabelsRemoved(Value);

        //
        // Summary:
        //     Gets the number of indexes added to the schema.
        public int IndexesAdded => Driver.I_COUNTERS.IndexesAdded(Value);

        //
        // Summary:
        //     Gets the number of indexes removed from the schema.
        public int IndexesRemoved => Driver.I_COUNTERS.IndexesRemoved(Value);

        //
        // Summary:
        //     Gets the number of constraints added to the schema.
        public int ConstraintsAdded => Driver.I_COUNTERS.ConstraintsAdded(Value);

        //
        // Summary:
        //     Gets the number of constraints removed from the schema.
        public int ConstraintsRemoved => Driver.I_COUNTERS.ConstraintsRemoved(Value);

        //
        // Summary:
        //     Gets the number of system updates performed by this query.
        public int SystemUpdates => Driver.I_COUNTERS.SystemUpdates(Value);

        //
        // Summary:
        //     If the query updated the system graph in any way, this method will return true.
        public bool ContainsSystemUpdates => Driver.I_COUNTERS.ContainsSystemUpdates(Value);
    }
}
