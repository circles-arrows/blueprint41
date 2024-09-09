using System;
using System.Collections.Generic;
using System.Text;

namespace Blueprint41.Driver
{
    public class QueryPlan
    {
        public QueryPlan(object instance)
        {
            _instance = instance;
        }
        internal object _instance { get; private set; }
        //
        // Summary:
        //     Gets the operation this plan is performing.
        public string OperatorType => Driver.QUERY_PLAN.OperatorType(_instance);

        //
        // Summary:
        //     Gets the arguments for the Neo4j.Driver.IPlan.OperatorType used. Many Neo4j.Driver.IPlan.OperatorType
        //     have arguments defining their specific behavior. This map contains those arguments.
        public IDictionary<string, object> Arguments => Driver.QUERY_PLAN.Arguments(_instance);

        //
        // Summary:
        //     Gets a list of identifiers used by this plan. Identifiers used by this part of
        //     the plan. These can be both identifiers introduce by you, or automatically generated
        //     identifiers.
        public IList<string> Identifiers => Driver.QUERY_PLAN.Identifiers(_instance);

        //
        // Summary:
        //     Gets zero or more child plans. A plan is a tree, where each child is another
        //     plan. The children are where this part of the plan gets its input records - unless
        //     this is an Neo4j.Driver.IPlan.OperatorType that introduces new records on its
        //     own.
        public IList<QueryPlan> Children => Driver.QUERY_PLAN.Children(_instance);
    }
}
