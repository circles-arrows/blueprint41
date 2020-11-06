using System;
using System.Collections.Generic;
using System.Text;

namespace Blueprint41.Neo4j.Persistence
{
    public class RelationshipIndex
    {
        public RelationshipIndex(OGM sourceEntity, OGM targetEntity, DateTime? startDate, DateTime? endDate)
        {
            SourceEntityInstance = sourceEntity;
            TargetEntityInstance = targetEntity;
            StartDate = startDate;
            EndDate = endDate;
        }
        public OGM SourceEntityInstance { get; set; }
        public OGM TargetEntityInstance { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
