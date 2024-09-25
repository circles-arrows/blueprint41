using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.Query
{
    public partial class NumericResult
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
        public static QueryCondition operator ==(NumericResult left, decimal right)
        {
            return new QueryCondition(left, Operator.Equals, Parameter.Constant(right));
        }
        public static QueryCondition operator ==(NumericResult left, decimal? right)
        {
            return new QueryCondition(left, Operator.Equals, Parameter.Constant(right));
        }
        public static QueryCondition operator ==(NumericResult left, Parameter right)
        {
            return new QueryCondition(left, Operator.Equals, right);
        }
        public static QueryCondition operator ==(NumericResult left, NumericResult right)
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
        public static QueryCondition operator !=(NumericResult left, decimal right)
        {
            return new QueryCondition(left, Operator.NotEquals, Parameter.Constant(right));
        }
        public static QueryCondition operator !=(NumericResult left, decimal? right)
        {
            return new QueryCondition(left, Operator.NotEquals, Parameter.Constant(right));
        }
        public static QueryCondition operator !=(NumericResult left, Parameter right)
        {
            return new QueryCondition(left, Operator.NotEquals, right);
        }
        public static QueryCondition operator !=(NumericResult left, NumericResult right)
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
        public static QueryCondition operator <(NumericResult left, decimal right)
        {
            return new QueryCondition(left, Operator.Less, Parameter.Constant(right));
        }
        public static QueryCondition operator <(NumericResult left, decimal? right)
        {
            return new QueryCondition(left, Operator.Less, Parameter.Constant(right));
        }
        public static QueryCondition operator <(NumericResult left, Parameter right)
        {
            return new QueryCondition(left, Operator.Less, right);
        }
        public static QueryCondition operator <(NumericResult left, NumericResult right)
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
        public static QueryCondition operator <=(NumericResult left, decimal right)
        {
            return new QueryCondition(left, Operator.LessOrEqual, Parameter.Constant(right));
        }
        public static QueryCondition operator <=(NumericResult left, decimal? right)
        {
            return new QueryCondition(left, Operator.LessOrEqual, Parameter.Constant(right));
        }
        public static QueryCondition operator <=(NumericResult left, Parameter right)
        {
            return new QueryCondition(left, Operator.LessOrEqual, right);
        }
        public static QueryCondition operator <=(NumericResult left, NumericResult right)
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
        public static QueryCondition operator >(NumericResult left, decimal right)
        {
            return new QueryCondition(left, Operator.Greater, Parameter.Constant(right));
        }
        public static QueryCondition operator >(NumericResult left, decimal? right)
        {
            return new QueryCondition(left, Operator.Greater, Parameter.Constant(right));
        }
        public static QueryCondition operator >(NumericResult left, Parameter right)
        {
            return new QueryCondition(left, Operator.Greater, right);
        }
        public static QueryCondition operator >(NumericResult left, NumericResult right)
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
        public static QueryCondition operator >=(NumericResult left, decimal right)
        {
            return new QueryCondition(left, Operator.GreaterOrEqual, Parameter.Constant(right));
        }
        public static QueryCondition operator >=(NumericResult left, decimal? right)
        {
            return new QueryCondition(left, Operator.GreaterOrEqual, Parameter.Constant(right));
        }
        public static QueryCondition operator >=(NumericResult left, Parameter right)
        {
            return new QueryCondition(left, Operator.GreaterOrEqual, right);
        }
        public static QueryCondition operator >=(NumericResult left, NumericResult right)
        {
            return new QueryCondition(left, Operator.GreaterOrEqual, right);
        }

        #endregion

        #region Arithmetic

        public static NumericResult operator +(NumericResult left, long right)
        {
            return new NumericResult(left, t => t.FnAdd, new[] { Parameter.Constant(right) });
        }
        public static NumericResult operator -(NumericResult left, long right)
        {
            return new NumericResult(left, t => t.FnSubtract, new[] { Parameter.Constant(right) });
        }
        public static NumericResult operator *(NumericResult left, long right)
        {
            return new NumericResult(left, t => t.FnMultiply, new[] { Parameter.Constant(right) });
        }
        public static NumericResult operator /(NumericResult left, long right)
        {
            return new NumericResult(left, t => t.FnDivide, new[] { Parameter.Constant(right) });
        }
        public static NumericResult operator %(NumericResult left, long right)
        {
            return new NumericResult(left, t => t.FnModulo, new[] { Parameter.Constant(right) });
        }
        public static NumericResult operator ^(NumericResult left, long right)
        {
            return new NumericResult(left, t => t.FnPower, new[] { Parameter.Constant(right) });
        }

        public static NumericResult operator +(NumericResult left, decimal right)
        {
            return new NumericResult(left, t => t.FnAdd, new[] { Parameter.Constant(right) });
        }
        public static NumericResult operator -(NumericResult left, decimal right)
        {
            return new NumericResult(left, t => t.FnSubtract, new[] { Parameter.Constant(right) });
        }

        public static NumericResult operator +(NumericResult left, NumericResult right)
        {
            return new NumericResult(left, t => t.FnAdd, new[] { right });
        }
        public static NumericResult operator -(NumericResult left, NumericResult right)
        {
            return new NumericResult(left, t => t.FnSubtract, new[] { right });
        }
        public static NumericResult operator *(NumericResult left, NumericResult right)
        {
            return new NumericResult(left, t => t.FnMultiply, new[] { right });
        }
        public static NumericResult operator /(NumericResult left, NumericResult right)
        {
            return new NumericResult(left, t => t.FnDivide, new[] { right });
        }
        public static NumericResult operator %(NumericResult left, NumericResult right)
        {
            return new NumericResult(left, t => t.FnModulo, new[] { right });
        }
        public static NumericResult operator ^(NumericResult left, NumericResult right)
        {
            return new NumericResult(left, t => t.FnPower, new[] { right });
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

        public QueryCondition In(IEnumerable<long> enumerable)
        {
            return new QueryCondition(this, Operator.In, Parameter.Constant(enumerable.ToArray(), typeof(long)));
        }
        public NumericResult Sign()
        {
            return new NumericResult(this, t => t.FnSign, null, typeof(long));
        }
        public NumericResult Abs()
        {
            return new NumericResult(this, t => t.FnAbs);
        }
        public NumericResult Sum()
        {
            return new NumericResult(this, t => t.FnSum);
        }
        public NumericResult Min()
        {
            return new NumericResult(this, t => t.FnMin);
        }
        public NumericResult Max()
        {
            return new NumericResult(this, t => t.FnMax);
        }
        public FloatResult Avg()
        {
            return new FloatResult(this, t => t.FnAvg, null, typeof(Double));
        }
        public NumericResult PercentileDisc(decimal percentile)
        {
            if (percentile < 0 || percentile > 1)
                throw new ArgumentOutOfRangeException("percentile", percentile, $"The value must be between 0 and 1");

            return new NumericResult(this, t => t.FnPercentileDisc, new object[] { Parameter.Constant((float)percentile) });
        }
        public FloatResult PercentileCont(decimal percentile)
        {
            if (percentile < 0 || percentile > 1)
                throw new ArgumentOutOfRangeException("percentile", percentile, $"The value must be between 0 and 1");

            return new FloatResult(this, t => t.FnPercentileCont, new object[] { Parameter.Constant((float)percentile) }, typeof(Double));
        }
        public FloatResult StDev()
        {
            return new FloatResult(this, t => t.FnStDev, null, typeof(Double));
        }
        public FloatResult StDevP()
        {
            return new FloatResult(this, t => t.FnStDevP, null, typeof(Double));
        }
    }
}
