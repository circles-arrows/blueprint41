using System;
using System.Collections.Generic;
using System.Text;

namespace Blueprint41.Driver
{
    public class ResultSummary
    {
        internal ResultSummary()
        {
            _instance = null!;
        }
        internal ResultSummary(object instance)
        {
            _instance = instance;
        }
        internal object _instance { get; private set; }

        public Query Query => IsVoid(Driver.I_RESULT_SUMMARY.Query);
        public Counters Counters => Driver.I_RESULT_SUMMARY.Counters(_instance);
        public IReadOnlyList<Notification> Notifications => IsVoid(Driver.I_RESULT_SUMMARY.Notifications, voidNotifications);

        public bool HasPlan => IsVoid(Driver.I_RESULT_SUMMARY.HasPlan);
        public bool HasProfile => IsVoid(Driver.I_RESULT_SUMMARY.HasProfile);
        public QueryPlan Plan => IsVoid(Driver.I_RESULT_SUMMARY.Plan);
        public QueryProfile Profile => IsVoid(Driver.I_RESULT_SUMMARY.Profile);

        public TimeSpan ResultAvailableAfter => IsVoid(Driver.I_RESULT_SUMMARY.ResultAvailableAfter, TimeSpan.Zero);
        public TimeSpan ResultConsumedAfter => IsVoid(Driver.I_RESULT_SUMMARY.ResultConsumedAfter, TimeSpan.Zero);

        public ServerInfo Server => IsVoid(Driver.I_RESULT_SUMMARY.Server);
        public DatabaseInfo Database => IsVoid(Driver.I_RESULT_SUMMARY.Database);

        private T IsVoid<T>(Func<object, T> value)
        {
            if (_instance is null)
                throw new NotSupportedException("Retrieving the Query is not possible when the driver is not initialized.");

            return value.Invoke(_instance);
        }
        private T IsVoid<T>(Func<object, T> value, T defaultValue)
        {
            if (_instance is null)
                return defaultValue;

            return value.Invoke(_instance);
        }

        private static readonly IReadOnlyList<Notification> voidNotifications = new List<Notification>(0);
    }
}
