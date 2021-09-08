using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blueprint41.Core;
using System.Reflection;
using System.Collections.ObjectModel;
using Blueprint41;
using System.Runtime.Serialization.Json;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace System.Linq
{
    public static partial class ExtensionMethods
    {
        public static string ToCSharp(this Type type, bool nullable = false)
        {
            CodeDomProvider csharpProvider = CodeDomProvider.CreateProvider("C#");
            CodeTypeReference typeReference = new CodeTypeReference(type);
            CodeVariableDeclarationStatement variableDeclaration = new CodeVariableDeclarationStatement(typeReference, "dummy");
            StringBuilder sb = new StringBuilder();
            using (StringWriter writer = new StringWriter(sb))
            {
                csharpProvider.GenerateCodeFromStatement(variableDeclaration, writer, new CodeGeneratorOptions());
            }
            sb.Replace(" dummy;\r\n", null);
            if (nullable && type.IsValueType)
                return sb.ToString() + "?";
            return sb.ToString();
        }

        public static bool IsSubclassOfOrSelf(this Type self, Type type)
        {
            if (self == type)
                return true;

            return self.IsSubclassOf(type);
        }

        #region Hashset Extensions

        private static class HashSetDelegateHolder<T>
        {
            private const BindingFlags Flags = BindingFlags.Instance | BindingFlags.NonPublic;
            private static MethodInfo InitializeMethodInfo = typeof(HashSet<T>).GetMethod("Initialize", Flags)!;
            public static Action<HashSet<T>, int> InitializeMethod { get; } = (Action<HashSet<T>, int>)Delegate.CreateDelegate(typeof(Action<HashSet<T>, int>), InitializeMethodInfo);

        }

        #endregion

        public static void ForEach<T>(this IEnumerable<T> items, Action<T> action)
        {
            if (action is null)
                return;
            
            foreach (T item in items)
                action.Invoke(item);
        }
        public static IEnumerable<ReadOnlyCollection<T>> Chunks<T>(this IEnumerable<T> source, int pageSize)
        {
            using (var enumerator = source.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    var currentPage = new List<T>(pageSize)
            {
                enumerator.Current
            };

                    while (currentPage.Count < pageSize && enumerator.MoveNext())
                    {
                        currentPage.Add(enumerator.Current);
                    }
                    yield return new ReadOnlyCollection<T>(currentPage);
                }
            }
        }

        internal static bool IsCompatible(this RelationshipType self, RelationshipType other)
        {
            if (self == RelationshipType.None || other == RelationshipType.None)
                return true;

            switch (self)
            {
                case RelationshipType.Collection:
                    {
                        switch (other)
                        {
                            case RelationshipType.Collection:
                            case RelationshipType.Collection_Collection:
                            case RelationshipType.Collection_Lookup:
                                return true;
                            default:
                                return false;
                        }
                    }
                case RelationshipType.Collection_Collection:
                    {
                        switch (other)
                        {
                            case RelationshipType.Collection:
                            case RelationshipType.Collection_Collection:
                                return true;
                            default:
                                return false;
                        }
                    }
                case RelationshipType.Collection_Lookup:
                    {
                        switch (other)
                        {
                            case RelationshipType.Collection:
                            case RelationshipType.Collection_Lookup:
                                return true;
                            default:
                                return false;
                        }
                    }
                case RelationshipType.Lookup:
                    {
                        switch (other)
                        {
                            case RelationshipType.Lookup:
                            case RelationshipType.Lookup_Collection:
                            case RelationshipType.Lookup_Lookup:
                                return true;
                            default:
                                return false;
                        }
                    }
                case RelationshipType.Lookup_Collection:
                    {
                        switch (other)
                        {
                            case RelationshipType.Lookup:
                            case RelationshipType.Lookup_Collection:
                                return true;
                            default:
                                return false;
                        }
                    }
                case RelationshipType.Lookup_Lookup:
                    {
                        switch (other)
                        {
                            case RelationshipType.Lookup:
                            case RelationshipType.Lookup_Lookup:
                                return true;
                            default:
                                return false;
                        }
                    }
                default:
                    throw new NotImplementedException();
            }
        }
        internal static bool IsImplicitLookup(this RelationshipType self)
        {
            switch (self)
            {
                case RelationshipType.None:
                case RelationshipType.Collection:
                case RelationshipType.Collection_Collection:
                    return false;
                case RelationshipType.Collection_Lookup:
                case RelationshipType.Lookup:
                case RelationshipType.Lookup_Collection:
                case RelationshipType.Lookup_Lookup:
                    return true;
                default:
                    throw new NotImplementedException();
            }
        }
        internal static bool IsImplicitCollection(this RelationshipType self)
        {
            switch (self)
            {
                case RelationshipType.None:
                case RelationshipType.Collection:
                case RelationshipType.Collection_Collection:
                case RelationshipType.Collection_Lookup:
                case RelationshipType.Lookup:
                case RelationshipType.Lookup_Collection:
                    return true;
                case RelationshipType.Lookup_Lookup:
                    return false;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
namespace System
{
    public static partial class ExtensionMethods
    {
        public static bool IsMin(this DateTime self) => (Conversion.MinDateTime - self).TotalDays >= -1;
        public static bool IsMin(this DateTime? self) => (self.HasValue) ? IsMin(self) : true;
        public static bool IsMax(this DateTime self) => (Conversion.MaxDateTime - self).TotalDays <= 1;
        public static bool IsMax(this DateTime? self) => (self.HasValue) ? IsMax(self) : true;

        public static string? ToJson<T>(this T self)
        {
            if (self is null)
                return null;

            using (MemoryStream writer = new MemoryStream())
            {
                Cache<T>.JsonSerializer.WriteObject(writer, self);
                return Encoding.UTF8.GetString(writer.ToArray());
            }
        }
        
        [return: MaybeNull]
        public static T FromJson<T>(this string self)
        {
            if (self is null)
                return default!;

            using (MemoryStream reader = new MemoryStream(Encoding.UTF8.GetBytes(self)))
            {
                return (T)Cache<T>.JsonSerializer.ReadObject(reader);
            }
        }

        [return: MaybeNull]
        public static T FromJson<T>(this CompressedString self)
        {
            if (self is null)
                return default!;

            return FromJson<T>(self.Value);
        }

        private class Cache<T>
        {
            public static DataContractJsonSerializer JsonSerializer = new DataContractJsonSerializer(typeof(T));
        }

        public static IEnumerable<T> NotNull<T>(this IEnumerable<T?> self)
            where T : class
        {
            return self.Where(item => !(item is null))!;
        }

        public static IEnumerable<object> NotNull(this IEnumerable self)
        {
            return self.Cast<object?>().Where(item => !(item is null))!;
        }

        //public static T GetTaskResult<T>(this Task<T> self) => self.GetAwaiter().GetResult();
        public static T GetTaskResult<T>(this Task<T> self) => AsyncHelper.RunSync(() => self);

        public static Task Continue(this Task self, Action callback)
        {
            return self.ContinueWith(task => callback.Invoke());
        }
        public static Task<TReturn> Continue<TReturn>(this Task self, Func<TReturn> callback)
        {
            return self.ContinueWith(task => callback.Invoke());
        }
        public static Task Continue<T>(this Task<T> self, Action<T> callback)
        {
            return self.ContinueWith(task => callback.Invoke(task.GetAwaiter().GetResult()));
        }
        public static Task<TReturn> Continue<T, TReturn>(this Task<T> self, Func<T, TReturn> callback)
        {
            return self.ContinueWith(task => callback.Invoke(task.GetAwaiter().GetResult()));
        }
    }
}
namespace Blueprint41.Core
{
    public static partial class ExtensionMethods
    {
        private static readonly TypeInfo EnumerableTypeInfo = typeof(IEnumerable).GetTypeInfo();

        private static readonly TypeInfo DictionaryTypeInfo = typeof(IDictionary).GetTypeInfo();

        [return: NotNullIfNotNull("value")]
        public static T As<T>(this object value)
        {
            if (value is null)
            {
                if (default(T) is null)
                {
                    return default!;
                }
                throw new InvalidCastException($"Unable to cast `null` to `{typeof(T)}`.");
            }
            object obj;
            if ((obj = value) is T)
            {
                return (T)obj;
            }

            if (AsRegistered(value, out T converted))
                return converted;

            Type type = value.GetType();
            Type type2 = Nullable.GetUnderlyingType(typeof(T)) ?? typeof(T);
            if (type2 == typeof(string))
            {
                return value.ToString().AsItIs<T>();
            }
            if (type2 == typeof(short))
            {
                return Convert.ToInt16(value).AsItIs<T>();
            }
            if (type2 == typeof(int))
            {
                return Convert.ToInt32(value).AsItIs<T>();
            }
            if (type2 == typeof(long))
            {
                return Convert.ToInt64(value).AsItIs<T>();
            }
            if (type2 == typeof(float))
            {
                return Convert.ToSingle(value, CultureInfo.InvariantCulture).AsItIs<T>();
            }
            if (type2 == typeof(double))
            {
                return Convert.ToDouble(value, CultureInfo.InvariantCulture).AsItIs<T>();
            }
            if (type2 == typeof(sbyte))
            {
                return Convert.ToSByte(value).AsItIs<T>();
            }
            if (type2 == typeof(ulong))
            {
                return Convert.ToUInt64(value).AsItIs<T>();
            }
            if (type2 == typeof(uint))
            {
                return Convert.ToUInt32(value).AsItIs<T>();
            }
            if (type2 == typeof(ushort))
            {
                return Convert.ToUInt16(value).AsItIs<T>();
            }
            if (type2 == typeof(byte))
            {
                return Convert.ToByte(value).AsItIs<T>();
            }
            if (type2 == typeof(char))
            {
                return Convert.ToChar(value).AsItIs<T>();
            }
            if (type2 == typeof(bool))
            {
                return Convert.ToBoolean(value).AsItIs<T>();
            }
            if (value is IConvertible)
            {
                return Convert.ChangeType(value, type2).AsItIs<T>();
            }
            TypeInfo typeInfo = type2.GetTypeInfo();
            IDictionary? dict;
            if (DictionaryTypeInfo.IsAssignableFrom(typeInfo) && typeInfo.IsGenericType && (dict = (value as IDictionary)) is not null)
            {
                return dict.AsDictionary<T>(typeInfo);
            }
            IEnumerable? value2;
            if (EnumerableTypeInfo.IsAssignableFrom(typeInfo) && typeInfo.IsGenericType && (value2 = (value as IEnumerable)) is not null)
            {
                return value2.AsList<T>(typeInfo);
            }
            throw new InvalidCastException($"Unable to cast object of type `{type}` to type `{typeof(T)}`.");
        }

        [return: NotNullIfNotNull("value")]
        internal static T ValueAs<T>(this object value)
        {
            return value.As<T>();
        }

        [return: NotNullIfNotNull("dict")]
        private static T AsDictionary<T>(this IDictionary dict, TypeInfo typeInfo)
        {
            Type[] genericTypeArguments = typeInfo.GenericTypeArguments;
            IDictionary dictionary = (IDictionary)Activator.CreateInstance(typeof(Dictionary<,>).MakeGenericType(genericTypeArguments));
            MethodInfo invocableAsMethod = GetInvocableAsMethod(genericTypeArguments[0]);
            MethodInfo invocableAsMethod2 = GetInvocableAsMethod(genericTypeArguments[1]);
            IDictionaryEnumerator enumerator = dict.GetEnumerator();
            while (enumerator.MoveNext())
            {
                dictionary.Add(invocableAsMethod.InvokeStatic(enumerator.Key), invocableAsMethod2.InvokeStatic(enumerator.Value));
            }
            return dictionary.AsItIs<T>();
        }

        [return: NotNullIfNotNull("value")]
        private static T AsList<T>(this IEnumerable value, TypeInfo typeInfo)
        {
            Type[] genericTypeArguments = typeInfo.GenericTypeArguments;
            IList list = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(genericTypeArguments));
            MethodInfo invocableAsMethod = GetInvocableAsMethod(genericTypeArguments[0]);
            foreach (object item in value)
            {
                list.Add(invocableAsMethod.InvokeStatic(item));
            }
            return list.AsItIs<T>();
        }

        [return: MaybeNull]
        private static object InvokeStatic(this MethodInfo method, params object[] parameters)
        {
            return method.Invoke(null, parameters);
        }

        private static MethodInfo GetInvocableAsMethod(params Type[] genericParameters)
        {
            return typeof(ExtensionMethods).GetRuntimeMethod("As", genericParameters).MakeGenericMethod(genericParameters);
        }

        [return: NotNullIfNotNull("value")]
        private static T AsItIs<T>(this object value)
        {
            object obj;
            if ((obj = value) is T)
            {
                return (T)obj;
            }
            throw new InvalidOperationException($"The expected value `{typeof(T)}` is different from the actual value `{value.GetType()}`");
        }

        [return: MaybeNull]
        private static bool AsRegistered<T>(this object instance, [NotNullWhen(true)] out T value)
        {
            if (instance is null)
                throw new NullReferenceException("this");

            if (specificConversions.TryGetValue(typeof(T), out var conversions))
            {
                foreach ((Type provider, Func<object, object?> conversion) in conversions)
                {
                    object? converted = conversion.Invoke(instance);
                    if (converted is not null)
                    {
                        value = (T)converted;
                        return true;
                    }
                }
            }

            value = default!;
            return false;
        }

        private static AtomicDictionary<Type, List<(Type provider, Func<object, object?> conversion)>> specificConversions = new AtomicDictionary<Type, List<(Type provider, Func<object, object?> conversion)>>();

        public static void RegisterAsConversion(Type persistenceProvider, Type type, Func<object, object?> conversion)
        {
            List<(Type provider, Func<object, object?> conversion)> conversions;
            if (type is not null)
            {
                conversions = specificConversions.TryGetOrAdd(type, key => new List<(Type provider, Func<object, object?> conversion)>());
                if (!conversions.Any(item => item.provider == persistenceProvider))
                    conversions.Add((persistenceProvider, conversion));
            }
        }
    }
}