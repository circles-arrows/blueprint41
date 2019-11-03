using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.Query
{
    public class AliasResult : Result
    {
        private object[] emptyArguments = new object[0];
        protected internal AliasResult()
        {
        }
        protected AliasResult(AliasResult parent, string function, object[]? arguments = null, Type? type = null)
        {
            Alias = parent;
            Node = parent.Node;
            FunctionText = function;
            FunctionArgs = arguments ?? emptyArguments;
            OverridenReturnType = type;
        }
        public AliasResult? Alias { get; private set; }
        private string? FunctionText { get; set; }
        private object[]? FunctionArgs { get; set; }
        private Type? OverridenReturnType { get; set; }


        public static QueryCondition operator ==(AliasResult a, AliasResult b)
        {
            return new QueryCondition(a, Operator.Equals, b);
        }
        public static QueryCondition operator ==(AliasResult a, Parameter b)
        {
            return new QueryCondition(a, Operator.Equals, b);
        }
        public static QueryCondition operator !=(AliasResult a, AliasResult b)
        {
            return new QueryCondition(a, Operator.NotEquals, b);
        }
        public static QueryCondition operator !=(AliasResult a, Parameter b)
        {
            return new QueryCondition(a, Operator.NotEquals, b);
        }

        public override bool Equals(object? obj)
        {
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public string? AliasName { get; protected internal set; }
        public Node? Node { get; protected set; }

        protected internal override void Compile(CompileState state)
        {
            if (FunctionText == null)
            {
                state.Text.Append(AliasName);
            }
            else
            {
                string[] compiledArgs = FunctionArgs.Select(arg => state.Preview(GetCompile(arg), state)).ToArray();
                string compiledText = string.Format(FunctionText.Replace("{base}", "{{base}}"), compiledArgs);

                if (Alias is null)
                {
                    state.Text.Append(compiledText);
                }
                else
                {
                    string[] split = compiledText.Split(new string[] { "{base}" }, StringSplitOptions.None);
                    if (split.Length == 0)
                        throw new NotSupportedException("Functions have to include compilation of the base they are derived from.");

                    string baseText = state.Preview(Alias.Compile, state);
                    state.Text.Append(string.Join(baseText, split));

                    //state.Text.Append(split[0]);
                    //Field.Compile(state);
                    //state.Text.Append(split[1]);
                }
            }
        }

        private Action<CompileState> GetCompile(object arg)
        {
            if (arg == null)
            {
                return delegate (CompileState state)
                {
                    state.Text.Append("NULL");
                };
            }
            else if (arg is Litheral)
            {
                Litheral param = (Litheral)arg;
                return param.Compile;
            }
            else if (arg is Parameter)
            {
                Parameter param = (Parameter)arg;
                return param.Compile;
            }
            else if (arg.GetType().IsSubclassOfOrSelf(typeof(FieldResult)))
            {
                FieldResult field = (FieldResult)arg;
                return field.Compile;
            }
            else if (arg is QueryCondition)
            {
                QueryCondition param = (QueryCondition)arg;
                return param.Compile;
            }
            else if (arg is AliasResult)
            {
                AliasResult param = (AliasResult)arg;
                return param.Compile;
            }
            else
            {
                throw new NotSupportedException($"Function arguments of type '{arg.GetType().Name}' are not supported.");
            }
        }

        public QueryCondition HasLabel(string label)
        {
            return new QueryCondition(this, Operator.HasLabel, new Litheral(label));
        }

        public QueryCondition Not(QueryCondition condition)
        {
            return new QueryCondition(string.Empty, Operator.Not, condition);
        }

        public AsResult As(string aliasName)
        {
            return new AsResult(this, aliasName);
        }
        public AsResult As(string aliasName, out AliasResult alias)
        {
            alias = new AliasResult()
            {
                AliasName = aliasName,
            };
            return new AsResult(this, aliasName);
        }

        public AsResult Properties(string alias, out PropertiesAliasResult propertiesAlias)
        {
            propertiesAlias = new PropertiesAliasResult()
            {
                AliasName = alias
            };
            return new AsResult(new MiscResult("properties({0})", new object[] { this }, null), alias);
        }

        public override string? GetFieldName()
        {
            return AliasName;
        }

        public override Type? GetResultType()
        {
            return OverridenReturnType ?? Alias?.GetResultType() ?? null;
        }

        new public StringResult ToString()
        {
            if (AliasName is null)
                throw new InvalidOperationException("You cannot use the labels function in this context.");

            return new StringResult(AliasName, null, typeof(string));
        }

        public virtual IReadOnlyDictionary<string, FieldResult> AliasFields { get { return emptyAliasFields; }  }
        private static Dictionary<string, FieldResult> emptyAliasFields = new Dictionary<string, FieldResult>();

        public StringListResult Labels()
        {
            if (AliasName is null)
                throw new InvalidOperationException("You cannot use the labels function in this context.");

            return new StringListResult(null, "LABELS({0})", new object[] { AliasName }, typeof(string));
        }
        public AliasListResult Collect()
        {
            return new AliasListResult(this, "collect({base})");
        }

    }
}
