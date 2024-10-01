using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.Core
{
    internal class KeyParameter : IParameter
    {

        internal KeyParameter(object? value, Type? type = null)
        {
            Name = "key";
            Type = type;
            Value = MaterializeValue(Type, value);
            HasValue = true;
        }

        public string Name { get; private set; }

        public object? Value { get; private set; }

        public bool HasValue { get; private set; }

        public Type? Type { get; private set; }

        public bool IsConstant => (Name is null);

        internal static object? MaterializeValue(Type? type, object? value)
        {
            type = type ?? value?.GetType();

            if (value is null || type is null)
                return null;

            if (type.IsGenericType)
            {
                if (value is IEnumerable)
                {
                    return fromEnumeratorCache.TryGetOrAdd(type, key =>
                    {
                        Func<object, object>? retval = null;

                        Type genericeType = type.GetGenericTypeDefinition();
                        if (genericeType != typeof(List<>))
                        {
                            Type? iface = null;
                            Type? search = type;
                            while (search is not null && iface is null)
                            {
                                foreach (Type item in search.GetInterfaces())
                                {
                                    if (item.IsGenericType && item.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                                    {
                                        iface = item;
                                        break;
                                    }
                                }
                                search = search.BaseType;
                            }

                            if (iface is not null)
                            {
                                Type typeT = iface.GenericTypeArguments[0];
                                MethodInfo? methodInfo = typeof(KeyParameter).GetMethod(nameof(FromEnumerator), BindingFlags.NonPublic | BindingFlags.Static)?.MakeGenericMethod(typeT);
                                if (!(methodInfo is null))
                                    retval = (Func<object, object>)Delegate.CreateDelegate(typeof(Func<object, object>), methodInfo);
                            }
                        }

                        // we could "return value;" here, but we're going to make sure we don't need to scan interfaces in the future anymore.
                        if (retval is null)
                            retval = delegate (object v) { return v; };

                        return retval;
                    }).Invoke(value); // Execute conversion logic
                }
                //else if (genericeType == typeof(IDictionary<,>) && genericeType != typeof(Dictionary<,>))
                //{
                //    throw new NotImplementedException(); // We think this is not needed because Neo4j doesn't support this, right?
                //}
            }

            return value;
        }
        private static object FromEnumerator<T>(object value)
        {
            return new List<T>((IEnumerable<T>)value);
        }
        private static AtomicDictionary<Type, Func<object, object>> fromEnumeratorCache = new AtomicDictionary<Type, Func<object, object>>();
    }
}
