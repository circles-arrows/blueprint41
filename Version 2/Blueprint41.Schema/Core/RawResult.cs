using System;
using System.Collections;
using System.Collections.Generic;

namespace Blueprint41.Core
{
    public class RawResult: IEnumerable<IDictionary<string, object>>
    {
        internal RawResult()
        {
            Keys = voidKeys;
        }
        internal RawResult(object iRecord)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyList<string> Keys { get; private set; }
        public IDictionary<string, object>? Peek()
        {
            return null;
        }
        public void Consume()
        {
        }
        public RawResultStatistics Statistics()
        {
            return voidStatistics;
        }
        public List<RawResultNotification> Notifications()
        {
            return voidNotifications;
        }
        public IEnumerator<IDictionary<string, object>> GetEnumerator()
        {
            return voidResults.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private static readonly List<string> voidKeys = new List<string>(0);
        private static readonly List<IDictionary<string, object>> voidResults = new List<IDictionary<string, object>>(0);
        private static readonly RawResultStatistics voidStatistics = new RawResultStatistics();
        private static readonly List<RawResultNotification> voidNotifications = new List<RawResultNotification>(0);
    }

    public class RawResultNotification
    {
        internal RawResultNotification(object iResultNotification)
        {
            throw new NotImplementedException();
        }

        public string Code { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public int Offset { get; private set; }
        public int Line { get; private set; }
        public int Column { get; private set; }
        public string Severity { get; private set; }
    }
}
