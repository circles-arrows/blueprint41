using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.Query
{
    public static class Cypher
    {
        public static IBlankQuery Search(Parameter searchWords, SearchOperator searchOperator, Node searchNodeType, params FieldResult[] searchFields)
        {
            return Query.Search(searchWords, searchOperator, searchNodeType, searchFields);
        }
        public static IBlankQuery Search(Parameter searchWords, SearchOperator searchOperator, Node searchNodeType, out FloatResult scoreAlias, params FieldResult[] searchFields)
        {
            return Query.Search(searchWords, searchOperator, searchNodeType, out scoreAlias, searchFields);
        }
        public static IMatchQuery Match(params Node[] patterns)
        {
            return Query.Match(patterns);
        }
        public static IOptionalMatchQuery OptionalMatch(params Node[] patterns)
        {
            return Query.OptionalMatch(patterns);
        }
        public static ICallSubqueryMatch CallSubQuery(Func<ICallSubquery, IReturnQuery> query)
        {
            return Query.CallSubQuery(query);
        }
        public static ICallSubqueryMatch CallSubQuery(ICompiled compiled)
        {
            return Query.CallSubQuery(compiled);
        }
        public static ICallSubquery SubQuery
        {
            get
            {
                return new Query(Transaction.RunningTransaction.PersistenceProvider);
            }
        }

        public static IBlankQuery Query
        {
            get
            {
                return new Query(Transaction.RunningTransaction.PersistenceProvider);
            }
        }

    }
}
