using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

using Blueprint41.Query;
using Blueprint41.Core;

namespace Blueprint41
{
    public class Parameter
    {
        #region Operators

        public static QueryCondition operator ==(Parameter left, string right)
        {
            return new QueryCondition(left, Operator.Equals, Parameter.Constant(right));
        }
        public static QueryCondition operator ==(Parameter left, Parameter right)
        {
            return new QueryCondition(left, Operator.Equals, right);
        }
        public static QueryCondition operator !=(Parameter left, string right)
        {
            return new QueryCondition(left, Operator.NotEquals, Parameter.Constant(right));
        }
        public static QueryCondition operator !=(Parameter left, Parameter right)
        {
            return new QueryCondition(left, Operator.NotEquals, right);
        }

        public static QueryCondition operator >(Parameter left, string right)
        {
            return new QueryCondition(left, Operator.Greater, Parameter.Constant(right));
        }
        public static QueryCondition operator >(Parameter left, Parameter right)
        {
            return new QueryCondition(left, Operator.Greater, right);
        }
        public static QueryCondition operator <(Parameter left, string right)
        {
            return new QueryCondition(left, Operator.Less, Parameter.Constant(right));
        }
        public static QueryCondition operator <(Parameter left, Parameter right)
        {
            return new QueryCondition(left, Operator.Less, right);
        }

        public static QueryCondition operator >=(Parameter left, string right)
        {
            return new QueryCondition(left, Operator.GreaterOrEqual, Parameter.Constant(right));
        }
        public static QueryCondition operator >=(Parameter left, Parameter right)
        {
            return new QueryCondition(left, Operator.GreaterOrEqual, right);
        }
        public static QueryCondition operator <=(Parameter left, string right)
        {
            return new QueryCondition(left, Operator.LessOrEqual, Parameter.Constant(right));
        }
        public static QueryCondition operator <=(Parameter left, Parameter right)
        {
            return new QueryCondition(left, Operator.LessOrEqual, right);
        }

        public override bool Equals(object? obj)
        {
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion

        private Parameter(string name)
        {
            Name = name;
        }
        public Parameter(string name, object? value, Type? type = null)
        {
            Name = name;
            IsConstant = name == null;
            Value = MaterializeValue(Type, value);
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
                            while (search != null && iface == null)
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

                            if (iface != null)
                            {
                                Type typeT = iface.GenericTypeArguments[0];
                                MethodInfo? methodInfo = typeof(Parameter).GetMethod(nameof(FromEnumerator), BindingFlags.NonPublic | BindingFlags.Static)?.MakeGenericMethod(typeT);
                                if (!(methodInfo is null))
                                    retval = (Func<object, object>)Delegate.CreateDelegate(typeof(Func<object, object>), methodInfo);
                            }
                        }

                        // we could "return value;" here, but we're going to make sure we don't need to scan interfaces in the future anymore.
                        if (retval == null)
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
            Parameter p = new Parameter(name);
            p.IsConstant = name == null;
            p.Value = null;
            p.HasValue = false;
            p.Type = typeof(T);
            return p;
        }
        public static Parameter New<T>(string name, T value)
        {
            Parameter p = new Parameter();
            p.IsConstant = name == null;
            p.Name = name;
            p.Type = typeof(T);
            p.Value = MaterializeValue(p.Type, value);
            p.HasValue = true;
            return p;
        }
        public static Parameter New(string name, Type type)
        {
            Parameter p = new Parameter(name);
            p.IsConstant = name == null;
            p.Value = null;
            p.HasValue = false;
            p.Type = type;
            return p;
        }
        public static Parameter New(string name, object? value, Type type)
        {
            Parameter p = new Parameter(name);
            p.IsConstant = name == null;
            p.HasValue = !(value is null);
            p.Type = type;
            p.Value = MaterializeValue(p.Type, value);
            return p;
        }

        [Obsolete("You cannot wrap a parameter into a constant", true)]
        internal static Parameter Constant(Parameter value)
        {
            throw new NotSupportedException();
        }
        [Obsolete("You cannot wrap a result into a constant", true)]
        internal static Parameter Constant(Result value)
        {
            throw new NotSupportedException();
        }
        [Obsolete("You cannot wrap a result into a constant", true)]
        internal static Parameter Constant(AliasResult value)
        {
            throw new NotSupportedException();
        }
        [Obsolete("You cannot wrap a result into a constant", true)]
        internal static Parameter Constant(AsResult value)
        {
            throw new NotSupportedException();
        }
        [Obsolete("You cannot wrap a result into a constant", true)]
        internal static Parameter Constant(BooleanResult value)
        {
            throw new NotSupportedException();
        }
        [Obsolete("You cannot wrap a result into a constant", true)]
        internal static Parameter Constant(NumericResult value)
        {
            throw new NotSupportedException();
        }
        [Obsolete("You cannot wrap a result into a constant", true)]
        internal static Parameter Constant(StringResult value)
        {
            throw new NotSupportedException();
        }
        [Obsolete("You cannot wrap a result into a constant", true)]
        internal static Parameter Constant(DateTimeResult value)
        {
            throw new NotSupportedException();
        }
        [Obsolete("You cannot wrap a result into a constant", true)]
        internal static Parameter Constant(RelationFieldResult value)
        {
            throw new NotSupportedException();
        }
        [Obsolete("You cannot wrap a result into a constant", true)]
        internal static Parameter Constant(MiscResult value)
        {
            throw new NotSupportedException();
        }
        [Obsolete("You cannot wrap a result into a constant", true)]
        internal static Parameter Constant(FloatResult value)
        {
            throw new NotSupportedException();
        }
        [Obsolete("You cannot wrap a result into a constant", true)]
        internal static Parameter Constant(Litheral value)
        {
            throw new NotSupportedException();
        }

        public static Parameter Null => Constant(null, null);
        public static Parameter Constant<T>(T value)
        {
            Parameter p = new Parameter("Constant");
            p.IsConstant = true;
            p.Value = value;
            p.HasValue = true;
            p.Type = typeof(T);
            return p;
        }
        public static Parameter Constant(object? value, Type? type)
        {
            Parameter p = new Parameter("Constant");
            p.IsConstant = true;
            p.Value = value;
            p.HasValue = true;
            p.Type = type;
            return p;
        }

        public string Name { get; private set; }
        public object? Value { get; private set; }
        public bool HasValue { get; private set; }

        public Type? Type { get; private set; }
        public bool IsConstant { get; private set; }

        internal void Compile(CompileState state)
        {
            if (!state.Parameters.Contains(this))
            {
                state.Parameters.Add(this);
                if (IsConstant)
                    state.Values.Add(this);
            }

            if (Name == null)
            {
                Name = $"param{state.paramSeq}";
                state.paramSeq++;
            }
            state.Text.Append("{");
            state.Text.Append(Name);
            state.Text.Append("}");
        }
    }
}
