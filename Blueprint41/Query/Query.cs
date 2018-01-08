using Blueprint41.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using query = Blueprint41.Query;

namespace Blueprint41.Query
{
    public class Query : IBlankQuery, IMatchQuery, IWhereQuery, IWithQuery, IReturnQuery, ISkipQuery, ILimitQuery, IOrderQuery, IPageQuery, ICompiled
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
        private Query Parent;

        private bool Distinct = true;
        private bool Ascending = true;
        private PartType Type = PartType.None;
        private Node[] Patterns = null;
        private Result[] WithResults = null;
        private AsResult[] Results = null;
        private QueryCondition[] Conditions = null;
        private FieldResult[] Fields = null;
        private AliasResult[] Aliases = null;
        Parameter SkipValue = Parameter.Constant(0);
        Parameter LimitValue = Parameter.Constant(0);

        public CompiledQuery CompiledQuery { get; private set; }

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
            Results = results.Select(delegate(Result item)
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
            return Skip(skip) as Query;
        }
        IWithQuery IWithQuery.OrderBy(params FieldResult[] fields)
        {
            return OrderBy(fields) as Query;
        }
        IWithQuery IWithQuery.OrderBy(bool ascending, params FieldResult[] fields)
        {
            return OrderBy(ascending, fields) as Query;
        }
        IWithQuery IWithQuery.Limit(int limit)
        {
            return Limit(limit) as Query;
        }
        IWithQuery IWithQuery.Skip(Parameter skip)
        {
            return Skip(skip) as Query;
        }
        IWithQuery IWithQuery.Limit(Parameter limit)
        {
            return Limit(limit) as Query;
        }
        IWithQuery IWithQuery.Page(int skip, int limit)
        {
            return Skip(skip).Limit(limit) as Query;
        }
        IWithQuery IWithQuery.Page(Parameter skip, Parameter limit)
        {
            return Skip(skip).Limit(limit) as Query;
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

            var state = new CompileState(PersistenceProvider.SupportedTypeMappings);
            Query[] parts = GetParts();
            ForEach(parts, state.Text, "\r\n", item => item?.Compile(state));
            CompiledQuery = new CompiledQuery(state, parts.Last(item => item.Results != null).Results);

            if (CompiledQuery.Errors.Count > 0)
                throw new QueryException(CompiledQuery);

            return this;
        }
        private void Compile(CompileState state)
        {
            switch (Type)
            {
                case PartType.Match:
                    state.Text.Append("MATCH ");
                    ForEach(Patterns, state.Text, ", ", item => item?.Compile(state));
                    break;
                case PartType.OptionalMatch:
                    state.Text.Append("OPTIONAL MATCH ");
                    ForEach(Patterns, state.Text, ", ", item => item?.Compile(state));
                    break;
                case PartType.UsingScan:
                    state.Text.Append("USING SCAN ");
                    ForEach(Aliases, state.Text, "\r\nUSING SCAN ", item =>
                    {
                        item?.Compile(state);
                        state.Text.Append(":" + item.Node.Neo4jLabel);
                    });
                    break;
                case PartType.UsingIndex:
                    state.Text.Append("USING INDEX ");
                    ForEach(Fields, state.Text, "\r\nUSING INDEX ", item =>
                    {
                        state.Text.Append(string.Format("{0}:{1}({2})", item.Alias.AliasName, item.Alias.Node.Neo4jLabel, item.FieldName));
                    });
                    break;
                case PartType.OrderBy:
                    state.Text.Append("ORDER BY ");
                    ForEach(Fields, state.Text, ", ", delegate(FieldResult item)
                    {
                        item?.Compile(state);
                        if (!Ascending)
                            state.Text.Append(" DESC");
                    });
                    break;
                case PartType.Return:
                    if (Distinct)
                        state.Text.Append("RETURN DISTINCT ");
                    else
                        state.Text.Append("RETURN ");

                    ForEach(Results, state.Text, ", ", item => item?.Compile(state));
                    break;
                case PartType.Where:
                    state.Text.Append("WHERE ");
                    ForEach(Conditions, state.Text, " AND ", item => item?.Compile(state));
                    break;
                case PartType.Or:
                    state.Text.Append("OR ");
                    ForEach(Conditions, state.Text, " AND ", item => item?.Compile(state));
                    break;
                case PartType.With:
                    state.Text.Append("WITH ");
                    ForEach(WithResults, state.Text, ", ", item => item?.Compile(state));
                    break;
                case PartType.Skip:
                    state.Text.Append("SKIP ");
                    SkipValue.Compile(state);
                    break;
                case PartType.Limit:
                    state.Text.Append("LIMIT ");
                    LimitValue.Compile(state);
                    break;
                case PartType.None:
                case PartType.Compiled:
                    // Ignore
                    break;
                default:
                    throw new NotImplementedException($"Compilation for the {Type.ToString()} clause is not supported yet.");
            }
        }

        public QueryExecutionContext GetExecutionContext()
        {
            return new QueryExecutionContext(CompiledQuery);
        }

        private Query[] GetParts()
        {
            LinkedList<Query> parts = new LinkedList<Query>();
            Query query = Parent;
            while (query != null)
            {
                parts.AddFirst(query);
                query = query.Parent;
            }

            return parts.ToArray();
        }
        private void ForEach<T>(T[] items, StringBuilder sb, string delimiter, Action<T> action)
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

        private void SetType(PartType type)
        {
            if (Type != PartType.None)
                throw new InvalidOperationException("You cannot change the query.");

            Type = type;
        }
        private enum PartType
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
            With,
            Skip,
            Limit
        }
    }

    #region Interfaces

    public interface IBlankQuery
    {
        IMatchQuery Match(params Node[] patterns);
        IOptionalMatchQuery OptionalMatch(params Node[] patterns);
    }
    public interface IOptionalMatchQuery : IBlankQuery
    {
        IWithQuery With(params Result[] results);
        IReturnQuery Return(params Result[] results);
        IWhereQuery Where(params QueryCondition[] conditions);
    }
    public interface IMatchQuery : IOptionalMatchQuery
    {
        IMatchQuery UsingScan(params AliasResult[] aliases);
        IMatchQuery UsingIndex(params FieldResult[] fields);
    }
    public interface IWhereQuery : IBlankQuery
    {
        IWithQuery With(params Result[] results);
        IReturnQuery Return(params Result[] results);
        IWhereQuery Or(params QueryCondition[] conditions);
    }
    public interface IWithQuery : IMatchQuery
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

    public interface IReturnQuery
    {
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

    public interface ISkipQuery
    {
        ILimitQuery Limit(Parameter limit);
        ILimitQuery Limit(int limit);
        ICompiled Compile();
    }

    public interface ILimitQuery
    {
        ICompiled Compile();
    }

    public interface IPageQuery : ILimitQuery
    {

    }

    public interface IOrderQuery
    {
        ISkipQuery Skip(Parameter skip);
        ILimitQuery Limit(Parameter limit);
        ISkipQuery Skip(int skip);
        ILimitQuery Limit(int limit);
        ILimitQuery Page(int skip, int limit);
        ILimitQuery Page(Parameter skip, Parameter limit);

        ICompiled Compile();
    }

    public interface ICompiled
    {
        QueryExecutionContext GetExecutionContext();
        CompiledQuery CompiledQuery { get; }
    }

    #endregion
}
