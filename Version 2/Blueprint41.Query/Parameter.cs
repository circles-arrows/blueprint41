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
    public class Parameter : IParameter
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

        private Parameter(string name, Type? type = null)
        {
            // Normal parameters
            Name = name;
            Type = type;
            Value = null;
            HasValue = false;

            if (name is null)
                throw new NotSupportedException("A constant has to always have a 'value', consider calling the other constructor.");
        }
        public Parameter(string name, object? value, Type? type = null)
        {
            // Constants or Optional parameters (used in Query) or actual value (used during Execution)
            Name = name;
            Type = type;
            Value = KeyParameter.MaterializeValue(Type, value);
            HasValue = true;
        }

        public static Parameter New<T>(string name)
        {
            if (name is null)
                throw new ArgumentNullException(nameof(name));

            return new Parameter(name, typeof(T));
        }
        public static Parameter New(string name, Type type)
        {
            if (name is null)
                throw new ArgumentNullException(nameof(name));

            return new Parameter(name, type);
        }
        public static Parameter New<T>(string name, T value)
        {
            if (name is null)
                throw new ArgumentNullException(nameof(name));

            return new Parameter(name, value, typeof(T));
        }
        public static Parameter New(string name, object? value, Type type)
        {
            if (name is null)
                throw new ArgumentNullException(nameof(name));

            return new Parameter(name, value, type);
        }

        #region Internally used for code generation?

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
        internal static Parameter Constant(Literal value)
        {
            throw new NotSupportedException();
        }

        #endregion

        public static Parameter Null => Constant(null, null);
        public static Parameter Constant<T>(T value)
        {
            return new Parameter(null!, value, typeof(T));
        }
        public static Parameter Constant(object? value, Type? type)
        {
            return new Parameter(null!, value, type);
        }

        public string Name { get; private set; }
        public object? Value { get; private set; }
        public bool HasValue { get; private set; }

        public Type? Type { get; private set; }
        public bool IsConstant => (Name is null);

        internal void Compile(CompileState state)
        {
            state.Translator.Compile(this, state);
        }
    }
}
