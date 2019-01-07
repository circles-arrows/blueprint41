using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion

        private Parameter() { }
        public Parameter(string name, object value, Type type = null)
        {
            Name = name;
            Value = value;
            IsConstant = name == null;
            Type = type ?? value?.GetType();
        }

        public static Parameter New<T>(string name)
        {
            Parameter p = new Parameter();
            p.IsConstant = name == null;
            p.Name = name;
            p.Value = null;
            p.HasValue = false;
            p.Type = typeof(T);
            return p;
        }
        public static Parameter New(string name, Type type)
        {
            Parameter p = new Parameter();
            p.IsConstant = name == null;
            p.Name = name;
            p.Value = null;
            p.HasValue = false;
            p.Type = type;
            return p;
        }
        public static Parameter New(string name, object value, Type type)
        {
            Parameter p = new Parameter();
            p.IsConstant = name == null;
            p.Name = name;
            p.Value = value;
            p.HasValue = true;
            p.Type = type;
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
            Parameter p = new Parameter();
            p.IsConstant = true;
            p.Name = null;
            p.Value = value;
            p.HasValue = true;
            p.Type = typeof(T);
            return p;
        }
        public static Parameter Constant(object value, Type type)
        {
            Parameter p = new Parameter();
            p.IsConstant = true;
            p.Name = null;
            p.Value = value;
            p.HasValue = true;
            p.Type = type;
            return p;
        }

        public string Name { get; private set; }
        public object Value { get; private set; }
        public bool HasValue { get; private set; }

        public Type Type { get; private set; }
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
