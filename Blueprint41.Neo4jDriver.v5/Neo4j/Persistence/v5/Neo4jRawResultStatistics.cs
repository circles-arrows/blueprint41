using System;
using System.Collections.Generic;
using System.Text;

using Neo4j.Driver;
using Blueprint41.Core;
using Blueprint41.Neo4j.Persistence;
using System.Linq;

namespace Blueprint41.Neo4j.Persistence.Driver.v5
{
    internal class Neo4jRawResultStatistics : RawResultStatistics
    {
        internal Neo4jRawResultStatistics(ICounters statistics)
        {
            Statistics = statistics;
        }
        private readonly ICounters Statistics;

        public override bool ContainsUpdates => Statistics.ContainsUpdates;
        public override int NodesCreated => Statistics.NodesCreated;
        public override int NodesDeleted => Statistics.NodesDeleted;
        public override int RelationshipsCreated => Statistics.RelationshipsCreated;
        public override int RelationshipsDeleted => Statistics.RelationshipsDeleted;
        public override int PropertiesSet => Statistics.PropertiesSet;
        public override int LabelsAdded => Statistics.LabelsAdded;
        public override int LabelsRemoved => Statistics.LabelsRemoved;
        public override int IndexesAdded => Statistics.IndexesAdded;
        public override int IndexesRemoved => Statistics.IndexesRemoved;
        public override int ConstraintsAdded => Statistics.ConstraintsAdded;
        public override int ConstraintsRemoved => Statistics.ConstraintsRemoved;
        public override int SystemUpdates => Statistics.SystemUpdates;
        public override bool ContainsSystemUpdates => Statistics.ContainsSystemUpdates;
    }
}
