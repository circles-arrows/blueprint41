using Blueprint41.Neo4j.Model;
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

        public override bool Equals(object? obj)
        {
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }


        private object[] emptyArguments = new object[0];
        private FieldResult(AliasResult? alias, string? fieldName, Entity? entity, Property? property, FieldResult? field, Func<QueryTranslator, string?>? function, object[]? arguments, Type? type)
        {
            Alias = alias;
            FieldName = fieldName;
            Entity = entity;
            Property = property;
            Field = field;
            FunctionText = function ?? delegate(QueryTranslator t) { return null; } ;
            FunctionArgs = arguments ?? emptyArguments;
            OverridenReturnType = type;
        }
        protected FieldResult(Func<QueryTranslator, string?>? function, object[]? arguments, Type? type) : this(null, null, null, null, null, function, arguments, type) { }
        protected FieldResult(AliasResult alias, string? fieldName, Entity? entity, Property? property, Type? overridenReturnType = null) : this(alias, fieldName, entity, property, null, null, null, null) { OverridenReturnType = overridenReturnType; }
        protected FieldResult(AliasResult alias, Func<QueryTranslator, string?>? function, object[]? arguments, Type? type) : this(alias, null, null, null, null, function, arguments, type) { }
        protected FieldResult(FieldResult? field, Func<QueryTranslator, string?>? function, object[]? arguments, Type? type) : this(field?.Alias, field?.FieldName, field?.Entity, field?.Property, field, function, arguments, type) { }
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

        public AliasResult? Alias { get; private set; }
        public string? FieldName { get; private set; }
        internal Entity? Entity { get; private set; }
        internal Property? Property { get; private set; }
        public FieldResult? Field { get; private set; }
        internal Func<QueryTranslator, string?> FunctionText { get; private set; }
        internal object[]? FunctionArgs { get; private set; }
        private Type? OverridenReturnType { get; set; }

        public virtual NumericResult Count()
        {
            return new NumericResult(this, t => t.FnCount, null, typeof(long));
        }
        public virtual NumericResult CountDistinct()
        {
            return new NumericResult(this, t => t.FnCountDistinct, null, typeof(long));
        }
        public virtual BooleanResult Exists()
        {
            return new BooleanResult(this, t => t.FnExists, null, typeof(bool));
        }
        public virtual BooleanResult ToBoolean()
        {
            return new BooleanResult(this, t => t.FnToBoolean, null, typeof(bool));
        }

        [Obsolete("Please use the 'ToInteger' method instead.")]
        public virtual NumericResult ToInt()
        {
            return ToInteger();
        }
        public virtual NumericResult ToInteger()
        {
            return new NumericResult(this, t => t.FnToInteger, null, typeof(long));
        }
        public virtual FloatResult ToFloat()
        {
            return new FloatResult(this, t => t.FnToFloat, null, typeof(Double));
        }
        new public virtual StringResult ToString()
        {
            return new StringResult(this, t => t.FnToString, null, typeof(string));
        }
        public virtual BooleanListResult ToBooleanList()
        {
            return new BooleanListResult(this, t => t.FnAsIs, null, typeof(bool));
        }
        public virtual NumericListResult ToIntegerList()
        {
            return new NumericListResult(this, t => t.FnAsIs, null, typeof(long));
        }
        public virtual FloatListResult ToFloatList()
        {
            return new FloatListResult(this, t => t.FnAsIs, null, typeof(Double));
        }
        public virtual StringListResult ToStringList()
        {
            return new StringListResult(this, t => t.FnAsIs, null, typeof(string));
        }

        protected internal override void Compile(CompileState state)
        {
            state.Translator.Compile(this, state);
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
            BooleanResult result = new BooleanResult(this, t => t.FnNot);
            return new QueryCondition(result, Operator.In, parameter);
        }

        public QueryCondition NotIn(Result alias)
        {
            BooleanResult result = new BooleanResult(this, t => t.FnNot);
            return new QueryCondition(result, Operator.In, alias);
        }

        public AsResult As(string aliasName)
        {
            return new AsResult(this, aliasName);
        }

        public override string GetFieldName()
        {
            throw new NotSupportedException();
        }
        public override Type? GetResultType()
        {
            if (OverridenReturnType is null && !(Field is null))
                return Field.GetResultType();

            if (OverridenReturnType is null && !(Property is null))
                return Property.SystemReturnType;

            return OverridenReturnType;
        }
    }
    public class FieldResult<TResult, TResultList, TType> : FieldResult
        where TResult : IPlainPrimitiveResult
        where TResultList : IPrimitiveListResult
    {
        internal FieldResult(FieldResult field) : base(field) { }
        public FieldResult(Func<QueryTranslator, string?>? function, object[]? arguments, Type? type) : base(function, arguments, type) { }
        public FieldResult(AliasResult alias, string? fieldName, Entity? entity, Property? property, Type? overridenReturnType = null) : base(alias, fieldName, entity, property, overridenReturnType) { }
        public FieldResult(AliasResult alias, Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? type = null) : base(alias, function, arguments, type) { }
        public FieldResult(FieldResult field, Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? type = null) : base(field, function, arguments, type) { }

        public TResult Coalesce(TType value)
        {
            return NewResult(t => t.FnCoalesce, new object[] { Parameter.Constant(value) });
        }
        public TResult Coalesce(TResult value)
        {
            return NewResult(t => t.FnCoalesce, new object[] { value });
        }
        public TResult Coalesce(Parameter value)
        {
            return NewResult(t => t.FnCoalesce, new object[] { value });
        }

        public TResultList Collect()
        {
            return NewList(t => t.FnCollect);
        }
        public TResultList CollectDistinct()
        {
            return NewList(t => t.FnCollectDistinct);
        }

        private TResult NewResult(Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? overridenReturnType = null) => ResultHelper.Of<TResult>().NewFieldResult((IPrimitiveResult)this, function, arguments, overridenReturnType);
        private TResultList NewList(Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? overridenReturnType = null) => ResultHelper.Of<TResultList>().NewFieldResult((IPrimitiveResult)this, function, arguments, overridenReturnType);
    }
}
