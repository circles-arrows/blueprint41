using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.Query
{
    public partial class NumericResult : FieldResult
    {
        public static explicit operator NumericResult(RelationFieldResult from)
        {
            return new NumericResult(from);
        }
        public static explicit operator NumericResult(DateTimeResult from)
        {
            return new NumericResult(from);
        }

        #region Operators

        #region Equals

        public static QueryCondition operator ==(NumericResult left, long right)
        {
            return new QueryCondition(left, Operator.Equals, Parameter.Constant(right));
        }
        public static QueryCondition operator ==(NumericResult left, long? right)
        {
            return new QueryCondition(left, Operator.Equals, Parameter.Constant(right));
        }
        public static QueryCondition operator ==(NumericResult left, Parameter right)
        {
            return new QueryCondition(left, Operator.Equals, right);
        }

        #endregion

        #region Not equals

        public static QueryCondition operator !=(NumericResult left, long right)
        {
            return new QueryCondition(left, Operator.NotEquals, Parameter.Constant(right));
        }
        public static QueryCondition operator !=(NumericResult left, long? right)
        {
            return new QueryCondition(left, Operator.NotEquals, Parameter.Constant(right));
        }
        public static QueryCondition operator !=(NumericResult left, Parameter right)
        {
            return new QueryCondition(left, Operator.NotEquals, right);
        }

        #endregion

        #region Less than

        public static QueryCondition operator <(NumericResult left, long right)
        {
            return new QueryCondition(left, Operator.Less, Parameter.Constant(right));
        }
        public static QueryCondition operator <(NumericResult left, long? right)
        {
            return new QueryCondition(left, Operator.Less, Parameter.Constant(right));
        }
        public static QueryCondition operator <(NumericResult left, Parameter right)
        {
            return new QueryCondition(left, Operator.Less, right);
        }

        #endregion

        #region Less than or equals

        public static QueryCondition operator <=(NumericResult left, long right)
        {
            return new QueryCondition(left, Operator.LessOrEqual, Parameter.Constant(right));
        }
        public static QueryCondition operator <=(NumericResult left, long? right)
        {
            return new QueryCondition(left, Operator.LessOrEqual, Parameter.Constant(right));
        }
        public static QueryCondition operator <=(NumericResult left, Parameter right)
        {
            return new QueryCondition(left, Operator.LessOrEqual, right);
        }

        #endregion

        #region Greater than

        public static QueryCondition operator >(NumericResult left, long right)
        {
            return new QueryCondition(left, Operator.Greater, Parameter.Constant(right));
        }
        public static QueryCondition operator >(NumericResult left, long? right)
        {
            return new QueryCondition(left, Operator.Greater, Parameter.Constant(right));
        }
        public static QueryCondition operator >(NumericResult left, Parameter right)
        {
            return new QueryCondition(left, Operator.Greater, right);
        }

        #endregion

        #region Greater than or equals

        public static QueryCondition operator >=(NumericResult left, long right)
        {
            return new QueryCondition(left, Operator.GreaterOrEqual, Parameter.Constant(right));
        }
        public static QueryCondition operator >=(NumericResult left, long? right)
        {
            return new QueryCondition(left, Operator.GreaterOrEqual, Parameter.Constant(right));
        }
        public static QueryCondition operator >=(NumericResult left, Parameter right)
        {
            return new QueryCondition(left, Operator.GreaterOrEqual, right);
        }

        #endregion

        public override bool Equals(object? obj)
        {
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion

        internal NumericResult(FieldResult field) : base(field) { }
        public NumericResult(string function, object[]? arguments, Type? type) : base(function, arguments, type) { }
        public NumericResult(AliasResult alias, string fieldName, Entity entity, Property property) : base(alias, fieldName, entity, property) { }
        public NumericResult(FieldResult field, string function, object[]? arguments = null, Type? type = null) : base(field, function, arguments, type) { }

        public QueryCondition In(IEnumerable<long> enumerable)
        {
            return new QueryCondition(this, Operator.In, Parameter.Constant(enumerable.ToArray(), typeof(long)));
        }
        public NumericResult Sign()
        {
            return new NumericResult(this, "sign({base})", null, typeof(long));
        }
        public NumericResult Abs()
        {
            return new NumericResult(this, "abs({base})");
        }
        public NumericResult Sum()
        {
            return new NumericResult(this, "sum({base})");
        }
        public NumericResult Min()
        {
            return new NumericResult(this, "min({base})");
        }
        public NumericResult Max()
        {
            return new NumericResult(this, "max({base})");
        }
        public FloatResult Avg()
        {
            return new FloatResult(this, "avg({base})", null, typeof(Double));
        }
        public NumericResult PercentileDisc(decimal percentile)
        {
            if (percentile < 0 || percentile > 1)
                throw new ArgumentOutOfRangeException("percentile", percentile, $"The value must be between 0 and 1");

            return new NumericResult(this, "percentileDisc({base}, {0})", new object[] { new Litheral(percentile) });
        }
        public FloatResult PercentileCont(decimal percentile)
        {
            if (percentile < 0 || percentile > 1)
                throw new ArgumentOutOfRangeException("percentile", percentile, $"The value must be between 0 and 1");

            return new FloatResult(this, "percentileCont({base}, {0})", new object[] { new Litheral(percentile) }, typeof(Double));
        }
        public FloatResult StDev()
        {
            return new FloatResult(this, "stdev({base})", null, typeof(Double));
        }
        public FloatResult StDevP()
        {
            return new FloatResult(this, "stdevp({base})", null, typeof(Double));
        }

        public NumericResult Coalesce(long value)
        {
            return new NumericResult(this, "coalesce({base}, {0})", new object[] { Parameter.Constant(value) });
        }
        public NumericResult Coalesce(NumericResult value)
        {
            return new NumericResult(this, "coalesce({base}, {0})", new object[] { value });
        }
        public NumericResult Coalesce(Parameter value)
        {
            return new NumericResult(this, "coalesce({base}, {0})", new object[] { value });
        }
    }
}
