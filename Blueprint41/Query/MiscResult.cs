using Blueprint41.Neo4j.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.Query
{
    public partial class MiscResult
    {
        public static QueryCondition operator ==(MiscResult left, object right)
        {
            if (right is Parameter || right is FieldResult)
                return new QueryCondition(left, Operator.Equals, right);

            return new QueryCondition(left, Operator.Equals, Parameter.Constant(right));
        }
        public static QueryCondition operator !=(MiscResult left, object right)
        {
            if (right is Parameter || right is FieldResult)
                return new QueryCondition(left, Operator.NotEquals, right);

            return new QueryCondition(left, Operator.NotEquals, Parameter.Constant(right));
        }
    }
}
