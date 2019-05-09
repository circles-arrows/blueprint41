using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Blueprint41.Query
{
    public abstract class ListResult<TList, TResult> : AliasResult
        where TList : ListResult<TList, TResult>
        where TResult : Result
    {
        static ListResult()
        {
            newResultCtor = new Lazy<Func<AliasResult, string, object[], Type, TResult>>(delegate ()
            {
                var exp = GetExp<TResult>(typeof(AliasResult), typeof(string), typeof(object[]), typeof(Type));
                return Expression.Lambda<Func<AliasResult, string, object[], Type, TResult>>(exp.body, exp.parameters).Compile();
            }, true);
            newListCtor = new Lazy<Func<AliasResult, string, object[], Type, TList>>(delegate ()
            {
                var exp = GetExp<TList>(typeof(AliasResult), typeof(string), typeof(object[]), typeof(Type));
                return Expression.Lambda<Func<AliasResult, string, object[], Type, TList>>(exp.body, exp.parameters).Compile();
            }, true);
        }

        protected ListResult(AliasResult parent, string function, object[] arguments = null, Type type = null): base(parent, function, arguments, type) { }

        private static (Expression body, ParameterExpression[] parameters) GetExp<T>(params Type[] types)
        {
            ParameterExpression[] parameters = new ParameterExpression[types.Length];
            for (int index = 0; index < types.Length; index++)
                parameters[index] = Expression.Parameter(types[index]);

            ConstructorInfo ctor = typeof(T).GetConstructor(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, null, types, null);
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

        public override string GetFieldName()
        {
            throw new NotSupportedException();
        }

        protected internal TResult NewResult(string function, object[] arguments = null, Type type = null)
        {
            return newResultCtor.Value.Invoke(this, function, arguments, type);
        }
        private  static Lazy<Func<AliasResult, string, object[], Type, TResult>> newResultCtor = null;

        protected internal TList NewList(string function, object[] arguments = null, Type type = null)
        {
            return newListCtor.Value.Invoke(this, function, arguments, type);
        }
        private static Lazy<Func<AliasResult, string, object[], Type, TList>> newListCtor = null;
    }
    public abstract class ListResult<TList, TResult, TType> : FieldResult
        where TList : ListResult<TList, TResult, TType>
        where TResult : Result
    {
        static ListResult()
        {
            newResultCtor = new Lazy<Func<FieldResult, string, object[], Type, TResult>>(delegate ()
            {
                var exp = GetExp<TResult>(typeof(FieldResult), typeof(string), typeof(object[]), typeof(Type));
                return Expression.Lambda<Func<FieldResult, string, object[], Type, TResult>>(exp.body, exp.parameters).Compile();
            }, true);
            newListCtor = new Lazy<Func<FieldResult, string, object[], Type, TList>>(delegate ()
            {
                var exp = GetExp<TList>(typeof(FieldResult), typeof(string), typeof(object[]), typeof(Type));
                return Expression.Lambda<Func<FieldResult, string, object[], Type, TList>>(exp.body, exp.parameters).Compile();
            }, true);
        }
        private static (Expression body, ParameterExpression[] parameters) GetExp<T>(params Type[] types)
        {
            ParameterExpression[] parameters = new ParameterExpression[types.Length];
            for (int index = 0; index < types.Length; index++)
                parameters[index] = Expression.Parameter(types[index]);

            ConstructorInfo ctor = typeof(T).GetConstructor(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, null, types, null);
            Expression body = Expression.New(ctor, parameters);

            return (body, parameters);
        }
        protected ListResult(FieldResult parent, string function, object[] arguments = null, Type type = null) : base(parent, function, arguments, type) { }
        protected ListResult(AliasResult alias, string fieldName, Entity entity, Property property) : base(alias, fieldName, entity, property) { }

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

        public TResult Reduce(TType init, Func<TResult, TResult, TResult> logic)
        {
            TResult valueField = NewResult("value", new object[0], typeof(TType));
            TResult itemField = NewResult("item", new object[0], typeof(TType));
            TResult result = logic.Invoke(valueField, itemField);

            return NewResult("reduce(value = {0}, item in {base} | {1})", new object[] { Parameter.Constant<TType>(init), result }, typeof(TType));
        }

        public QueryCondition All(TType value)
        {
            return new QueryCondition(new BooleanResult(Field, "all(item IN {base} WHERE item = {0})", new object[] { Parameter.Constant<TType>(value) }));
        }
        public QueryCondition Any(TType value)
        {
            return new QueryCondition(new BooleanResult(Field, "any(item IN {base} WHERE item = {0})", new object[] { Parameter.Constant<TType>(value) }));
        }
        public QueryCondition None(TType value)
        {
            return new QueryCondition(new BooleanResult(Field, "none(item IN {base} WHERE item = {0})", new object[] { Parameter.Constant<TType>(value) }));
        }
        public QueryCondition Single(TType value)
        {
            return new QueryCondition(new BooleanResult(Field, "single(item IN {base} WHERE item = {0})", new object[] { Parameter.Constant<TType>(value) }));
        }

        protected internal TResult NewResult(string function, object[] arguments = null, Type type = null)
        {
            return newResultCtor.Value.Invoke(this, function, arguments, type);
        }
        private static Lazy<Func<FieldResult, string, object[], Type, TResult>> newResultCtor = null;

        protected internal TList NewList(string function, object[] arguments = null, Type type = null)
        {
            return newListCtor.Value.Invoke(this, function, arguments, type);
        }
        private static Lazy<Func<FieldResult, string, object[], Type, TList>> newListCtor = null;
    }
}
