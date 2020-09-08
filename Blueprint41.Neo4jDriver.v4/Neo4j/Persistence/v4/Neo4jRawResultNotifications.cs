using System;
using System.Collections.Generic;
using System.Text;

using Neo4j.Driver;
using Blueprint41.Core;
using Blueprint41.Neo4j.Persistence;
using System.Linq;

namespace Blueprint41.Neo4j.Persistence.Driver.v4
{
    internal class Neo4jRawResultNotifications : RawResultNotification
    {
        internal Neo4jRawResultNotifications(INotification notification)
        {
            Notification = notification;
        }
        private INotification Notification;

        public override string Code => Notification.Code;
        public override string Title => Notification.Title;
        public override string Description => Notification.Description;
        public override int Offset => Notification.Position.Offset;
        public override int Line => Notification.Position.Line;
        public override int Column => Notification.Position.Column;
        public override string Severity => Notification.Severity;
    }
}
