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
        public bool ContainsUpdates => Driver.ICOUNTERS.ContainsUpdates(Value);

        //
        // Summary:
        //     Gets the number of nodes created.
        public int NodesCreated => Driver.ICOUNTERS.NodesCreated(Value);

        //
        // Summary:
        //     Gets the number of nodes deleted.
        public int NodesDeleted => Driver.ICOUNTERS.NodesDeleted(Value);

        //
        // Summary:
        //     Gets the number of relationships created.
        public int RelationshipsCreated => Driver.ICOUNTERS.RelationshipsCreated(Value);

        //
        // Summary:
        //     Gets the number of relationships deleted.
        public int RelationshipsDeleted => Driver.ICOUNTERS.RelationshipsDeleted(Value);

        //
        // Summary:
        //     Gets the number of properties (on both nodes and relationships) set.
        public int PropertiesSet => Driver.ICOUNTERS.PropertiesSet(Value);

        //
        // Summary:
        //     Gets the number of labels added to nodes.
        public int LabelsAdded => Driver.ICOUNTERS.LabelsAdded(Value);

        //
        // Summary:
        //     Gets the number of labels removed from nodes.
        public int LabelsRemoved => Driver.ICOUNTERS.LabelsRemoved(Value);

        //
        // Summary:
        //     Gets the number of indexes added to the schema.
        public int IndexesAdded => Driver.ICOUNTERS.IndexesAdded(Value);

        //
        // Summary:
        //     Gets the number of indexes removed from the schema.
        public int IndexesRemoved => Driver.ICOUNTERS.IndexesRemoved(Value);

        //
        // Summary:
        //     Gets the number of constraints added to the schema.
        public int ConstraintsAdded => Driver.ICOUNTERS.ConstraintsAdded(Value);

        //
        // Summary:
        //     Gets the number of constraints removed from the schema.
        public int ConstraintsRemoved => Driver.ICOUNTERS.ConstraintsRemoved(Value);

        //
        // Summary:
        //     Gets the number of system updates performed by this query.
        public int SystemUpdates => Driver.ICOUNTERS.SystemUpdates(Value);

        //
        // Summary:
        //     If the query updated the system graph in any way, this method will return true.
        public bool ContainsSystemUpdates => Driver.ICOUNTERS.ContainsSystemUpdates(Value);
    }
}
