using System;
using System.Collections.Generic;
using System.Text;

namespace Blueprint41.Persistence
{
    public class QueryProfile : QueryPlan
    {
        public QueryProfile(object instance) : base(instance)
        {
        }

        //
        // Summary:
        //     Gets the number of times this part of the plan touched the underlying data stores
        public long DbHits => Driver.QUERY_PROFILE.DbHits(_instance);

        //
        // Summary:
        //     Gets the number of records this part of the plan produced
        public long Records => Driver.QUERY_PROFILE.Records(_instance);

        //
        // Summary:
        //     Gets number of page cache hits caused by executing the associated execution step.
        public long PageCacheHits => Driver.QUERY_PROFILE.PageCacheHits(_instance);

        //
        // Summary:
        //     Gets number of page cache misses caused by executing the associated execution
        //     step
        public long PageCacheMisses => Driver.QUERY_PROFILE.PageCacheMisses(_instance);

        //
        // Summary:
        //     Gets the ratio of page cache hits to total number of lookups or 0 if no data
        //     is available
        public double PageCacheHitRatio => Driver.QUERY_PROFILE.PageCacheHitRatio(_instance);

        //
        // Summary:
        //     Gets amount of time spent in the associated execution step.
        public long Time => Driver.QUERY_PROFILE.Time(_instance);

        //
        // Summary:
        //     Gets if the number page cache hits and misses and the ratio was recorded.
        public bool HasPageCacheStats => Driver.QUERY_PROFILE.HasPageCacheStats(_instance);

        //
        // Summary:
        //     Gets zero or more child profiled plans. A profiled plan is a tree, where each
        //     child is another profiled plan. The children are where this part of the plan
        //     gets its input records - unless this is an Neo4j.Driver.IPlan.OperatorType that
        //     introduces new records on its own.
        public new IList<QueryProfile> Children => Driver.QUERY_PROFILE.Children(_instance);
    }
}
