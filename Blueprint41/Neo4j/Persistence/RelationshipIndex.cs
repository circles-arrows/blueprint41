using System;
using System.Collections.Generic;
using System.Text;

namespace Blueprint41.Neo4j.Persistence
{
    public class RelationshipIndex
    {
        public RelationshipIndex(object sourceEntityKey, object targetEntityKey, DateTime? startDate, DateTime? endDate)
        {
            SourceEntityKey = sourceEntityKey;
            TargetEntityKey = targetEntityKey;
            StartDate = startDate;
            EndDate = endDate;
        }
        public object SourceEntityKey { get; set; }
        public object TargetEntityKey { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
