using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Blueprint41.Async.Core;
using node = Blueprint41.Async.Query.Node;

namespace Blueprint41.Async.Query
{
    public class QueryCondition
    {
        public static QueryCondition operator &(QueryCondition a, QueryCondition b)
        {
            if (a is null)
                return b;
            else if (b is null)
                return a;

            return new QueryCondition(a, Operator.And, b);
        }
        public static QueryCondition operator |(QueryCondition a, QueryCondition b)
        {
            if (a is null)
                return b;
            else if (b is null)
                return a;

            return new QueryCondition(a, Operator.Or, b);
        }

        public QueryCondition(object left, Operator op, object right)
        {
            if (op == Operator.Boolean && left is not BooleanResult)
                throw new InvalidOperationException("The left side of an boolean operator must be of type BooleanResult");

            Left = left;
            Operator = op;
            Right = right;
        }
        public QueryCondition(BooleanResult booleanResult)
        {
            Left = booleanResult;
            Operator = Operator.Boolean;
            Right = null;
        }
        public QueryCondition(string literal)
        {
            Left = new Literal(literal);
            Operator = Operator.Boolean;
            Right = null;
        }

        public QueryCondition(node node, bool not = false)
        {
            Left = string.Empty;
            Operator = (not) ? Operator.NotPattern : Operator.Pattern;
            Right = node;
        }

        internal object? Left;
        internal Operator Operator;
        internal object? Right;

        internal void Compile(CompileState state)
        {
            state.Translator.Compile(this, state);
        }
    }
}
