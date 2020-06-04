using System;
using System.Collections.Generic;
using System.Text;

using Blueprint41.Core;

namespace Blueprint41.Neo4j.Persistence.Void
{
    internal class Neo4jRawResultStatistics : RawResultStatistics
    {
        internal Neo4jRawResultStatistics()
        {
        }

        public override bool ContainsUpdates => false;
        public override int NodesCreated => 0;
        public override int NodesDeleted => 0;
        public override int RelationshipsCreated => 0;
        public override int RelationshipsDeleted => 0;
        public override int PropertiesSet => 0;
        public override int LabelsAdded => 0;
        public override int LabelsRemoved => 0;
        public override int IndexesAdded => 0;
        public override int IndexesRemoved => 0;
        public override int ConstraintsAdded => 0;
        public override int ConstraintsRemoved => 0;
        public override int SystemUpdates => 0;
        public override bool ContainsSystemUpdates => false;
    }
}
