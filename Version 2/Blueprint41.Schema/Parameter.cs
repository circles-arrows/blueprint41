using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

using Blueprint41.Core;

namespace Blueprint41
{
    public class Parameter
    {
        static internal readonly string CONSTANT_NAME = null!;

        private Parameter(string name, Type? type = null)
        {
            // Normal parameters
            Name = name;
            Type = type;
            IsConstant = false;
            Value = null;
            HasValue = false;

            if (name == CONSTANT_NAME)
                throw new NotSupportedException("A constant has to always have a 'value', consider calling the other constructor.");
        }
        public Parameter(string name, object? value, Type? type = null)
        {
            // Constants or Optional parameters (used in Query) or actual value (used during Execution)
            Name = name;
            Type = type;
            IsConstant = name == CONSTANT_NAME;
            Value = MaterializeValue(Type, value);
            HasValue = true;
        }

        private static object? MaterializeValue(Type? type, object? value)
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
                                MethodInfo? methodInfo = typeof(Parameter).GetMethod(nameof(FromEnumerator), BindingFlags.NonPublic | BindingFlags.Static)?.MakeGenericMethod(typeT);
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

        public static Parameter New<T>(string name)
        {
            return new Parameter(name, typeof(T));
        }
        public static Parameter New(string name, Type type)
        {
            return new Parameter(name, type);
        }
        public static Parameter New<T>(string name, T value)
        {
            return new Parameter(name, value, typeof(T));
        }
        public static Parameter New(string name, object? value, Type type)
        {
            return new Parameter(name, value, type);
        }

        public static Parameter Null => Constant(null, null);
        public static Parameter Constant<T>(T value)
        {
            return new Parameter(CONSTANT_NAME, value, typeof(T));
        }
        public static Parameter Constant(object? value, Type? type)
        {
            return new Parameter(CONSTANT_NAME, value, type);
        }

        public string Name { get; internal set; }
        public object? Value { get; private set; }
        public bool HasValue { get; private set; }

        public Type? Type { get; private set; }
        public bool IsConstant { get; private set; }
    }
}
