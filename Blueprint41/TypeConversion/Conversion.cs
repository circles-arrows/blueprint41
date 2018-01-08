using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.Core
{
    public abstract class Conversion
    {
        public abstract Type FromType { get; }
        public abstract Type ToType { get; }

        static internal bool registrationsLoaded = false;
        internal abstract void RegisterConversion();

        protected static List<Conversion> registeredConverters = new List<Conversion>();
        private static Dictionary<Type, Dictionary<Type, Conversion>> cache = new Dictionary<Type, Dictionary<Type, Conversion>>();
        public static object Convert(Type from, Type to, object value)
        {
            if (value == null && from?.BaseType == typeof(ValueType) && (!from.IsGenericType || from.GetGenericTypeDefinition() != typeof(Nullable<>)))
                throw new NullReferenceException($"Cannot convert null to '{from.Name}'");

            if (from == to)
                return value;

            Conversion converter = GetConverter(from, to);
            return converter.Convert(value);
        }

        internal static Conversion GetConverter(Type fromType, Type toType)
        {
            if (fromType == toType)
                return null;

            if (!registrationsLoaded)
                Conversion<bool, bool?>.Convert(true); // trick it into doing an initialize...

            Dictionary<Type, Conversion> toCache;
            if (!cache.TryGetValue(fromType, out toCache))
            {
                lock (cache)
                {
                    if (!cache.TryGetValue(fromType, out toCache))
                    {
                        toCache = new Dictionary<Type, Core.Conversion>();
                        cache.Add(fromType, toCache);
                    }
                }
            }

            Conversion converter;
            if (!toCache.TryGetValue(toType, out converter))
            {
                lock (toCache)
                {
                    if (!toCache.TryGetValue(toType, out converter))
                    {
                        converter = registeredConverters.FirstOrDefault(item=>item.FromType == fromType && item.ToType == toType);
                        if (converter == null)
                        {
                            Type genericConversionType = typeof(ConversionInstance<,>).MakeGenericType(fromType, toType);
                            converter = (Conversion)Activator.CreateInstance(genericConversionType, true);
                            if (!converter.IsValidConversion())
                                converter = null;
                        }
                        toCache.Add(toType, converter);
                    }
                }
            }

            return converter;
        }

        internal abstract bool IsValidConversion();
        public abstract object Convert(object value);
    }
}
