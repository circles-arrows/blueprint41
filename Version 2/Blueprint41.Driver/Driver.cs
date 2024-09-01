﻿#pragma warning disable S3881

using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CSharp;

using Blueprint41.Core;

namespace Blueprint41.Driver
{
    public sealed class Driver : IDisposable, IAsyncDisposable
    {
        internal Driver(object instance)
        {
            _instance = instance;
        }
        internal object _instance { get; private set; }

        public static Driver Get(Uri uri, AuthToken authToken, Action<ConfigBuilder> configBuilder)
        {
            return GRAPH_DATABASE.Driver(uri, authToken, configBuilder);
        }

        public Session AsyncSession() => I_DRIVER.AsyncSession(this);
        public Session AsyncSession(Action<SessionConfigBuilder> configBuilder) => I_DRIVER.AsyncSession(this, configBuilder);

        public static void Configure<TDriverInterface>()
        {
            DriverTypeInfo.SearchAssembly = typeof(TDriverInterface).Assembly;

            if (typeof(TDriverInterface) != I_DRIVER.Type)
                throw new InvalidOperationException($"Please pass type of {I_DRIVER.Names} as generic argument TDriverInterface.");

            foreach (FieldInfo field in typeof(Driver).GetFields(BindingFlags.NonPublic | BindingFlags.Static).Where(item => typeof(DriverTypeInfo).IsAssignableFrom(item.FieldType) && item.Name == item.Name.ToUpperInvariant()))
            {
                DriverTypeInfo typeInfo = (DriverTypeInfo)field.GetValue(null);
                if (!typeInfo.Exists)
                    throw new TypeLoadException(typeInfo.Names);

                Dictionary<string, List<Member>> fieldGroups = new Dictionary<string, List<Member>>();

                foreach (FieldInfo member in typeInfo.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance).Where(item => item.FieldType.IsGenericType && item.FieldType.GenericTypeArguments.Length == 1 && item.FieldType.Name == "Lazy`1" && typeof(Member).IsAssignableFrom(item.FieldType.GenericTypeArguments[0]) && item.Name.StartsWith("_")))
                {
                    OneOfAttribute? oneOf = (OneOfAttribute?)member.GetCustomAttribute(typeof(OneOfAttribute));

                    Member memberInfo = (Member)((dynamic)member.GetValue(typeInfo)).Value;
                    if (oneOf is null && !memberInfo.Exists)
                    {
                        throw new MissingMemberException($"Member {string.Join("or ", memberInfo.Names.Select(name => $"'{name}'"))} missing on type {typeInfo.Names}.");
                    }
                    else if (oneOf is not null)
                    {
                        List<Member> group;
                        if (!fieldGroups.TryGetValue(oneOf.Group, out group))
                        {
                            group = new List<Member>();
                            fieldGroups.Add(oneOf.Group, group);
                        }
                        group.Add(memberInfo);
                    }
                }

                foreach (KeyValuePair<string, List<Member>> group in fieldGroups)
                {
                    if (!group.Value.Exists(item => item.Exists))
                        throw new MissingMemberException($"At least 1 member from the group '{group.Key}' must exist on type {typeInfo.Names}.");
                }
            }
        }

        #region DriverTypeInfo definitions 

        internal static readonly IDriverInfo                  I_DRIVER                             = new IDriverInfo("Neo4j.Driver.IDriver");
        internal static readonly ConfigBuilderInfo            CONFIG_BUILDER                       = new ConfigBuilderInfo("Neo4j.Driver.ConfigBuilder");
        internal static readonly GraphDatabaseInfo            GRAPH_DATABASE                       = new GraphDatabaseInfo("Neo4j.Driver.GraphDatabase");
        internal static readonly AuthTokensInfo               AUTH_TOKENS                          = new AuthTokensInfo("Neo4j.Driver.AuthTokens");
        internal static readonly IResultCursorInfo            I_RESULT_CURSOR                      = new IResultCursorInfo("Neo4j.Driver.IResultCursor");
        internal static readonly IResultSummaryInfo           I_RESULT_SUMMARY                     = new IResultSummaryInfo("Neo4j.Driver.IResultSummary");
        internal static readonly IRecord                      I_RECORD                             = new IRecord("Neo4j.Driver.IRecord");
        internal static readonly IAsyncSessionInfo            I_ASYNC_SESSION                      = new IAsyncSessionInfo("Neo4j.Driver.IAsyncSession");
        internal static readonly SessionConfigBuilderInfo     SESSION_CONFIG_BUILDER               = new SessionConfigBuilderInfo("Neo4j.Driver.SessionConfigBuilder");
        internal static readonly IAsyncTransactionInfo        I_ASYNC_TRANSACTION                  = new IAsyncTransactionInfo("Neo4j.Driver.IAsyncTransaction");
        internal static readonly TransactionConfigBuilderInfo TRANSACTION_CONFIG_BUILDER           = new TransactionConfigBuilderInfo("Neo4j.Driver.TransactionConfigBuilder");
        internal static readonly AccessModeInfo               ACCESS_MODE                          = new AccessModeInfo("Neo4j.Driver.AccessMode");
        internal static readonly BookmarksInfo                BOOKMARKS                            = new BookmarksInfo("Neo4j.Driver.Bookmarks", "Neo4j.Driver.Bookmark");
        internal static readonly QueryInfo                    QUERY                                = new QueryInfo("Neo4j.Driver.Query");
        internal static readonly ICountersInfo                I_COUNTERS                           = new ICountersInfo("Neo4j.Driver.ICounters");
        internal static readonly INotificationInfo            I_NOTIFICATION                       = new INotificationInfo("Neo4j.Driver.INotification");
        internal static readonly IInputPositionInfo           I_INPUT_POSITION                     = new IInputPositionInfo("Neo4j.Driver.IInputPosition");
        internal static readonly ValueExtensionsInfo          VALUE_EXTENSIONS                     = new ValueExtensionsInfo("Neo4j.Driver.ValueExtensions");
        internal static readonly INodeInfo                    I_NODE                               = new INodeInfo("Neo4j.Driver.INode");
        internal static readonly IRelationshipInfo            I_RELATIONSHIP                       = new IRelationshipInfo("Neo4j.Driver.IRelationship");
        internal static readonly EncryptionLevelInfo          ENCRYPTION_LEVEL                     = new EncryptionLevelInfo("Neo4j.Driver.EncryptionLevel");
        internal static readonly ServerAddressInfo            SERVER_ADDRESS                       = new ServerAddressInfo("Neo4j.Driver.ServerAddress");
        internal static readonly TrustManagerInfo             TRUST_MANAGER                        = new TrustManagerInfo("Neo4j.Driver.TrustManager");


        internal static readonly IServerAddressResolverInfo   I_SERVER_ADDRESS_RESOLVER            = new IServerAddressResolverInfo("Neo4j.Driver.IServerAddressResolver");

        internal static readonly DriverTypeInfo               I_AUTH_TOKEN                         = new DriverTypeInfo("Neo4j.Driver.IAuthToken");
        internal static readonly DriverTypeInfo               SERVER_ADDRESS_RESOLVER_PROXY        = new DriverTypeInfo(RuntimeCodeGen.MakeServerAddressResolverProxy);
        //Logger & ILogger

        // NOT SUPPORTED IN DRIVER v4.4

        //internal static readonly CategoryInfo                 CATEGORY                             = new CategoryInfo("Neo4j.Driver.Category");
        //internal static readonly SeverityInfo                 SEVERITY                             = new SeverityInfo("Neo4j.Driver.Severity");
        //internal static readonly CertificateTrustRuleInfo     CERTIFICATE_TRUST_RULE               = new CertificateTrustRuleInfo("Neo4j.Driver.CertificateTrustRule");

        #endregion

        #region Type, Property, Method & Enum helpers

        internal static class Generic
        {
            internal static DriverTypeInfo Of(Type type, params DriverTypeInfo[] args) => new DriverTypeInfo(type.MakeGenericType(args.Select(item => item.Type).ToArray()));
        }
        internal static class Type<T>
        {
            internal static readonly DriverTypeInfo Info = new DriverTypeInfo(typeof(T));
        }
        [AttributeUsage(AttributeTargets.Field)]
        internal class OneOfAttribute : Attribute
        {
            public OneOfAttribute(string group)
            {
                Group = group;
            }

            public string Group { get; private set; }
        }
        internal static class RuntimeCodeGen
        {
            public static Type MakeServerAddressResolverProxy()
            {
                string code = """
                    using System;
                    using System.Linq;
                    using System.Collections.Generic;
                    using System.Reflection;

                    using neo4j = Neo4j.Driver;
                    using bp41 = Blueprint41.Driver;

                    namespace Blueprint41.Driver.RuntimeGeneration
                    {
                        public class ServerAddressResolverProxy : neo4j.IServerAddressResolver
                        {
                            public static ServerAddressResolverProxy Get(bp41.ServerAddressResolver instance) => new ServerAddressResolverProxy() { _instance = instance };
                            private bp41.ServerAddressResolver _instance;

                            public ISet<neo4j.ServerAddress> Resolve(neo4j.ServerAddress address)
                            {
                                ISet<bp41.ServerAddress> result = _instance.Resolve(new ServerAddress(address));

                                return new HashSet<neo4j.ServerAddress>(result.Select(item => (neo4j.ServerAddress)item._instance));
                            }
                        }
                    }
                    """;

                CompilerParameters options = new CompilerParameters();
                options.GenerateExecutable = false;
                options.GenerateInMemory = true;
                options.CoreAssemblyFileName = typeof(object).Assembly.Location;
                var netstandard = Assembly.Load("netstandard, Version=2.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51");
                options.ReferencedAssemblies.Add(netstandard.Location);
                options.ReferencedAssemblies.Add(typeof(ISet<>).Assembly.Location);
                options.ReferencedAssemblies.Add(typeof(Enumerable).Assembly.Location);
                options.ReferencedAssemblies.Add(typeof(Driver).Assembly.Location);
                options.ReferencedAssemblies.Add(I_DRIVER.Type.Assembly.Location);
                options.OutputAssembly = "Blueprint41.Driver.RuntimeGeneration.dll";

                CSharpCodeProvider provider = new CSharpCodeProvider();
                CompilerResults compile = provider.CompileAssemblyFromSource(options, code);

                return compile.CompiledAssembly.GetType("Blueprint41.Driver.RuntimeGeneration.ServerAddressResolverProxy");

                //object abc = Activator.CreateInstance(type);

                //MethodInfo method = type.GetMethod("Get");
                //object result = method.Invoke(abc, null);

                //Console.WriteLine(result); //output: abc

                //return type;
            }

        }

        internal class DriverTypeInfo
        {
            internal DriverTypeInfo(params string[] names)
            {
                _typeInit = new Lazy<Type?>(delegate ()
                {
                    foreach (string name in names)
                    {
                        Type? type = SearchAssembly?.GetType(name, false, false);
                        if (type is not null)
                            return type;
                    }
                    return null;
                }, true);
                Names = string.Join("or ", names.Select(name => $"'{name}'"));

                _array = InitArray();
            }
            internal DriverTypeInfo(Type type)
            {
                _typeInit = new Lazy<Type?>(() => type, true);
                Names = null;

                _array = InitArray();
            }
            internal DriverTypeInfo(Func<Type?> typeInitializer)
            {
                _typeInit = new Lazy<Type?>(typeInitializer, true);
                Names = null;

                _array = InitArray();
            }
            private Lazy<DriverTypeInfo> InitArray()
            {
                return new Lazy<DriverTypeInfo>(delegate ()
                {
                    if (Type.IsArray)
                        return this;

                    return new DriverTypeInfo(() => Type.MakeArrayType());
                });
            }

            public Type Type => _typeInit.Value ?? Throw();
            private readonly Lazy<Type?> _typeInit;

            public bool Exists => (_typeInit.Value is not null);

            public string? Names { get; private set; }

            internal static Assembly? SearchAssembly { get; set; }
            private Type Throw()
            {
                if (SearchAssembly is null)
                    throw new InvalidOperationException("Please configure which Neo4j.Driver to use by running 'Driver.Configure<IDriver>();' first.");

                if (Names is not null)
                    throw new TypeLoadException($"Not a compatible driver, type {Names} is missing.");

                throw new TypeLoadException($"Custom initialization logic failed to initialize the type.");
            }


#nullable disable
            protected static Task AsTask(object value)
            {
                if (value is not Task)
                    throw new InvalidOperationException("Not a task.");

                return (Task)value;
            }
            protected static async Task<T> AsTask<T>(object value, Func<object, T> wrapper = null)
            {
                if (value is not Task)
                    throw new InvalidOperationException("Not a task.");

                Task task = (Task)value;
                await task.ConfigureAwait(false);

                if (wrapper is null)
                {
                    // Harvest the result. Ugly but works
                    return (T)((dynamic)task).Result;
                }
                else
                {
                    // Harvest and wrap the result. Ugly but works
                    return wrapper.Invoke((object)((dynamic)task).Result);
                }
            }

            public Array ToArray(object items, int? count = null)
            {
                if (items is null)
                    throw new ArgumentNullException("items");

                Type type = items.GetType();
                if (typeof(Array).IsAssignableFrom(type))
                    return ToArray((Array)items);
                else if (typeof(IList).IsAssignableFrom(type))
                    return ToArray((IList)items);
                else if (typeof(IEnumerable).IsAssignableFrom(type))
                    return ToArray((IEnumerable)items, count);
                else
                    throw new NotSupportedException();
            }
            public Array ToArray(Array items)
            {
                Array array = CreateArray(Type, items.Length);

                for (int index = 0; index < items.Length; index++)
                    array.SetValue(items.GetValue(index), index);

                return array;
            }
            public Array ToArray(IList items)
            {
                Array array = CreateArray(Type, items.Count);

                for (int index = 0; index < items.Count; index++)
                    array.SetValue(items[index], index);

                return array;
            }
            public Array ToArray(IEnumerable items, int? count = null)
            {
                Array array = CreateArray(Type, count ?? items.Cast<object>().Count());

                int index = 0;
                foreach (object item in items)
                    array.SetValue(item, index++);

                return array;
            }
            public T[] ToArray<T>(object items, Func<object, T> wrapper, int? count = null)
            {
                if (items is null)
                    throw new ArgumentNullException("items");

                Type type = items.GetType();
                if (typeof(Array).IsAssignableFrom(type))
                    return ToArray((Array)items, wrapper);
                else if (typeof(IList).IsAssignableFrom(type))
                    return ToArray((IList)items, wrapper);
                else if (typeof(IEnumerable).IsAssignableFrom(type))
                    return ToArray((IEnumerable)items, wrapper, count);
                else
                    throw new NotSupportedException();
            }
            public T[] ToArray<T>(Array items, Func<object, T> wrapper)
            {
                T[] array = CreateArray<T>(items.Length);

                for (int index = 0; index < items.Length; index++)
                    array.SetValue(wrapper.Invoke(items.GetValue(index)), index);

                return array;
            }
            public T[] ToArray<T>(IList items, Func<object, T> wrapper)
            {
                T[] array = CreateArray<T>(items.Count);

                for (int index = 0; index < items.Count; index++)
                    array.SetValue(wrapper.Invoke(items[index]), index);

                return array;
            }
            public T[] ToArray<T>(IEnumerable items, Func<object, T> wrapper, int? count = null)
            {
                T[] array = CreateArray<T>(count ?? items.Cast<object>().Count());

                int index = 0;
                foreach (object item in items)
                    array.SetValue(wrapper.Invoke(item), index++);

                return array;
            }

            public IList ToList(object items, int? count = null)
            {
                if (items is null)
                    throw new ArgumentNullException("items");

                Type type = items.GetType();
                if (typeof(Array).IsAssignableFrom(type))
                    return ToList((Array)items);
                else if (typeof(IList).IsAssignableFrom(type))
                    return ToList((IList)items);
                else if (typeof(IEnumerable).IsAssignableFrom(type))
                    return ToList((IEnumerable)items, count);
                else
                    throw new NotSupportedException();
            }
            public IList ToList(Array items)
            {
                IList list = CreateList(Type, items.Length);

                for (int index = 0; index < items.Length; index++)
                    list.Add(items.GetValue(index));

                return list;
            }
            public IList ToList(IList items)
            {
                IList list = CreateList(Type, items.Count);

                for (int index = 0; index < items.Count; index++)
                    list.Add(items[index]);

                return list;
            }
            public IList ToList(IEnumerable items, int? count = null)
            {
                IList list = CreateList(Type, count);

                foreach (object item in items)
                    list.Add(item);

                return list;
            }
            public List<T> ToList<T>(object items, Func<object, T> wrapper, int? count = null)
            {
                if (items is null)
                    throw new ArgumentNullException("items");

                Type type = items.GetType();
                if (typeof(Array).IsAssignableFrom(type))
                    return ToList((Array)items, wrapper);
                else if (typeof(IList).IsAssignableFrom(type))
                    return ToList((IList)items, wrapper);
                else if (typeof(IEnumerable).IsAssignableFrom(type))
                    return ToList((IEnumerable)items, wrapper, count);
                else
                    throw new NotSupportedException();
            }
            public List<T> ToList<T>(Array items, Func<object, T> wrapper)
            {
                List<T> list = CreateList<T>(items.Length);

                for (int index = 0; index < items.Length; index++)
                    list.Add(wrapper.Invoke(items.GetValue(index)));

                return list;
            }
            public List<T> ToList<T>(IList items, Func<object, T> wrapper)
            {
                List<T> list = CreateList<T>(items.Count);

                for (int index = 0; index < items.Count; index++)
                    list.Add(wrapper.Invoke(items[index]));

                return list;
            }
            public List<T> ToList<T>(IEnumerable items, Func<object, T> wrapper, int? count = null)
            {
                List<T> list = CreateList<T>(count);

                foreach (object item in items)
                    list.Add(wrapper.Invoke(item));

                return list;
            }

            private Array CreateArray(Type type, int count)
            {
                return Array.CreateInstance(type, count);
            }
            private T[] CreateArray<T>(int count)
            {
                return (T[])Array.CreateInstance(typeof(T), count);
            }
            private IList CreateList(Type type, int? count)
            {
                Type genericListType = typeof(List<>).MakeGenericType(type);
                IList list = (count.HasValue) ? (IList)Activator.CreateInstance(genericListType, count) : (IList)Activator.CreateInstance(genericListType);
                return list;
            }
            private List<T> CreateList<T>(int? count)
            {
                Type genericListType = typeof(List<>).MakeGenericType(typeof(T));
                List<T> list = (count.HasValue) ? (List<T>)Activator.CreateInstance(genericListType, count) : (List<T>)Activator.CreateInstance(genericListType);
                return list;
            }

#nullable enable

            public DriverTypeInfo ARRAY => _array.Value;
            private readonly Lazy<DriverTypeInfo> _array;
        }
        internal abstract class Member
        {
            protected Member(DriverTypeInfo parent, DriverTypeInfo? returnType, string[] names, DriverTypeInfo?[]? arguments = null)
            {
                Parent = parent;
                ReturnType = returnType ?? parent;
                Names = names;
                Arguments = (arguments is null) ? new DriverTypeInfo[0] : arguments.Cast<DriverTypeInfo>().ToArray();
                IsEnum = (returnType is null);

                if (arguments is not null && Array.Exists(arguments, item => item is null))
                    throw new InvalidOperationException("Argument types can never be defined as null");

                if (returnType is null && arguments is not null)
                    throw new InvalidOperationException("Enum must have both returnType and arguments set to null");
            }

            public DriverTypeInfo Parent { get; private set; }
            public DriverTypeInfo ReturnType { get; private set; }
            public string[] Names { get; private set; }
            public DriverTypeInfo[] Arguments { get; private set; }
            public bool IsEnum { get; private set; }

            public abstract bool Exists { get; }

            protected MemberType GetMemberType<T>() where T : Member => GetMemberType(typeof(T));
            protected MemberType GetMemberType(Type type)
            {
                if (type.IsAssignableFrom(typeof(StaticMethod)) || type.IsAssignableFrom(typeof(InstanceMethod)))
                    return MemberType.Method;
                else if (type.IsAssignableFrom(typeof(StaticProperty)) || type.IsAssignableFrom(typeof(InstanceProperty)))
                    return MemberType.Property;
                else if (type.IsAssignableFrom(typeof(EnumField)))
                    return MemberType.EnumValue;
                else if (type.IsAssignableFrom(typeof(Generic)))
                    return MemberType.Generic;
                else
                    throw new NotSupportedException();
            }

            protected MethodInfo? GetMethodInfo(BindingFlags flags, Type declaringType, Type returnType, string name, Type[] arguments)
            {
                foreach (MethodInfo method in declaringType.GetMethods(flags| BindingFlags.Public | BindingFlags.NonPublic))
                {
                    List<Mapping>? mapping = null;

                    if (method.Name != name)
                        continue;

                    ParameterInfo[] parameters = method.GetParameters();
                    if (arguments.Length != parameters.Length)
                        continue;

                    bool all = true;
                    for (int index = 0; index < arguments.Length; index++)
                    {
                        if (!MatchesType(parameters[index].ParameterType, arguments[index], ref mapping))
                        {
                            all = false;
                            break;
                        }
                    }
                    if (!all)
                        continue;
                    
                    if (!MatchesType(method.ReturnType, returnType, ref mapping))
                        continue;

                    if (method.ContainsGenericParameters)
                    {
                        if (mapping is null)
                            continue;

                        bool incompatibleGenericParameters = false;
                        foreach ((Type key, List<Mapping> items) in mapping.GroupBy(item => item.GenericParameter).Select(item => (key: item.Key, items: item.ToList())))
                        {
                            if (items.Count > 0)
                            {
                                Type specialized = items[0].SpecializedParameter;
                                if (items.Exists(item => item.SpecializedParameter != specialized))
                                {
                                    incompatibleGenericParameters = true;
                                    break;
                                }
                            }
                        }
                        if (incompatibleGenericParameters)
                            continue;

                        Type[] genericParameters = method.GetGenericArguments();
                        Type[] specializedParameters = new Type[genericParameters.Length];

                        for (int index = 0; index < genericParameters.Length; index++)
                        {
                            Type? specializedParameter = mapping.FirstOrDefault(item => true)?.SpecializedParameter;
                            if (specializedParameter is null)
                                throw new InvalidOperationException("Generic type mapping is missing...");

                            specializedParameters[index] = specializedParameter;
                        }
                        return method.MakeGenericMethod(specializedParameters);
                    }

                    return method;
                }
                return null;
            }
            private bool MatchesType(Type memberType, Type passedInType, ref List<Mapping>? genericParameterMapping)
            {
                if (memberType.IsGenericParameter)
                {
                    genericParameterMapping ??= new List<Mapping>();
                    genericParameterMapping.Add(new Mapping(memberType, passedInType));
                    return true;
                }

                return memberType.IsAssignableFrom(passedInType);
            }
            private sealed class Mapping
            {
                internal Mapping(Type generic, Type specialized)
                {
                    GenericParameter = generic;
                    SpecializedParameter = specialized;
                }

                public Type GenericParameter { get; private set; }
                public Type SpecializedParameter { get; private set; }
            } 
        }
        internal abstract class Member<T> : Member
            where T : MemberInfo
        {
            protected Member(DriverTypeInfo parent, DriverTypeInfo? returnType, string name, DriverTypeInfo?[]? arguments = null) : base(parent, returnType, new string[] { name }, arguments) { }
            protected Member(DriverTypeInfo parent, DriverTypeInfo? returnType, string[] names, DriverTypeInfo?[]? arguments = null) : base(parent, returnType, names, arguments) { }

            protected abstract T? Search(Type type, string name, Type? returnType, Type[] args);
            
            protected Lazy<T?> ForEachType(bool deepSearch = true)
            {
                return new Lazy<T?>(delegate()
                {
                    foreach (string name in Names)
                    {
                        T? member = ForEachType(Parent.Type, name, ReturnType.Type, Arguments.Select(item => item.Type).ToArray(), deepSearch);
                        if (member is not null)
                            return member;
                    }
                    return null;
                }, true);
            }
            private T? ForEachType(Type type, string name, Type? returnType, Type[] arguments, bool deepSearch)
            {
                T? result = null;
                if (type is not null)
                {
                    result = Search(type, name, returnType, arguments);

                    if (deepSearch)
                    {
                        if (result is null)
                        {
                            foreach (Type iface in type.GetInterfaces())
                            {
                                result = ForEachType(iface, name, returnType, arguments, deepSearch);
                                if (result is not null)
                                    break;
                            }
                        }

                        if (result is null && !type.IsInterface)
                        {
                            result = ForEachType(type.BaseType, name, returnType, arguments, deepSearch);
                        }
                    }
                }
                return result;
            }
        }
        internal enum MemberType
        {
            Method,
            Property,
            EnumValue,
            Generic
        }

        internal sealed class StaticProperty : Member<PropertyInfo>
        {
            internal StaticProperty(DriverTypeInfo parent, DriverTypeInfo returnType, string name, DriverTypeInfo? indexer = null) : base(parent, returnType, name, (indexer is null) ? new DriverTypeInfo[0] : new DriverTypeInfo[] { indexer })
            {
                _propertyInfo = ForEachType();
            }
            internal StaticProperty(DriverTypeInfo parent, DriverTypeInfo returnType, string[] names, DriverTypeInfo? indexer = null) : base(parent, returnType, names, (indexer is null) ? new DriverTypeInfo[0] : new DriverTypeInfo[] { indexer })
            {
                _propertyInfo = ForEachType();
            }
            protected override PropertyInfo? Search(Type type, string name, Type? returnType, Type[] args)
            {
                return type.GetProperty(name, BindingFlags.Public | BindingFlags.Static, null, returnType, args, null);
            }
            public override bool Exists => (_propertyInfo.Value is not null);

            public object GetValue(object? index = null)
            {
                if (index is null)
                    return PropertyInfo.GetValue(null);
                else
                    return PropertyInfo.GetValue(null, new object[] { index });
            }
            public void SetValue(object? value, object? index = null)
            {
                if (index is null)
                    PropertyInfo.SetValue(null, value);
                else
                    PropertyInfo.SetValue(null, value, new object[] { index });
            }

            private PropertyInfo PropertyInfo => _propertyInfo.Value ?? throw new MissingMethodException();
            private readonly Lazy<PropertyInfo?> _propertyInfo;
        }
        internal sealed class InstanceProperty : Member<PropertyInfo>
        {
            internal InstanceProperty(DriverTypeInfo parent, DriverTypeInfo returnType, string name, DriverTypeInfo? indexer = null) : base(parent, returnType, name, (indexer is null) ? new DriverTypeInfo[0] : new DriverTypeInfo[] { indexer })
            {
                _propertyInfo = ForEachType();
            }
            internal InstanceProperty(DriverTypeInfo parent, DriverTypeInfo returnType, string[] names, DriverTypeInfo? indexer = null) : base(parent, returnType, names, (indexer is null) ? new DriverTypeInfo[0] : new DriverTypeInfo[] { indexer })
            {
                _propertyInfo = ForEachType();
            }
            protected override PropertyInfo? Search(Type type, string name, Type? returnType, Type[] args)
            {
                return type.GetProperty(name, BindingFlags.Public | BindingFlags.Instance, null, returnType, args, null);
            }
            public override bool Exists => (_propertyInfo.Value is not null);

            public object GetValue(object instance, object? index = null)
            {
                if (index is null)
                    return PropertyInfo.GetValue(instance);
                else
                    return PropertyInfo.GetValue(instance, new object[] { index });
            }
            public void SetValue(object instance, object? value, object? index = null)
            {
                if (index is null)
                    PropertyInfo.SetValue(instance, value);
                else
                    PropertyInfo.SetValue(instance, value, new object[] { index });
            }

            private PropertyInfo PropertyInfo => _propertyInfo.Value ?? throw new MissingMethodException();
            private readonly Lazy<PropertyInfo?> _propertyInfo;
        }
        internal sealed class StaticMethod : Member<MethodInfo>
        {
            internal StaticMethod(DriverTypeInfo parent, DriverTypeInfo returnType, string name, params DriverTypeInfo[] arguments) : base(parent, returnType, name, arguments)
            {
                _methodInfo = ForEachType();
            }
            internal StaticMethod(DriverTypeInfo parent, DriverTypeInfo returnType, string[] names, params DriverTypeInfo[] arguments) : base(parent, returnType, names, arguments)
            {
                _methodInfo = ForEachType();
            }
            protected override MethodInfo? Search(Type type, string name, Type? returnType, Type[] args)
            {
                return GetMethodInfo(BindingFlags.Static, type, returnType!, name, args);
            }
            public override bool Exists => (_methodInfo.Value is not null);

            public object Invoke()
            {
                return MethodInfo.Invoke(null, new object?[0]);
            }
            public object Invoke(object? arg1)
            {
                return MethodInfo.Invoke(null, new object?[] { arg1 });
            }
            public object Invoke(object? arg1, object? arg2)
            {
                return MethodInfo.Invoke(null, new object?[] { arg1, arg2 });
            }
            public object Invoke(object? arg1, object? arg2, object? arg3)
            {
                return MethodInfo.Invoke(null, new object?[] { arg1, arg2, arg3 });
            }
            public object Invoke(object? arg1, object? arg2, object? arg3, object? arg4)
            {
                return MethodInfo.Invoke(null, new object?[] { arg1, arg2, arg3, arg4 });
            }
            public object Invoke(object? arg1, object? arg2, object? arg3, object? arg4, object? arg5)
            {
                return MethodInfo.Invoke(null, new object?[] { arg1, arg2, arg3, arg4, arg5 });
            }

            private MethodInfo MethodInfo => _methodInfo.Value ?? throw new MissingMethodException();
            private readonly Lazy<MethodInfo?> _methodInfo;
        }
        internal sealed class InstanceMethod : Member<MethodInfo>
        {
            internal InstanceMethod(DriverTypeInfo parent, DriverTypeInfo returnType, string name, params DriverTypeInfo[] arguments) : base(parent, returnType, name, arguments)
            {
                _methodInfo = ForEachType();

            }
            internal InstanceMethod(DriverTypeInfo parent, DriverTypeInfo returnType, string[] names, params DriverTypeInfo[] arguments) : base(parent, returnType, names, arguments)
            {
                _methodInfo = ForEachType();

            }
            protected override MethodInfo? Search(Type type, string name, Type? returnType, Type[] args)
            {
                return GetMethodInfo(BindingFlags.Instance, type, returnType!, name, args);
            }
            public override bool Exists => (_methodInfo.Value is not null);

            public object Invoke(object instance)
            {
                return MethodInfo.Invoke(instance, new object?[0]);
            }
            public object Invoke(object instance, object? arg1)
            {
                return MethodInfo.Invoke(instance, new object?[] { arg1 });
            }
            public object Invoke(object instance, object? arg1, object? arg2)
            {
                return MethodInfo.Invoke(instance, new object?[] { arg1, arg2 });
            }
            public object Invoke(object instance, object? arg1, object? arg2, object? arg3)
            {
                return MethodInfo.Invoke(instance, new object?[] { arg1, arg2, arg3 });
            }
            public object Invoke(object instance, object? arg1, object? arg2, object? arg3, object? arg4)
            {
                return MethodInfo.Invoke(instance, new object?[] { arg1, arg2, arg3, arg4 });
            }
            public object Invoke(object instance, object? arg1, object? arg2, object? arg3, object? arg4, object? arg5)
            {
                return MethodInfo.Invoke(instance, new object?[] { arg1, arg2, arg3, arg4, arg5 });
            }

            private MethodInfo MethodInfo => _methodInfo.Value ?? throw new MissingMethodException();
            private readonly Lazy<MethodInfo?> _methodInfo;
        }
        internal sealed class EnumField : Member<FieldInfo>
        {
            internal EnumField(DriverTypeInfo parent, string name) : base(parent, null, name)
            {
                _fieldInfo = ForEachType(false);
            }
            internal EnumField(DriverTypeInfo parent, string[] names) : base(parent, null, names)
            {
                _fieldInfo = ForEachType(false);
            }
            protected override FieldInfo? Search(Type type, string name, Type? returnType, Type[] args)
            {
                return type.GetFields().FirstOrDefault(fi => fi.IsLiteral && fi.Name == name);
            }
            public override bool Exists => (_fieldInfo.Value is not null);

            public object Value => FieldInfo.GetRawConstantValue();
            public bool Test(object instance) => Value == instance;

            private FieldInfo FieldInfo => _fieldInfo.Value ?? throw new MissingMethodException();
            private readonly Lazy<FieldInfo?> _fieldInfo;
        }

        internal sealed class Generic<TMember> : Member<ConstructorInfo>
            where TMember : Member
        {
            internal Generic(DriverTypeInfo parent, string name) : base(parent, Type<TMember>.Info, name, null)
            {
                MemberType = GetMemberType<TMember>();
                _constructorInfo = ForEachType(false);
            }
            internal Generic(DriverTypeInfo parent, string[] names) : base(parent, Type<TMember>.Info, names, null)
            {
                MemberType = GetMemberType<TMember>();
                _constructorInfo = ForEachType(false);
            }
            private readonly MemberType MemberType;

            protected override ConstructorInfo? Search(Type type, string name, Type? returnType, Type[] args)
            {
                Type[] signature = MemberType switch
                {
                    MemberType.Method => new Type[] { typeof(DriverTypeInfo), typeof(DriverTypeInfo), typeof(string[]), typeof(DriverTypeInfo[]) },
                    MemberType.Property => new Type[] { typeof(DriverTypeInfo), typeof(DriverTypeInfo), typeof(string[]), typeof(DriverTypeInfo) },
                    MemberType.EnumValue => new Type[] { typeof(DriverTypeInfo), typeof(string[]) },
                    _ => throw new NotImplementedException()
                };

                return typeof(TMember).GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, signature, null);
            }
            public override bool Exists => (_constructorInfo.Value is not null);

            public TMember ForTypes(DriverTypeInfo returnType, params DriverTypeInfo[] arguments)
            {
                TMember member;

                GenericSignature signature = new GenericSignature(returnType, arguments);
                if (!_cache.TryGetValue(signature, out member))
                {
                    lock (_cache)
                    {
                        if (!_cache.TryGetValue(signature, out member))
                        {
                            Dictionary<GenericSignature, TMember> _newCache = new Dictionary<GenericSignature, TMember>(_cache);
                            member = CreateInstance(Names);
                            _newCache.Add(signature, member);

                            Interlocked.Exchange(ref _cache, _newCache);
                        }
                    }
                }
                return member;

                TMember CreateInstance(string[] names)
                {
                    DriverTypeInfo? argument = (arguments.Length > 0 ? arguments[0] : null);

                    object?[] args = MemberType switch
                    {
                        MemberType.Method => new object?[] { Parent, returnType, names, arguments },
                        MemberType.Property => new object?[] { Parent, returnType, names, argument },
                        MemberType.EnumValue => new object?[] { Parent, names },
                        _ => throw new NotImplementedException()
                    };

                    object? instance = ConstructorInfo.Invoke(args);
                    if (instance is null)
                        throw new InvalidOperationException();

                    return (TMember)instance;
                }
            }
            private Dictionary<GenericSignature, TMember> _cache = new Dictionary<GenericSignature, TMember>();
            
            private ConstructorInfo ConstructorInfo => _constructorInfo.Value ?? throw new MissingMethodException();
            private readonly Lazy<ConstructorInfo?> _constructorInfo;

            internal struct GenericSignature
            {
                internal GenericSignature(DriverTypeInfo returnType, DriverTypeInfo[] arguments)
                {
                    ReturnType = returnType;
                    Arguments = arguments;
                }

                public DriverTypeInfo ReturnType { get; private set; }
                public DriverTypeInfo[] Arguments { get; private set; }

                public override int GetHashCode()
                {
                    int hashcode = CombineHashCodes(ReturnType?.Type?.GetHashCode(), Arguments?.Length.GetHashCode());

                    if (Arguments is not null)
                    {
                        for (int index = 0; index < Arguments.Length; index++)
                        {
                            hashcode = CombineHashCodes(hashcode, Arguments[index]?.Type?.GetHashCode());
                        }
                    }

                    return hashcode;

                    static int CombineHashCodes(int? h1, int? h2)
                    {
                        unchecked
                        {
                            // Code copied from System.Tuple so it must be the best way to combine hash codes or at least a good one.
                            return (((h1 ?? 0) << 5) + (h1 ?? 0)) ^ (h2 ?? 0);
                        }
                    }
                }
                public override bool Equals(object obj)
                {
                    if (obj is null)
                        return false;

                    if (obj is not GenericSignature)
                        return false;

                    GenericSignature other = (GenericSignature)obj;

                    if (ReturnType != other.ReturnType)
                        return false;

                    if (Arguments.Length != other.Arguments.Length)
                        return false;

                    for (int index = 0; index < Arguments.Length; index++)
                    {
                        if (Arguments[index] != other.Arguments[index])
                            return false;
                    }

                    return true;
                }
            }
        }

        #endregion

        #region Specific Neo4j Driver Types

        internal sealed class IDriverInfo : DriverTypeInfo
        {
            public IDriverInfo(params string[] names) : base(names) { }

            //bool Encrypted { get; }

            public Session AsyncSession(Driver driver) => new Session(_asyncSession1.Value.Invoke(driver._instance));
            private readonly Lazy<InstanceMethod> _asyncSession1 = new Lazy<InstanceMethod>(() => new InstanceMethod(I_DRIVER, I_ASYNC_SESSION, "AsyncSession"), true);

            public Session AsyncSession(Driver driver, Action<SessionConfigBuilder> configBuilder) => new Session(_asyncSession2.Value.Invoke(driver._instance, DelegateHelper.WrapDelegate(Generic.Of(typeof(Action<>), SESSION_CONFIG_BUILDER).Type, (object o) => configBuilder.Invoke(new SessionConfigBuilder(o)))));
            private readonly Lazy<InstanceMethod> _asyncSession2 = new Lazy<InstanceMethod>(() => new InstanceMethod(I_DRIVER, I_ASYNC_SESSION, "AsyncSession", Generic.Of(typeof(Action<>), SESSION_CONFIG_BUILDER)), true);

            //Task<IServerInfo> GetServerInfoAsync();

            public Task VerifyConnectivityAsync(Driver driver) => AsTask(_verifyConnectivityAsync.Value.Invoke(driver._instance));
            private readonly Lazy<InstanceMethod> _verifyConnectivityAsync = new Lazy<InstanceMethod>(() => new InstanceMethod(I_DRIVER, Type<Task>.Info, "VerifyConnectivityAsync"), true);

            //Task VerifyConnectivityAsync();

            public Task<bool> SupportsMultiDbAsync(Driver driver) => AsTask<bool>(_supportsMultiDbAsync.Value.Invoke(driver._instance));
            private readonly Lazy<InstanceMethod> _supportsMultiDbAsync = new Lazy<InstanceMethod>(() => new InstanceMethod(I_DRIVER, Type<Task<bool>>.Info, "SupportsMultiDbAsync"), true);
        }
        internal sealed class ConfigBuilderInfo : DriverTypeInfo
        {
            public ConfigBuilderInfo(params string[] names) : base(names) { }

            public void WithMaxIdleConnectionPoolSize(object instance, int size) => _withMaxIdleConnectionPoolSize.Value.Invoke(instance, size);
            private readonly Lazy<InstanceMethod> _withMaxIdleConnectionPoolSize = new Lazy<InstanceMethod>(() => new InstanceMethod(CONFIG_BUILDER, CONFIG_BUILDER, "WithMaxIdleConnectionPoolSize", Type<int>.Info), true);

            public void WithMaxConnectionPoolSize(object instance, int size) => _withMaxConnectionPoolSize.Value.Invoke(instance, size);
            private readonly Lazy<InstanceMethod> _withMaxConnectionPoolSize = new Lazy<InstanceMethod>(() => new InstanceMethod(CONFIG_BUILDER, CONFIG_BUILDER, "WithMaxConnectionPoolSize", Type<int>.Info), true);

            public void WithConnectionAcquisitionTimeout(object instance, TimeSpan timeSpan) => _withConnectionAcquisitionTimeout.Value.Invoke(instance, timeSpan);
            private readonly Lazy<InstanceMethod> _withConnectionAcquisitionTimeout = new Lazy<InstanceMethod>(() => new InstanceMethod(CONFIG_BUILDER, CONFIG_BUILDER, "WithConnectionAcquisitionTimeout", Type<TimeSpan>.Info), true);

            public void WithConnectionTimeout(object instance, TimeSpan timeSpan) => _withConnectionTimeout.Value.Invoke(instance, timeSpan);
            private readonly Lazy<InstanceMethod> _withConnectionTimeout = new Lazy<InstanceMethod>(() => new InstanceMethod(CONFIG_BUILDER, CONFIG_BUILDER, "WithConnectionTimeout", Type<TimeSpan>.Info), true);

            public void WithSocketKeepAliveEnabled(object instance, bool enable) => _withSocketKeepAliveEnabled.Value.Invoke(instance, enable);
            private readonly Lazy<InstanceMethod> _withSocketKeepAliveEnabled = new Lazy<InstanceMethod>(() => new InstanceMethod(CONFIG_BUILDER, CONFIG_BUILDER, "WithSocketKeepAliveEnabled", Type<bool>.Info), true);

            public void WithMaxTransactionRetryTime(object instance, TimeSpan time) => _withMaxTransactionRetryTime.Value.Invoke(instance, time);
            private readonly Lazy<InstanceMethod> _withMaxTransactionRetryTime = new Lazy<InstanceMethod>(() => new InstanceMethod(CONFIG_BUILDER, CONFIG_BUILDER, "WithMaxTransactionRetryTime", Type<TimeSpan>.Info), true);

            public void WithConnectionIdleTimeout(object instance, TimeSpan timeSpan) => _withConnectionIdleTimeout.Value.Invoke(instance, timeSpan);
            private readonly Lazy<InstanceMethod> _withConnectionIdleTimeout = new Lazy<InstanceMethod>(() => new InstanceMethod(CONFIG_BUILDER, CONFIG_BUILDER, "WithConnectionIdleTimeout", Type<TimeSpan>.Info), true);

            public void WithMaxConnectionLifetime(object instance, TimeSpan timeSpan) => _withMaxConnectionLifetime.Value.Invoke(instance, timeSpan);
            private readonly Lazy<InstanceMethod> _withMaxConnectionLifetime = new Lazy<InstanceMethod>(() => new InstanceMethod(CONFIG_BUILDER, CONFIG_BUILDER, "WithMaxConnectionLifetime", Type<TimeSpan>.Info), true);

            public void WithIpv6Enabled(object instance, bool enable) => _withIpv6Enabled.Value.Invoke(instance, enable);
            private readonly Lazy<InstanceMethod> _withIpv6Enabled = new Lazy<InstanceMethod>(() => new InstanceMethod(CONFIG_BUILDER, CONFIG_BUILDER, "WithIpv6Enabled", Type<bool>.Info), true);

            public void WithDefaultReadBufferSize(object instance, int defaultReadBufferSize) => _withDefaultReadBufferSize.Value.Invoke(instance, defaultReadBufferSize);
            private readonly Lazy<InstanceMethod> _withDefaultReadBufferSize = new Lazy<InstanceMethod>(() => new InstanceMethod(CONFIG_BUILDER, CONFIG_BUILDER, "WithDefaultReadBufferSize", Type<int>.Info), true);

            public void WithMaxReadBufferSize(object instance, int maxReadBufferSize) => _withMaxReadBufferSize.Value.Invoke(instance, maxReadBufferSize);
            private readonly Lazy<InstanceMethod> _withMaxReadBufferSize = new Lazy<InstanceMethod>(() => new InstanceMethod(CONFIG_BUILDER, CONFIG_BUILDER, "WithMaxReadBufferSize", Type<int>.Info), true);

            public void WithDefaultWriteBufferSize(object instance, int defaultWriteBufferSize) => _withDefaultWriteBufferSize.Value.Invoke(instance, defaultWriteBufferSize);
            private readonly Lazy<InstanceMethod> _withDefaultWriteBufferSize = new Lazy<InstanceMethod>(() => new InstanceMethod(CONFIG_BUILDER, CONFIG_BUILDER, "WithDefaultWriteBufferSize", Type<int>.Info), true);

            public void WithMaxWriteBufferSize(object instance, int maxWriteBufferSize) => _withMaxWriteBufferSize.Value.Invoke(instance, maxWriteBufferSize);
            private readonly Lazy<InstanceMethod> _withMaxWriteBufferSize = new Lazy<InstanceMethod>(() => new InstanceMethod(CONFIG_BUILDER, CONFIG_BUILDER, "WithMaxWriteBufferSize", Type<int>.Info), true);

            public void WithFetchSize(object instance, long size) => _withFetchSize.Value.Invoke(instance, size);
            private readonly Lazy<InstanceMethod> _withFetchSize = new Lazy<InstanceMethod>(() => new InstanceMethod(CONFIG_BUILDER, CONFIG_BUILDER, "WithFetchSize", Type<long>.Info), true);
        }
        internal sealed class GraphDatabaseInfo : DriverTypeInfo
        {
            public GraphDatabaseInfo(params string[] names) : base(names) { }

            public Driver Driver(Uri uri, AuthToken authToken, Action<ConfigBuilder> configBuilder) => new Driver(_driver.Value.Invoke(uri, authToken._instance, DelegateHelper.WrapDelegate(Generic.Of(typeof(Action<>), CONFIG_BUILDER).Type, (object o) => configBuilder.Invoke(new ConfigBuilder(o)))));
            private readonly Lazy<StaticMethod> _driver = new Lazy<StaticMethod>(() => new StaticMethod(GRAPH_DATABASE, I_DRIVER, "Driver", Type<Uri>.Info, I_AUTH_TOKEN, Generic.Of(typeof(Action<>), CONFIG_BUILDER)), true);
        }
        internal sealed class AuthTokensInfo : DriverTypeInfo
        {
            public AuthTokensInfo(params string[] names) : base(names) { }

            public AuthToken None { get => new AuthToken(_none.Value.GetValue()); set => _none.Value.SetValue(value._instance); }
            private readonly Lazy<StaticProperty> _none = new Lazy<StaticProperty>(() => new StaticProperty(AUTH_TOKENS, I_AUTH_TOKEN, "None"), true);

            public AuthToken Basic(string username, string password) => new AuthToken(_basic1.Value.Invoke(username, password));
            private readonly Lazy<StaticMethod> _basic1 = new Lazy<StaticMethod>(() => new StaticMethod(AUTH_TOKENS, I_AUTH_TOKEN, "Basic", Type<string>.Info, Type<string>.Info), true);

            public AuthToken Basic(string username, string password, string realm) => new AuthToken(_basic2.Value.Invoke(username, password, realm));
            private readonly Lazy<StaticMethod> _basic2 = new Lazy<StaticMethod>(() => new StaticMethod(AUTH_TOKENS, I_AUTH_TOKEN, "Basic", Type<string>.Info, Type<string>.Info, Type<string>.Info), true);

            public AuthToken Kerberos(string base64EncodedTicket) => new AuthToken(_kerberos.Value.Invoke(base64EncodedTicket));
            private readonly Lazy<StaticMethod> _kerberos = new Lazy<StaticMethod>(() => new StaticMethod(AUTH_TOKENS, I_AUTH_TOKEN, "Kerberos", Type<string>.Info), true);

            public AuthToken Custom(string principal, string credentials, string realm, string scheme) => new AuthToken(_custom1.Value.Invoke(principal, credentials, realm, scheme));
            private readonly Lazy<StaticMethod> _custom1 = new Lazy<StaticMethod>(() => new StaticMethod(AUTH_TOKENS, I_AUTH_TOKEN, "Custom", Type<string>.Info, Type<string>.Info, Type<string>.Info, Type<string>.Info), true);

            public AuthToken Custom(string principal, string credentials, string realm, string scheme, Dictionary<string, object> parameters) => new AuthToken(_custom2.Value.Invoke(principal, credentials, realm, scheme, parameters));
            private readonly Lazy<StaticMethod> _custom2 = new Lazy<StaticMethod>(() => new StaticMethod(AUTH_TOKENS, I_AUTH_TOKEN, "Custom", Type<string>.Info, Type<string>.Info, Type<string>.Info, Type<string>.Info, Type<Dictionary<string, object>>.Info), true);

            public AuthToken Bearer(string token) => new AuthToken(_bearer.Value.Invoke(token));
            private readonly Lazy<StaticMethod> _bearer = new Lazy<StaticMethod>(() => new StaticMethod(AUTH_TOKENS, I_AUTH_TOKEN, "Bearer", Type<string>.Info), true);
        }
        internal sealed class IResultCursorInfo : DriverTypeInfo
        {
            public IResultCursorInfo(params string[] names) : base(names) { }

            public Record Current(object instance) => new Record(_current.Value.GetValue(instance));
            public object CurrentInternal(object instance) => _current.Value.GetValue(instance);
            private readonly Lazy<InstanceProperty> _current = new Lazy<InstanceProperty>(() => new InstanceProperty(I_RESULT_CURSOR, I_RECORD, "Current"), true);

            public Task<string[]> KeysAsync(object instance) => AsTask<string[]>(_keysAsync.Value.Invoke(instance));
            private readonly Lazy<InstanceMethod> _keysAsync = new Lazy<InstanceMethod>(() => new InstanceMethod(I_RESULT_CURSOR, Type<Task<string[]>>.Info, "KeysAsync"), true);

            public Task<ResultSummary> ConsumeAsync(object instance) => AsTask(_consumeAsync.Value.Invoke(instance), instance => new ResultSummary(instance));
            private readonly Lazy<InstanceMethod> _consumeAsync = new Lazy<InstanceMethod>(() => new InstanceMethod(I_RESULT_CURSOR, Generic.Of(typeof(Task<>), I_RESULT_SUMMARY), "ConsumeAsync"), true);

            public Task<Record> PeekAsync(object instance) => AsTask(_peekAsync.Value.Invoke(instance), instance => new Record(instance));
            public Task<object> PeekAsyncInternal(object instance) => AsTask<object>(_peekAsync.Value.Invoke(instance));
            private readonly Lazy<InstanceMethod> _peekAsync = new Lazy<InstanceMethod>(() => new InstanceMethod(I_RESULT_CURSOR, Generic.Of(typeof(Task<>), I_RECORD), "PeekAsync"), true);

            public Task<bool> FetchAsync(object instance) => AsTask<bool>(_fetchAsync.Value.Invoke(instance));
            private readonly Lazy<InstanceMethod> _fetchAsync = new Lazy<InstanceMethod>(() => new InstanceMethod(I_RESULT_CURSOR, Type<Task<bool>>.Info, "FetchAsync"), true);
        }
        internal sealed class IResultSummaryInfo : DriverTypeInfo
        {
            public IResultSummaryInfo(params string[] names) : base(names) { }

            public Query Query(object instance) => new Query(_query.Value.GetValue(instance));
            private readonly Lazy<InstanceProperty> _query = new Lazy<InstanceProperty>(() => new InstanceProperty(I_RESULT_SUMMARY, QUERY, "Query"), true);

            public Counters Counters(object instance) => new Counters(_counters.Value.GetValue(instance));
            private readonly Lazy<InstanceProperty> _counters = new Lazy<InstanceProperty>(() => new InstanceProperty(I_RESULT_SUMMARY, I_COUNTERS, "Counters"), true);

            public List<Notification> Notifications(object instance) => ToList<Notification>(_notifications.Value.GetValue(instance), item => new Notification(item)); //  IList<INotification> Notifications { get; }
            private readonly Lazy<InstanceProperty> _notifications = new Lazy<InstanceProperty>(() => new InstanceProperty(I_RESULT_SUMMARY, Generic.Of(typeof(IList<>), I_NOTIFICATION), "Notifications"), true);
        }
        internal sealed class QueryInfo : DriverTypeInfo
        {
            public QueryInfo(params string[] names) : base(names) { }

            public string Text(object instance) => (string)_text.Value.GetValue(instance);
            private readonly Lazy<InstanceProperty> _text = new Lazy<InstanceProperty>(() => new InstanceProperty(QUERY, Type<string>.Info, "Text"), true);

            public IDictionary<string, object> Parameters(object instance) => (IDictionary<string, object>)_parameters.Value.GetValue(instance);
            private readonly Lazy<InstanceProperty> _parameters = new Lazy<InstanceProperty>(() => new InstanceProperty(QUERY, Type<IDictionary<string, object>>.Info, "Parameters"), true);
        }
        internal sealed class ICountersInfo : DriverTypeInfo
        {
            public ICountersInfo(params string[] names) : base(names) { }

            public bool ContainsUpdates(object instance) => (bool)_ContainsUpdates.Value.GetValue(instance);
            private readonly Lazy<InstanceProperty> _ContainsUpdates = new Lazy<InstanceProperty>(() => new InstanceProperty(I_COUNTERS, Type<bool>.Info, "ContainsUpdates"), true);

            public int NodesCreated(object instance) => (int)_NodesCreated.Value.GetValue(instance);
            private readonly Lazy<InstanceProperty> _NodesCreated = new Lazy<InstanceProperty>(() => new InstanceProperty(I_COUNTERS, Type<int>.Info, "NodesCreated"), true);

            public int NodesDeleted(object instance) => (int)_NodesDeleted.Value.GetValue(instance);
            private readonly Lazy<InstanceProperty> _NodesDeleted = new Lazy<InstanceProperty>(() => new InstanceProperty(I_COUNTERS, Type<int>.Info, "NodesDeleted"), true);

            public int RelationshipsCreated(object instance) => (int)_RelationshipsCreated.Value.GetValue(instance);
            private readonly Lazy<InstanceProperty> _RelationshipsCreated = new Lazy<InstanceProperty>(() => new InstanceProperty(I_COUNTERS, Type<int>.Info, "RelationshipsCreated"), true);

            public int RelationshipsDeleted(object instance) => (int)_RelationshipsDeleted.Value.GetValue(instance);
            private readonly Lazy<InstanceProperty> _RelationshipsDeleted = new Lazy<InstanceProperty>(() => new InstanceProperty(I_COUNTERS, Type<int>.Info, "RelationshipsDeleted"), true);

            public int PropertiesSet(object instance) => (int)_PropertiesSet.Value.GetValue(instance);
            private readonly Lazy<InstanceProperty> _PropertiesSet = new Lazy<InstanceProperty>(() => new InstanceProperty(I_COUNTERS, Type<int>.Info, "PropertiesSet"), true);

            public int LabelsAdded(object instance) => (int)_LabelsAdded.Value.GetValue(instance);
            private readonly Lazy<InstanceProperty> _LabelsAdded = new Lazy<InstanceProperty>(() => new InstanceProperty(I_COUNTERS, Type<int>.Info, "LabelsAdded"), true);

            public int LabelsRemoved(object instance) => (int)_LabelsRemoved.Value.GetValue(instance);
            private readonly Lazy<InstanceProperty> _LabelsRemoved = new Lazy<InstanceProperty>(() => new InstanceProperty(I_COUNTERS, Type<int>.Info, "LabelsRemoved"), true);

            public int IndexesAdded(object instance) => (int)_IndexesAdded.Value.GetValue(instance);
            private readonly Lazy<InstanceProperty> _IndexesAdded = new Lazy<InstanceProperty>(() => new InstanceProperty(I_COUNTERS, Type<int>.Info, "IndexesAdded"), true);

            public int IndexesRemoved(object instance) => (int)_IndexesRemoved.Value.GetValue(instance);
            private readonly Lazy<InstanceProperty> _IndexesRemoved = new Lazy<InstanceProperty>(() => new InstanceProperty(I_COUNTERS, Type<int>.Info, "IndexesRemoved"), true);

            public int ConstraintsAdded(object instance) => (int)_ConstraintsAdded.Value.GetValue(instance);
            private readonly Lazy<InstanceProperty> _ConstraintsAdded = new Lazy<InstanceProperty>(() => new InstanceProperty(I_COUNTERS, Type<int>.Info, "ConstraintsAdded"), true);

            public int ConstraintsRemoved(object instance) => (int)_ConstraintsRemoved.Value.GetValue(instance);
            private readonly Lazy<InstanceProperty> _ConstraintsRemoved = new Lazy<InstanceProperty>(() => new InstanceProperty(I_COUNTERS, Type<int>.Info, "ConstraintsRemoved"), true);

            public int SystemUpdates(object instance) => (int)_SystemUpdates.Value.GetValue(instance);
            private readonly Lazy<InstanceProperty> _SystemUpdates = new Lazy<InstanceProperty>(() => new InstanceProperty(I_COUNTERS, Type<int>.Info, "SystemUpdates"), true);

            public bool ContainsSystemUpdates(object instance) => (bool)_ContainsSystemUpdates.Value.GetValue(instance);
            private readonly Lazy<InstanceProperty> _ContainsSystemUpdates = new Lazy<InstanceProperty>(() => new InstanceProperty(I_COUNTERS, Type<bool>.Info, "ContainsSystemUpdates"), true);

        }
        internal sealed class IRecord : DriverTypeInfo
        {
            public IRecord(params string[] names) : base(names) { }

            public object Item(object instance, int index) => _item1.Value.GetValue(instance, index);
            private readonly Lazy<InstanceProperty> _item1 = new Lazy<InstanceProperty>(() => new InstanceProperty(I_RECORD, Type<object>.Info, "Item", Type<int>.Info), true);

            public object Item(object instance, string key) => _item2.Value.GetValue(instance, key);
            private readonly Lazy<InstanceProperty> _item2 = new Lazy<InstanceProperty>(() => new InstanceProperty(I_RECORD, Type<object>.Info, "Item", Type<string>.Info), true);

            public IReadOnlyDictionary<string, object?> Values(object instance) => (IReadOnlyDictionary<string, object?>)_values.Value.GetValue(instance);
            private readonly Lazy<InstanceProperty> _values = new Lazy<InstanceProperty>(() => new InstanceProperty(I_RECORD, Type<IReadOnlyDictionary<string, object>>.Info, "Values"), true);

            public IReadOnlyList<string> Keys(object instance) => (IReadOnlyList<string>)_keys.Value.GetValue(instance);
            private readonly Lazy<InstanceProperty> _keys = new Lazy<InstanceProperty>(() => new InstanceProperty(I_RECORD, Type<IReadOnlyList<string>>.Info, "Keys"), true);
        }
        internal sealed class IAsyncSessionInfo : DriverTypeInfo
        {
            public IAsyncSessionInfo(params string[] names) : base(names) { }

            public Bookmarks LastBookmarks(object instance) => new Bookmarks(_lastBookmarks.Value.GetValue(instance));
            private readonly Lazy<InstanceProperty> _lastBookmarks = new Lazy<InstanceProperty>(() => new InstanceProperty(I_ASYNC_SESSION, BOOKMARKS, new string[] { "LastBookmarks", "LastBookmark" }), true);

            public Task<Transaction> BeginTransactionAsync(object instance) => AsTask(_beginTransactionAsync1.Value.Invoke(instance), instance => new Transaction(instance));
            private readonly Lazy<InstanceMethod> _beginTransactionAsync1 = new Lazy<InstanceMethod>(() => new InstanceMethod(I_ASYNC_SESSION, Generic.Of(typeof(Task<>), I_ASYNC_TRANSACTION), "BeginTransactionAsync"), true);

            public Task<Transaction> BeginTransactionAsync(object instance, Action<TransactionConfigBuilder> configBuilder) => AsTask(_beginTransactionAsync2.Value.Invoke(instance, DelegateHelper.WrapDelegate(Generic.Of(typeof(Action<>), TRANSACTION_CONFIG_BUILDER).Type, (object o) => configBuilder.Invoke(new TransactionConfigBuilder(o)))), instance => new Transaction(instance));
            private readonly Lazy<InstanceMethod> _beginTransactionAsync2 = new Lazy<InstanceMethod>(() => new InstanceMethod(I_ASYNC_SESSION, Generic.Of(typeof(Task<>), I_ASYNC_TRANSACTION), "BeginTransactionAsync", Generic.Of(typeof(Action<>), TRANSACTION_CONFIG_BUILDER)), true);

            public Task<ResultCursor> RunAsync(object instance, string query) => AsTask(_runAsync1.Value.Invoke(instance, query), instance => new ResultCursor(instance));
            private readonly Lazy<InstanceMethod> _runAsync1 = new Lazy<InstanceMethod>(() => new InstanceMethod(I_ASYNC_SESSION, Generic.Of(typeof(Task<>), I_RESULT_CURSOR), "RunAsync", Type<string>.Info), true);

            public Task<ResultCursor> RunAsync(object instance, string query, IDictionary<string, object?> parameters) => AsTask(_runAsync2.Value.Invoke(instance, query, parameters), instance => new ResultCursor(instance));
            private readonly Lazy<InstanceMethod> _runAsync2 = new Lazy<InstanceMethod>(() => new InstanceMethod(I_ASYNC_SESSION, Generic.Of(typeof(Task<>), I_RESULT_CURSOR), "RunAsync", Type<string>.Info, Type<IDictionary<string, object>>.Info), true);
        }
        internal sealed class SessionConfigBuilderInfo : DriverTypeInfo
        {
            public SessionConfigBuilderInfo(params string[] names) : base(names) { }

            public void WithDatabase(object instance, string database) => _withDatabase.Value.Invoke(instance, database);
            private readonly Lazy<InstanceMethod> _withDatabase = new Lazy<InstanceMethod>(() => new InstanceMethod(SESSION_CONFIG_BUILDER, SESSION_CONFIG_BUILDER, "WithDatabase", Type<string>.Info), true);

            public void WithDefaultAccessMode(object instance, object defaultAccessMode) => _withDefaultAccessMode.Value.Invoke(instance, defaultAccessMode);
            private readonly Lazy<InstanceMethod> _withDefaultAccessMode = new Lazy<InstanceMethod>(() => new InstanceMethod(SESSION_CONFIG_BUILDER, SESSION_CONFIG_BUILDER, "WithDefaultAccessMode", ACCESS_MODE), true);

            public void WithBookmarks(object instance, Bookmarks[] bookmarks) => _withBookmarks.Value.Invoke(instance, BOOKMARKS.ToArray(bookmarks.Select(item => item._instance), bookmarks.Length));
            private readonly Lazy<InstanceMethod> _withBookmarks = new Lazy<InstanceMethod>(() => new InstanceMethod(SESSION_CONFIG_BUILDER, SESSION_CONFIG_BUILDER, "WithBookmarks", BOOKMARKS.ARRAY), true);

            public void WithFetchSize(object instance, long size) => _withFetchSize.Value.Invoke(instance, size);
            private readonly Lazy<InstanceMethod> _withFetchSize = new Lazy<InstanceMethod>(() => new InstanceMethod(SESSION_CONFIG_BUILDER, SESSION_CONFIG_BUILDER, "WithFetchSize", Type<long>.Info), true);

            public void WithImpersonatedUser(object instance, string impersonatedUser) => _withImpersonatedUser.Value.Invoke(instance, impersonatedUser);
            private readonly Lazy<InstanceMethod> _withImpersonatedUser = new Lazy<InstanceMethod>(() => new InstanceMethod(SESSION_CONFIG_BUILDER, SESSION_CONFIG_BUILDER, "WithImpersonatedUser", Type<string>.Info), true);
        }
        internal sealed class IAsyncTransactionInfo : DriverTypeInfo
        {
            public IAsyncTransactionInfo(params string[] names) : base(names) { }

            public Task CommitAsync(object instance) => AsTask(_commitAsync.Value.Invoke(instance));
            private readonly Lazy<InstanceMethod> _commitAsync = new Lazy<InstanceMethod>(() => new InstanceMethod(I_ASYNC_TRANSACTION, Type<Task>.Info, "CommitAsync"), true);

            public Task RollbackAsync(object instance) => AsTask(_rollbackAsync.Value.Invoke(instance));
            private readonly Lazy<InstanceMethod> _rollbackAsync = new Lazy<InstanceMethod>(() => new InstanceMethod(I_ASYNC_TRANSACTION, Type<Task>.Info, "RollbackAsync"), true);

            public Task<ResultCursor> RunAsync(object instance, string query) => AsTask(_runAsync1.Value.Invoke(instance, query), instance => new ResultCursor(instance));
            private readonly Lazy<InstanceMethod> _runAsync1 = new Lazy<InstanceMethod>(() => new InstanceMethod(I_ASYNC_TRANSACTION, Generic.Of(typeof(Task<>), I_RESULT_CURSOR), "RunAsync", Type<string>.Info), true);

            public Task<ResultCursor> RunAsync(object instance, string query, IDictionary<string, object?> parameters) => AsTask(_runAsync2.Value.Invoke(instance, query, parameters), instance => new ResultCursor(instance));
            private readonly Lazy<InstanceMethod> _runAsync2 = new Lazy<InstanceMethod>(() => new InstanceMethod(I_ASYNC_TRANSACTION, Generic.Of(typeof(Task<>), I_RESULT_CURSOR), "RunAsync", Type<string>.Info, Type<IDictionary<string, object>>.Info), true);
        }
        internal sealed class TransactionConfigBuilderInfo : DriverTypeInfo
        {
            public TransactionConfigBuilderInfo(params string[] names) : base(names) { }

            public void WithTimeout(object instance, TimeSpan? timeout)
            {
                if (_withTimeout.Value.Exists)
                    _withTimeout.Value.Invoke(instance, timeout);
                else
                    _withTimeoutOld.Value.Invoke(instance, timeout ?? TimeSpan.Zero);
            }
            [OneOf(nameof(WithTimeout))]
            private readonly Lazy<InstanceMethod> _withTimeout = new Lazy<InstanceMethod>(() => new InstanceMethod(TRANSACTION_CONFIG_BUILDER, TRANSACTION_CONFIG_BUILDER, "WithTimeout", Type<TimeSpan?>.Info), true); // Driver 5
            [OneOf(nameof(WithTimeout))]
            private readonly Lazy<InstanceMethod> _withTimeoutOld = new Lazy<InstanceMethod>(() => new InstanceMethod(TRANSACTION_CONFIG_BUILDER, TRANSACTION_CONFIG_BUILDER, "WithTimeout", Type<TimeSpan>.Info), true); // Driver 4

            public void WithMetadata(object instance, IDictionary<string, object> metadata) => _withMetadata.Value.Invoke(instance, metadata);
            private readonly Lazy<InstanceMethod> _withMetadata = new Lazy<InstanceMethod>(() => new InstanceMethod(TRANSACTION_CONFIG_BUILDER, TRANSACTION_CONFIG_BUILDER, "WithMetadata", Type<IDictionary<string, object>>.Info), true);
        }
        internal sealed class AccessModeInfo : DriverTypeInfo
        {
            public AccessModeInfo(params string[] names) : base(names) { }

            public object Read => _read.Value.Value;
            public bool IsRead(object value) => _read.Value.Test(value);
            private readonly Lazy<EnumField> _read = new Lazy<EnumField>(() => new EnumField(ACCESS_MODE, "Read"), true);

            public object Write => _write.Value.Value;
            public bool IsWrite(object value) => _write.Value.Test(value);
            private readonly Lazy<EnumField> _write = new Lazy<EnumField>(() => new EnumField(ACCESS_MODE, "Write"), true);
        }
        internal sealed class BookmarksInfo : DriverTypeInfo
        {
            public BookmarksInfo(params string[] names) : base(names) { }

            public object From(string[] bookmarks) => _from.Value.Invoke(bookmarks);
            private readonly Lazy<StaticMethod> _from = new Lazy<StaticMethod>(() => new StaticMethod(BOOKMARKS, BOOKMARKS, "From", Type<string[]>.Info), true);

            public string[] Values(object instance) => (string[])_values.Value.GetValue(instance);
            private readonly Lazy<InstanceProperty> _values = new Lazy<InstanceProperty>(() => new InstanceProperty(BOOKMARKS, Type<string[]>.Info, "Values"), true);
        }
        internal sealed class INotificationInfo : DriverTypeInfo
        {
            public INotificationInfo(params string[] names) : base(names) { }

            public string Code(object instance) => (string)_code.Value.GetValue(instance);
            private readonly Lazy<InstanceProperty> _code = new Lazy<InstanceProperty>(() => new InstanceProperty(I_NOTIFICATION, Type<string>.Info, "Code"), true);

            public string Title(object instance) => (string)_title.Value.GetValue(instance);
            private readonly Lazy<InstanceProperty> _title = new Lazy<InstanceProperty>(() => new InstanceProperty(I_NOTIFICATION, Type<string>.Info, "Title"), true);

            public string Description(object instance) => (string)_description.Value.GetValue(instance);
            private readonly Lazy<InstanceProperty> _description = new Lazy<InstanceProperty>(() => new InstanceProperty(I_NOTIFICATION, Type<string>.Info, "Description"), true);

            public object Position(object instance) => _position.Value.GetValue(instance);
            private readonly Lazy<InstanceProperty> _position = new Lazy<InstanceProperty>(() => new InstanceProperty(I_NOTIFICATION, I_INPUT_POSITION, "Position"), true);
        }
        internal sealed class IInputPositionInfo : DriverTypeInfo
        {
            public IInputPositionInfo(params string[] names) : base(names) { }

            public int Offset(object instance) => (int)_code.Value.GetValue(instance);
            private readonly Lazy<InstanceProperty> _code = new Lazy<InstanceProperty>(() => new InstanceProperty(I_INPUT_POSITION, Type<int>.Info, "Offset"), true);

            public int Line(object instance) => (int)_line.Value.GetValue(instance);
            private readonly Lazy<InstanceProperty> _line = new Lazy<InstanceProperty>(() => new InstanceProperty(I_INPUT_POSITION, Type<int>.Info, "Line"), true);

            public int Column(object instance) => (int)_column.Value.GetValue(instance);
            private readonly Lazy<InstanceProperty> _column = new Lazy<InstanceProperty>(() => new InstanceProperty(I_INPUT_POSITION, Type<int>.Info, "Column"), true);
        }
        internal sealed class INodeInfo : DriverTypeInfo
        {
            public INodeInfo(params string[] names) : base(names) { }

            public string ElementId(object instance)
            {
                if (_elementId.Value.Exists)
                    return (string)_elementId.Value.GetValue(instance);
                else
                    return ((long)_id.Value.GetValue(instance)).ToString();
            }
            [OneOf(nameof(ElementId))]
            private readonly Lazy<InstanceProperty> _elementId = new Lazy<InstanceProperty>(() => new InstanceProperty(I_NODE, Type<string>.Info, "ElementId"), true);
            [OneOf(nameof(ElementId))]
            private readonly Lazy<InstanceProperty> _id = new Lazy<InstanceProperty>(() => new InstanceProperty(I_NODE, Type<long>.Info, "Id"), true);

            public IReadOnlyList<string> Labels(object instance) => (IReadOnlyList<string>)_labels.Value.GetValue(instance);
            private readonly Lazy<InstanceProperty> _labels = new Lazy<InstanceProperty>(() => new InstanceProperty(I_NODE, Type<IReadOnlyList<string>>.Info, "Labels"), true);

            public object Item(object instance, string key) => _item.Value.GetValue(instance, key);
            private readonly Lazy<InstanceProperty> _item = new Lazy<InstanceProperty>(() => new InstanceProperty(I_NODE, Type<object>.Info, "Item", Type<string>.Info), true);

            public IReadOnlyDictionary<string, object?> Properties(object instance) => (IReadOnlyDictionary<string, object?>)_properties.Value.GetValue(instance);
            private readonly Lazy<InstanceProperty> _properties = new Lazy<InstanceProperty>(() => new InstanceProperty(I_NODE, Type<IReadOnlyDictionary<string, object>>.Info, "Properties"), true);

            public bool EqualsINode(object instance, object value) => (bool)_equals.Value.Invoke(instance, value);
            private readonly Lazy<InstanceMethod> _equals = new Lazy<InstanceMethod>(() => new InstanceMethod(I_NODE, Type<bool>.Info, "Equals", I_NODE));
        }
        internal sealed class IRelationshipInfo : DriverTypeInfo
        {
            public IRelationshipInfo(params string[] names) : base(names) { }

            public string ElementId(object instance)
            {
                if (_elementId.Value.Exists)
                    return (string)_elementId.Value.GetValue(instance);
                else
                    return ((long)_id.Value.GetValue(instance)).ToString();
            }
            [OneOf(nameof(ElementId))]
            private readonly Lazy<InstanceProperty> _elementId = new Lazy<InstanceProperty>(() => new InstanceProperty(I_RELATIONSHIP, Type<string>.Info, "ElementId"), true);
            [OneOf(nameof(ElementId))]
            private readonly Lazy<InstanceProperty> _id = new Lazy<InstanceProperty>(() => new InstanceProperty(I_NODE, Type<long>.Info, "Id"), true);

            public string RelationshipType(object instance) => (string)_type.Value.GetValue(instance);
            private readonly Lazy<InstanceProperty> _type = new Lazy<InstanceProperty>(() => new InstanceProperty(I_RELATIONSHIP, Type<string>.Info, "Type"), true);

            public string StartNodeElementId(object instance)
            {
                if (_elementId.Value.Exists)
                    return (string)_startNodeElementId.Value.GetValue(instance);
                else
                    return ((long)_startNodeId.Value.GetValue(instance)).ToString();
            }
            [OneOf(nameof(StartNodeElementId))]
            private readonly Lazy<InstanceProperty> _startNodeElementId = new Lazy<InstanceProperty>(() => new InstanceProperty(I_RELATIONSHIP, Type<string>.Info, "StartNodeElementId"), true);
            [OneOf(nameof(StartNodeElementId))]
            private readonly Lazy<InstanceProperty> _startNodeId = new Lazy<InstanceProperty>(() => new InstanceProperty(I_RELATIONSHIP, Type<long>.Info, "StartNodeId"), true);

            public string EndNodeElementId(object instance)
            {
                if (_elementId.Value.Exists)
                    return (string)_endNodeElementId.Value.GetValue(instance);
                else
                    return ((long)_endNodeId.Value.GetValue(instance)).ToString();
            }
            [OneOf(nameof(EndNodeElementId))] 
            private readonly Lazy<InstanceProperty> _endNodeElementId = new Lazy<InstanceProperty>(() => new InstanceProperty(I_RELATIONSHIP, Type<string>.Info, "EndNodeElementId"), true);
            [OneOf(nameof(EndNodeElementId))] 
            private readonly Lazy<InstanceProperty> _endNodeId = new Lazy<InstanceProperty>(() => new InstanceProperty(I_RELATIONSHIP, Type<long>.Info, "EndNodeId"), true);

            public object Item(object instance, string key) => _item.Value.GetValue(instance, key);
            private readonly Lazy<InstanceProperty> _item = new Lazy<InstanceProperty>(() => new InstanceProperty(I_RELATIONSHIP, Type<object>.Info, "Item", Type<string>.Info), true);

            public IReadOnlyDictionary<string, object?> Properties(object instance) => (IReadOnlyDictionary<string, object?>)_properties.Value.GetValue(instance);
            private readonly Lazy<InstanceProperty> _properties = new Lazy<InstanceProperty>(() => new InstanceProperty(I_RELATIONSHIP, Type<IReadOnlyDictionary<string, object>>.Info, "Properties"), true);

            public bool EqualsIRelationship(object instance, object value) => (bool)_equals.Value.Invoke(instance, value);
            private readonly Lazy<InstanceMethod> _equals = new Lazy<InstanceMethod>(() => new InstanceMethod(I_RELATIONSHIP, Type<bool>.Info, "Equals", I_RELATIONSHIP));
        }
        internal sealed class ValueExtensionsInfo : DriverTypeInfo
        {
            public ValueExtensionsInfo(params string[] names) : base(names) { }

            public T As<T>(object value) => (T)_as.Value.ForTypes(Type<T>.Info, Type<object>.Info).Invoke(value);
            public object As(DriverTypeInfo type, object value) => _as.Value.ForTypes(VALUE_EXTENSIONS, type, Type<object>.Info).Invoke(value);

            private readonly Lazy<Generic<StaticMethod>> _as = new Lazy<Generic<StaticMethod>>(() => new Generic<StaticMethod>(VALUE_EXTENSIONS, "As"));
        }
        internal sealed class EncryptionLevelInfo : DriverTypeInfo
        {
            public EncryptionLevelInfo(params string[] names) : base(names) { }

            public object None => _none.Value.Value;
            public bool IsNone(object value) => _none.Value.Test(value);
            private readonly Lazy<EnumField> _none = new Lazy<EnumField>(() => new EnumField(ENCRYPTION_LEVEL, "None"), true);

            public object Encrypted => _encrypted.Value.Value;
            public bool IsEncrypted(object value) => _encrypted.Value.Test(value);
            private readonly Lazy<EnumField> _encrypted = new Lazy<EnumField>(() => new EnumField(ENCRYPTION_LEVEL, "Encrypted"), true);
        }
        internal sealed class CategoryInfo : DriverTypeInfo
        {
            public CategoryInfo(params string[] names) : base(names) { }

            //Hint,
            //Unrecognized,
            //Unsupported,
            //Performance,
            //Deprecation,
            //Generic
        }
        internal sealed class SeverityInfo : DriverTypeInfo
        {
            public SeverityInfo(params string[] names) : base(names) { }

            //Warning,
            //Information
        }
        internal sealed class CertificateTrustRuleInfo : DriverTypeInfo
        {
            public CertificateTrustRuleInfo(params string[] names) : base(names) { }

            //TrustSystem,
            //TrustList,
            //TrustAny
        }
        internal sealed class ServerAddressInfo : DriverTypeInfo
        {
            public ServerAddressInfo(params string[] names) : base(names) { }

            //public static ServerAddress From(string host, int port) => null!;
            //public static ServerAddress From(Uri uri) => null!;

            //public bool Equals(ServerAddress other) => false!;
        }
        internal sealed class TrustManagerInfo : DriverTypeInfo
        {
            public TrustManagerInfo(params string[] names) : base(names) { }

            //public static TrustManager CreateInsecure(bool verifyHostname = false) => null!;
            //public static TrustManager CreateChainTrust(bool verifyHostname = true, X509RevocationMode revocationMode = X509RevocationMode.NoCheck, X509RevocationFlag revocationFlag = X509RevocationFlag.ExcludeRoot, bool useMachineContext = false) => null!;
            //public static TrustManager CreatePeerTrust(bool verifyHostname = true, bool useMachineContext = false) => null!;
            //public static TrustManager CreateCertTrust(IEnumerable<X509Certificate2> trusted, bool verifyHostname = true) => null!;
        }
        internal sealed class IServerAddressResolverInfo : DriverTypeInfo
        {
            public IServerAddressResolverInfo(params string[] names) : base(names) { }

            public object ConvertToIServerAddressResolver(ServerAddressResolver instance) => _get.Value.Invoke(instance);
            private readonly Lazy<StaticMethod> _get = new Lazy<StaticMethod>(() => new StaticMethod(SERVER_ADDRESS_RESOLVER_PROXY, SERVER_ADDRESS_RESOLVER_PROXY, "Get", Type<ServerAddressResolver>.Info), true);
        }

        #endregion

        public void Dispose() => ((IDisposable)_instance).Dispose();
        public ValueTask DisposeAsync() => ((IAsyncDisposable)_instance).DisposeAsync();
    }
}
