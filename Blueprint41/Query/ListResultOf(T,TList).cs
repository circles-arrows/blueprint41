using Blueprint41.Neo4j.Model;
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
            newResultCtor = new Lazy<Func<AliasResult, Func<QueryTranslator, string?>?, object[]?, Type?, TResult>>(delegate ()
            {
                var exp = GetExp<TResult>(typeof(AliasResult), typeof(Func<QueryTranslator, string>), typeof(object[]), typeof(Type));
                return Expression.Lambda<Func<AliasResult, Func<QueryTranslator, string?>?, object[]?, Type?, TResult>>(exp.body, exp.parameters).Compile();
            }, true);
            newListCtor = new Lazy<Func<AliasResult, Func<QueryTranslator, string?>?, object[]?, Type?, TList>>(delegate ()
            {
                var exp = GetExp<TList>(typeof(AliasResult), typeof(Func<QueryTranslator, string>), typeof(object[]), typeof(Type));
                return Expression.Lambda<Func<AliasResult, Func<QueryTranslator, string?>?, object[]?, Type?, TList>>(exp.body, exp.parameters).Compile();
            }, true);
        }

        protected ListResult(AliasResult parent, Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? type = null): base(parent, function, arguments, type) { }

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
                return NewResult(t => t.FnListGetItem, new object[] { Parameter.Constant<int>(index) });
            }
        }

        public TResult Head()
        {
            return NewResult(t => t.FnListHead);
        }
        public TResult Last()
        {
            return NewResult(t => t.FnListLast);
        }
        public TList Sort()
        {
            return NewList(t => t.FnListSort);
        }

        public TList Union(StringListResult stringListResult)
        {
            return NewList(t => t.FnListUnion, new object[] { stringListResult });
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

        protected internal TResult NewResult(Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? type = null)
        {
            return newResultCtor!.Value.Invoke(this, function, arguments, type);
        }
        private static Lazy<Func<AliasResult, Func<QueryTranslator, string?>?, object[]?, Type?, TResult>>? newResultCtor = null;

        protected internal TList NewList(Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? type = null)
        {
            return newListCtor!.Value.Invoke(this, function, arguments, type);
        }
        private static Lazy<Func<AliasResult, Func<QueryTranslator, string?>?, object[]?, Type?, TList>>? newListCtor = null;
    }
    public abstract partial class ListResult<TList, TResult, TType> : FieldResult
        where TList : ListResult<TList, TResult, TType>
        where TResult : FieldResult
    {
        static ListResult()
        {
            newResultCtor = new Lazy<Func<FieldResult, Func<QueryTranslator, string?>?, object[]?, Type?, TResult>>(delegate ()
            {
                var exp = GetExp<TResult>(typeof(FieldResult), typeof(Func<QueryTranslator, string>), typeof(object[]), typeof(Type));
                return Expression.Lambda<Func<FieldResult, Func<QueryTranslator, string?>?, object[]?, Type?, TResult>>(exp.body, exp.parameters).Compile();
            }, true);
            newListCtor = new Lazy<Func<FieldResult, Func<QueryTranslator, string?>?, object[]?, Type?, TList>>(delegate ()
            {
                var exp = GetExp<TList>(typeof(FieldResult), typeof(Func<QueryTranslator, string>), typeof(object[]), typeof(Type));
                return Expression.Lambda<Func<FieldResult, Func<QueryTranslator, string?>?, object[]?, Type?, TList>>(exp.body, exp.parameters).Compile();
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
        protected ListResult(FieldResult? parent, Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? type = null) : base(parent, function, arguments, type) { }
        protected ListResult(AliasResult alias, string? fieldName, Entity? entity, Property? property) : base(alias, fieldName, entity, property) { }

        public TResult this[int index]
        {
            get
            {
                return NewResult(t => t.FnListGetItem, new object[] { Parameter.Constant<int>(index) });
            }
        }

        public TResult Head()
        {
            return NewResult(t => t.FnListHead);
        }
        public TResult Last()
        {
            return NewResult(t => t.FnListLast);
        }
        public TList Sort()
        {
            return NewList(t => t.FnListSort);
        }
        public override NumericResult Count()
        {
            return new NumericResult(this, t => t.FnListSize, null, typeof(long));
        }

        public TList Union(StringListResult stringListResult)
        {
            return NewList(t => t.FnListUnion, new object[] { stringListResult });
        }

        public StringResult Reduce(string init, Func<StringResult, TResult, StringResult> logic)
        {
            StringResult valueField = new StringResult(null!, t => "value", new object[0], typeof(string));
            TResult itemField = NewResult(t => "item", new object[0], typeof(TType));
            StringResult result = logic.Invoke(valueField, itemField);

            return new StringResult(this, t => t.FnListReduce, new object[] { Parameter.Constant<string>(init), result }, typeof(string));
        }

        public StringResult Join(string separator, Func<TResult, StringResult> logic)
        {
            TResult itemField = NewResult(t => "item", new object[0], typeof(TType));
            StringResult result = logic.Invoke(itemField);

            return Reduce("", (value, item) => value.Concat(Parameter.Constant(separator), result).Substring(separator.Length));
        }
        public QueryCondition All(TType value)
        {
            return All(item => item == Parameter.Constant(value));
        }
        public QueryCondition All(Func<TResult, QueryCondition> condition)
        {
            TResult itemField = NewResult(t => "item", new object[0], typeof(TType));
            QueryCondition test = condition.Invoke(itemField);

            return new QueryCondition(new BooleanResult(this, t => t.FnListAll, new object[] { test }));
        }
        public QueryCondition Any(TType value)
        {
            return Any(item => item == Parameter.Constant(value));
        }
        public QueryCondition Any(Func<TResult, QueryCondition> condition)
        {
            TResult itemField = NewResult(t => "item", new object[0], typeof(TType));
            QueryCondition test = condition.Invoke(itemField);

            return new QueryCondition(new BooleanResult(this, t => t.FnListAny, new object[] { test }));
        }
        public QueryCondition None(TType value)
        {
            return None(item => item == Parameter.Constant(value));
        }
        public QueryCondition None(Func<TResult, QueryCondition> condition)
        {
            TResult itemField = NewResult(t => "item", new object[0], typeof(TType));
            QueryCondition test = condition.Invoke(itemField);

            return new QueryCondition(new BooleanResult(this, t => t.FnListNone, new object[] { test }));
        }
        public QueryCondition Single(TType value)
        {
            return Single(item => item == Parameter.Constant(value));
        }
        public QueryCondition Single(Func<TResult, QueryCondition> condition)
        {
            TResult itemField = NewResult(t => "item", new object[0], typeof(TType));
            QueryCondition test = condition.Invoke(itemField);

            return new QueryCondition(new BooleanResult(itemField, t => t.FnListSingle, new object[] { test }));
        }

        protected internal TResult NewResult(Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? type = null)
        {
            return newResultCtor!.Value.Invoke(this, function, arguments, type);
        }
        private static Lazy<Func<FieldResult, Func<QueryTranslator, string?>?, object[]?, Type?, TResult>>? newResultCtor = null;

        protected internal TList NewList(Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? type = null)
        {
            return newListCtor!.Value.Invoke(this, function, arguments, type);
        }
        private static Lazy<Func<FieldResult, Func<QueryTranslator, string?>?, object[]?, Type?, TList>>? newListCtor = null;
    }
}
