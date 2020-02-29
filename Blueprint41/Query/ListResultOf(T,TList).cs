using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Blueprint41.Query
{
    public abstract partial class ListResult<TList, TResult> : AliasResult
        where TList : ListResult<TList, TResult>
        where TResult : AliasResult
    {
        static ListResult()
        {
            newResultCtor = new Lazy<Func<AliasResult, string, object[]?, Type?, TResult>>(delegate ()
            {
                var exp = GetExp<TResult>(typeof(AliasResult), typeof(string), typeof(object[]), typeof(Type));
                return Expression.Lambda<Func<AliasResult, string, object[]?, Type?, TResult>>(exp.body, exp.parameters).Compile();
            }, true);
            newListCtor = new Lazy<Func<AliasResult, string, object[]?, Type?, TList>>(delegate ()
            {
                var exp = GetExp<TList>(typeof(AliasResult), typeof(string), typeof(object[]), typeof(Type));
                return Expression.Lambda<Func<AliasResult, string, object[]?, Type?, TList>>(exp.body, exp.parameters).Compile();
            }, true);
        }

        protected ListResult(AliasResult parent, string function, object[]? arguments = null, Type? type = null): base(parent, function, arguments, type) { }

        private static (Expression body, ParameterExpression[] parameters) GetExp<T>(params Type[] types)
        {
            ParameterExpression[] parameters = new ParameterExpression[types.Length];
            for (int index = 0; index < types.Length; index++)
                parameters[index] = Expression.Parameter(types[index]);

            ConstructorInfo? ctor = typeof(T).GetConstructor(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, null, types, null);
            if (ctor is null)
                throw new ArgumentException($"The type {typeof(T).Name} is not supported for use in a ListResult");

            Expression body = Expression.New(ctor, parameters);

            return (body, parameters);
        }

        public TResult this[int index]
        {
            get
            {
                return NewResult("{base}[{0}]", new object[] { Parameter.Constant<int>(index) });
            }
        }

        public TResult Head()
        {
            return NewResult("HEAD({base})");
        }
        public TResult Last()
        {
            return NewResult("LAST({base})");
        }
        public TList Sort()
        {
            return NewList("apoc.coll.sort({base})");
        }

        public TList Union(StringListResult stringListResult)
        {
            return NewList("{base} + {0}", new object[] { stringListResult });
        }

        //public QueryCondition All(Func<TResult, QueryCondition> condition)
        //{
        //    TResult alias = NewResult("item", new object[0]);
        //    QueryCondition test = condition.Invoke(alias);

        //    return new QueryCondition(new BooleanResult("all(item IN {base} WHERE {0})", new object[] { test }));
        //}
        //public QueryCondition Any(Func<TResult, QueryCondition> condition)
        //{
        //    TResult alias = NewResult("item", new object[0]);
        //    QueryCondition test = condition.Invoke(itemField);

        //    return new QueryCondition(new BooleanResult(itemField, "any(item IN {base} WHERE {0})", new object[] { test }));
        //}
        //public QueryCondition None(Func<TResult, QueryCondition> condition)
        //{
        //    TResult alias = NewResult("item", new object[0]);
        //    QueryCondition test = condition.Invoke(itemField);

        //    return new QueryCondition(new BooleanResult(this, "none(item IN {base} WHERE {0})", new object[] { test }));
        //}
        //public QueryCondition Single(Func<TResult, QueryCondition> condition)
        //{
        //    TResult alias = NewResult("item", new object[0]);
        //    QueryCondition test = condition.Invoke(itemField);

        //    return new QueryCondition(new BooleanResult(this, "single(item IN {base} WHERE {0})", new object[] { test }));
        //}

        public override string GetFieldName()
        {
            throw new NotSupportedException();
        }

        protected internal TResult NewResult(string function, object[]? arguments = null, Type? type = null)
        {
            return newResultCtor!.Value.Invoke(this, function, arguments, type);
        }
        private static Lazy<Func<AliasResult, string, object[]?, Type?, TResult>>? newResultCtor = null;

        protected internal TList NewList(string function, object[]? arguments = null, Type? type = null)
        {
            return newListCtor!.Value.Invoke(this, function, arguments, type);
        }
        private static Lazy<Func<AliasResult, string, object[]?, Type?, TList>>? newListCtor = null;
    }
    public abstract partial class ListResult<TList, TResult, TType> : FieldResult
        where TList : ListResult<TList, TResult, TType>
        where TResult : FieldResult
    {
        static ListResult()
        {
            newResultCtor = new Lazy<Func<FieldResult, string, object[]?, Type?, TResult>>(delegate ()
            {
                var exp = GetExp<TResult>(typeof(FieldResult), typeof(string), typeof(object[]), typeof(Type));
                return Expression.Lambda<Func<FieldResult, string, object[]?, Type?, TResult>>(exp.body, exp.parameters).Compile();
            }, true);
            newListCtor = new Lazy<Func<FieldResult, string, object[]?, Type?, TList>>(delegate ()
            {
                var exp = GetExp<TList>(typeof(FieldResult), typeof(string), typeof(object[]), typeof(Type));
                return Expression.Lambda<Func<FieldResult, string, object[]?, Type?, TList>>(exp.body, exp.parameters).Compile();
            }, true);
        }
        private static (Expression body, ParameterExpression[] parameters) GetExp<T>(params Type[] types)
        {
            ParameterExpression[] parameters = new ParameterExpression[types.Length];
            for (int index = 0; index < types.Length; index++)
                parameters[index] = Expression.Parameter(types[index]);

            ConstructorInfo? ctor = typeof(T).GetConstructor(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, null, types, null);
            if (ctor is null)
                throw new ArgumentException($"The type {typeof(T).Name} is not supported for use in a ListResult");

            Expression body = Expression.New(ctor, parameters);

            return (body, parameters);
        }
        protected ListResult(FieldResult? parent, string function, object[]? arguments = null, Type? type = null) : base(parent, function, arguments, type) { }
        protected ListResult(AliasResult alias, string? fieldName, Entity? entity, Property? property) : base(alias, fieldName, entity, property) { }

        public TResult this[int index]
        {
            get
            {
                return NewResult("{base}[{0}]", new object[] { Parameter.Constant<int>(index) });
            }
        }

        public TResult Head()
        {
            return NewResult("HEAD({base})");
        }
        public TResult Last()
        {
            return NewResult("LAST({base})");
        }
        public TList Sort()
        {
            return NewList("apoc.coll.sort({base})");
        }
        public override NumericResult Count()
        {
            return new NumericResult(this, "size({base})", null, typeof(long));
        }

        public TList Union(StringListResult stringListResult)
        {
            return NewList("{base} + {0}", new object[] { stringListResult });
        }

        public StringResult Reduce(string init, Func<StringResult, TResult, StringResult> logic)
        {
            StringResult valueField = new StringResult(null!, "value", new object[0], typeof(string));
            TResult itemField = NewResult("item", new object[0], typeof(TType));
            StringResult result = logic.Invoke(valueField, itemField);

            return new StringResult(this, "reduce(value = {0}, item in {base} | {1})", new object[] { Parameter.Constant<string>(init), result }, typeof(string));
        }

        public StringResult Join(string separator, Func<TResult, StringResult> logic)
        {
            TResult itemField = NewResult("item", new object[0], typeof(TType));
            StringResult result = logic.Invoke(itemField);

            return Reduce("", (value, item) => value.Concat(Parameter.Constant(separator), result).Substring(separator.Length));
        }
        public QueryCondition All(TType value)
        {
            return All(item => item == Parameter.Constant(value));
        }
        public QueryCondition All(Func<TResult, QueryCondition> condition)
        {
            TResult itemField = NewResult("item", new object[0], typeof(TType));
            QueryCondition test = condition.Invoke(itemField);

            return new QueryCondition(new BooleanResult(this, "all(item IN {base} WHERE {0})", new object[] { test }));
        }
        public QueryCondition Any(TType value)
        {
            return Any(item => item == Parameter.Constant(value));
        }
        public QueryCondition Any(Func<TResult, QueryCondition> condition)
        {
            TResult itemField = NewResult("item", new object[0], typeof(TType));
            QueryCondition test = condition.Invoke(itemField);

            return new QueryCondition(new BooleanResult(this, "any(item IN {base} WHERE {0})", new object[] { test }));
        }
        public QueryCondition None(TType value)
        {
            return None(item => item == Parameter.Constant(value));
        }
        public QueryCondition None(Func<TResult, QueryCondition> condition)
        {
            TResult itemField = NewResult("item", new object[0], typeof(TType));
            QueryCondition test = condition.Invoke(itemField);

            return new QueryCondition(new BooleanResult(this, "none(item IN {base} WHERE {0})", new object[] { test }));
        }
        public QueryCondition Single(TType value)
        {
            return Single(item => item == Parameter.Constant(value));
        }
        public QueryCondition Single(Func<TResult, QueryCondition> condition)
        {
            TResult itemField = NewResult("item", new object[0], typeof(TType));
            QueryCondition test = condition.Invoke(itemField);

            return new QueryCondition(new BooleanResult(itemField, "single(item IN {base} WHERE {0})", new object[] { test }));
        }

        protected internal TResult NewResult(string function, object[]? arguments = null, Type? type = null)
        {
            return newResultCtor!.Value.Invoke(this, function, arguments, type);
        }
        private static Lazy<Func<FieldResult, string, object[]?, Type?, TResult>>? newResultCtor = null;

        protected internal TList NewList(string function, object[]? arguments = null, Type? type = null)
        {
            return newListCtor!.Value.Invoke(this, function, arguments, type);
        }
        private static Lazy<Func<FieldResult, string, object[]?, Type?, TList>>? newListCtor = null;
    }
}
