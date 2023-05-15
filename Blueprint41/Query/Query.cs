using Blueprint41.Core;
using Blueprint41.DatastoreTemplates;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using query = Blueprint41.Query;

namespace Blueprint41.Query
{
    [DebuggerDisplay("{DebuggerDisplay}")]
    public partial class Query : IBlankQuery, IMatchQuery, IWhereQuery, IWithQuery, IReturnQuery, IModifyQuery, IMergeQuery, ISkipQuery, ILimitQuery, IOrderQuery, IPageQuery, ICallSubquery, ICallSubqueryMatch, ICompiled
    { 
        internal Query(PersistenceProvider persistenceProvider)
        {
            PersistenceProvider = persistenceProvider;
        }
        private PersistenceProvider PersistenceProvider;

        internal Query(Query parent)
        {
            Parent = parent;
            PersistenceProvider = parent.PersistenceProvider;

        }
        private Query? Parent;

        internal bool Last = false;
        internal bool Distinct = true;
        internal bool SetAdd = false;
        internal bool SetFunctionalId = false;
        internal bool Detach = false;
        internal bool Ascending = true;
        internal PartType Type = PartType.None;
        internal Parameter? SearchWords = null;
        internal SearchOperator? SearchOperator = null;
        internal Node[]? Patterns = null;
        internal Assignment[]? Assignments = null;
        internal Result[]? Results = null;
        internal AsResult[]? AsResults = null;
        internal QueryCondition[]? Conditions = null;
        internal FieldResult[]? Fields = null;
        internal AliasResult[]? Aliases = null;
        internal bool UnionWithDuplicates = true;
        internal Parameter SkipValue = Parameter.Constant(0);
        internal Parameter LimitValue = Parameter.Constant(0);
        internal Query? SubQueryPart;

        public CompiledQuery? CompiledQuery { get; private set; }
        Query ICompiled.Query { get { return this; } }

        public IBlankQuery Search(Parameter searchWords, SearchOperator searchOperator, Node searchNodeType, params FieldResult[] searchFields)
        {
            SetType(PartType.Search);

            SearchWords = searchWords;
            SearchOperator = searchOperator;
            Patterns = new[] { searchNodeType };
            Fields = searchFields;

            if(searchNodeType.Entity.FullTextIndexProperties.Count == 0)
                throw new ArgumentException($"The searchNodeType '{searchNodeType.Entity.Name}' is not part of the full text index.");

            if ((object?)searchNodeType.NodeAlias is null)
                throw new ArgumentException($"The searchNodeType does not have an alias. Rewite your query to: {Example()}");

            foreach (FieldResult field in Fields)
            {
                if ((object?)field.Alias != searchNodeType.NodeAlias)
                    throw new ArgumentException($"The searchfield should be retrieved from the searchNodeType it's alias. Rewite your query to: {Example()}");

                if ((object?)field.Alias != searchNodeType.NodeAlias)
                    throw new ArgumentException($"The searchfield '{field.FieldName}' is not supported for free text searching. Add it to the free text index in an upgrade script.");
            }

            return New;

            string Example()
            {
                return $"Search({SearchWordExample()}, {OperatorExample()}, {NodeTypeExample()}, {SearchFieldExample()}, ...)";
            }
            string SearchFieldExample()
            {
                return $"{AliasExample()}.{searchFields.FirstOrDefault().FieldName ?? "FieldName"}";
            }
            string NodeTypeExample()
            {
                return $"node.{searchNodeType.Neo4jLabel}.Alias(out var {AliasExample()})";
            }
            string AliasExample()
            {
                return searchNodeType.Neo4jLabel.ToCamelCase();
            }
            string OperatorExample()
            {
                return $"SearchOperator.{searchOperator.ToString()}";
            }
            string SearchWordExample()
            {
                return $"Parameter.New<string>(\"{searchWords?.Name ?? "SearchWords"}\")";
            }
        }
        public IBlankQuery Search(Parameter searchWords, SearchOperator searchOperator, Node searchNodeType, out FloatResult scoreAlias, params FieldResult[] searchFields)
        {
            AliasResult scoreResult = new AliasResult();
            scoreAlias = new FloatResult(scoreResult, null, null, typeof(double));
            Aliases = new[] { scoreResult };

            return Search(searchWords, searchOperator, searchNodeType, searchFields);
        }

        public IMatchQuery Match(params Node[] patterns)
        {
            SetType(PartType.Match);
            Patterns = patterns;

            return New;
        }
        public IMatchQuery UsingScan(params AliasResult[] aliases)
        {
            SetType(PartType.UsingScan);
            Aliases = aliases;

            return New;
        }
        public IMatchQuery UsingIndex(params FieldResult[] fields)
        {
            SetType(PartType.UsingIndex);
            Fields = fields;

            return New;
        }
        public IOptionalMatchQuery OptionalMatch(params Node[] patterns)
        {
            SetType(PartType.OptionalMatch);
            Patterns = patterns;

            return New;
        }
        public IWithQuery With(params Result[] results)
        {
            return With(true, results);
        }
        public IWithQuery With(bool distinct, params Result[] results)
        {
            SetType(PartType.With);
            Results = results;
            Distinct = distinct;

            return New;
        }
        public IReturnQuery Return(params Result[] results)
        {
            return Return(true, results);
        }
        public IReturnQuery Return(bool distinct, params Result[] results)
        {
            SetType(PartType.Return);
            int index = 0;
            Distinct = distinct;
            AsResults = results.Select(delegate (Result item)
            {
                index++;
                if (item is AsResult)
                    return (AsResult)item;
                else
                    return new AsResult(item, string.Concat("Column", index));
            }).ToArray();
            Last = true;

            return New;
        }

        public IModifyQuery Create(params Node[] nodes)
        {
            SetType(PartType.Create);
            Patterns = nodes;
            Last = true;

            return New;
        }
        public IMergeQuery Merge(params Node[] nodes)
        {
            SetType(PartType.Merge);
            Patterns = nodes;
            Last = true;

            return New;
        }
        public IModifyQuery Set(Assignment[] assignments, bool add = false, bool setFunctionalId = false)
        {
            SetType(PartType.Set);
            Assignments = assignments;
            SetAdd = add;
            SetFunctionalId = setFunctionalId;
            Last = true;

            return New;
        }
        public IModifyQuery Delete(params Result[] delete)
        {
            SetType(PartType.Delete);
            Detach = false;
            Results = delete;
            Last = true;

            return New;
        }
        public IModifyQuery Delete(bool detach, params Result[] delete)
        {
            SetType(PartType.Delete);
            Detach = detach;
            Results = delete;
            Last = true;

            return New;
        }
        public IMergeQuery OnCreateSet(Assignment[] assignments, bool add = false, bool setFunctionalId = false)
        {
            SetType(PartType.OnCreate);
            Assignments = assignments;
            SetAdd = add;
            SetFunctionalId = setFunctionalId;
            Last = true;

            return New;
        }
        public IMergeQuery OnMatchSet(Assignment[] assignments, bool add = false, bool setFunctionalId = false)
        {
            SetType(PartType.OnMatch);
            Assignments = assignments;
            SetAdd = add;
            SetFunctionalId = setFunctionalId;
            Last = true;

            return New;
        }

        public IWhereQuery Where(params QueryCondition[] conditions)
        {
            SetType(PartType.Where);
            Conditions = conditions;

            return New;
        }
        public IWhereQuery Or(params QueryCondition[] conditions)
        {
            SetType(PartType.Or);
            Conditions = conditions;

            return New;
        }
        public IWhereQuery And(params QueryCondition[] conditions)
        {
            if (Parent is null || (Parent.Type != PartType.Where && Parent.Type != PartType.Or))
                throw new InvalidOperationException("You can only And if you did Where clause before.");

            int len = (Parent.Conditions?.Length ?? 0);

            QueryCondition[] newArray = new QueryCondition[conditions.Length + len];
            if(Parent.Conditions is not null)
                Parent.Conditions.CopyTo(newArray, 0);

            conditions.CopyTo(newArray, len);

            Parent.Conditions = newArray;

            return this;
        }

        public IMatchQuery UnionMatch(bool duplicates = true, params Node[] patterns)
        {
            SetType(PartType.UnionMatch);
            Patterns = patterns;
            UnionWithDuplicates = duplicates;
            return New;
        }
        public IBlankQuery UnionSearch(bool duplicates, Parameter searchWords, SearchOperator searchOperator, Node searchNodeType, params FieldResult[] searchFields)
        {
            SetType(PartType.UnionSearch);
            UnionWithDuplicates = duplicates;
            SearchWords = searchWords;
            SearchOperator = searchOperator;
            Patterns = new[] { searchNodeType };
            Fields = searchFields;

            if (searchNodeType.Entity.FullTextIndexProperties.Count == 0)
                throw new ArgumentException($"The searchNodeType '{searchNodeType.Entity.Name}' is not part of the full text index.");

            if ((object?)searchNodeType.NodeAlias is null)
                throw new ArgumentException($"The searchNodeType does not have an alias. Rewite your query to: {Example()}");

            foreach (FieldResult field in Fields)
            {
                if ((object?)field.Alias != searchNodeType.NodeAlias)
                    throw new ArgumentException($"The searchfield should be retrieved from the searchNodeType it's alias. Rewite your query to: {Example()}");

                if ((object?)field.Alias != searchNodeType.NodeAlias)
                    throw new ArgumentException($"The searchfield '{field.FieldName}' is not supported for free text searching. Add it to the free text index in an upgrade script.");
            }

            return New;

            string Example()
            {
                return $"Search({SearchWordExample()}, {OperatorExample()}, {NodeTypeExample()}, {SearchFieldExample()}, ...)";
            }
            string SearchFieldExample()
            {
                return $"{AliasExample()}.{searchFields.FirstOrDefault().FieldName ?? "FieldName"}";
            }
            string NodeTypeExample()
            {
                return $"node.{searchNodeType.Neo4jLabel}.Alias(out var {AliasExample()})";
            }
            string AliasExample()
            {
                return searchNodeType.Neo4jLabel.ToCamelCase();
            }
            string OperatorExample()
            {
                return $"SearchOperator.{searchOperator.ToString()}";
            }
            string SearchWordExample()
            {
                return $"Parameter.New<string>(\"{searchWords?.Name ?? "SearchWords"}\")";
            }
        }
        public IBlankQuery UnionSearch(bool duplicates, Parameter searchWords, SearchOperator searchOperator, Node searchNodeType, out FloatResult scoreAlias, params FieldResult[] searchFields)
        {
            AliasResult scoreResult = new AliasResult();
            scoreAlias = new FloatResult(scoreResult, null, null, typeof(double));
            Aliases = new[] { scoreResult };

            return UnionSearch(duplicates, searchWords, searchOperator, searchNodeType, searchFields);
        }

        public ISkipQuery Skip(Parameter skip)
        {
            SetType(PartType.Skip);
            SkipValue = skip;
            return New;
        }
        public ISkipQuery Skip(int skip)
        {
            return Skip(Parameter.Constant(skip));
        }
        public ILimitQuery Limit(Parameter limit)
        {
            SetType(PartType.Limit);
            LimitValue = limit;
            return New;
        }
        public ILimitQuery Limit(int limit)
        {
            return Limit(Parameter.Constant(limit));
        }

        IWithQuery IWithQuery.Skip(int skip)
        {
            return (Query)Skip(skip);
        }
        IWithQuery IWithQuery.OrderBy(params FieldResult[] fields)
        {
            return (Query)OrderBy(fields);
        }
        IWithQuery IWithQuery.OrderBy(bool ascending, params FieldResult[] fields)
        {
            return (Query)OrderBy(ascending, fields);
        }
        IWithQuery IWithQuery.Limit(int limit)
        {
            return (Query)Limit(limit);
        }
        IWithQuery IWithQuery.Skip(Parameter skip)
        {
            return (Query)Skip(skip);
        }
        IWithQuery IWithQuery.Limit(Parameter limit)
        {
            return (Query)Limit(limit);
        }
        IWithQuery IWithQuery.Page(int skip, int limit)
        {
            return (Query)Skip(skip).Limit(limit);
        }
        IWithQuery IWithQuery.Page(Parameter skip, Parameter limit)
        {
            return (Query)Skip(skip).Limit(limit);
        }
        public ILimitQuery Page(int skip, int limit)
        {
            return Skip(skip).Limit(limit);
        }
        public ILimitQuery Page(Parameter skip, Parameter limit)
        {
            return Skip(skip).Limit(limit);
        }

        public IOrderQuery OrderBy(params FieldResult[] fields)
        {
            SetType(PartType.OrderBy);
            Fields = fields;
            Ascending = true;

            return New;
        }
        public IOrderQuery OrderBy(bool ascending, params FieldResult[] fields)
        {
            SetType(PartType.OrderBy);
            Fields = fields;
            Ascending = ascending;

            return New;
        }

        IWithQuery ICallSubquery.With(params Result[] results)
        {
            return With(false, results);
        }
        public ICallSubqueryMatch CallSubQuery(Func<ICallSubquery, IReturnQuery> pattern)
        {
            SetType(PartType.CallSubquery);
            SubQueryPart = (Query)pattern.Invoke(new Query(PersistenceProvider));

            return New;
        }
        public ICallSubqueryMatch CallSubQuery(ICompiled compiled)
        {
            if (compiled is null)
                throw new ArgumentNullException(nameof(compiled));

            if (compiled.CompiledQuery is not null)
            {
                foreach (var parameter in compiled.CompiledQuery.Parameters.Where(item => item.IsConstant))
                {
                    parameter.Name = null!;
                }
            }

            SetType(PartType.CallSubquery);
            SubQueryPart = compiled.Query;

            return New;
        }

        private Query New { get { return new Query(this); } }


        public ICompiled Compile()
        {
            SetType(PartType.Compiled);

            var state = new CompileState(PersistenceProvider.SupportedTypeMappings, PersistenceProvider.Translator);

            Query[] parts = CompileParts(state);
            CompiledQuery = new CompiledQuery(state, parts.Last(item => item.Last).AsResults ?? new AsResult[0]);

            if (CompiledQuery.Errors.Count > 0)
                throw new QueryException(CompiledQuery);

            return this;
        }
        internal Query[] CompileParts(CompileState state)
        {
            Query[] parts = GetParts();
            ForEach(parts, state.Text, "\r\n", item => item?.Compile(state));

            return parts;
        }

        internal void Compile(CompileState state)
        {
            state.Translator.Compile(this, state);
        }

        public QueryExecutionContext GetExecutionContext()
        {
            if (CompiledQuery is null)
                throw new InvalidOperationException("You need to call the 'Compile' method before you can get an execution context.");

            if(CompiledQuery.QueryText.StartsWith("WITH "))
                throw new InvalidOperationException("Sub query should be called within a main query.");

            return new QueryExecutionContext(CompiledQuery);
        }

        public override string ToString()
        {
            if (CompiledQuery is null)
                return string.Empty;

            Transaction transaction = Transaction.RunningTransaction;
            string cypherQuery = CompiledQuery.QueryText;
            Dictionary<string, object?> parameterValues = new Dictionary<string, object?>();

            foreach (var queryParameter in CompiledQuery.ConstantValues)
            {
                if (queryParameter.Value is null)
                    parameterValues.Add(string.Format("{{{0}}}", queryParameter.Name), null);
                else
                    parameterValues.Add(string.Format("{{{0}}}", queryParameter.Name), transaction.PersistenceProviderFactory.ConvertToStoredType(queryParameter.Value.GetType(), queryParameter.Value));
            }

            foreach (var queryParam in parameterValues)
            {
                string paramValue = queryParam.Value?.GetType() == typeof(string) ? string.Format("'{0}'", queryParam.Value.ToString()) : queryParam.Value?.ToString() ?? "NULL";
                cypherQuery = cypherQuery.Replace(queryParam.Key, paramValue);
            }
            return cypherQuery;
        }

        internal void ForEach<T>(T[]? items, StringBuilder sb, string delimiter, Action<T> action)
            where T : notnull
        {
            if (items is null || items.Length == 0)
                return;

            bool first = true;

            for (int index = 0; index < items.Length; index++)
            {
                if (first)
                    first = false;
                else
                    sb.Append(delimiter);

                action.Invoke(items[index]);
            }
        }
        private Query[] GetParts()
        {
            LinkedList<Query> parts = new LinkedList<Query>();
            Query? query = Parent;
            while (query is not null)
            {
                parts.AddFirst(query);
                query = query.Parent;
            }

            return parts.ToArray();
        }
        private void SetType(PartType type)
        {
            if (Type != PartType.None)
                throw new InvalidOperationException("You cannot change the query.");

            Type = type;
        }

        private string DebuggerDisplay => ToString();
    }

    internal enum PartType
    {
        Compiled,
        Search,
        Match,
        UsingScan,
        UsingIndex,
        None,
        OptionalMatch,
        Or,
        OrderBy,
        Return,
        Create,
        Merge,
        OnCreate,
        OnMatch,
        Set,
        Delete,
        Where,
        Unwind,
        With,
        Skip,
        Limit,
        UnionMatch,
        UnionSearch,
        CallSubquery
    }

    #region Interfaces

    public partial interface IBlankQuery : ISemiBlankQuery
    {
        IBlankQuery Search(Parameter searchWords, SearchOperator searchOperator, Node searchNodeType, params FieldResult[] searchFields);
        IBlankQuery Search(Parameter searchWords, SearchOperator searchOperator, Node searchNodeType, out FloatResult scoreAlias, params FieldResult[] searchFields);
    }
    public partial interface ISemiBlankQuery
    {
        IMatchQuery Match(params Node[] patterns);
        IOptionalMatchQuery OptionalMatch(params Node[] patterns);
        ICallSubqueryMatch CallSubQuery(Func<ICallSubquery, IReturnQuery> query);
        ICallSubqueryMatch CallSubQuery(ICompiled compiled);
    }
    public partial interface IOptionalMatchQuery : ISemiBlankQuery, IModifyQuery
    {
        IWithQuery With(params Result[] results);
        IWithQuery With(bool distinct, params Result[] results);
        IWhereQuery Where(params QueryCondition[] conditions);
    }
    public partial interface IMatchQuery : IOptionalMatchQuery
    {
        IMatchQuery UsingScan(params AliasResult[] aliases);
        IMatchQuery UsingIndex(params FieldResult[] fields);
    }
    public partial interface IWhereQuery : ISemiBlankQuery, IModifyQuery
    {
        IWithQuery With(params Result[] results);
        IWithQuery With(bool distinct, params Result[] results);
        IWhereQuery Or(params QueryCondition[] conditions);
        IWhereQuery And(params QueryCondition[] conditions);
    }
    //public partial interface IUnwindQuery<T>
    //{
    //    IMatchQuery As(string aliasName, out T alias);
    //}
    public partial interface IWithQuery : IMatchQuery
    {
        IWithQuery Skip(Parameter skip);
        IWithQuery Limit(Parameter limit);
        IWithQuery Skip(int skip);
        IWithQuery Limit(int limit);
        IWithQuery OrderBy(params FieldResult[] fields);
        IWithQuery OrderBy(bool ascending, params FieldResult[] fields);
        IWithQuery Page(int skip, int limit);
        IWithQuery Page(Parameter skip, Parameter limit);
    }
    public partial interface IReturnQuery
    {
        IMatchQuery UnionMatch(bool duplicates = true, params Node[] patterns);
        IBlankQuery UnionSearch(bool duplicates, Parameter searchWords, SearchOperator searchOperator, Node searchNodeType, params FieldResult[] searchFields);
        IBlankQuery UnionSearch(bool duplicates, Parameter searchWords, SearchOperator searchOperator, Node searchNodeType, out FloatResult scoreAlias, params FieldResult[] searchFields);
        ISkipQuery Skip(Parameter skip);
        ILimitQuery Limit(Parameter limit);
        ISkipQuery Skip(int skip);
        ILimitQuery Limit(int limit);
        IOrderQuery OrderBy(params FieldResult[] fields);
        IOrderQuery OrderBy(bool ascending, params FieldResult[] fields);
        ILimitQuery Page(int skip, int limit);
        ILimitQuery Page(Parameter skip, Parameter limit);
        ICompiled Compile();
    }
    public partial interface ISkipQuery
    {
        ILimitQuery Limit(Parameter limit);
        ILimitQuery Limit(int limit);
        ICompiled Compile();
    }
    public partial interface ILimitQuery
    {
        ICompiled Compile();
    }
    public partial interface IPageQuery : ILimitQuery
    {

    }

    public interface ICallSubquery
    {
        IWithQuery With(params Result[] results);

        IMatchQuery Match(params Node[] patterns);

        IBlankQuery Search(Parameter searchWords, SearchOperator searchOperator, Node searchNodeType, params FieldResult[] searchFields);

        IBlankQuery Search(Parameter searchWords, SearchOperator searchOperator, Node searchNodeType, out FloatResult scoreAlias, params FieldResult[] searchFields);
    }
    
    public partial interface ICallSubqueryMatch : ISemiBlankQuery, IModifyQuery
    {
        IWithQuery With(params Result[] results);
        IWithQuery With(bool distinct, params Result[] results);
        IMatchQuery UnionMatch(bool duplicates = true, params Node[] patterns);
    }

    public partial interface IOrderQuery
    {
        ISkipQuery Skip(Parameter skip);
        ILimitQuery Limit(Parameter limit);
        ISkipQuery Skip(int skip);
        ILimitQuery Limit(int limit);
        ILimitQuery Page(int skip, int limit);
        ILimitQuery Page(Parameter skip, Parameter limit);

        ICompiled Compile();
    }

    public partial interface IModifyQuery
    {
        IModifyQuery Create(params Node[] nodes);
        IMergeQuery Merge(params Node[] nodes);
        IModifyQuery Set(Assignment[] assignments, bool add = false, bool setFunctionalId = false);
        IModifyQuery Delete(params Result[] delete);
        IModifyQuery Delete(bool detach, params Result[] delete);
        IReturnQuery Return(params Result[] results);
        IReturnQuery Return(bool distinct, params Result[] results);
        ICompiled Compile();
    }
    public partial interface IMergeQuery : IModifyQuery
    {
        IMergeQuery OnCreateSet(Assignment[] assignments, bool add = false, bool setFunctionalId = false);
        IMergeQuery OnMatchSet(Assignment[] assignments, bool add = false, bool setFunctionalId = false);
    }

    public partial interface ICompiled
    {
        QueryExecutionContext GetExecutionContext();
        CompiledQuery? CompiledQuery { get; }
        Query Query { get; }
        string ToString();
    }

    #endregion
}
