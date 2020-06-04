using Blueprint41.Neo4j.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.Query
{
    public partial class BooleanResult : FieldResult
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
        public BooleanResult(Func<QueryTranslator, string?>? function, object[] arguments, Type type) : base(function, arguments, type) { }
        public BooleanResult(AliasResult alias, string fieldName, Entity entity, Property property) : base(alias, fieldName, entity, property) { }
        public BooleanResult(FieldResult field, Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? type = null) : base(field, function, arguments, type) { }

        public BooleanResult Coalesce(bool value)
        {
            return new BooleanResult(this, t => t.FnCoalesce, new object[] { Parameter.Constant(value) });
        }
        public BooleanResult Coalesce(MiscResult value)
        {
            return new BooleanResult(this, t => t.FnCoalesce, new object[] { value });
        }
        public BooleanResult Coalesce(Parameter value)
        {
            return new BooleanResult(this, t => t.FnCoalesce, new object[] { value });
        }

        public override NumericResult ToInteger()
        {
            return new NumericResult(t => t.FnConvertToBoolean, new object[] { this }, typeof(long));
        }

        private static readonly Parameter ZERO = Parameter.Constant<long>(0);
        private static readonly Parameter ONE = Parameter.Constant<long>(1);
    }
}
