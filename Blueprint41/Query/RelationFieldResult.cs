using Blueprint41.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.Query
{
    public class RelationFieldResult : FieldResult
    {
        public static explicit operator DateTimeResult(RelationFieldResult from)
        {
            return new DateTimeResult(from);
        }
        public static explicit operator RelationFieldResult(DateTimeResult from)
        {
            return new RelationFieldResult(from);
        }

        public static QueryCondition operator ==(RelationFieldResult left, DateTime right)
        {
            return new QueryCondition(left, Operator.Equals, Parameter.Constant(right));
        }
        public static QueryCondition operator ==(RelationFieldResult left, DateTime? right)
        {
            return new QueryCondition(left, Operator.Equals, Parameter.Constant(right));
        }
        public static QueryCondition operator ==(RelationFieldResult left, Parameter right)
        {
            return new QueryCondition(left, Operator.Equals, right);
        }
        public static QueryCondition operator !=(RelationFieldResult left, DateTime right)
        {
            return new QueryCondition(left, Operator.NotEquals, Parameter.Constant(right));
        }
        public static QueryCondition operator !=(RelationFieldResult left, DateTime? right)
        {
            return new QueryCondition(left, Operator.NotEquals, Parameter.Constant(right));
        }
        public static QueryCondition operator !=(RelationFieldResult left, Parameter right)
        {
            return new QueryCondition(left, Operator.NotEquals, right);
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        internal RelationFieldResult(FieldResult field) : base(field) { }
        //public RelationFieldResult(string function, object[] arguments, Type type) : base(function, arguments, type) { }
        public RelationFieldResult(AliasResult alias, string fieldName) : base(alias, fieldName, null, null, typeof(DateTime)) { }
        public RelationFieldResult(FieldResult field, string function, object[] arguments = null, Type type = null) : base(field, function, arguments, type) { }

        protected internal override void Compile(CompileState state)
        {
            Alias.Compile(state);
            state.Text.Append(".");
            state.Text.Append(FieldName);
        }

        public QueryCondition In(IEnumerable<DateTime> enumerable)
        {
            return new QueryCondition(this, Operator.In, Parameter.Constant(enumerable.ToArray(), typeof(DateTime)));
        }

        public DateTimeResult Coalesce(DateTime value)
        {
            return new DateTimeResult(this, "coalesce({base}, {0})", new object[] { Parameter.Constant(Conversion<DateTime, long>.Convert(value)) });
        }

        public DateTimeResult Coalesce(RelationFieldResult value)
        {
            return new DateTimeResult(this, "coalesce({base}, {0})", new object[] { value });
        }
        public DateTimeResult Coalesce(DateTimeResult value)
        {
            return new DateTimeResult(this, "coalesce({base}, {0})", new object[] { value });
        }
        public DateTimeResult Coalesce(Parameter value)
        {
            return new DateTimeResult(this, "coalesce({base}, {0})", new object[] { value });
        }

        public override Type GetResultType()
        {
            return typeof(DateTime);
        }
    }
}
