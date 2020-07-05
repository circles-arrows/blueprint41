using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

using Blueprint41.Core;
using Blueprint41.Neo4j.Model;

namespace Blueprint41.Query
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
                if (!IsList)
                    return null;

                if (itemType is null)
                {
                    if (IsList)
                    {

                    }
                    if (IsJaggedList)
                    {

                    }
                    else
                    {
                        throw new NotSupportedException($"You shouldn't end up in this piece of code, please file a bug report for 'NotSupportedException in ResultHelper<{Type.FullName}>.ItemType' at: https://github.com/circles-arrows/blueprint41/issues");
                    }

                }
                return itemType;
            }
        }
        public ResultHelper? itemType = null;
        public ResultHelper? ListType
        {
            get
            {
                if (!IsJaggedList)
                    return null;

                if (itemType is null)
                {
                    if (IsPrimitive)
                    {
                        
                    }
                    if (IsList)
                    {

                    }
                    else
                    {
                        throw new NotSupportedException($"You shouldn't end up in this piece of code, please file a bug report for 'NotSupportedException in ResultHelper<{Type.FullName}>.ListType' at: https://github.com/circles-arrows/blueprint41/issues");
                    }
                }
                return itemType;
            }
        }
        protected ResultHelper? listType = null;
        public Type Type { get; protected set; } = null!;
        public Type? UnderlyingType { get; protected set; } = null!;

        #endregion

        #region Functions

        public IResult NewFunctionResult(Func<QueryTranslator, string?>? function, object[]? arguments, Type? overridenReturnType) => NewResultInternal(function, arguments, overridenReturnType);
        protected abstract IResult NewResultInternal(Func<QueryTranslator, string?>? function, object[]? arguments, Type? overridenReturnType);

        public IResult NewAliasResult(IPlainAliasResult alias, string? fieldName, Entity? entity, Property? property, Type? overridenReturnType = null) => NewResultInternal(alias, fieldName, entity, property, overridenReturnType);
        protected abstract IResult NewResultInternal(IPlainAliasResult alias, string? fieldName, Entity? entity, Property? property, Type? overridenReturnType = null);

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

                return (ResultHelper?)Activator.CreateInstance(typeof(ResultHelper<>).MakeGenericType(key));
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
            newAliasResultCtor = new Lazy<Func<AliasResult, string?, Entity?, Property?, Type?, T>>(delegate ()
            {
                var exp = GetExp<T>(typeof(AliasResult), typeof(string), typeof(Entity), typeof(Property), typeof(Type));
                return Expression.Lambda<Func<AliasResult, string?, Entity?, Property?, Type?, T>>(exp.body, exp.parameters).Compile();
            }, true);
            newFieldResultCtor = new Lazy<Func<FieldResult, Func<QueryTranslator, string?>?, object[]?, Type?, T>>(delegate ()
            {
                var exp = GetExp<T>(typeof(FieldResult), typeof(Func<QueryTranslator, string>), typeof(object[]), typeof(Type));
                return Expression.Lambda<Func<FieldResult, Func<QueryTranslator, string?>?, object[]?, Type?, T>>(exp.body, exp.parameters).Compile();
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
                nameof(ListOfMiscListResult)     => typeof(object[]),
                nameof(ListOfStringListResult)   => typeof(string[]), 
                nameof(ListOfBooleanListResult)  => typeof(bool[]), 
                nameof(ListOfDateTimeListResult) => typeof(DateTime[]),
                nameof(ListOfFloatListResult)    => typeof(double[]),
                nameof(ListOfNumericListResult)  => typeof(long[]),   
                _                                => null,
            };
        }

        internal static ResultHelper<T> Instance = (ResultHelper<T>)Of(typeof(T));

        #endregion

        #region Functions

        new public T NewFunctionResult(Func<QueryTranslator, string?>? function, object[]? arguments, Type? overridenReturnType)
        {
            if (IsPrimitive)
                return newFunctionResultCtor!.Value.Invoke(function, arguments, overridenReturnType);

            throw new NotSupportedException();
        }
        protected sealed override IResult NewResultInternal(Func<QueryTranslator, string?>? function, object[]? arguments, Type? overridenReturnType) => NewFunctionResult(function, arguments, overridenReturnType);
        private static Lazy<Func<Func<QueryTranslator, string?>?, object[]?, Type?, T>>? newFunctionResultCtor = null;

        new public T NewAliasResult(IPlainAliasResult alias, string? fieldName, Entity? entity, Property? property, Type? overridenReturnType = null)
        {
            if (IsPrimitive)
                return newAliasResultCtor!.Value.Invoke((AliasResult)alias, fieldName, entity, property, overridenReturnType);

            throw new NotSupportedException();
        }
        protected sealed override IResult NewResultInternal(IPlainAliasResult alias, string? fieldName, Entity? entity, Property? property, Type? overridenReturnType = null) => NewAliasResult(alias, fieldName, entity, property, overridenReturnType);
        private static Lazy<Func<AliasResult, string?, Entity?, Property?, Type?, T>>? newAliasResultCtor = null;

        new public T NewAliasResult(IPlainAliasResult alias, Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? overridenReturnType = null)
        {
            return newAliasResult2Ctor!.Value.Invoke((AliasResult)alias, function, arguments, overridenReturnType);
        }
        protected sealed override IResult NewResultInternal(IPlainAliasResult alias, Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? overridenReturnType = null) => NewAliasResult(alias, function, arguments, overridenReturnType);
        private static Lazy<Func<AliasResult, Func<QueryTranslator, string?>?, object[]?, Type?, T>>? newAliasResult2Ctor = null;

        new public T NewFieldResult(IPrimitiveResult field, Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? overridenReturnType = null) 
        {
            if (IsPrimitive)
                return newFieldResultCtor!.Value.Invoke((FieldResult)field, function, arguments, overridenReturnType);

            throw new NotSupportedException();
        }
        protected sealed override IResult NewResultInternal(IPrimitiveResult field, Func<QueryTranslator, string?>? function, object[]? arguments = null, Type? overridenReturnType = null) => NewFieldResult(field, function, arguments, overridenReturnType);
        private static Lazy<Func<FieldResult, Func<QueryTranslator, string?>?, object[]?, Type?, T>>? newFieldResultCtor = null;

        #endregion
    }
}
