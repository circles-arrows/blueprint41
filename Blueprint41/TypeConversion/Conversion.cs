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
        private static AtomicDictionary<Type, AtomicDictionary<Type, Conversion?>> cache = new AtomicDictionary<Type, AtomicDictionary<Type, Conversion?>>();
        public static object? Convert(Type from, Type to, object? value)
        {
            if (value is null && from?.BaseType == typeof(ValueType) && (!from.IsGenericType || from.GetGenericTypeDefinition() != typeof(Nullable<>)))
                throw new NullReferenceException($"Cannot convert null to '{from.Name}'");

            if (from == to)
                return value;

            Conversion? converter = GetConverter(from!, to); // BUG: C#8 thinks it might be null due to -> if (from?.BaseType... etc
            if (converter is null)
                return value;

            return converter.Convert(value);
        }

        internal static Conversion? GetConverter(Type fromType, Type toType)
        {
            if (fromType == toType)
                return null;

            if (!registrationsLoaded)
                Conversion<bool, bool?>.Convert(true); // trick it into doing an initialize...

            AtomicDictionary<Type, Conversion?> toCache = cache.TryGetOrAdd(fromType, key => new AtomicDictionary<Type, Core.Conversion?>());

            Conversion? converter = toCache.TryGetOrAdd(toType, key =>
            {
                converter = registeredConverters.FirstOrDefault(item => item.FromType == fromType && item.ToType == toType);
                if (converter == null)
                {
                    Type genericConversionType = typeof(ConversionInstance<,>).MakeGenericType(fromType, toType);
                    converter = (Conversion)Activator.CreateInstance(genericConversionType, true)!;
                    if (!converter.IsValidConversion())
                        converter = null;
                }
                return converter;
            });

            return converter;
        }

        internal abstract bool IsValidConversion();
        public abstract object? Convert(object? value);
    }
}
