using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.Query
{
    public class FloatResult : FieldResult
    {
        #region Operators

        #region Equals

        public static QueryCondition operator ==(FloatResult left, double right)
        {
            return new QueryCondition(left, Operator.Equals, Parameter.Constant(right));
        }
        public static QueryCondition operator ==(FloatResult left, double? right)
        {
            return new QueryCondition(left, Operator.Equals, Parameter.Constant(right));
        }
        public static QueryCondition operator ==(FloatResult left, Parameter right)
        {
            return new QueryCondition(left, Operator.Equals, right);
        }

        #endregion

        #region Not equals

        public static QueryCondition operator !=(FloatResult left, double right)
        {
            return new QueryCondition(left, Operator.NotEquals, Parameter.Constant(right));
        }
        public static QueryCondition operator !=(FloatResult left, double? right)
        {
            return new QueryCondition(left, Operator.NotEquals, Parameter.Constant(right));
        }
        public static QueryCondition operator !=(FloatResult left, Parameter right)
        {
            return new QueryCondition(left, Operator.NotEquals, right);
        }

        #endregion

        #region Less than

        public static QueryCondition operator <(FloatResult left, double right)
        {
            return new QueryCondition(left, Operator.Less, Parameter.Constant(right));
        }
        public static QueryCondition operator <(FloatResult left, double? right)
        {
            return new QueryCondition(left, Operator.Less, Parameter.Constant(right));
        }
        public static QueryCondition operator <(FloatResult left, Parameter right)
        {
            return new QueryCondition(left, Operator.Less, right);
        }

        #endregion

        #region Less than or equals

        public static QueryCondition operator <=(FloatResult left, double right)
        {
            return new QueryCondition(left, Operator.LessOrEqual, Parameter.Constant(right));
        }
        public static QueryCondition operator <=(FloatResult left, double? right)
        {
            return new QueryCondition(left, Operator.LessOrEqual, Parameter.Constant(right));
        }
        public static QueryCondition operator <=(FloatResult left, Parameter right)
        {
            return new QueryCondition(left, Operator.LessOrEqual, right);
        }

        #endregion

        #region Greater than

        public static QueryCondition operator >(FloatResult left, double right)
        {
            return new QueryCondition(left, Operator.Greater, Parameter.Constant(right));
        }
        public static QueryCondition operator >(FloatResult left, double? right)
        {
            return new QueryCondition(left, Operator.Greater, Parameter.Constant(right));
        }
        public static QueryCondition operator >(FloatResult left, Parameter right)
        {
            return new QueryCondition(left, Operator.Greater, right);
        }

        #endregion

        #region Greater than or equals

        public static QueryCondition operator >=(FloatResult left, double right)
        {
            return new QueryCondition(left, Operator.GreaterOrEqual, Parameter.Constant(right));
        }
        public static QueryCondition operator >=(FloatResult left, double? right)
        {
            return new QueryCondition(left, Operator.GreaterOrEqual, Parameter.Constant(right));
        }
        public static QueryCondition operator >=(FloatResult left, Parameter right)
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

        internal FloatResult(FieldResult field) : base(field) { }
        public FloatResult(AliasResult alias, string fieldName, Entity entity, Property property) : base(alias, fieldName, entity, property) { }
        public FloatResult(FieldResult field, string function, object[]? arguments = null, Type? type = null) : base(field, function, arguments, type) { }
        public FloatResult(string function, object[]? arguments = null, Type? type = null) : base(function, arguments, type) { }

        public QueryCondition In(IEnumerable<double> enumerable)
        {
            return new QueryCondition(this, Operator.In, Parameter.Constant(enumerable.ToArray(), typeof(double)));
        }

        public FloatListResult Collect()
        {
            return new FloatListResult(this, "collect({base})");
        }
        public NumericResult Sign()
        {
            return new NumericResult(this, "sign({base})", null, typeof(long));
        }
        public FloatResult Abs()
        {
            return new FloatResult(this, "abs({base})");
        }
        public FloatResult Sum()
        {
            return new FloatResult(this, "sum({base})");
        }
        public FloatResult Min()
        {
            return new FloatResult(this, "min({base})");
        }
        public FloatResult Max()
        {
            return new FloatResult(this, "max({base})");
        }
        public FloatResult Avg()
        {
            return new FloatResult(this, "avg({base})", null, typeof(Double));
        }
        public FloatResult PercentileDisc(decimal percentile)
        {
            if (percentile < 0 || percentile > 1)
                throw new ArgumentOutOfRangeException("percentile", percentile, $"The value must be between 0 and 1");

            return new FloatResult(this, $"percentileDisc({{base}}, {percentile.ToString()})");
        }
        public FloatResult PercentileCont(decimal percentile)
        {
            if (percentile < 0 || percentile > 1)
                throw new ArgumentOutOfRangeException("percentile", percentile, $"The value must be between 0 and 1");

            return new FloatResult(this, $"percentileCont({{base}}, {percentile.ToString()})", null, typeof(Double));
        }
        public FloatResult StDev()
        {
            return new FloatResult(this, "stdev({base})", null, typeof(Double));
        }
        public FloatResult StDevP()
        {
            return new FloatResult(this, "stdevp({base})", null, typeof(Double));
        }
        public NumericResult Round()
        {
            return new NumericResult(this, "round({base})",null, typeof(long));
        }
        public FloatResult Sqrt()
        {
            return new FloatResult(this, "sqrt({base})");
        }
        public FloatResult Sin()
        {
            return new FloatResult(this, "sin({base})");
        }
        public FloatResult Cos()
        {
            return new FloatResult(this, "cos({base})");
        }
        public FloatResult Tan()
        {
            return new FloatResult(this, "tan({base})");
        }
        public FloatResult Cot()
        {
            return new FloatResult(this, "cot({base})");
        }
        public FloatResult Asin()
        {
            return new FloatResult(this, "asin({base})");
        }
        public FloatResult Acos()
        {
            return new FloatResult(this, "acos({base})");
        }
        public FloatResult Atan()
        {
            return new FloatResult(this, "atan({base})");
        }
        public FloatResult Atan2()
        {
            return new FloatResult(this, "atan2({base})");
        }
        public FloatResult Haversin()
        {
            return new FloatResult(this, "haversin({base})");
        }
        public FloatResult Degrees()
        {
            return new FloatResult(this, "degrees({base})");
        }
        public FloatResult Radians()
        {
            return new FloatResult(this, "radians({base})");
        }
        public FloatResult Log10()
        {
            return new FloatResult(this, "log10({base})");
        }
        public FloatResult Log()
        {
            return new FloatResult(this, "log({base})");
        }
        public FloatResult Exp()
        {
            return new FloatResult(this, "exp({base})");
        }

        public FloatResult Coalesce(double value)
        {
            return new FloatResult(this, "coalesce({base}, {0})", new object[] { Parameter.Constant(value) });
        }
        public FloatResult Coalesce(MiscResult value)
        {
            return new FloatResult(this, "coalesce({base}, {0})", new object[] { value });
        }
        public FloatResult Coalesce(Parameter value)
        {
            return new FloatResult(this, "coalesce({base}, {0})", new object[] { value });
        }
    }
}
