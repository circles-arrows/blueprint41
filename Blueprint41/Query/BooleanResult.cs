using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.Query
{
    public class BooleanResult : FieldResult
    {
        #region Operator

        public static QueryCondition operator ==(BooleanResult left, bool right)
        {
            return new QueryCondition(left, Operator.Equals, Parameter.Constant(right));
        }
        public static QueryCondition operator ==(BooleanResult left, bool? right)
        {
            return new QueryCondition(left, Operator.Equals, Parameter.Constant(right));
        }
        public static QueryCondition operator ==(BooleanResult left, Parameter right)
        {
            return new QueryCondition(left, Operator.Equals, right);
        }
        public static QueryCondition operator !=(BooleanResult left, bool right)
        {
            return new QueryCondition(left, Operator.NotEquals, Parameter.Constant(right));
        }
        public static QueryCondition operator !=(BooleanResult left, bool? right)
        {
            return new QueryCondition(left, Operator.NotEquals, Parameter.Constant(right));
        }
        public static QueryCondition operator !=(BooleanResult left, Parameter right)
        {
            return new QueryCondition(left, Operator.NotEquals, right);
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

        internal BooleanResult(FieldResult field) : base(field) { }
        public BooleanResult(string function, object[] arguments, Type type) : base(function, arguments, type) { }
        public BooleanResult(AliasResult alias, string fieldName, Entity entity, Property property) : base(alias, fieldName, entity, property) { }
        public BooleanResult(FieldResult field, string function, object[]? arguments = null, Type? type = null) : base(field, function, arguments, type) { }

        public BooleanListResult Collect()
        {
            return new BooleanListResult(this, "collect({base})");
        }
        public BooleanResult Coalesce(bool value)
        {
            return new BooleanResult(this, "coalesce({base}, {0})", new object[] { Parameter.Constant(value) });
        }
        public BooleanResult Coalesce(MiscResult value)
        {
            return new BooleanResult(this, "coalesce({base}, {0})", new object[] { value });
        }
        public BooleanResult Coalesce(Parameter value)
        {
            return new BooleanResult(this, "coalesce({base}, {0})", new object[] { value });
        }

        public override NumericResult ToInteger()
        {
            string caseWhen = @"CASE WHEN {0} IS NULL THEN NULL WHEN {0} THEN 1 ELSE 0 END";
            return new NumericResult(caseWhen, new object[] { this }, typeof(long));
        }

        private static readonly Parameter ZERO = Parameter.Constant<long>(0);
        private static readonly Parameter ONE = Parameter.Constant<long>(1);
    }
}
