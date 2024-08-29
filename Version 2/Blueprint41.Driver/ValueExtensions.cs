using System;
using System.Collections.Generic;
using System.Text;

namespace Blueprint41.Driver
{
    public static class ValueExtensions
    {
#nullable disable
        public static T As<T>(this object value, T defaultValue)
        {
            if (value != null)
            {
                return value.As<T>();
            }

            return defaultValue;
        }
        public static T As<T>(this object value)
        {
            if (typeof(T) == typeof(Node))
                return (T)(object)new Node(Driver.VALUE_EXTENSIONS.As(Driver.I_NODE, value));
            else if (typeof(T) == typeof(Relationship))
                return (T)(object)new Relationship(Driver.VALUE_EXTENSIONS.As(Driver.I_RELATIONSHIP, value));
            else
                return Driver.VALUE_EXTENSIONS.As<T>(value);
        }

        internal static object As(this object value, Driver.DriverTypeInfo type, object defaultValue)
        {
            if (value != null)
            {
                return value.As(type);
            }

            return defaultValue;
        }
        internal static object As(this object value, Driver.DriverTypeInfo type) => Driver.VALUE_EXTENSIONS.As(type, value);

#nullable enable
    }
}
