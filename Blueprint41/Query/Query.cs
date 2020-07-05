using Blueprint41.Core;
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
    public partial class Query : IBlankQuery, IMatchQuery, IWhereQuery, IWithQuery, IReturnQuery, ISkipQuery, ILimitQuery, IOrderQuery, IPageQuery, ICompiled, IUnionQuery
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

        internal bool Distinct = true;
        internal bool Ascending = true;
        internal PartType Type = PartType.None;
        internal Node[]? Patterns = null;
        internal Result[]? WithResults = null;
        internal AsResult[]? Results = null;
        internal QueryCondition[]? Conditions = null;
        internal FieldResult[]? Fields = null;
        internal AliasResult[]? Aliases = null;
        internal bool UnionWithDuplicates = true;
        internal Parameter SkipValue = Parameter.Constant(0);
        internal Parameter LimitValue = Parameter.Constant(0);

        public CompiledQuery? CompiledQuery { get; private set; }

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
            SetType(PartType.With);
            WithResults = results;

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
            Distinct = true;
            Results = results.Select(delegate (Result item)
            {
                index++;
                if (item is AsResult)
                    return (AsResult)item;
                else
                    return new AsResult(item, string.Concat("Column", index));
            }).ToArray();

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
        public IUnionQuery UnionMatch(bool duplicates = true, params Node[] patterns)
        {
            SetType(PartType.UnionMatch);
            Patterns = patterns;
            UnionWithDuplicates = duplicates;
            return New;
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


        private Query New { get { return new Query(this); } }


        public ICompiled Compile()
        {
            SetType(PartType.Compiled);

            var state = new CompileState(PersistenceProvider.SupportedTypeMappings, PersistenceProvider.Translator);
            Query[] parts = GetParts();
            ForEach(parts, state.Text, "\r\n", item => item?.Compile(state));
            CompiledQuery = new CompiledQuery(state, parts.Last(item => item.Results != null).Results ?? new AsResult[0]);

            if (CompiledQuery.Errors.Count > 0)
                throw new QueryException(CompiledQuery);

            return this;
        }
        private void Compile(CompileState state)
        {
            state.Translator.Compile(this, state);
        }

        public QueryExecutionContext GetExecutionContext()
        {
            return new QueryExecutionContext(CompiledQuery);
        }

        public override string ToString()
        {
            if (CompiledQuery == null)
                return string.Empty;

            Transaction transaction = Transaction.RunningTransaction;
            string cypherQuery = CompiledQuery.QueryText;
            Dictionary<string, object?> parameterValues = new Dictionary<string, object?>();

            foreach (var queryParameter in CompiledQuery.ConstantValues)
            {
                if (queryParameter.Value == null)
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
            if (items == null || items.Length == 0)
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
            while (query != null)
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

        private string DebuggerDisplay { get => ToString(); }
    }

    internal enum PartType
    {
        Compiled,
        Match,
        UsingScan,
        UsingIndex,
        None,
        OptionalMatch,
        Or,
        OrderBy,
        Return,
        Where,
        Unwind,
        With,
        Skip,
        Limit,
        UnionMatch
    }

    #region Interfaces

    public partial interface IBlankQuery
    {
        IMatchQuery Match(params Node[] patterns);
        IOptionalMatchQuery OptionalMatch(params Node[] patterns);
    }
    public partial interface IOptionalMatchQuery : IBlankQuery
    {
        IWithQuery With(params Result[] results);
        IReturnQuery Return(params Result[] results);
        IWhereQuery Where(params QueryCondition[] conditions);
    }
    public partial interface IMatchQuery : IOptionalMatchQuery
    {
        IMatchQuery UsingScan(params AliasResult[] aliases);
        IMatchQuery UsingIndex(params FieldResult[] fields);
    }
    public partial interface IWhereQuery : IBlankQuery
    {
        IWithQuery With(params Result[] results);
        IReturnQuery Return(params Result[] results);
        IWhereQuery Or(params QueryCondition[] conditions);
    }
    public partial interface IUnwindQuery<T>
    {
        IMatchQuery As(string aliasName, out T alias);
    }
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
        IUnionQuery UnionMatch(bool duplicates = true, params Node[] patterns);
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
    public partial interface IUnionQuery : IMatchQuery
    {
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

    public partial interface ICompiled
    {
        QueryExecutionContext GetExecutionContext();
        CompiledQuery? CompiledQuery { get; }
        string ToString();
    }

    #endregion
}
