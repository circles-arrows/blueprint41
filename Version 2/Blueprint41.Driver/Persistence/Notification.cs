using System;
using System.Collections.Generic;
using System.Text;

namespace Blueprint41.Persistence
{
    public class Notification
    {
        internal Notification(object instance)
        {
            _instance = instance;
            InputPosition = new Lazy<object>(() => Driver.I_NOTIFICATION.Position(_instance), true);
        }
        internal object _instance { get; private set; }
        private readonly Lazy<object> InputPosition;

        public string Code => Driver.I_NOTIFICATION.Code(_instance);
        public string Title => Driver.I_NOTIFICATION.Title(_instance);
        public string Description => Driver.I_NOTIFICATION.Description(_instance);

        public int Offset => Driver.I_INPUT_POSITION.Offset(InputPosition);
        public int Line => Driver.I_INPUT_POSITION.Line(InputPosition);
        public int Column => Driver.I_INPUT_POSITION.Column(InputPosition);
    }
}
