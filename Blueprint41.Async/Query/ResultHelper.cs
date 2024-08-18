using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

using Blueprint41.Async.Core;
using Blueprint41.Async.Neo4j.Model;

namespace Blueprint41.Async.Query
{
    internal abstract class ResultHelper
    {
        #region Result Info

        public bool IsAlias { get; protected set; }
        public bool IsJaggedList { get; protected set; }
        public bool IsList { get; protected set; }
        public bool IsPrimitive { get; protected set; }
        public ResultHelper? ItemType
        {
            get
            {
                string? type;
                string? targetType;

                if (!IsList)
                    return null;

                if (itemType is null)
                {
                    targetType = null;
                    type = Type.Name;

                    if (IsList)
                    {
                        if (IsAlias)
                            SearchEnd("ListAlias", "Alias");
                        else if (IsPrimitive)
                            SearchEnd("ListResult", "Result");
                    }
                    if (IsJaggedList)
                    {
                        if (IsAlias)
                            SearchEnd("JaggedListAlias", "ListAlias");
                        else if (IsPrimitive) 
                            SearchEnd("JaggedListResult", "ListResult");
                    }

                    if (targetType is null)
                        throw new NotSupportedException($"You shouldn't end up in this piece of code, please file a bug report for 'NotSupportedException in ResultHelper<{Type.FullName}>.ItemType' at: https://github.com/circles-arrows/blueprint41/issues");

                    Type resultHelperType = Type.Assembly.GetType(targetType, true, false);
                    itemType = ResultHelper.Of(resultHelperType);
                }
                return itemType;

                void SearchEnd(string search, string replace) => ComputeTypeName(Type, search, replace, ref targetType);
            }
        }
        private ResultHelper? itemType = null;
        public ResultHelper? ListType
        {
            get
            {
                string? type;
                string? targetType;

                if (IsJaggedList)
                    return null;

                if (listType is null)
                {
                    targetType = null;
                    type = Type.Name;

                    if (IsAlias)
                    {
                        SearchEnd("Alias", "ListAlias");
                    }
                    else if (IsPrimitive)
                    {
                        SearchEnd("Result", "ListResult");
                    }
                    else if (IsList)
                    {
                        if (IsAlias)
                            SearchEnd("ListAlias", "JaggedListAlias");
                        else if (IsPrimitive) 
                            SearchEnd("ListResult", "JaggedListResult");
                    }

                    if (targetType is null)
                        throw new NotSupportedException($"You shouldn't end up in this piece of code, please file a bug report for 'NotSupportedException in ResultHelper<{Type.FullName}>.ListType' at: https://github.com/circles-arrows/blueprint41/issues");

                    Type resultHelperType = Type.Assembly.GetType(targetType, true, false);
                    listType = ResultHelper.Of(resultHelperType);
                }
                return listType;

                void SearchEnd(string search, string replace) => ComputeTypeName(Type, search, replace, ref targetType);
            }
        }
        private ResultHelper? listType = null;

        public Type Type { get; protected set; } = null!;
        public Type? UnderlyingType { get; protected set; } = null!;

        static private void ComputeTypeName(Type type, string search, string replace, ref string? targetType)
        {
            if (targetType is not null)
                return;

            int index = type.Name.LastIndexOf(search);
            if (index == -1)
                return;

            int expectedIndex = type.Name.Length - search.Length;
            if (index == expectedIndex)
            {
                targetType = string.Concat(type.Namespace, ".", type.Name.Substring(0, index), replace);
            }
        }

        #endregion

        #region Functions

        public IResult NewFunctionResult(Func<QueryTranslator, string?>? function, object[]? arguments, Type? overridenReturnType) => NewResultInternal(function, arguments, overridenReturnType);
        protected abstract IResult NewResultInternal(Func<QueryTranslator, string?>? function, object[]? arguments, Type? overridenReturnType);

        public IResult NewAliasResult(IPlainAliasResult alias, string? fieldName, IEntity? entity, Property? property, Type? overridenReturnType = null) => NewResultInternal(alias, fieldName, entity, property, overridenReturnType);
        protected abstract IResult NewResultInternal(IPlainAliasResult alias, string? fieldName, IEntity? entity, Property? property, Type? overridenReturnType = null);

        public IResult NewAliasResult(IPlainAliasResult alias, Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? overridenReturnType = null) => NewResultInternal(alias, function, arguments, overridenReturnType);
        protected abstract IResult NewResultInternal(IPlainAliasResult alias, Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? overridenReturnType = null);

        public IResult NewFieldResult(IPrimitiveResult field, Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? overridenReturnType = null) => NewResultInternal(field, function, arguments, overridenReturnType);
        protected abstract IResult NewResultInternal(IPrimitiveResult field, Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? overridenReturnType = null);

        #endregion

        #region Helpers

        public static ResultHelper<T> Of<T>() where T : IResult => ResultHelper<T>.Instance;

        public static ResultHelper Of(Type type)
        {
            ResultHelper? retval = resultCache.TryGetOrAdd(type, delegate (Type key)
            {
                if (!typeof(IResult).IsAssignableFrom(key))
                    return null;

                return (ResultHelper?)Activator.CreateInstance(typeof(ResultHelper<>).MakeGenericType(key), true);
            });

            if (retval is null)
                throw new ArgumentException($"The type {type.FullName} must implement interface IResult.");

            return retval;
        }
        private static AtomicDictionary<Type, ResultHelper?> resultCache = new AtomicDictionary<Type, ResultHelper?>();

        protected private static (Expression body, ParameterExpression[] parameters) GetExp<T>(params Type[] types)
        {
            ParameterExpression[] parameters = new ParameterExpression[types.Length];
            for (int index = 0; index < types.Length; index++)
                parameters[index] = Expression.Parameter(types[index]);

            ConstructorInfo? ctor = typeof(T).GetConstructor(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, null, types, null);
            if (ctor is null)
                throw new ArgumentException($"The type {typeof(T).Name} is not supported for use in a Result");

            Expression body = Expression.New(ctor, parameters);

            return (body, parameters);
        }
        protected private static (Expression body, ParameterExpression[] parameters) GetExp<T>(string name, Type ret, params Type[] types)
        {
            Type thisType = types.FirstOrDefault();
            Type[] sigParams = types.Skip(1).ToArray();

            ParameterExpression[] allParams = new ParameterExpression[types.Length];
            for (int index = 0; index < types.Length; index++)
                allParams[index] = Expression.Parameter(types[index]);

            ParameterExpression instance = allParams.First();
            ParameterExpression[] parameters = allParams.Skip(1).ToArray();

            MethodInfo[] methods = typeof(T).GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.FlattenHierarchy).Where(item => item.Name == name).ToArray();
            MethodInfo? method = methods.FirstOrDefault(item => MatchParameters(item.GetParameters(), sigParams));

            if (method is null)
                throw new ArgumentException($"The method {name} on {typeof(T).Name} is not supported");

            //if (method.DeclaringType != thisType)
            //    throw new ArgumentException($"The methods first argument {thisType.Name} does not match the type of ResultHelper<{typeof(T).Name} >");

            if (method.ReturnType != ret)
                throw new ArgumentException($"The methods return type {method.ReturnType.Name} does not match the expected return type {ret.Name}");

            Expression body = Expression.Call(instance, method, parameters);

            return (body, allParams);

            bool MatchParameters(ParameterInfo[] method, Type[] expected)
            {
                if (method.Length != expected.Length)
                    return false;

                for (int index = 0; index < method.Length; index++)
                    if (method[index].ParameterType != expected[index])
                        return false;

                return true;
            }
        }

        #endregion
    }
    internal sealed class ResultHelper<T> : ResultHelper
        where T : IResult
    {
        #region Constructors

        static ResultHelper()
        {
            newFunctionResultCtor = new Lazy<Func<Func<QueryTranslator, string?>?, object[]?, Type?, T>>(delegate ()
            {
                var exp = GetExp<T>(typeof(Func<QueryTranslator, string>), typeof(object[]), typeof(Type));
                return Expression.Lambda<Func<Func<QueryTranslator, string?>?, object[]?, Type?, T>>(exp.body, exp.parameters).Compile();
            }, true);
            newAliasResultCtor = new Lazy<Func<AliasResult, string?, IEntity?, Property?, Type?, T>>(delegate ()
            {
                var exp = GetExp<T>(typeof(AliasResult), typeof(string), typeof(Entity), typeof(Property), typeof(Type));
                return Expression.Lambda<Func<AliasResult, string?, IEntity?, Property?, Type?, T>>(exp.body, exp.parameters).Compile();
            }, true);
            newAliasResult2Ctor = new Lazy<Func<AliasResult, Func<QueryTranslator, string?>?, object[]?, Type?, T>>(delegate ()
            {
                var exp = GetExp<T>(typeof(AliasResult), typeof(Func<QueryTranslator, string>), typeof(object[]), typeof(Type));
                return Expression.Lambda<Func<AliasResult, Func<QueryTranslator, string?>?, object[]?, Type?, T>>(exp.body, exp.parameters).Compile();
            }, true);
            newFieldResultCtor = new Lazy<Func<FieldResult, Func<QueryTranslator, string?>?, object[]?, Type?, T>>(delegate ()
            {
                var exp = GetExp<T>(typeof(FieldResult), typeof(Func<QueryTranslator, string>), typeof(object[]), typeof(Type));
                return Expression.Lambda<Func<FieldResult, Func<QueryTranslator, string?>?, object[]?, Type?, T>>(exp.body, exp.parameters).Compile();
            }, true);
            asMethod = new Lazy<AsDelegate<T>>(delegate()
            {
                var exp = GetExp<T>("As", typeof(AsResult), typeof(T), typeof(string), typeof(T).MakeByRefType());
                return Expression.Lambda<AsDelegate<T>>(exp.body, exp.parameters).Compile();
            }, true);
        }
        private ResultHelper()
        {
            IsAlias = typeof(IAliasResult).IsAssignableFrom(typeof(T));
            IsJaggedList = typeof(IJaggedListResult).IsAssignableFrom(typeof(T));
            IsList = typeof(IListResult).IsAssignableFrom(typeof(T));
            IsPrimitive = typeof(IPrimitiveResult).IsAssignableFrom(typeof(T));
            Type = typeof(T);
            UnderlyingType = typeof(T).Name switch
            {
                nameof(MiscResult)               => typeof(object),
                nameof(BooleanResult)            => typeof(bool),
                nameof(NumericResult)            => typeof(long),
                nameof(FloatResult)              => typeof(double),
                nameof(StringResult)             => typeof(string),
                nameof(DateTimeResult)           => typeof(DateTime),
                nameof(RelationFieldResult)      => typeof(DateTime),
                nameof(MiscListResult)           => typeof(object),
                nameof(StringListResult)         => typeof(string),
                nameof(BooleanListResult)        => typeof(bool),
                nameof(DateTimeListResult)       => typeof(DateTime),
                nameof(FloatListResult)          => typeof(double),
                nameof(NumericListResult)        => typeof(long),
                nameof(MiscJaggedListResult)     => typeof(object[]),
                nameof(StringJaggedListResult)   => typeof(string[]),
                nameof(BooleanJaggedListResult)  => typeof(bool[]),
                nameof(DateTimeJaggedListResult) => typeof(DateTime[]),
                nameof(FloatJaggedListResult)    => typeof(double[]),
                nameof(NumericJaggedListResult)  => typeof(long[]),
                _                                => null,
            };
        }

        internal static ResultHelper<T> Instance => instance.Value;
        private static Lazy<ResultHelper<T>> instance = new Lazy<ResultHelper<T>>(() => (ResultHelper<T>)Of(typeof(T)), true);

        #endregion

        #region Functions

        new public T NewFunctionResult(Func<QueryTranslator, string?>? function, object[]? arguments, Type? overridenReturnType)
        {
            List<AliasResult> aliases = arguments.OfType<T>().OfType<AliasResult>().Where(item => item.Entity is not null).ToList();
            if (aliases.Count != 0)
            {
                AliasResult? aliasResult = Entity.FindCommonBaseClass(aliases);
                if ((object?)aliasResult is not null)
                    return newAliasResult2Ctor!.Value.Invoke(aliasResult, function, arguments, overridenReturnType);
            }
            return newFunctionResultCtor!.Value.Invoke(function, arguments, overridenReturnType);
        }
        protected sealed override IResult NewResultInternal(Func<QueryTranslator, string?>? function, object[]? arguments, Type? overridenReturnType) => NewFunctionResult(function, arguments, overridenReturnType);
        private static Lazy<Func<Func<QueryTranslator, string?>?, object[]?, Type?, T>>? newFunctionResultCtor = null;

        new public T NewAliasResult(IPlainAliasResult alias, string? fieldName, IEntity? entity, Property? property, Type? overridenReturnType = null)
        {
            if (IsPrimitive)
                return newAliasResultCtor!.Value.Invoke((AliasResult)alias, fieldName, entity, property, overridenReturnType);

            throw new NotSupportedException();
        }
        protected sealed override IResult NewResultInternal(IPlainAliasResult alias, string? fieldName, IEntity? entity, Property? property, Type? overridenReturnType = null) => NewAliasResult(alias, fieldName, entity, property, overridenReturnType);
        private static Lazy<Func<AliasResult, string?, IEntity?, Property?, Type?, T>>? newAliasResultCtor = null;

        new public T NewAliasResult(IPlainAliasResult alias, Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? overridenReturnType = null)
        {
            return newAliasResult2Ctor!.Value.Invoke((AliasResult)alias, function, arguments, overridenReturnType);
        }
        protected sealed override IResult NewResultInternal(IPlainAliasResult alias, Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? overridenReturnType = null) => NewAliasResult(alias, function, arguments, overridenReturnType);
        private static Lazy<Func<AliasResult, Func<QueryTranslator, string?>?, object[]?, Type?, T>>? newAliasResult2Ctor = null;

        new public T NewFieldResult(IPrimitiveResult field, Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? overridenReturnType = null)
        {
            return newFieldResultCtor!.Value.Invoke((FieldResult)field, function, arguments, overridenReturnType);
        }
        protected sealed override IResult NewResultInternal(IPrimitiveResult field, Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? overridenReturnType = null) => NewFieldResult(field, function, arguments, overridenReturnType);
        private static Lazy<Func<FieldResult, Func<QueryTranslator, string?>?, object[]?, Type?, T>>? newFieldResultCtor = null;

        public AsResult As(T self, string aliasName, out T alias)
        {
            return asMethod!.Value.Invoke(self, aliasName, out alias);
        }
        private static Lazy<AsDelegate<T>>? asMethod = null;

        #endregion
    }
    internal delegate AsResult AsDelegate<T>(T self, string aliasName, out T alias);
}
