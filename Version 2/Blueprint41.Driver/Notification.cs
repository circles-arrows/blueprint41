using System;
using System.Collections.Generic;
using System.Text;

namespace Blueprint41.Driver
{
    public class Notification
    {
        internal Notification(object value)
        {
            Value = value;
            InputPosition = new Lazy<object>(() => Driver.I_NOTIFICATION.Position(Value), true);
        }
        internal object Value { get; private set; }
        private readonly Lazy<object> InputPosition;

        public string Code => Driver.I_NOTIFICATION.Code(Value);
        public string Title => Driver.I_NOTIFICATION.Title(Value);
        public string Description => Driver.I_NOTIFICATION.Description(Value);

        public int Offset => Driver.I_INPUT_POSITION.Offset(InputPosition);
        public int Line => Driver.I_INPUT_POSITION.Line(InputPosition);
        public int Column => Driver.I_INPUT_POSITION.Column(InputPosition);
    }
}
