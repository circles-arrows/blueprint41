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

        public bool HasPlan => Driver.I_RESULT_SUMMARY.HasPlan(_instance);
        public bool HasProfile => Driver.I_RESULT_SUMMARY.HasProfile(_instance);
        public QueryPlan Plan => Driver.I_RESULT_SUMMARY.Plan(_instance);
        public QueryProfile Profile => Driver.I_RESULT_SUMMARY.Profile(_instance);

        public TimeSpan ResultAvailableAfter => Driver.I_RESULT_SUMMARY.ResultAvailableAfter(_instance);
        public TimeSpan ResultConsumedAfter => Driver.I_RESULT_SUMMARY.ResultConsumedAfter(_instance);

        public ServerInfo Server => Driver.I_RESULT_SUMMARY.Server(_instance);
        public DatabaseInfo Database => Driver.I_RESULT_SUMMARY.Database(_instance);
    }
}
