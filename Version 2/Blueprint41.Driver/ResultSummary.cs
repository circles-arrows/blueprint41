using System;
using System.Collections.Generic;
using System.Text;

namespace Blueprint41.Driver
{
    public class ResultSummary
    {
        internal ResultSummary(object instance)
        {
            _instance = instance;
        }
        internal object _instance { get; private set; }

        public Query Query => Driver.I_RESULT_SUMMARY.Query(_instance);
        public Counters Counters => Driver.I_RESULT_SUMMARY.Counters(_instance);
        public IReadOnlyList<Notification> Notifications => Driver.I_RESULT_SUMMARY.Notifications(_instance);

        ////
        //// Summary:
        ////     Gets if the result contained a query plan or not, i.e. is the summary of a Cypher
        ////     PROFILE or EXPLAIN query.
        public bool HasPlan => Driver.I_RESULT_SUMMARY.HasPlan(_instance);
        ////
        //// Summary:
        ////     Gets if the result contained profiling information or not, i.e. is the summary
        ////     of a Cypher PROFILE query.
        public bool HasProfile => Driver.I_RESULT_SUMMARY.HasProfile(_instance);

        ////
        //// Summary:
        ////     Gets query plan for the executed query if available, otherwise null.
        ////
        //// Remarks:
        ////     This describes how the database will execute your query.
        public QueryPlan Plan => Driver.I_RESULT_SUMMARY.Plan(_instance);

        ////
        //// Summary:
        ////     Gets profiled query plan for the executed query if available, otherwise null.
        ////
        ////
        //// Remarks:
        ////     This describes how the database did execute your query. If the query you executed
        ////     (Neo4j.Driver.IResultSummary.HasProfile was profiled), the query plan will contain
        ////     detailed information about what each step of the plan did. That more in-depth
        ////     version of the query plan becomes available here.
        //IProfiledPlan Profile { get; }

        ////
        //// Summary:
        ////     The time it took the server to make the result available for consumption. Default
        ////     to -00:00:00.001 if the server version does not support this field in summary.
        ////
        ////
        //// Remarks:
        ////     Field introduced in Neo4j 3.1.
        //TimeSpan ResultAvailableAfter { get; }

        ////
        //// Summary:
        ////     The time it took the server to consume the result. Default to -00:00:00.001 if
        ////     the server version does not support this field in summary.
        ////
        //// Remarks:
        ////     Field introduced in Neo4j 3.1.
        //TimeSpan ResultConsumedAfter { get; }

        ////
        //// Summary:
        ////     Get some basic information of the server where the query is carried out
        //IServerInfo Server { get; }

        ////
        //// Summary:
        ////     Get the database information that this summary is generated from.
        //IDatabaseInfo Database { get; }
    }
}
