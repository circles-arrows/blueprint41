using Blueprint41.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.Query
{
    public class DateTimeResult : FieldResult
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

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion

        internal DateTimeResult(FieldResult field) : base(field) { }
        public DateTimeResult(string function, object[] arguments, Type type) : base(function, arguments, type) { }
        public DateTimeResult(AliasResult alias, string fieldName, Entity entity, Property property) : base(alias, fieldName, entity, property) { }
        public DateTimeResult(FieldResult field, string function, object[] arguments = null, Type type = null) : base(field, function, arguments, type) { }

        public QueryCondition In(IEnumerable<DateTime> enumerable)
        {
            return new QueryCondition(this, Operator.In, Parameter.Constant(enumerable.ToArray(), typeof(DateTime)));
        }

        public DateTimeListResult Collect()
        {
            return new DateTimeListResult(this, "collect({base})");
        }
        public DateTimeResult Coalesce(DateTime value)
        {
            return new DateTimeResult(this, "coalesce({base}, {0})", new object[] { Parameter.Constant(value) });
        }
        public DateTimeResult Coalesce(DateTimeResult value)
        {
            return new DateTimeResult(this, "coalesce({base}, {0})", new object[] { value });
        }
        public DateTimeResult Coalesce(RelationFieldResult value)
        {
            return new DateTimeResult(this, "coalesce({base}, {0})", new object[] { value });
        }
        public DateTimeResult Coalesce(Parameter value)
        {
            return new DateTimeResult(this, "coalesce({base}, {0})", new object[] { value });
        }
        public DateTimeResult Min()
        {
            return new DateTimeResult(this, "min({base})");
        }
        public DateTimeResult Max()
        {
            return new DateTimeResult(this, "max({base})");
        }
        public DateTimeResult ConvertMinToNull()
        {
            return new DateTimeResult(this, string.Format("CASE WHEN {{base}} = {0} THEN NULL ELSE {{base}} END", MinDateTime.Value), new object[0]);
        }
        public DateTimeResult ConvertMaxToNull()
        {
            return new DateTimeResult(this, string.Format("CASE WHEN {{base}} = {0} THEN NULL ELSE {{base}} END", MaxDateTime.Value), new object[0]);
        }
        public DateTimeResult ConvertMinOrMaxToNull()
        {
            return new DateTimeResult(this, string.Format("CASE WHEN {{base}} = {0} OR {{base}} = {1} THEN NULL ELSE {{base}} END", MinDateTime.Value, MaxDateTime.Value), new object[0]);
        }

        private Lazy<long> MinDateTime = new Lazy<long>(delegate()
        {
            return Conversion<DateTime, long>.Convert(DateTime.MinValue);
        }, true);
        private Lazy<long> MaxDateTime = new Lazy<long>(delegate ()
        {
            return Conversion<DateTime, long>.Convert(DateTime.MaxValue);
        }, true);
    }
}
