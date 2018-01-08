using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace Blueprint41.Core
{
    /// <summary>
	///   A helper class to do common <see cref = "Delegate" /> operations.
	///   TODO: Add extra contracts to reenforce correct usage.
	/// </summary>
	/// <author>Steven Jeuris</author>
	public static class DelegateHelper
    {
        /// <summary>
        ///   Options which specify what type of delegate should be created.
        /// </summary>
        public enum CreateOptions
        {
            None,
            /// <summary>
            ///   Downcasts of delegate parameter types to the correct types required for the method are done where necessary.
            ///   Of course only valid casts will work.
            /// </summary>
            Downcasting
        }


        /// <summary>
        ///   Holds the expressions for the parameters when creating delegates.
        /// </summary>
        struct ParameterConversionExpressions
        {
            public IEnumerable<ParameterExpression> OriginalParameters;
            public IEnumerable<Expression> ConvertedParameters;
        }


        /// <summary>
        ///   The name of the Invoke method of a Delegate.
        /// </summary>
        const string InvokeMethod = "Invoke";


        /// <summary>
        ///   Get method info for a specified delegate type.
        /// </summary>
        /// <param name = "delegateType">The delegate type to get info for.</param>
        /// <returns>The method info for the given delegate type.</returns>
        public static MethodInfo MethodInfoFromDelegateType(Type delegateType)
        {
            return delegateType.GetMethod(InvokeMethod);
        }

        /// <summary>
        ///   Creates a delegate of a specified type which wraps another similar delegate, doing downcasts where necessary.
        ///   The created delegate will only work in case the casts are valid.
        /// </summary>
        /// <typeparam name = "TDelegate">The type for the delegate to create.</typeparam>
        /// <param name = "toWrap">The delegate which needs to be wrapped by another delegate.</param>
        /// <returns>A new delegate which wraps the passed delegate, doing downcasts where necessary.</returns>
        public static TDelegate WrapDelegate<TDelegate>(Delegate toWrap)
            where TDelegate : class
        {
            MethodInfo toCreateInfo = MethodInfoFromDelegateType(typeof(TDelegate));
            MethodInfo toWrapInfo = toWrap.Method;

            // Create delegate original and converted parameters.
            var toCreateArguments = toCreateInfo.GetParameters().Select(d => d.ParameterType);
            var toWrapArguments = toWrapInfo.GetParameters().Select(p => p.ParameterType);
            var parameterExpressions = CreateParameterConversionExpressions(toCreateArguments, toWrapArguments);

            // Create call to wrapped delegate.
            Expression delegateCall = Expression.Invoke(
                Expression.Constant(toWrap),
                parameterExpressions.ConvertedParameters);

            return Expression.Lambda<TDelegate>(
                ConvertOrWrapDelegate(delegateCall, toCreateInfo.ReturnType),
                parameterExpressions.OriginalParameters
                ).Compile();
        }


        /// <summary>
        ///   Creates a delegate of a specified type that represents the specified static or instance method,
        ///   with the specified first argument.
        /// </summary>
        /// <typeparam name = "TDelegate">The type for the delegate.</typeparam>
        /// <param name = "method">The MethodInfo describing the static or instance method the delegate is to represent.</param>
        /// <param name = "instance">When method is an instance method, the instance to call this method on. Null for static methods.</param>
        /// <param name = "options">Options which specify what type of delegate should be created.</param>
        public static TDelegate CreateDelegate<TDelegate>(
            MethodInfo method,
            object instance = null,
            CreateOptions options = CreateOptions.None)
            where TDelegate : class
        {
            switch (options)
            {
                case CreateOptions.None:
                    // Ordinary delegate creation, maintaining variance safety.
                    return Delegate.CreateDelegate(typeof(TDelegate), instance, method) as TDelegate;

                case CreateOptions.Downcasting:
                    {
                        MethodInfo delegateInfo = MethodInfoFromDelegateType(typeof(TDelegate));

                        // Create delegate original and converted arguments.
                        var delegateTypes = delegateInfo.GetParameters().Select(d => d.ParameterType);
                        var methodTypes = method.GetParameters().Select(p => p.ParameterType);
                        var delegateParameterExpressions = CreateParameterConversionExpressions(delegateTypes, methodTypes);

                        // Create method call.
                        Expression methodCall = Expression.Call(
                            instance == null ? null : Expression.Constant(instance),
                            method,
                            delegateParameterExpressions.ConvertedParameters);

                        return Expression.Lambda<TDelegate>(
                            ConvertOrWrapDelegate(methodCall, delegateInfo.ReturnType), // Convert return type when necessary.
                            delegateParameterExpressions.OriginalParameters
                            ).Compile();
                    }

                default:
                    throw new NotSupportedException();
            }
        }

        /// <summary>
        ///   Creates a delegate of a specified type that represents a method which can be executed on an instance passed as parameter.
        /// </summary>
        /// <typeparam name = "TDelegate">
        ///   The type for the delegate. This delegate needs at least one (first) type parameter denoting the type of the instance
        ///   which will be passed.
        ///   E.g. Action&lt;ExampleObject, object&gt;,
        ///   where ExampleObject denotes the instance type and object denotes the desired type of the first parameter of the method.
        /// </typeparam>
        /// <param name = "method">The MethodInfo describing the method of the instance type.</param>
        /// <param name = "options">Options which specify what type of delegate should be created.</param>
        public static TDelegate CreateOpenInstanceDelegate<TDelegate>(
            MethodInfo method,
            CreateOptions options = CreateOptions.None)
            where TDelegate : class
        {
            switch (options)
            {
                case CreateOptions.None:
                    // Ordinary delegate creation, maintaining variance safety.
                    return Delegate.CreateDelegate(typeof(TDelegate), method) as TDelegate;

                case CreateOptions.Downcasting:
                    {
                        MethodInfo delegateInfo = MethodInfoFromDelegateType(typeof(TDelegate));
                        var delegateParameters = delegateInfo.GetParameters();

                        // Convert instance type when necessary.
                        Type delegateInstanceType = delegateParameters.Select(p => p.ParameterType).First();
                        Type methodInstanceType = method.DeclaringType;
                        ParameterExpression instance = Expression.Parameter(delegateInstanceType);
                        Expression convertedInstance = ConvertOrWrapDelegate(instance, methodInstanceType);

                        // Create delegate original and converted arguments.
                        var delegateTypes = delegateParameters.Select(d => d.ParameterType).Skip(1);
                        var methodTypes = method.GetParameters().Select(m => m.ParameterType);
                        var delegateParameterExpressions = CreateParameterConversionExpressions(delegateTypes, methodTypes);

                        // Create method call.
                        Expression methodCall = Expression.Call(
                            convertedInstance,
                            method,
                            delegateParameterExpressions.ConvertedParameters);

                        return Expression.Lambda<TDelegate>(
                            ConvertOrWrapDelegate(methodCall, delegateInfo.ReturnType), // Convert return type when necessary.
                            new[] { instance }.Concat(delegateParameterExpressions.OriginalParameters)
                            ).Compile();
                    }

                default:
                    throw new NotSupportedException();
            }
        }

        /// <summary>
        ///   Creates the expressions for delegate parameters and their conversions
        ///   to the corresponding required types where necessary.
        /// </summary>
        /// <param name = "toCreateTypes">The types of the parameters of the delegate to create.</param>
        /// <param name = "toWrapTypes">The types of the parameters of the call to wrap.</param>
        /// <returns>An object containing the delegate expressions.</returns>
        static ParameterConversionExpressions CreateParameterConversionExpressions(
            IEnumerable<Type> toCreateTypes,
            IEnumerable<Type> toWrapTypes)
        {
            var originalParameters = toCreateTypes.Select(Expression.Parameter).ToArray(); // ToArray prevents deferred execution.   

            return new ParameterConversionExpressions
            {
                OriginalParameters = originalParameters,

                // Convert the parameters from the delegate parameter type to the required type when necessary.
                ConvertedParameters = originalParameters.Zip(toWrapTypes, ConvertOrWrapDelegate)
            };
        }

        /// <summary>
        ///   Converts the result of the given expression to the desired type,
        ///   or when it is a delegate, tries to wrap it with a delegate which attempts to do downcasts where necessary.
        /// </summary>
        /// <param name="expression">The expression of which the result needs to be converted.</param>
        /// <param name="toType">The type to which the result needs to be converted.</param>
        /// <returns>An expression which converts the given expression to the desired type.</returns>
        static Expression ConvertOrWrapDelegate(Expression expression, Type toType)
        {
            Expression convertedExpression;
            Type fromType = expression.Type;

            if (toType == fromType)
            {
                convertedExpression = expression;   // No conversion of the return type needed.
            }
            else
            {
                // TODO: CanConvertTo is incomplete. For the current purpose it returns the correct result, but might not in all cases.
                if (fromType.CanConvertTo(toType))
                {
                    convertedExpression = Expression.Convert(expression, toType);
                }
                else
                {
                    // When the return type is a delegate, attempt recursively wrapping it, adding extra conversions where needed. E.g. Func<T>
                    if (fromType.IsDelegate() && fromType.IsGenericType)
                    {
                        Func<Delegate, object> wrapDelegateDelegate = WrapDelegate<object>;
                        MethodInfo wrapDelegateMethod = wrapDelegateDelegate.Method.GetGenericMethodDefinition().MakeGenericMethod(toType);
                        MethodCallExpression wrapDelegate = Expression.Call(wrapDelegateMethod, expression);
                        convertedExpression = wrapDelegate;
                    }
                    else
                    {
                        throw new InvalidOperationException("Can't downcast the return type to its desired type.");
                    }
                }
            }

            return convertedExpression;
        }

        static bool CanConvertTo(this Type fromType, Type targetType, bool sameHierarchy = true, bool switchVariance = false)
        {
            Func<Type, Type, bool> covarianceCheck = sameHierarchy
                ? (Func<Type, Type, bool>)IsInHierarchy
                : (from, to) => from == to || from.IsSubclassOf(to);
            Func<Type, Type, bool> contravarianceCheck = sameHierarchy
                ? (Func<Type, Type, bool>)IsInHierarchy
                : (from, to) => from == to || to.IsSubclassOf(from);

            if (switchVariance)
            {
                Func<Type, Type, bool> intermediate = covarianceCheck;
                covarianceCheck = contravarianceCheck;
                contravarianceCheck = intermediate;
            }

            // Simple hierarchy check.
            if (covarianceCheck(fromType, targetType))
            {
                return true;
            }

            // Interface check.
            if ((targetType.IsInterface && fromType.ImplementsInterface(targetType))
                || (sameHierarchy && fromType.IsInterface && targetType.ImplementsInterface(fromType)))
            {
                return true;
            }

            // Explicit value type conversions (including enums).
            if (sameHierarchy && (fromType.IsValueType && targetType.IsValueType))
            {
                return true;
            }

            // Recursively verify when it is a generic type.
            if (targetType.IsGenericType)
            {
                Type genericDefinition = targetType.GetGenericTypeDefinition();
                Type sourceGeneric = fromType.GetMatchingGenericType(genericDefinition);

                // Delegates never support casting in the 'opposite' direction than their varience type parameters dictate.
                bool cast = fromType.IsDelegate() ? false : sameHierarchy;

                if (sourceGeneric != null) // Same generic types.
                {
                    // Check whether parameters correspond, taking into account variance rules.
                    return sourceGeneric.GetGenericArguments().Zip(
                        targetType.GetGenericArguments(), genericDefinition.GetGenericArguments(),
                        (from, to, generic)
                            => !(from.IsValueType || to.IsValueType)    // Variance applies only to reference types.
                                ? generic.GenericParameterAttributes.HasFlag(GenericParameterAttributes.Covariant)
                                    ? CanConvertTo(from, to, cast, false)
                                    : generic.GenericParameterAttributes.HasFlag(GenericParameterAttributes.Contravariant)
                                        ? CanConvertTo(from, to, cast, true)
                                        : false
                                : false)
                        .All(match => match);
                }
            }

            return false;
        }

        public static Type GetMatchingGenericType(this Type source, Type type)
        {
            Type[] genericArguments = type.GetGenericArguments();
            Type rawType = type.IsGenericType ? type.GetGenericTypeDefinition() : type;

            // Used to compare type arguments and see whether they match.
            Func<Type[], bool> argumentsMatch
                = arguments => genericArguments
                    .Zip(arguments, Tuple.Create)
                    .All(t => t.Item1.IsGenericParameter // No type specified.
                       || t.Item1 == t.Item2);

            Type matchingType = null;
            if (type.IsInterface && !source.IsInterface)
            {
                // Traverse across all interfaces to find a matching interface.
                matchingType = (
                    from t in source.GetInterfaces()
                    let rawInterface = t.IsGenericType ? t.GetGenericTypeDefinition() : t
                    where rawInterface == rawType && argumentsMatch(t.GetGenericArguments())
                    select t
                    ).FirstOrDefault();
            }
            else
            {
                // Traverse across the type, and all it's base types.
                Type baseType = source;
                while (baseType != null && baseType != typeof(object))
                {
                    Type rawCurrent = baseType.IsGenericType ? baseType.GetGenericTypeDefinition() : baseType;
                    if (rawType == rawCurrent)
                    {
                        // Same raw generic type, compare type arguments.
                        if (argumentsMatch(baseType.GetGenericArguments()))
                        {
                            matchingType = baseType;
                            break;
                        }
                    }
                    baseType = baseType.BaseType;
                }
            }

            return matchingType;
        }
        public static bool ImplementsInterface(this Type source, Type interfaceType)
        {
            return source.GetInterface(interfaceType.ToString()) != null;
        }
        public static bool IsInHierarchy(this Type source, Type type)
        {
            return source == type || source.IsSubclassOf(type) || type.IsSubclassOf(source);
        }
        public static bool IsDelegate(this Type source)
        {
            return source.IsSubclassOf(typeof(Delegate));
        }

        public static IEnumerable<TResult> Zip<TFirst, TSecond, TThird, TResult>(this IEnumerable<TFirst> first, IEnumerable<TSecond> second, IEnumerable<TThird> third, Func<TFirst, TSecond, TThird, TResult> resultSelector)
        {
            using (IEnumerator<TFirst> iterator1 = first.GetEnumerator())
            using (IEnumerator<TSecond> iterator2 = second.GetEnumerator())
            using (IEnumerator<TThird> iterator3 = third.GetEnumerator())
            {
                while (iterator1.MoveNext() && iterator2.MoveNext() && iterator3.MoveNext())
                {
                    yield return resultSelector(iterator1.Current, iterator2.Current, iterator3.Current);
                }
            }
        }
    }
}
