using Blueprint41.Core;
using Blueprint41.Neo4j.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.Query
{
    public partial class DateTimeResult
    {
        #region Operators

        #region Equals

        public static QueryCondition operator ==(DateTimeResult left, DateTime right)
        {
            return new QueryCondition(left, Operator.Equals, Parameter.Constant(right));
        }
        public static QueryCondition operator ==(DateTimeResult left, DateTime? right)
        {
            return new QueryCondition(left, Operator.Equals, Parameter.Constant(right));
        }
        public static QueryCondition operator ==(DateTimeResult left, Parameter right)
        {
            return new QueryCondition(left, Operator.Equals, right);
        }
        public static QueryCondition operator ==(DateTimeResult left, DateTimeResult right)
        {
            return new QueryCondition(left, Operator.Equals, right);
        }

        #endregion

        #region Not equals

        public static QueryCondition operator !=(DateTimeResult left, DateTime right)
        {
            return new QueryCondition(left, Operator.NotEquals, Parameter.Constant(right));
        }
        public static QueryCondition operator !=(DateTimeResult left, DateTime? right)
        {
            return new QueryCondition(left, Operator.NotEquals, Parameter.Constant(right));
        }
        public static QueryCondition operator !=(DateTimeResult left, Parameter right)
        {
            return new QueryCondition(left, Operator.NotEquals, right);
        }
        public static QueryCondition operator !=(DateTimeResult left, DateTimeResult right)
        {
            return new QueryCondition(left, Operator.NotEquals, right);
        }

        #endregion

        #region Less than

        public static QueryCondition operator <(DateTimeResult left, DateTime right)
        {
            return new QueryCondition(left, Operator.Less, Parameter.Constant(right));
        }
        public static QueryCondition operator <(DateTimeResult left, DateTime? right)
        {
            return new QueryCondition(left, Operator.Less, Parameter.Constant(right));
        }
        public static QueryCondition operator <(DateTimeResult left, Parameter right)
        {
            return new QueryCondition(left, Operator.Less, right);
        }
        public static QueryCondition operator <(DateTimeResult left, DateTimeResult right)
        {
            return new QueryCondition(left, Operator.Less, right);
        }
        #endregion

        #region Less than or equals

        public static QueryCondition operator <=(DateTimeResult left, DateTime right)
        {
            return new QueryCondition(left, Operator.LessOrEqual, Parameter.Constant(right));
        }
        public static QueryCondition operator <=(DateTimeResult left, DateTime? right)
        {
            return new QueryCondition(left, Operator.LessOrEqual, Parameter.Constant(right));
        }
        public static QueryCondition operator <=(DateTimeResult left, Parameter right)
        {
            return new QueryCondition(left, Operator.LessOrEqual, right);
        }
        public static QueryCondition operator <=(DateTimeResult left, DateTimeResult right)
        {
            return new QueryCondition(left, Operator.LessOrEqual, right);
        }

        #endregion

        #region Greater than

        public static QueryCondition operator >(DateTimeResult left, DateTime right)
        {
            return new QueryCondition(left, Operator.Greater, Parameter.Constant(right));
        }
        public static QueryCondition operator >(DateTimeResult left, DateTime? right)
        {
            return new QueryCondition(left, Operator.Greater, Parameter.Constant(right));
        }
        public static QueryCondition operator >(DateTimeResult left, Parameter right)
        {
            return new QueryCondition(left, Operator.Greater, right);
        }
        public static QueryCondition operator >(DateTimeResult left, DateTimeResult right)
        {
            return new QueryCondition(left, Operator.Greater, right);
        }

        #endregion

        #region Greater than or equals

        public static QueryCondition operator >=(DateTimeResult left, DateTime right)
        {
            return new QueryCondition(left, Operator.GreaterOrEqual, Parameter.Constant(right));
        }
        public static QueryCondition operator >=(DateTimeResult left, DateTime? right)
        {
            return new QueryCondition(left, Operator.GreaterOrEqual, Parameter.Constant(right));
        }
        public static QueryCondition operator >=(DateTimeResult left, Parameter right)
        {
            return new QueryCondition(left, Operator.GreaterOrEqual, right);
        }
        public static QueryCondition operator >=(DateTimeResult left, DateTimeResult right)
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

        public QueryCondition In(IEnumerable<DateTime> enumerable)
        {
            return new QueryCondition(this, Operator.In, Parameter.Constant(enumerable.ToArray(), typeof(DateTime)));
        }

        public DateTimeResult Coalesce(RelationFieldResult value)
        {
            return new DateTimeResult(this, t => t.FnCoalesce, new object[] { value });
        }
        public DateTimeResult Min()
        {
            return new DateTimeResult(this, t => t.FnMin);
        }
        public DateTimeResult Max()
        {
            return new DateTimeResult(this, t => t.FnMax);
        }
        public DateTimeResult ConvertMinToNull()
        {
            return new DateTimeResult(this, t => t.FnConvertMinOrMaxToNull, new object[] { MinDateTime.Value });
        }
        public DateTimeResult ConvertMaxToNull()
        {
            return new DateTimeResult(this, t => t.FnConvertMinOrMaxToNull, new object[] { MaxDateTime.Value });
        }
        public DateTimeResult ConvertMinOrMaxToNull()
        {
            return new DateTimeResult(this, t => t.FnConvertMinAndMaxToNull, new object[] { MinDateTime.Value, MaxDateTime.Value });
        }

        private Lazy<Parameter> MinDateTime = new Lazy<Parameter>(delegate()
        {
            return Parameter.Constant(Conversion<DateTime, long>.Convert(DateTime.MinValue));
        }, true);
        private Lazy<Parameter> MaxDateTime = new Lazy<Parameter>(delegate ()
        {
            return Parameter.Constant(Conversion<DateTime, long>.Convert(DateTime.MaxValue));
        }, true);
    }
}
