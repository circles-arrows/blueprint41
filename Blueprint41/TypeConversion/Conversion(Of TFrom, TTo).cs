using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.Core
{
    public abstract class Conversion<TFrom, TTo> : Conversion
    {
        public override Type FromType { get { return typeof(TFrom); } }
        public override Type ToType { get { return typeof(TTo); } }

        #region Initialize

        static private bool isInitialized = false;
        static private Func<TFrom, TTo>? converterMethod = null;

        static private void Initialize()
        {
            if (!isInitialized)
            {
                lock (typeof(Converter<TFrom, TTo>))
                {
                    if (!isInitialized)
                    {

                        Register();

                        // Nullable version of existing non-nullable conversion
                        if (converterMethod == null)
                        {
                            Type fromType = typeof(TFrom);
                            bool fromIsNullable = false;
                            Type toType = typeof(TTo);
                            bool toIsNullable = false;

                            if (fromType.IsGenericType && fromType.GetGenericTypeDefinition() == typeof(Nullable<>))
                            {
                                fromType = Nullable.GetUnderlyingType(fromType)!;
                                fromIsNullable = true;
                            }
                            
                            if (toType.IsGenericType && toType.GetGenericTypeDefinition() == typeof(Nullable<>))
                            {
                                toType = Nullable.GetUnderlyingType(toType)!;
                                toIsNullable = true;
                            }

                            if (fromIsNullable || toIsNullable)
                            {
                                Expression<Func<TFrom, TTo>>? uncompiledConvert = CreateNullableVersion(fromType, fromIsNullable, toType, toIsNullable);
                                Func<TFrom, TTo>? compiledConvert = null;
                                converterMethod = delegate (TFrom value)
                                {
                                    if (compiledConvert is null && !(uncompiledConvert is null))
                                        compiledConvert = uncompiledConvert.Compile();

                                    if (compiledConvert is null)
                                        return default(TTo)!;

                                    return compiledConvert.Invoke(value);
                                };
                            }
                        }

                        // Basic cast conversion
                        MethodInfo op_Impl_Expl = IsCastable();
                        if (converterMethod == null && op_Impl_Expl != null)
                        {
                            ParameterExpression fromParam = Expression.Parameter(typeof(TFrom), "value");
                            ParameterExpression toParam = Expression.Parameter(typeof(TTo));
                            Expression<Func<TFrom, TTo>> expression = (Expression<Func<TFrom, TTo>>)Expression.Lambda<Func<TFrom, TTo>>(Expression.Call(null, op_Impl_Expl, fromParam), fromParam);
                            converterMethod = expression.Compile();
                        }

                        // IConvertable
                        if (converterMethod == null && typeof(TFrom).GetTypeInfo().ImplementedInterfaces.Any(item => item == typeof(IConvertible)))
                            converterMethod = delegate (TFrom value) { return (TTo)System.Convert.ChangeType(value, typeof(TTo))!; };

                        isInitialized = true;
                    }
                }
            }
        }

        #region Standard Conversions

        static private MethodInfo IsCastable()
        {
            var methods =   typeof(TFrom).GetMethods(BindingFlags.Public | BindingFlags.Static)
                                .Where(
                                    m => m.ReturnType == typeof(TTo) && m.GetParameters().FirstOrDefault()?.ParameterType == typeof(TFrom) &&
                                        (m.Name == "op_Implicit" ||
                                        m.Name == "op_Explicit")
                                ).Union(
                            typeof(TTo).GetMethods(BindingFlags.Public | BindingFlags.Static)
                                .Where(
                                    m => m.ReturnType == typeof(TTo) && m.GetParameters().FirstOrDefault()?.ParameterType == typeof(TFrom) &&
                                        (m.Name == "op_Implicit" ||
                                        m.Name == "op_Explicit")
                                )
                            );
            return methods.FirstOrDefault();
        }
        private static Expression<Func<TFrom, TTo>>? CreateNullableVersion(Type fromType, bool fromIsNullable, Type toType, bool toIsNullable)
        {
            Type converterType = typeof(Conversion<,>).MakeGenericType(new Type[] { fromType, toType });
            MethodInfo canConvert = converterType.GetTypeInfo().DeclaredMethods.FirstOrDefault(item => item.Name == "CanConvert");

            object? canConv = canConvert.Invoke(null, new object[0]);
            if (canConv is null || !(canConv is bool))
                throw new NotSupportedException($"You cannot convert a {typeof(TFrom).Name} to a {typeof(TTo).Name}.");

            if ((bool)canConv == true)
            {
                MethodInfo convertMethodInfo = converterType.GetTypeInfo().DeclaredMethods.FirstOrDefault(item => item.Name == "Convert");
                PropertyInfo valuePropertyInfo = typeof(TFrom).GetTypeInfo().DeclaredProperties.FirstOrDefault(item => item.Name == "Value");
                PropertyInfo hasValuePropertyInfo = typeof(TFrom).GetTypeInfo().DeclaredProperties.FirstOrDefault(item => item.Name == "HasValue");

                ParameterExpression fromParam = Expression.Parameter(typeof(TFrom), "value");
                ParameterExpression toParam = Expression.Parameter(typeof(TTo));

                Expression fromValue = (fromIsNullable) ?
                                            (Expression)Expression.Property(
                                                    fromParam,
                                                    valuePropertyInfo
                                                ) :
                                            (Expression)fromParam;

                Expression elseExpr = (toIsNullable) ?
                                            (Expression)Expression.Convert(
                                                    Expression.Call(null, convertMethodInfo, fromValue),
                                                    typeof(TTo)
                                                ) :
                                            (Expression)Expression.Call(null, convertMethodInfo, fromValue);

                if (fromIsNullable || fromType.IsClass)
                {
                    LabelTarget label = Expression.Label(typeof(TTo));

                    var returnValue = Expression.Variable(typeof(TTo), "returnValue");

                    Expression thenExpr = (toIsNullable || toType.IsClass) ?
                                        (Expression)Expression.Assign(returnValue, Expression.Constant(default(TTo)!, typeof(TTo))) :
                                        (Expression)Expression.Throw(
                                                        Expression.Constant(new NullReferenceException($"Cannot convert null to '{typeof(TTo).Name}'"))
                                                    );

                    LambdaExpression expression = Expression.Lambda<Func<TFrom, TTo>>(
                            Expression.Block(typeof(TTo), new[] { returnValue },
                                    Expression.IfThenElse(
                                            (fromIsNullable) ? (Expression)Expression.Not(Expression.Property(fromParam, hasValuePropertyInfo)) :
                                                               (Expression)Expression.Equal(fromParam, Expression.Constant(null)),
                                            thenExpr,
                                            Expression.Assign(returnValue, elseExpr)
                                        ),
                                    returnValue
                                ),
                            fromParam
                        );

                    return (Expression<Func<TFrom, TTo>>)expression;
                }
                else
                {
                    LambdaExpression expression = Expression.Lambda<Func<TFrom, TTo>>(
                           elseExpr,
                           fromParam
                       );

                    return (Expression<Func<TFrom, TTo>>)expression;
                }
            }

            return null;
        }

        #endregion

        #region Custom Conversions

        static private void Register()
        {
            if (!registrationsLoaded)
            {
                lock (typeof(Conversion))
                {
                    if (!registrationsLoaded)
                    {
                        foreach (Assembly assembly in GetAssemblies())
                        {
                            if (assembly == null)
                                continue;

                            foreach (ConversionAttribute attribute in assembly.GetCustomAttributes<ConversionAttribute>())
                            {
                                Conversion converter = (Conversion)Activator.CreateInstance(attribute.Converter)!;
                                converter.RegisterConversion();
                                registeredConverters.Add(converter);
                            }
                        }

                        registrationsLoaded = true;
                    }
                }
            }            
        }
        public static IEnumerable<Assembly> GetAssemblies()
        {
            var list = new HashSet<string>();
            var stack = new Stack<Assembly>();

            stack.Push(typeof(Conversion).Assembly);
            Assembly? entry = Assembly.GetEntryAssembly();
            if (!(entry is null))
                stack.Push(entry);

            do
            {
                var asm = stack.Pop();

                yield return asm;

                foreach (var reference in asm.GetReferencedAssemblies())
                {
                    if (reference.FullName.StartsWith("mscorlib") || reference.FullName.StartsWith("netstandard") || reference.FullName.StartsWith("System") || reference.FullName.StartsWith("Microsoft") || reference.FullName.StartsWith("Xml.Schema"))
                        continue;

                    if (!list.Contains(reference.FullName))
                    {
                        try
                        {
                            Assembly a = Assembly.Load(reference);
                            stack.Push(a);
                            list.Add(reference.FullName);
                        }
                        catch (FileNotFoundException)
                        {
                            // This is relatively normal... This usually happens when
                            // a referenced assembly is not in the active code path
                            // and therefore was not included in the deployment.
                        }
                    }
                }
            }
            while (stack.Count > 0);
        }

        [return: MaybeNull]
        protected abstract TTo Converter([AllowNull] TFrom value);
        internal override void RegisterConversion()
        {
            converterMethod = Converter!;
        }

        #endregion

        #endregion

        [return: MaybeNull]
        static public TTo Convert([AllowNull] TFrom value)
        {
            Initialize();

            if (converterMethod != null)
                return converterMethod.Invoke(value!);

            throw new InvalidCastException(string.Format("Conversion from '{0}' to '{1}' is not supported.", typeof(TFrom).Name, typeof(TTo).Name));
        }
        static public bool CanConvert()
        {
            Initialize();

            return (converterMethod != null);
        }
        internal override bool IsValidConversion()
        {
            return CanConvert();
        }

        public sealed override object? Convert(object? value)
        {
            return Convert((TFrom)value!);
        }
    }

    internal class ConversionInstance<TFrom, TTo> : Conversion<TFrom, TTo>
    {
        private ConversionInstance()
        {
            // Effectively make this unusable.. except via reflection...
        }

        [return: MaybeNull]
        protected override TTo Converter([AllowNull] TFrom value)
        {
            throw new NotImplementedException();
        }
        sealed internal override void RegisterConversion()
        {
        }
    }
}
