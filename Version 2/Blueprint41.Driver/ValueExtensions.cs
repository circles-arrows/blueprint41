using System;
using System.Collections.Generic;
using System.Text;

using driver = Blueprint41.Driver;

namespace System
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
            if (typeof(T) == typeof(driver .Node))
                return (T)(object)new driver .Node(driver.Driver.VALUE_EXTENSIONS.As(driver.Driver.I_NODE, value));
            else if (typeof(T) == typeof(driver .Relationship))
                return (T)(object)new driver.Relationship(driver .Driver.VALUE_EXTENSIONS.As(driver.Driver.I_RELATIONSHIP, value));
            else
                return driver.Driver.VALUE_EXTENSIONS.As<T>(value);
        }

        internal static object As(this object value, driver.Driver.DriverTypeInfo type, object defaultValue)
        {
            if (value != null)
            {
                return value.As(type);
            }

            return defaultValue;
        }
        internal static object As(this object value, driver.Driver.DriverTypeInfo type) => driver.Driver.VALUE_EXTENSIONS.As(type, value);

#nullable enable
    }
}
