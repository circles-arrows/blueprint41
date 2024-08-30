#pragma warning disable S3881

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

using Blueprint41.Core;

namespace Blueprint41.Driver
{
    public sealed class Driver : IDisposable, IAsyncDisposable
    {
        internal Driver(object value)
        {
            Value = value;
        }
        internal object Value { get; private set; }

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
        }

        #region DriverTypeInfo definitions 

        internal static readonly IDriverInfo                  I_DRIVER                             = new IDriverInfo("Neo4j.Driver.IDriver");
        internal static readonly ConfigBuilderInfo            CONFIG_BUILDER                       = new ConfigBuilderInfo("Neo4j.Driver.ConfigBuilder");
        internal static readonly GraphDatabaseInfo            GRAPH_DATABASE                       = new GraphDatabaseInfo("Neo4j.Driver.GraphDatabase");
        internal static readonly IAuthTokenInfo               I_AUTH_TOKEN                         = new IAuthTokenInfo("Neo4j.Driver.IAuthToken");
        internal static readonly AuthTokensInfo               AUTH_TOKENS                          = new AuthTokensInfo("Neo4j.Driver.AuthTokens");
        internal static readonly IResultCursorInfo            I_RESULT_CURSOR                      = new IResultCursorInfo("Neo4j.Driver.IResultCursor");
        internal static readonly IResultSummaryInfo           I_RESULT_SUMMARY                     = new IResultSummaryInfo("Neo4j.Driver.IResultSummary");
        internal static readonly IRecord                      I_RECORD                             = new IRecord("Neo4j.Driver.IRecord");
        internal static readonly IAsyncSessionInfo            I_ASYNC_SESSION                      = new IAsyncSessionInfo("Neo4j.Driver.IAsyncSession");
        internal static readonly SessionConfigBuilderInfo     SESSION_CONFIG_BUILDER               = new SessionConfigBuilderInfo("Neo4j.Driver.SessionConfigBuilder");
        internal static readonly IAsyncTransactionInfo        I_ASYNC_TRANSACTION                  = new IAsyncTransactionInfo("Neo4j.Driver.IAsyncTransaction");
        internal static readonly TransactionConfigBuilderInfo TRANSACTION_CONFIG_BUILDER           = new TransactionConfigBuilderInfo("Neo4j.Driver.TransactionConfigBuilder");
        internal static readonly IAsyncQueryRunnerInfo        I_ASYNC_QUERY_RUNNER                 = new IAsyncQueryRunnerInfo("Neo4j.Driver.IAsyncQueryRunner");
        internal static readonly AccessModeInfo               ACCESS_MODE                          = new AccessModeInfo("Neo4j.Driver.AccessMode");
        internal static readonly BookmarksInfo                BOOKMARKS                            = new BookmarksInfo("Neo4j.Driver.Bookmarks", "Neo4j.Driver.Bookmark");
        internal static readonly QueryInfo                    QUERY                                = new QueryInfo("Neo4j.Driver.Query");
        internal static readonly ICountersInfo                I_COUNTERS                           = new ICountersInfo("Neo4j.Driver.ICounters");
        internal static readonly INotificationInfo            I_NOTIFICATION                       = new INotificationInfo("Neo4j.Driver.INotification");
        internal static readonly IInputPositionInfo           I_INPUT_POSITION                     = new IInputPositionInfo("Neo4j.Driver.IInputPosition");
        internal static readonly ValueExtensionsInfo          VALUE_EXTENSIONS                     = new ValueExtensionsInfo("Neo4j.Driver.ValueExtensions");
        internal static readonly INodeInfo                    I_NODE                               = new INodeInfo("Neo4j.Driver.INode");
        internal static readonly IRelationshipInfo            I_RELATIONSHIP                       = new IRelationshipInfo("Neo4j.Driver.IRelationship");

        internal static readonly DriverTypeInfo               I_ENTITY                             = new DriverTypeInfo("Neo4j.Driver.IEntity");

        internal static class Generic
        {
            internal static DriverTypeInfo Of(Type type, params DriverTypeInfo[] args) => new DriverTypeInfo(type.MakeGenericType(args.Select(item => item.Type).ToArray()));
        }
        internal static class Type<T>
        {
            internal static readonly DriverTypeInfo Info = new DriverTypeInfo(typeof(T));
        }

        #endregion

        #region Type, Property, Method & Enum helpers

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

                ARRAY = new DriverTypeInfo(() => Type.MakeArrayType());
            }
            internal DriverTypeInfo(Type type)
            {
                _typeInit = new Lazy<Type?>(() => type, true);
                Names = null;

                ARRAY = new DriverTypeInfo(() => Type.MakeArrayType());
            }
            internal DriverTypeInfo(Func<Type?> typeInitializer)
            {
                _typeInit = new Lazy<Type?>(typeInitializer, true);
                Names = null;

                ARRAY = new DriverTypeInfo(() => Type.MakeArrayType());
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

            public object ToArray(Array items)
            {
                Array array = Array.CreateInstance(Type, items.Length);

                for (int index = 0; index < items.Length; index++)
                    array.SetValue(items.GetValue(index), index);

                return array;
            }
            public object ToArray(IList items)
            {
                Array array = Array.CreateInstance(Type, items.Count);

                for (int index = 0; index < items.Count; index++)
                    array.SetValue(items[index], index);

                return array;
            }
            public object ToArray<T>(IEnumerable<T> items, int? count = null)
            {
                Array array = Array.CreateInstance(Type, count ?? items.Count());

                int index = 0;
                foreach (object item in items)
                    array.SetValue(item, index++);

                return array;
            }

            public object ToList(Array items)
            {
                IList list = CreateList(items.Length);

                for (int index = 0; index < items.Length; index++)
                    list.Add(items.GetValue(index));

                return list;
            }
            public object ToList(IList items)
            {
                IList list = CreateList(items.Count);

                for (int index = 0; index < items.Count; index++)
                    list.Add(items[index]);

                return list;
            }
            public object ToList<T>(IEnumerable<T> items, int? count = null)
            {
                IList list = CreateList(count);

                foreach (object item in items)
                    list.Add(item);

                return list;
            }
            private IList CreateList(int? count)
            {
                Type genericListType = typeof(List<>).MakeGenericType(Type);
                IList list = (count.HasValue) ? (IList)Activator.CreateInstance(genericListType, count) : (IList)Activator.CreateInstance(genericListType);
                return list;
            }

#nullable enable

            public readonly DriverTypeInfo ARRAY;
        }
        internal interface IMember { }
        internal sealed class StaticProperty : IMember
        {
            internal StaticProperty(DriverTypeInfo parent, DriverTypeInfo returnType, string name, DriverTypeInfo? indexer = null)
            {
                _propertyInfo = new Lazy<PropertyInfo?>(delegate ()
                {
                    PropertyInfo? propertyInfo = parent.Type.GetProperty(name, BindingFlags.Public | BindingFlags.Static, null, returnType.Type, (indexer is null) ? new Type[0] : new Type[] { indexer.Type }, null);
                    if (propertyInfo is not null)
                        return propertyInfo;

                    return null;
                }, true);
            }
            internal StaticProperty(DriverTypeInfo parent, DriverTypeInfo returnType, string[] names, DriverTypeInfo? indexer = null)
            {
                _propertyInfo = new Lazy<PropertyInfo?>(delegate ()
                {
                    foreach (string name in names)
                    {
                        PropertyInfo? propertyInfo = parent.Type.GetProperty(name, BindingFlags.Public | BindingFlags.Static, null, returnType.Type, (indexer is null) ? new Type[0] : new Type[] { indexer.Type }, null);
                        if (propertyInfo is not null)
                            return propertyInfo;
                    }
                    return null;
                }, true);
            }

            public bool Exists => (_propertyInfo.Value is not null);

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
        internal sealed class InstanceProperty : IMember
        {
            internal InstanceProperty(DriverTypeInfo parent, DriverTypeInfo returnType, string name, DriverTypeInfo? indexer = null)
            {
                _propertyInfo = new Lazy<PropertyInfo?>(delegate ()
                {
                    PropertyInfo? propertyInfo = parent.Type.GetProperty(name, BindingFlags.Public | BindingFlags.Instance, null, returnType.Type, (indexer is null) ? new Type[0] : new Type[] { indexer.Type }, null);
                    if (propertyInfo is not null)
                        return propertyInfo;

                    return null;
                }, true);
            }
            internal InstanceProperty(DriverTypeInfo parent, DriverTypeInfo returnType, string[] names, DriverTypeInfo? indexer = null)
            {
                _propertyInfo = new Lazy<PropertyInfo?>(delegate ()
                {
                    foreach (string name in names)
                    {
                        PropertyInfo? propertyInfo = parent.Type.GetProperty(name, BindingFlags.Public | BindingFlags.Instance, null, returnType.Type, (indexer is null) ? new Type[0] : new Type[] { indexer.Type }, null);
                        if (propertyInfo is not null)
                            return propertyInfo;
                    }
                    return null;
                }, true);
            }

            public bool Exists => (_propertyInfo.Value is not null);

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
        internal sealed class StaticMethod : IMember
        {
            internal StaticMethod(DriverTypeInfo parent, DriverTypeInfo returnType, string name, params DriverTypeInfo[] arguments)
            {
                _methodInfo = new Lazy<MethodInfo?>(delegate ()
                {
                    MethodInfo? methodInfo = parent.Type.GetMethod(name, BindingFlags.Public | BindingFlags.Static, null, arguments?.Select(info => info.Type).ToArray() ?? new Type[0], null);
                    if (methodInfo?.ReturnType == returnType.Type)
                        return methodInfo;

                    return null;
                }, true);
            }
            internal StaticMethod(DriverTypeInfo parent, DriverTypeInfo returnType, string[] names, params DriverTypeInfo[] arguments)
            {
                _methodInfo = new Lazy<MethodInfo?>(delegate ()
                {
                    foreach (string name in names)
                    {
                        MethodInfo? methodInfo = parent.Type.GetMethod(name, BindingFlags.Public | BindingFlags.Static, null, arguments?.Select(info => info.Type).ToArray() ?? new Type[0], null);
                        if (methodInfo?.ReturnType == returnType.Type)
                            return methodInfo;
                    }
                    return null;
                }, true);
            }

            public bool Exists => (_methodInfo.Value is not null);

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
        internal sealed class InstanceMethod : IMember
        {
            internal InstanceMethod(DriverTypeInfo parent, DriverTypeInfo returnType, string name, params DriverTypeInfo[] arguments)
            {
                _methodInfo = new Lazy<MethodInfo?>(delegate ()
                {
                    MethodInfo? methodInfo = parent.Type.GetMethod(name, BindingFlags.Public | BindingFlags.Instance, null, arguments?.Select(info => info.Type).ToArray() ?? new Type[0], null);
                    if (methodInfo?.ReturnType == returnType.Type)
                        return methodInfo;

                    return null;
                }, true);
            }
            internal InstanceMethod(DriverTypeInfo parent, DriverTypeInfo returnType, string[] names, params DriverTypeInfo[] arguments)
            {
                _methodInfo = new Lazy<MethodInfo?>(delegate ()
                {
                    foreach (string name in names)
                    {
                        MethodInfo? methodInfo = parent.Type.GetMethod(name, BindingFlags.Public | BindingFlags.Instance, null, arguments?.Select(info => info.Type).ToArray() ?? new Type[0], null);
                        if (methodInfo?.ReturnType == returnType.Type)
                            return methodInfo;
                    }
                    return null;
                }, true);
            }

            public bool Exists => (_methodInfo.Value is not null);

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
        internal sealed class EnumField : IMember
        {
            internal EnumField(DriverTypeInfo parent, string name)
            {
                _fieldInfo = new Lazy<FieldInfo?>(delegate ()
                {
                    FieldInfo? fieldInfo = parent.Type.GetFields().FirstOrDefault(fi => fi.IsLiteral && fi.Name == name);
                    if (fieldInfo is not null)
                        return fieldInfo;

                    return null;
                }, true);
            }
            internal EnumField(DriverTypeInfo parent, string[] names)
            {
                _fieldInfo = new Lazy<FieldInfo?>(delegate ()
                {
                    foreach (string name in names)
                    {
                        FieldInfo? fieldInfo = parent.Type.GetFields().FirstOrDefault(fi => fi.IsLiteral && fi.Name == name);
                        if (fieldInfo is not null)
                            return fieldInfo;
                    }
                    return null;
                }, true);
            }

            public bool Exists => (_fieldInfo.Value is not null);

            public object Value => FieldInfo.GetRawConstantValue();
            public bool Test(object instance) => Value == instance;

            private FieldInfo FieldInfo => _fieldInfo.Value ?? throw new MissingMethodException();
            private readonly Lazy<FieldInfo?> _fieldInfo;
        }

        internal sealed class Generic<TMember>
            where TMember : IMember
        {
            internal Generic(DriverTypeInfo parent, string name)
            {
                Parent = parent;
                Name = name;
                Names = null;
                HasReturnType = (typeof(TMember) != typeof(EnumField));
            }
            internal Generic(DriverTypeInfo parent, string[] names)
            {
                Parent = parent;
                Name = null;
                Names = names;
                HasReturnType = (typeof(TMember) != typeof(EnumField));
            }

            private readonly DriverTypeInfo Parent;
            private readonly string? Name;
            private readonly string[]? Names;
            private readonly bool HasReturnType;

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
                            member = CreateInstance(Name is not null ? Name : Names);
                            _newCache.Add(signature, member);

                            Interlocked.Exchange(ref _cache, _newCache);
                        }
                    }
                }
                return member;

                TMember CreateInstance(object? name)
                {
                    if (name is null)
                        throw new InvalidOperationException();

                    object? instance;
                    if (HasReturnType)
                    {
                        instance = Activator.CreateInstance(typeof(TMember), new object[] { Parent, returnType, name, arguments });
                    }
                    else
                    {
                        instance = Activator.CreateInstance(typeof(TMember), new object[] { Parent, name, arguments });
                    }

                    if (instance is null)
                        throw new InvalidOperationException();

                    return (TMember)instance;
                }
            }
            private Dictionary<GenericSignature, TMember> _cache = new Dictionary<GenericSignature, TMember>();

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

            public Session AsyncSession(Driver driver) => new Session(_asyncSession1.Value.Invoke(driver.Value));
            private readonly Lazy<InstanceMethod> _asyncSession1 = new Lazy<InstanceMethod>(() => new InstanceMethod(I_DRIVER, I_ASYNC_SESSION, "AsyncSession"), true);

            public Session AsyncSession(Driver driver, Action<SessionConfigBuilder> configBuilder) => new Session(_asyncSession2.Value.Invoke(driver.Value, DelegateHelper.WrapDelegate(Generic.Of(typeof(Action<>), SESSION_CONFIG_BUILDER).Type, (object o) => configBuilder.Invoke(new SessionConfigBuilder(o)))));
            private readonly Lazy<InstanceMethod> _asyncSession2 = new Lazy<InstanceMethod>(() => new InstanceMethod(I_DRIVER, I_ASYNC_SESSION, "AsyncSession", Generic.Of(typeof(Action<>), SESSION_CONFIG_BUILDER)), true);

            //Task<IServerInfo> GetServerInfoAsync();

            public Task<bool> TryVerifyConnectivityAsync(Driver driver) => AsTask<bool>(_tryVerifyConnectivityAsync.Value.Invoke(driver.Value));
            private readonly Lazy<InstanceMethod> _tryVerifyConnectivityAsync = new Lazy<InstanceMethod>(() => new InstanceMethod(I_DRIVER, Type<Task<bool>>.Info, "TryVerifyConnectivityAsync", Generic.Of(typeof(Action<>), SESSION_CONFIG_BUILDER)), true);

            //Task VerifyConnectivityAsync();

            public Task<bool> SupportsMultiDbAsync(Driver driver) => AsTask<bool>(_supportsMultiDbAsync.Value.Invoke(driver.Value));
            private readonly Lazy<InstanceMethod> _supportsMultiDbAsync = new Lazy<InstanceMethod>(() => new InstanceMethod(I_DRIVER, Type<Task<bool>>.Info, "SupportsMultiDbAsync", Generic.Of(typeof(Action<>), SESSION_CONFIG_BUILDER)), true);
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

            public Driver Driver(Uri uri, AuthToken authToken, Action<ConfigBuilder> configBuilder) => new Driver(_driver.Value.Invoke(uri, authToken.Value, DelegateHelper.WrapDelegate(Generic.Of(typeof(Action<>), CONFIG_BUILDER).Type, (object o) => configBuilder.Invoke(new ConfigBuilder(o)))));
            private readonly Lazy<StaticMethod> _driver = new Lazy<StaticMethod>(() => new StaticMethod(GRAPH_DATABASE, I_DRIVER, "Driver", Type<Uri>.Info, I_AUTH_TOKEN, Generic.Of(typeof(Action<>), CONFIG_BUILDER)), true);
        }
        internal sealed class IAuthTokenInfo : DriverTypeInfo
        {
            public IAuthTokenInfo(params string[] names) : base(names) { }
        }
        internal sealed class AuthTokensInfo : DriverTypeInfo
        {
            public AuthTokensInfo(params string[] names) : base(names) { }

            public AuthToken None { get => new AuthToken(_none.Value.GetValue()); set => _none.Value.SetValue(value.Value); }
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

            public bool IsOpen(object instance) => (bool)_isOpen.Value.GetValue(instance);
            private readonly Lazy<InstanceProperty> _isOpen = new Lazy<InstanceProperty>(() => new InstanceProperty(I_RESULT_CURSOR, Type<bool>.Info, "IsOpen"), true);

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

            public List<Notification> Notifications(object instance) => (List<Notification>)ToList((IList)_notifications.Value.GetValue(instance)); //  IList<INotification> Notifications { get; }
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
        }
        internal sealed class SessionConfigBuilderInfo : DriverTypeInfo
        {
            public SessionConfigBuilderInfo(params string[] names) : base(names) { }

            public void WithDatabase(object instance, string database) => _withDatabase.Value.Invoke(instance, database);
            private readonly Lazy<InstanceMethod> _withDatabase = new Lazy<InstanceMethod>(() => new InstanceMethod(SESSION_CONFIG_BUILDER, SESSION_CONFIG_BUILDER, "WithDatabase", Type<string>.Info), true);

            public void WithDefaultAccessMode(object instance, object defaultAccessMode) => _withDefaultAccessMode.Value.Invoke(instance, defaultAccessMode);
            private readonly Lazy<InstanceMethod> _withDefaultAccessMode = new Lazy<InstanceMethod>(() => new InstanceMethod(SESSION_CONFIG_BUILDER, SESSION_CONFIG_BUILDER, "WithDefaultAccessMode", ACCESS_MODE), true);

            public void WithBookmarks(object instance, Bookmarks[] bookmarks) => _withBookmarks.Value.Invoke(instance, BOOKMARKS.ToArray(bookmarks.Select(item => item.Value), bookmarks.Length));
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
            private readonly Lazy<InstanceMethod> _withTimeout = new Lazy<InstanceMethod>(() => new InstanceMethod(TRANSACTION_CONFIG_BUILDER, TRANSACTION_CONFIG_BUILDER, "WithTimeout", Type<TimeSpan?>.Info), true); // Driver 5
            private readonly Lazy<InstanceMethod> _withTimeoutOld = new Lazy<InstanceMethod>(() => new InstanceMethod(TRANSACTION_CONFIG_BUILDER, TRANSACTION_CONFIG_BUILDER, "WithTimeout", Type<TimeSpan>.Info), true); // Driver 4

            public void WithMetadata(object instance, IDictionary<string, object> metadata) => _withMetadata.Value.Invoke(instance, metadata);
            private readonly Lazy<InstanceMethod> _withMetadata = new Lazy<InstanceMethod>(() => new InstanceMethod(TRANSACTION_CONFIG_BUILDER, TRANSACTION_CONFIG_BUILDER, "WithMetadata", Type<IDictionary<string, object>>.Info), true);
        }
        internal sealed class IAsyncQueryRunnerInfo : DriverTypeInfo
        {
            public IAsyncQueryRunnerInfo(params string[] names) : base(names) { }

            public Task<ResultCursor> RunAsync(object instance, string query) => AsTask(_runAsync1.Value.Invoke(instance, query), instance => new ResultCursor(instance));
            private readonly Lazy<InstanceMethod> _runAsync1 = new Lazy<InstanceMethod>(() => new InstanceMethod(I_ASYNC_QUERY_RUNNER, Generic.Of(typeof(Task<>), I_RESULT_CURSOR), "RunAsync", Type<string>.Info), true);

            public Task<ResultCursor> RunAsync(object instance, string query, IDictionary<string, object?> parameters) => AsTask(_runAsync2.Value.Invoke(instance, query, parameters), instance => new ResultCursor(instance));
            private readonly Lazy<InstanceMethod> _runAsync2 = new Lazy<InstanceMethod>(() => new InstanceMethod(I_ASYNC_QUERY_RUNNER, Generic.Of(typeof(Task<>), I_RESULT_CURSOR), "RunAsync", Type<string>.Info, Type<IDictionary<string, object>>.Info), true);
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
        internal sealed class ValueExtensionsInfo : DriverTypeInfo
        {
            public ValueExtensionsInfo(params string[] names) : base(names) { }

            public T As<T>(object value) => (T)_as.Value.ForTypes(VALUE_EXTENSIONS, Type<T>.Info, Type<object>.Info).Invoke(value);
            public object As(DriverTypeInfo type, object value) => _as.Value.ForTypes(VALUE_EXTENSIONS, type, Type<object>.Info).Invoke(value);

            private readonly Lazy<Generic<StaticMethod>> _as = new Lazy<Generic<StaticMethod>>(() => new Generic<StaticMethod>(VALUE_EXTENSIONS, "As"));
        }
        internal sealed class INodeInfo : DriverTypeInfo
        {
            public INodeInfo(params string[] names) : base(names) { }

            public string ElementId(object instance) => (string)_elementId.Value.GetValue(instance);
            private readonly Lazy<InstanceProperty> _elementId = new Lazy<InstanceProperty>(() => new InstanceProperty(I_ENTITY, Type<string>.Info, "ElementId"), true);

            public IReadOnlyList<string> Labels(object instance) => (IReadOnlyList<string>)_labels.Value.GetValue(instance);
            private readonly Lazy<InstanceProperty> _labels = new Lazy<InstanceProperty>(() => new InstanceProperty(I_RELATIONSHIP, Type<IReadOnlyList<string>>.Info, "Labels"), true);

            public object Item(object instance, string key) => _item.Value.GetValue(instance, key);
            private readonly Lazy<InstanceProperty> _item = new Lazy<InstanceProperty>(() => new InstanceProperty(I_ENTITY, Type<object>.Info, "Item", Type<string>.Info), true);

            public IReadOnlyDictionary<string, object?> Properties(object instance) => (IReadOnlyDictionary<string, object?>)_properties.Value.GetValue(instance);
            private readonly Lazy<InstanceProperty> _properties = new Lazy<InstanceProperty>(() => new InstanceProperty(I_ENTITY, Type<IReadOnlyDictionary<string, object>>.Info, "Properties"), true);

            public bool EqualsINode(object instance, object value) => (bool)_equals.Value.Invoke(instance, value);
            private readonly Lazy<InstanceMethod> _equals = new Lazy<InstanceMethod>(() => new InstanceMethod(Generic.Of(typeof(IEquatable<>), I_NODE), Type<bool>.Info, "Equals", I_NODE));
        }
        internal sealed class IRelationshipInfo : DriverTypeInfo
        {
            public IRelationshipInfo(params string[] names) : base(names) { }

            public string ElementId(object instance) => (string)_elementId.Value.GetValue(instance);
            private readonly Lazy<InstanceProperty> _elementId = new Lazy<InstanceProperty>(() => new InstanceProperty(I_ENTITY, Type<string>.Info, "ElementId"), true);

            public string RelationshipType(object instance) => (string)_type.Value.GetValue(instance);
            private readonly Lazy<InstanceProperty> _type = new Lazy<InstanceProperty>(() => new InstanceProperty(I_RELATIONSHIP, Type<string>.Info, "Type"), true);

            public string StartNodeElementId(object instance) => (string)_startNodeElementId.Value.GetValue(instance);
            private readonly Lazy<InstanceProperty> _startNodeElementId = new Lazy<InstanceProperty>(() => new InstanceProperty(I_RELATIONSHIP, Type<string>.Info, "StartNodeElementId"), true);

            public string EndNodeElementId(object instance) => (string)_endNodeElementId.Value.GetValue(instance);
            private readonly Lazy<InstanceProperty> _endNodeElementId = new Lazy<InstanceProperty>(() => new InstanceProperty(I_RELATIONSHIP, Type<string>.Info, "EndNodeElementId"), true);

            public object Item(object instance, string key) => _item.Value.GetValue(instance, key);
            private readonly Lazy<InstanceProperty> _item = new Lazy<InstanceProperty>(() => new InstanceProperty(I_ENTITY, Type<object>.Info, "Item", Type<string>.Info), true);

            public IReadOnlyDictionary<string, object?> Properties(object instance) => (IReadOnlyDictionary<string, object?>)_properties.Value.GetValue(instance);
            private readonly Lazy<InstanceProperty> _properties = new Lazy<InstanceProperty>(() => new InstanceProperty(I_ENTITY, Type<IReadOnlyDictionary<string, object>>.Info, "Properties"), true);

            public bool EqualsIRelationship(object instance, object value) => (bool)_equals.Value.Invoke(instance, value);
            private readonly Lazy<InstanceMethod> _equals = new Lazy<InstanceMethod>(() => new InstanceMethod(Generic.Of(typeof(IEquatable<>), I_RELATIONSHIP), Type<bool>.Info, "Equals", I_NODE));
        }

        #endregion

        public void Dispose() => ((IDisposable)Value).Dispose();
        public ValueTask DisposeAsync() => ((IAsyncDisposable)Value).DisposeAsync();
    }
}
