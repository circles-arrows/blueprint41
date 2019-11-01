using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.Query
{
    public abstract class FieldResult : Result
    {
        public static QueryCondition operator ==(FieldResult a, FieldResult b)
        {
            return new QueryCondition(a, Operator.Equals, b);
        }
        public static QueryCondition operator ==(FieldResult a, Parameter b)
        {
            return new QueryCondition(a, Operator.Equals, b);
        }
        public static QueryCondition operator !=(FieldResult a, FieldResult b)
        {
            return new QueryCondition(a, Operator.NotEquals, b);
        }
        public static QueryCondition operator !=(FieldResult a, Parameter b)
        {
            return new QueryCondition(a, Operator.NotEquals, b);
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }


        private object[] emptyArguments = new object[0];
        private FieldResult(AliasResult alias, string fieldName, Entity entity, Property property, FieldResult field, string function, object[] arguments, Type type)
        {
            Alias = alias;
            FieldName = fieldName;
            Entity = entity;
            Property = property;
            Field = field;
            FunctionText = function;
            FunctionArgs = arguments ?? emptyArguments;
            OverridenReturnType = type;
        }
        protected FieldResult(string function, object[] arguments, Type type) : this(null, null, null, null, null, function, arguments, type) { }
        protected FieldResult(AliasResult alias, string fieldName, Entity entity, Property property, Type overridenReturnType = null) : this(alias, fieldName, entity, property, null, null, null, null) { OverridenReturnType = overridenReturnType; }
        protected FieldResult(FieldResult field, string function, object[] arguments, Type type) : this(field?.Alias, field?.FieldName, field?.Entity, field?.Property, field, function, arguments, type) { }
        protected FieldResult(FieldResult field)
        {
            Alias = field.Alias;
            FieldName = field.FieldName;
            Entity = field.Entity;
            Property = field.Property;
            Field = field.Field;
            FunctionText = field.FunctionText;
            FunctionArgs = field.FunctionArgs;
            OverridenReturnType = field.OverridenReturnType;
        }

        public AliasResult Alias { get; private set; }
        public string FieldName { get; private set; }
        internal Entity Entity { get; private set; }
        internal Property Property { get; private set; }
        public FieldResult Field { get; private set; }
        private string FunctionText { get; set; }
        private object[] FunctionArgs { get; set; }
        private Type OverridenReturnType { get; set; }

        public virtual NumericResult Count()
        {
            return new NumericResult(this, "count({base})", null, typeof(long));
        }
        public virtual NumericResult CountDistinct()
        {
            return new NumericResult(this, "count(DISTINCT {base})", null, typeof(long));
        }
        public virtual BooleanResult Exists()
        {
            return new BooleanResult(this, "exists({base})", null, typeof(bool));
        }
        public virtual BooleanResult ToBoolean()
        {
            return new BooleanResult(this, "toBoolean({base})", null, typeof(bool));
        }

        [Obsolete("Please use the 'ToInteger' method instead.")]
        public virtual NumericResult ToInt()
        {
            return ToInteger();
        }
        public virtual NumericResult ToInteger()
        {
            return new NumericResult(this, "toInteger({base})", null, typeof(long));
        }
        public virtual FloatResult ToFloat()
        {
            return new FloatResult(this, "toFloat({base})", null, typeof(Double));
        }
        new public virtual StringResult ToString()
        {
            return new StringResult(this, "toString({base})", null, typeof(string));
        }
        public virtual BooleanListResult ToBooleanList()
        {
            return new BooleanListResult(this, "{base}", null, typeof(bool));
        }
        public virtual NumericListResult ToIntegerList()
        {
            return new NumericListResult(this, "{base}", null, typeof(long));
        }
        public virtual FloatListResult ToFloatList()
        {
            return new FloatListResult(this, "{base}", null, typeof(Double));
        }
        public virtual StringListResult ToStringList()
        {
            return new StringListResult(this, "{base}", null, typeof(string));
        }



        protected internal override void Compile(CompileState state)
        {
            if (FunctionText == null)
            {
                Alias.Compile(state);
                if (!string.IsNullOrEmpty(FieldName))
                {
                    state.Text.Append(".");
                    state.Text.Append(FieldName);
                }
            }
            else
            {
                string[] compiledArgs = FunctionArgs.Select(arg => state.Preview(GetCompile(arg), state)).ToArray();
                string compiledText = string.Format(FunctionText.Replace("{base}", "{{base}}"), compiledArgs);

                if ((object)Field == null)
                {
                    state.Text.Append(compiledText);
                }
                else
                {
                    string[] split = compiledText.Split(new string[] { "{base}" }, StringSplitOptions.None);
                    if (split.Length == 0)
                        throw new NotSupportedException("Functions have to include compilation of the base they are derived from.");

                    string baseText = state.Preview(Field.Compile, state);
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

        public QueryCondition In(Parameter parameter)
        {
            return new QueryCondition(this, Operator.In, parameter);
        }

        public QueryCondition In(Result alias)
        {
            return new QueryCondition(this, Operator.In, alias);
        }

        public QueryCondition NotIn(Parameter parameter)
        {
            BooleanResult result = new BooleanResult(this, "NOT ({base})");
            return new QueryCondition(result, Operator.In, parameter);
        }

        public QueryCondition NotIn(Result alias)
        {
            BooleanResult result = new BooleanResult(this, "NOT ({base})");
            return new QueryCondition(result, Operator.In, alias);
        }

        public AsResult As(string aliasName)
        {
            return new AsResult(this, aliasName);
        }

        public AsResult As(string aliasName, out AliasResult alias)
        {
            alias = new AliasResult()
            {
                AliasName = aliasName
            };
            return new AsResult(this, aliasName);
        }

        public override string GetFieldName()
        {
            throw new NotSupportedException();
        }
        public override Type GetResultType()
        {
            if (OverridenReturnType == null && (object)Field != null)
                return Field.GetResultType();

            if (OverridenReturnType == null && (object)Property != null)
                return Property.SystemReturnType;

            return OverridenReturnType;
        }
    }
}
