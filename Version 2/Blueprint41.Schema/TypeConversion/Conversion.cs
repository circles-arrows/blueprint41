using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Blueprint41.Core
{
    public abstract class Conversion
    {
        #region DateTime Specific

        internal const long MaxDateTimeInMS = 253402300800000;
        internal const long MinDateTimeInMS = -62135596800000;

        public static readonly DateTime MinDateTime = FromTimeInMS(MinDateTimeInMS);
        public static readonly DateTime MaxDateTime = FromTimeInMS(MaxDateTimeInMS);

        private protected static DateTime FromTimeInMS(long value)
        {
            long ticks = (value >= MaxDateTimeInMS) ? (MaxDateTimeInMS * 10000L) - 1 : value * 10000L;
            ticks = (value <= MinDateTimeInMS) ? unchecked((MinDateTimeInMS * 10000L)) : ticks;
            return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).Add(new TimeSpan(ticks)).ToUniversalTime();
        }
        private protected static long ToTimeInMS(DateTime value)
        {
            //if date is not utc convert to utc.
            DateTime utcTime = value.Kind != DateTimeKind.Utc ? value.ToUniversalTime() : value;

            //return value.Ticks;
            DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return (long)utcTime.Subtract(dt).TotalMilliseconds;
        }

        protected static DateTime FixDateTime(DateTime value)
        {
            if (value.IsMin())
                return MinDateTime;

            if (value.IsMax())
                return MaxDateTime;

            return value;
        }

        #endregion

        public abstract Type FromType { get; }
        public abstract Type ToType { get; }

        static internal bool registrationsLoaded = false;
        internal abstract void RegisterConversion();

        static private protected void Register()
        {
            if (!registrationsLoaded)
            {
                lock (typeof(Conversion))
                {
                    if (!registrationsLoaded)
                    {
                        foreach (Assembly assembly in GetAssemblies())
                        {
                            if (assembly is null)
                                continue;

                            foreach (ConversionAttribute attribute in assembly.GetCustomAttributes<ConversionAttribute>())
                            {
                                Conversion converter = (Conversion)Activator.CreateInstance(attribute.Converter)!;
                                converter.RegisterConversion();
                                registeredConverters.Add(converter);
                            }
                        }

                        registrationsLoaded = true;
                    }
                }
            }
        }
        public static IEnumerable<Assembly> GetAssemblies()
        {
            var list = new HashSet<string>();
            var stack = new Stack<Assembly>();

            stack.Push(typeof(Conversion).Assembly);
            Assembly? entry = Assembly.GetEntryAssembly();
            if (!(entry is null))
                stack.Push(entry);

            do
            {
                var asm = stack.Pop();

                yield return asm;

                foreach (var reference in asm.GetReferencedAssemblies())
                {
                    if (reference.FullName.StartsWith("mscorlib") || reference.FullName.StartsWith("netstandard") || reference.FullName.StartsWith("System") || reference.FullName.StartsWith("Microsoft") || reference.FullName.StartsWith("Xml.Schema"))
                        continue;

                    if (!list.Contains(reference.FullName))
                    {
                        try
                        {
                            Assembly a = Assembly.Load(reference);
                            stack.Push(a);
                            list.Add(reference.FullName);
                        }
                        catch (FileNotFoundException)
                        {
                            // This is relatively normal... This usually happens when
                            // a referenced assembly is not in the active code path
                            // and therefore was not included in the deployment.
                        }
                    }
                }
            }
            while (stack.Count > 0);
        }
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
                if (converter is null)
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
