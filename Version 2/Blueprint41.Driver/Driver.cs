#pragma warning disable S3881

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

        internal static readonly DriverTypeInfo               TASK                                 = new DriverTypeInfo(typeof(Task));
        internal static readonly DriverTypeInfo               TASK_OF_BOOLEAN                      = new DriverTypeInfo(typeof(Task<bool>));
        internal static readonly DriverTypeInfo               TASK_OF_STRING                       = new DriverTypeInfo(typeof(Task<string>));
        internal static readonly DriverTypeInfo               TASK_OF_STRING_ARRAY                 = new DriverTypeInfo(typeof(Task<string[]>));
        internal static readonly DriverTypeInfo               TASK_OF_I_ASYNC_TRANSACTION          = new DriverTypeInfo(() => typeof(Task<>).MakeGenericType(I_ASYNC_TRANSACTION.Type));
        internal static readonly DriverTypeInfo               TASK_OF_I_RESULT_CURSOR              = new DriverTypeInfo(() => typeof(Task<>).MakeGenericType(I_RESULT_CURSOR.Type));
        internal static readonly DriverTypeInfo               TASK_OF_I_RESULT_SUMMARY             = new DriverTypeInfo(() => typeof(Task<>).MakeGenericType(I_RESULT_SUMMARY.Type));
        internal static readonly DriverTypeInfo               TASK_OF_I_RECORD                     = new DriverTypeInfo(() => typeof(Task<>).MakeGenericType(I_RECORD.Type));
        internal static readonly DriverTypeInfo               BOOKMARKS_ARRAY                      = new DriverTypeInfo(() => BOOKMARKS.Type.MakeArrayType());

        internal static readonly DriverTypeInfo               VOID                                 = new DriverTypeInfo(typeof(void));
        internal static readonly DriverTypeInfo               OBJECT                               = new DriverTypeInfo(typeof(object));
        internal static readonly DriverTypeInfo               BOOLEAN                              = new DriverTypeInfo(typeof(bool));
        internal static readonly DriverTypeInfo               INTEGER                              = new DriverTypeInfo(typeof(int));
        internal static readonly DriverTypeInfo               BIGINTEGER                           = new DriverTypeInfo(typeof(long));
        internal static readonly DriverTypeInfo               STRING                               = new DriverTypeInfo(typeof(string));
        internal static readonly DriverTypeInfo               STRING_ARRAY                         = new DriverTypeInfo(typeof(string[]));
        internal static readonly DriverTypeInfo               TIMESPAN                             = new DriverTypeInfo(typeof(TimeSpan));
        internal static readonly DriverTypeInfo               NULLABLE_TIMESPAN                    = new DriverTypeInfo(typeof(TimeSpan?));
        internal static readonly DriverTypeInfo               URI                                  = new DriverTypeInfo(typeof(Uri));
        internal static readonly DriverTypeInfo               I_READONLY_LIST_OF_STRING            = new DriverTypeInfo(typeof(IReadOnlyList<string>));
        internal static readonly DriverTypeInfo               I_LIST_OF_I_NOTIFICATION             = new DriverTypeInfo(() => typeof(IList<>).MakeGenericType(I_NOTIFICATION.Type));
        internal static readonly DriverTypeInfo               DICT_OF_STRING_AND_OBJECT            = new DriverTypeInfo(typeof(Dictionary<string, object>));
        internal static readonly DriverTypeInfo               I_DICT_OF_STRING_AND_OBJECT          = new DriverTypeInfo(typeof(IDictionary<string, object>));
        internal static readonly DriverTypeInfo               I_READONLY_DICT_OF_STRING_AND_OBJECT = new DriverTypeInfo(typeof(IReadOnlyDictionary<string, object>));
        internal static readonly DriverTypeInfo               ACTION_OF_CONFIG_BUILDER             = new DriverTypeInfo(() => typeof(Action<>).MakeGenericType(CONFIG_BUILDER.Type));
        internal static readonly DriverTypeInfo               ACTION_OF_SESSION_BUILDER            = new DriverTypeInfo(() => typeof(Action<>).MakeGenericType(SESSION_CONFIG_BUILDER.Type));
        internal static readonly DriverTypeInfo               ACTION_OF_TRANSACTION_BUILDER        = new DriverTypeInfo(() => typeof(Action<>).MakeGenericType(TRANSACTION_CONFIG_BUILDER.Type));

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
            }
            internal DriverTypeInfo(Type type)
            {
                _typeInit = new Lazy<Type?>(() => type, true);
                Names = null;
            }
            internal DriverTypeInfo(Func<Type?> typeInitializer)
            {
                _typeInit = new Lazy<Type?>(typeInitializer, true);
                Names = null;
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
        }
        internal sealed class StaticProperty
        {
            internal StaticProperty(DriverTypeInfo parent, DriverTypeInfo returnType, string name, DriverTypeInfo? indexer = null)
            {
                _propertyInfo = new Lazy<PropertyInfo?>(delegate()
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
        internal sealed class InstanceProperty
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
        internal sealed class StaticMethod
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
        internal sealed class InstanceMethod
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
        internal sealed class EnumField
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

        #endregion

        #region Specific Neo4j Driver Types

        internal sealed class IDriverInfo : DriverTypeInfo
        {
            public IDriverInfo(params string[] names) : base (names) { }

            //bool Encrypted { get; }

            public Session AsyncSession(Driver driver) => new Session(_asyncSession1.Value.Invoke(driver.Value));
            private readonly Lazy<InstanceMethod> _asyncSession1 = new Lazy<InstanceMethod>(() => new InstanceMethod(I_DRIVER, I_ASYNC_SESSION, "AsyncSession"), true);

            public Session AsyncSession(Driver driver, Action<SessionConfigBuilder> configBuilder) => new Session(_asyncSession2.Value.Invoke(driver.Value, DelegateHelper.WrapDelegate(ACTION_OF_SESSION_BUILDER.Type, (object o) => configBuilder.Invoke(new SessionConfigBuilder(o)))));
            private readonly Lazy<InstanceMethod> _asyncSession2 = new Lazy<InstanceMethod>(() => new InstanceMethod(I_DRIVER, I_ASYNC_SESSION, "AsyncSession", ACTION_OF_SESSION_BUILDER), true);

            //Task<IServerInfo> GetServerInfoAsync();

            public Task<bool> TryVerifyConnectivityAsync(Driver driver) => AsTask<bool>(_tryVerifyConnectivityAsync.Value.Invoke(driver.Value));
            private readonly Lazy<InstanceMethod> _tryVerifyConnectivityAsync = new Lazy<InstanceMethod>(() => new InstanceMethod(I_DRIVER, TASK_OF_BOOLEAN, "TryVerifyConnectivityAsync", ACTION_OF_SESSION_BUILDER), true);

            //Task VerifyConnectivityAsync();

            public Task<bool> SupportsMultiDbAsync(Driver driver) => AsTask<bool>(_supportsMultiDbAsync.Value.Invoke(driver.Value));
            private readonly Lazy<InstanceMethod> _supportsMultiDbAsync = new Lazy<InstanceMethod>(() => new InstanceMethod(I_DRIVER, TASK_OF_BOOLEAN, "SupportsMultiDbAsync", ACTION_OF_SESSION_BUILDER), true);
        }
        internal sealed class ConfigBuilderInfo : DriverTypeInfo
        {
            public ConfigBuilderInfo(params string[] names) : base(names) { }

            public void WithMaxIdleConnectionPoolSize(object instance, int size) => _withMaxIdleConnectionPoolSize.Value.Invoke(instance, size);
            private readonly Lazy<InstanceMethod> _withMaxIdleConnectionPoolSize = new Lazy<InstanceMethod>(() => new InstanceMethod(CONFIG_BUILDER, CONFIG_BUILDER, "WithMaxIdleConnectionPoolSize", INTEGER), true);

            public void WithMaxConnectionPoolSize(object instance, int size) => _withMaxConnectionPoolSize.Value.Invoke(instance, size);
            private readonly Lazy<InstanceMethod> _withMaxConnectionPoolSize = new Lazy<InstanceMethod>(() => new InstanceMethod(CONFIG_BUILDER, CONFIG_BUILDER, "WithMaxConnectionPoolSize", INTEGER), true);

            public void WithConnectionAcquisitionTimeout(object instance, TimeSpan timeSpan) => _withConnectionAcquisitionTimeout.Value.Invoke(instance, timeSpan);
            private readonly Lazy<InstanceMethod> _withConnectionAcquisitionTimeout = new Lazy<InstanceMethod>(() => new InstanceMethod(CONFIG_BUILDER, CONFIG_BUILDER, "WithConnectionAcquisitionTimeout", TIMESPAN), true);

            public void WithConnectionTimeout(object instance, TimeSpan timeSpan) => _withConnectionTimeout.Value.Invoke(instance, timeSpan);
            private readonly Lazy<InstanceMethod> _withConnectionTimeout = new Lazy<InstanceMethod>(() => new InstanceMethod(CONFIG_BUILDER, CONFIG_BUILDER, "WithConnectionTimeout", TIMESPAN), true);

            public void WithSocketKeepAliveEnabled(object instance, bool enable) => _withSocketKeepAliveEnabled.Value.Invoke(instance, enable);
            private readonly Lazy<InstanceMethod> _withSocketKeepAliveEnabled = new Lazy<InstanceMethod>(() => new InstanceMethod(CONFIG_BUILDER, CONFIG_BUILDER, "WithSocketKeepAliveEnabled", BOOLEAN), true);
            
            public void WithMaxTransactionRetryTime(object instance, TimeSpan time) => _withMaxTransactionRetryTime.Value.Invoke(instance, time);
            private readonly Lazy<InstanceMethod> _withMaxTransactionRetryTime = new Lazy<InstanceMethod>(() => new InstanceMethod(CONFIG_BUILDER, CONFIG_BUILDER, "WithMaxTransactionRetryTime", TIMESPAN), true);

            public void WithConnectionIdleTimeout(object instance, TimeSpan timeSpan) => _withConnectionIdleTimeout.Value.Invoke(instance, timeSpan);
            private readonly Lazy<InstanceMethod> _withConnectionIdleTimeout = new Lazy<InstanceMethod>(() => new InstanceMethod(CONFIG_BUILDER, CONFIG_BUILDER, "WithConnectionIdleTimeout", TIMESPAN), true);

            public void WithMaxConnectionLifetime(object instance, TimeSpan timeSpan) => _withMaxConnectionLifetime.Value.Invoke(instance, timeSpan);
            private readonly Lazy<InstanceMethod> _withMaxConnectionLifetime = new Lazy<InstanceMethod>(() => new InstanceMethod(CONFIG_BUILDER, CONFIG_BUILDER, "WithMaxConnectionLifetime", TIMESPAN), true);

            public void WithIpv6Enabled(object instance, bool enable) => _withIpv6Enabled.Value.Invoke(instance, enable);
            private readonly Lazy<InstanceMethod> _withIpv6Enabled = new Lazy<InstanceMethod>(() => new InstanceMethod(CONFIG_BUILDER, CONFIG_BUILDER, "WithIpv6Enabled", BOOLEAN), true);

            public void WithDefaultReadBufferSize(object instance, int defaultReadBufferSize) => _withDefaultReadBufferSize.Value.Invoke(instance, defaultReadBufferSize);
            private readonly Lazy<InstanceMethod> _withDefaultReadBufferSize = new Lazy<InstanceMethod>(() => new InstanceMethod(CONFIG_BUILDER, CONFIG_BUILDER, "WithDefaultReadBufferSize", INTEGER), true);

            public void WithMaxReadBufferSize(object instance, int maxReadBufferSize) => _withMaxReadBufferSize.Value.Invoke(instance, maxReadBufferSize);
            private readonly Lazy<InstanceMethod> _withMaxReadBufferSize = new Lazy<InstanceMethod>(() => new InstanceMethod(CONFIG_BUILDER, CONFIG_BUILDER, "WithMaxReadBufferSize", INTEGER), true);

            public void WithDefaultWriteBufferSize(object instance, int defaultWriteBufferSize) => _withDefaultWriteBufferSize.Value.Invoke(instance, defaultWriteBufferSize);
            private readonly Lazy<InstanceMethod> _withDefaultWriteBufferSize = new Lazy<InstanceMethod>(() => new InstanceMethod(CONFIG_BUILDER, CONFIG_BUILDER, "WithDefaultWriteBufferSize", INTEGER), true);

            public void WithMaxWriteBufferSize(object instance, int maxWriteBufferSize) => _withMaxWriteBufferSize.Value.Invoke(instance, maxWriteBufferSize);
            private readonly Lazy<InstanceMethod> _withMaxWriteBufferSize = new Lazy<InstanceMethod>(() => new InstanceMethod(CONFIG_BUILDER, CONFIG_BUILDER, "WithMaxWriteBufferSize", INTEGER), true);

            public void WithFetchSize(object instance, long size) => _withFetchSize.Value.Invoke(instance, size);
            private readonly Lazy<InstanceMethod> _withFetchSize = new Lazy<InstanceMethod>(() => new InstanceMethod(CONFIG_BUILDER, CONFIG_BUILDER, "WithFetchSize", BIGINTEGER), true);
        }
        internal sealed class GraphDatabaseInfo : DriverTypeInfo
        {
            public GraphDatabaseInfo(params string[] names) : base(names) { }

            public Driver Driver(Uri uri, AuthToken authToken, Action<ConfigBuilder> configBuilder) => new Driver(_driver.Value.Invoke(uri, authToken.Value, DelegateHelper.WrapDelegate(ACTION_OF_CONFIG_BUILDER.Type, (object o) => configBuilder.Invoke(new ConfigBuilder(o)))));
            private readonly Lazy<StaticMethod> _driver = new Lazy<StaticMethod>(() => new StaticMethod(GRAPH_DATABASE, I_DRIVER, "Driver", URI, I_AUTH_TOKEN, ACTION_OF_CONFIG_BUILDER), true);
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
            private readonly Lazy<StaticMethod> _basic1 = new Lazy<StaticMethod>(()=> new StaticMethod(AUTH_TOKENS, I_AUTH_TOKEN, "Basic", STRING, STRING), true);

            public AuthToken Basic(string username, string password, string realm) => new AuthToken(_basic2.Value.Invoke(username, password, realm));
            private readonly Lazy<StaticMethod> _basic2 = new Lazy<StaticMethod>(() => new StaticMethod(AUTH_TOKENS, I_AUTH_TOKEN, "Basic", STRING, STRING, STRING), true);

            public AuthToken Kerberos(string base64EncodedTicket) => new AuthToken(_kerberos.Value.Invoke(base64EncodedTicket));
            private readonly Lazy<StaticMethod> _kerberos = new Lazy<StaticMethod>(() => new StaticMethod(AUTH_TOKENS, I_AUTH_TOKEN, "Kerberos", STRING), true);

            public AuthToken Custom(string principal, string credentials, string realm, string scheme) => new AuthToken(_custom1.Value.Invoke(principal, credentials, realm, scheme));
            private readonly Lazy<StaticMethod> _custom1 = new Lazy<StaticMethod>(() => new StaticMethod(AUTH_TOKENS, I_AUTH_TOKEN, "Custom", STRING, STRING, STRING, STRING), true);

            public AuthToken Custom(string principal, string credentials, string realm, string scheme, Dictionary<string, object> parameters) => new AuthToken(_custom2.Value.Invoke(principal, credentials, realm, scheme, parameters));
            private readonly Lazy<StaticMethod> _custom2 = new Lazy<StaticMethod>(() => new StaticMethod(AUTH_TOKENS, I_AUTH_TOKEN, "Custom", STRING, STRING, STRING, STRING, DICT_OF_STRING_AND_OBJECT), true);

            public AuthToken Bearer(string token) => new AuthToken(_bearer.Value.Invoke(token));
            private readonly Lazy<StaticMethod> _bearer = new Lazy<StaticMethod>(() => new StaticMethod(AUTH_TOKENS, I_AUTH_TOKEN, "Bearer", STRING), true);
        }
        internal sealed class IResultCursorInfo : DriverTypeInfo
        {
            public IResultCursorInfo(params string[] names) : base(names) { }

            public Record Current(object instance) => new Record(_current.Value.GetValue(instance));
            public object CurrentInternal(object instance) => _current.Value.GetValue(instance);
            private readonly Lazy<InstanceProperty> _current = new Lazy<InstanceProperty>(() => new InstanceProperty(I_RESULT_CURSOR, I_RECORD, "Current"), true);

            public bool IsOpen(object instance) => (bool)_isOpen.Value.GetValue(instance);
            private readonly Lazy<InstanceProperty> _isOpen = new Lazy<InstanceProperty>(() => new InstanceProperty(I_RESULT_CURSOR, BOOLEAN, "IsOpen"), true);

            public Task<string[]> KeysAsync(object instance) => AsTask<string[]>(_keysAsync.Value.Invoke(instance));
            private readonly Lazy<InstanceMethod> _keysAsync = new Lazy<InstanceMethod>(() => new InstanceMethod(I_RESULT_CURSOR, TASK_OF_STRING_ARRAY, "KeysAsync"), true);

            public Task<ResultSummary> ConsumeAsync(object instance) => AsTask(_consumeAsync.Value.Invoke(instance), instance => new ResultSummary(instance));
            private readonly Lazy<InstanceMethod> _consumeAsync = new Lazy<InstanceMethod>(() => new InstanceMethod(I_RESULT_CURSOR, TASK_OF_I_RESULT_SUMMARY, "ConsumeAsync"), true);

            public Task<Record> PeekAsync(object instance) => AsTask(_peekAsync.Value.Invoke(instance), instance => new Record(instance));
            public Task<object> PeekAsyncInternal(object instance) => AsTask<object>(_peekAsync.Value.Invoke(instance));
            private readonly Lazy<InstanceMethod> _peekAsync = new Lazy<InstanceMethod>(() => new InstanceMethod(I_RESULT_CURSOR, TASK_OF_I_RECORD, "PeekAsync"), true);

            public Task<bool> FetchAsync(object instance) => AsTask<bool>(_fetchAsync.Value.Invoke(instance));
            private readonly Lazy<InstanceMethod> _fetchAsync = new Lazy<InstanceMethod>(() => new InstanceMethod(I_RESULT_CURSOR, TASK_OF_BOOLEAN, "FetchAsync"), true);
        }
        internal sealed class IResultSummaryInfo : DriverTypeInfo
        {
            public IResultSummaryInfo(params string[] names) : base(names) { }

            public Query Query(object instance) => new Query(_query.Value.GetValue(instance));
            private readonly Lazy<InstanceProperty> _query = new Lazy<InstanceProperty>(() => new InstanceProperty(I_RESULT_SUMMARY, QUERY, "Query"), true);

            public Counters Counters(object instance) => new Counters(_counters.Value.GetValue(instance));
            private readonly Lazy<InstanceProperty> _counters = new Lazy<InstanceProperty>(() => new InstanceProperty(I_RESULT_SUMMARY, I_COUNTERS, "Counters"), true);

            public List<Notification> Notifications(object instance) => (List<Notification>)ToList((IList)_notifications.Value.GetValue(instance)); //  IList<INotification> Notifications { get; }
            private readonly Lazy<InstanceProperty> _notifications = new Lazy<InstanceProperty>(() => new InstanceProperty(I_RESULT_SUMMARY, I_LIST_OF_I_NOTIFICATION, "Notifications"), true);
        }
        internal sealed class QueryInfo : DriverTypeInfo
        {
            public QueryInfo(params string[] names) : base(names) { }

            public string Text(object instance) => (string)_text.Value.GetValue(instance);
            private readonly Lazy<InstanceProperty> _text = new Lazy<InstanceProperty>(() => new InstanceProperty(QUERY, STRING, "Text"), true);

            public IDictionary<string, object> Parameters(object instance) => (IDictionary<string, object>)_parameters.Value.GetValue(instance);
            private readonly Lazy<InstanceProperty> _parameters = new Lazy<InstanceProperty>(() => new InstanceProperty(QUERY, I_DICT_OF_STRING_AND_OBJECT, "Parameters"), true);

        }
        internal sealed class ICountersInfo : DriverTypeInfo
        {
            public ICountersInfo(params string[] names) : base(names) { }

            public bool ContainsUpdates(object instance) => (bool)_ContainsUpdates.Value.GetValue(instance);
            private readonly Lazy<InstanceProperty> _ContainsUpdates = new Lazy<InstanceProperty>(() => new InstanceProperty(I_COUNTERS, BOOLEAN, "ContainsUpdates"), true);

            public int NodesCreated(object instance) => (int)_NodesCreated.Value.GetValue(instance);
            private readonly Lazy<InstanceProperty> _NodesCreated = new Lazy<InstanceProperty>(() => new InstanceProperty(I_COUNTERS, INTEGER, "NodesCreated"), true);

            public int NodesDeleted(object instance) => (int)_NodesDeleted.Value.GetValue(instance);
            private readonly Lazy<InstanceProperty> _NodesDeleted = new Lazy<InstanceProperty>(() => new InstanceProperty(I_COUNTERS, INTEGER, "NodesDeleted"), true);

            public int RelationshipsCreated(object instance) => (int)_RelationshipsCreated.Value.GetValue(instance);
            private readonly Lazy<InstanceProperty> _RelationshipsCreated = new Lazy<InstanceProperty>(() => new InstanceProperty(I_COUNTERS, INTEGER, "RelationshipsCreated"), true);

            public int RelationshipsDeleted(object instance) => (int)_RelationshipsDeleted.Value.GetValue(instance);
            private readonly Lazy<InstanceProperty> _RelationshipsDeleted = new Lazy<InstanceProperty>(() => new InstanceProperty(I_COUNTERS, INTEGER, "RelationshipsDeleted"), true);

            public int PropertiesSet(object instance) => (int)_PropertiesSet.Value.GetValue(instance);
            private readonly Lazy<InstanceProperty> _PropertiesSet = new Lazy<InstanceProperty>(() => new InstanceProperty(I_COUNTERS, INTEGER, "PropertiesSet"), true);

            public int LabelsAdded(object instance) => (int)_LabelsAdded.Value.GetValue(instance);
            private readonly Lazy<InstanceProperty> _LabelsAdded = new Lazy<InstanceProperty>(() => new InstanceProperty(I_COUNTERS, INTEGER, "LabelsAdded"), true);

            public int LabelsRemoved(object instance) => (int)_LabelsRemoved.Value.GetValue(instance);
            private readonly Lazy<InstanceProperty> _LabelsRemoved = new Lazy<InstanceProperty>(() => new InstanceProperty(I_COUNTERS, INTEGER, "LabelsRemoved"), true);

            public int IndexesAdded(object instance) => (int)_IndexesAdded.Value.GetValue(instance);
            private readonly Lazy<InstanceProperty> _IndexesAdded = new Lazy<InstanceProperty>(() => new InstanceProperty(I_COUNTERS, INTEGER, "IndexesAdded"), true);

            public int IndexesRemoved(object instance) => (int)_IndexesRemoved.Value.GetValue(instance);
            private readonly Lazy<InstanceProperty> _IndexesRemoved = new Lazy<InstanceProperty>(() => new InstanceProperty(I_COUNTERS, INTEGER, "IndexesRemoved"), true);

            public int ConstraintsAdded(object instance) => (int)_ConstraintsAdded.Value.GetValue(instance);
            private readonly Lazy<InstanceProperty> _ConstraintsAdded = new Lazy<InstanceProperty>(() => new InstanceProperty(I_COUNTERS, INTEGER, "ConstraintsAdded"), true);

            public int ConstraintsRemoved(object instance) => (int)_ConstraintsRemoved.Value.GetValue(instance);
            private readonly Lazy<InstanceProperty> _ConstraintsRemoved = new Lazy<InstanceProperty>(() => new InstanceProperty(I_COUNTERS, INTEGER, "ConstraintsRemoved"), true);

            public int SystemUpdates(object instance) => (int)_SystemUpdates.Value.GetValue(instance);
            private readonly Lazy<InstanceProperty> _SystemUpdates = new Lazy<InstanceProperty>(() => new InstanceProperty(I_COUNTERS, INTEGER, "SystemUpdates"), true);

            public bool ContainsSystemUpdates(object instance) => (bool)_ContainsSystemUpdates.Value.GetValue(instance);
            private readonly Lazy<InstanceProperty> _ContainsSystemUpdates = new Lazy<InstanceProperty>(() => new InstanceProperty(I_COUNTERS, BOOLEAN, "ContainsSystemUpdates"), true);

        }
        internal sealed class IRecord : DriverTypeInfo
        {
            public IRecord(params string[] names) : base(names) { }

            public object Item(object instance, int index) => _item1.Value.GetValue(instance, index);
            private readonly Lazy<InstanceProperty> _item1 = new Lazy<InstanceProperty>(() => new InstanceProperty(I_RECORD, OBJECT, "Item", INTEGER), true);

            public object Item(object instance, string key) => _item2.Value.GetValue(instance, key);
            private readonly Lazy<InstanceProperty> _item2 = new Lazy<InstanceProperty>(() => new InstanceProperty(I_RECORD, OBJECT, "Item", STRING), true);

            public IReadOnlyDictionary<string, object?> Values(object instance) => (IReadOnlyDictionary<string, object?>)_values.Value.GetValue(instance);
            private readonly Lazy<InstanceProperty> _values = new Lazy<InstanceProperty>(() => new InstanceProperty(I_RECORD, I_READONLY_DICT_OF_STRING_AND_OBJECT, "Values"), true);

            public IReadOnlyList<string> Keys(object instance) => (IReadOnlyList<string>)_keys.Value.GetValue(instance);
            private readonly Lazy<InstanceProperty> _keys = new Lazy<InstanceProperty>(() => new InstanceProperty(I_RECORD, I_READONLY_LIST_OF_STRING, "Keys"), true);
        }
        internal sealed class IAsyncSessionInfo : DriverTypeInfo
        {
            public IAsyncSessionInfo(params string[] names) : base(names) { }

            public Bookmarks LastBookmarks(object instance) => new Bookmarks(_lastBookmarks.Value.GetValue(instance));
            private readonly Lazy<InstanceProperty> _lastBookmarks = new Lazy<InstanceProperty>(() => new InstanceProperty(I_ASYNC_SESSION, BOOKMARKS, new string[] { "LastBookmarks", "LastBookmark" }), true);

            public Task<Transaction> BeginTransactionAsync(object instance) => AsTask(_beginTransactionAsync1.Value.Invoke(instance), instance => new Transaction(instance));
            private readonly Lazy<InstanceMethod> _beginTransactionAsync1 = new Lazy<InstanceMethod>(() => new InstanceMethod(I_ASYNC_SESSION, TASK_OF_I_ASYNC_TRANSACTION, "BeginTransactionAsync"), true);

            public Task<Transaction> BeginTransactionAsync(object instance, Action<TransactionConfigBuilder> configBuilder) => AsTask(_beginTransactionAsync2.Value.Invoke(instance, DelegateHelper.WrapDelegate(ACTION_OF_TRANSACTION_BUILDER.Type, (object o) => configBuilder.Invoke(new TransactionConfigBuilder(o)))), instance => new Transaction(instance));
            private readonly Lazy<InstanceMethod> _beginTransactionAsync2 = new Lazy<InstanceMethod>(() => new InstanceMethod(I_ASYNC_SESSION, TASK_OF_I_ASYNC_TRANSACTION, "BeginTransactionAsync", ACTION_OF_TRANSACTION_BUILDER), true);
        }
        internal sealed class SessionConfigBuilderInfo : DriverTypeInfo
        {
            public SessionConfigBuilderInfo(params string[] names) : base(names) { }

            public void WithDatabase(object instance, string database) => _withDatabase.Value.Invoke(instance, database);
            private readonly Lazy<InstanceMethod> _withDatabase = new Lazy<InstanceMethod>(() => new InstanceMethod(SESSION_CONFIG_BUILDER, SESSION_CONFIG_BUILDER, "WithDatabase", STRING), true);

            public void WithDefaultAccessMode(object instance, object defaultAccessMode) => _withDefaultAccessMode.Value.Invoke(instance, defaultAccessMode);
            private readonly Lazy<InstanceMethod> _withDefaultAccessMode = new Lazy<InstanceMethod>(() => new InstanceMethod(SESSION_CONFIG_BUILDER, SESSION_CONFIG_BUILDER, "WithDefaultAccessMode", ACCESS_MODE), true);

            public void WithBookmarks(object instance, Bookmarks[] bookmarks) => _withBookmarks.Value.Invoke(instance, BOOKMARKS.ToArray(bookmarks.Select(item => item.Value), bookmarks.Length));
            private readonly Lazy<InstanceMethod> _withBookmarks = new Lazy<InstanceMethod>(() => new InstanceMethod(SESSION_CONFIG_BUILDER, SESSION_CONFIG_BUILDER, "WithBookmarks", BOOKMARKS_ARRAY), true);

            public void WithFetchSize(object instance, long size) => _withFetchSize.Value.Invoke(instance, size);
            private readonly Lazy<InstanceMethod> _withFetchSize = new Lazy<InstanceMethod>(() => new InstanceMethod(SESSION_CONFIG_BUILDER, SESSION_CONFIG_BUILDER, "WithFetchSize", BIGINTEGER), true);

            public void WithImpersonatedUser(object instance, string impersonatedUser) => _withImpersonatedUser.Value.Invoke(instance, impersonatedUser);
            private readonly Lazy<InstanceMethod> _withImpersonatedUser = new Lazy<InstanceMethod>(() => new InstanceMethod(SESSION_CONFIG_BUILDER, SESSION_CONFIG_BUILDER, "WithImpersonatedUser", STRING), true);
        }
        internal sealed class IAsyncTransactionInfo : DriverTypeInfo
        {
            public IAsyncTransactionInfo(params string[] names) : base(names) { }

            public Task CommitAsync(object instance) => AsTask(_commitAsync.Value.Invoke(instance));
            private readonly Lazy<InstanceMethod> _commitAsync = new Lazy<InstanceMethod>(() => new InstanceMethod(I_ASYNC_TRANSACTION, TASK, "CommitAsync"), true);

            public Task RollbackAsync(object instance) => AsTask(_rollbackAsync.Value.Invoke(instance));
            private readonly Lazy<InstanceMethod> _rollbackAsync = new Lazy<InstanceMethod>(() => new InstanceMethod(I_ASYNC_TRANSACTION, TASK, "RollbackAsync"), true);
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
            private readonly Lazy<InstanceMethod> _withTimeout = new Lazy<InstanceMethod>(() => new InstanceMethod(TRANSACTION_CONFIG_BUILDER, TRANSACTION_CONFIG_BUILDER, "WithTimeout", NULLABLE_TIMESPAN), true); // Driver 5
            private readonly Lazy<InstanceMethod> _withTimeoutOld = new Lazy<InstanceMethod>(() => new InstanceMethod(TRANSACTION_CONFIG_BUILDER, TRANSACTION_CONFIG_BUILDER, "WithTimeout", TIMESPAN), true); // Driver 4

            public void WithMetadata(object instance, IDictionary<string, object> metadata) => _withMetadata.Value.Invoke(instance, metadata);
            private readonly Lazy<InstanceMethod> _withMetadata = new Lazy<InstanceMethod>(() => new InstanceMethod(TRANSACTION_CONFIG_BUILDER, TRANSACTION_CONFIG_BUILDER, "WithMetadata", I_DICT_OF_STRING_AND_OBJECT), true);
        }
        internal sealed class IAsyncQueryRunnerInfo : DriverTypeInfo
        {
            public IAsyncQueryRunnerInfo(params string[] names) : base(names) { }

            public Task<ResultCursor> RunAsync(object instance, string query) => AsTask(_runAsync1.Value.Invoke(instance, query), instance => new ResultCursor(instance));
            private readonly Lazy<InstanceMethod> _runAsync1 = new Lazy<InstanceMethod>(() => new InstanceMethod(I_ASYNC_QUERY_RUNNER, TASK_OF_I_RESULT_CURSOR, "RunAsync", STRING), true);

            public Task<ResultCursor> RunAsync(object instance, string query, IDictionary<string, object?> parameters) => AsTask(_runAsync2.Value.Invoke(instance, query, parameters), instance => new ResultCursor(instance));
            private readonly Lazy<InstanceMethod> _runAsync2 = new Lazy<InstanceMethod>(() => new InstanceMethod(I_ASYNC_QUERY_RUNNER, TASK_OF_I_RESULT_CURSOR, "RunAsync", STRING, I_DICT_OF_STRING_AND_OBJECT), true);
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
            private readonly Lazy<StaticMethod> _from = new Lazy<StaticMethod>(() => new StaticMethod(BOOKMARKS, BOOKMARKS, "From", STRING_ARRAY), true);
            
            public string[] Values(object instance) => (string[])_values.Value.GetValue(instance);
            private readonly Lazy<InstanceProperty> _values = new Lazy<InstanceProperty>(() => new InstanceProperty(BOOKMARKS, STRING_ARRAY, "Values"), true);
        }
        internal sealed class INotificationInfo : DriverTypeInfo
        {
            public INotificationInfo(params string[] names) : base(names) { }

            public string Code(object instance) => (string)_code.Value.GetValue(instance);
            private readonly Lazy<InstanceProperty> _code = new Lazy<InstanceProperty>(() => new InstanceProperty(I_NOTIFICATION, STRING, "Code"), true);

            public string Title(object instance) => (string)_title.Value.GetValue(instance);
            private readonly Lazy<InstanceProperty> _title = new Lazy<InstanceProperty>(() => new InstanceProperty(I_NOTIFICATION, STRING, "Title"), true);

            public string Description(object instance) => (string)_description.Value.GetValue(instance);
            private readonly Lazy<InstanceProperty> _description = new Lazy<InstanceProperty>(() => new InstanceProperty(I_NOTIFICATION, STRING, "Description"), true);

            public object Position(object instance) => _position.Value.GetValue(instance);
            private readonly Lazy<InstanceProperty> _position = new Lazy<InstanceProperty>(() => new InstanceProperty(I_NOTIFICATION, I_INPUT_POSITION, "Position"), true);
        }
        internal sealed class IInputPositionInfo : DriverTypeInfo
        {
            public IInputPositionInfo(params string[] names) : base(names) { }

            public int Offset(object instance) => (int)_code.Value.GetValue(instance);
            private readonly Lazy<InstanceProperty> _code = new Lazy<InstanceProperty>(() => new InstanceProperty(I_INPUT_POSITION, INTEGER, "Offset"), true);

            public int Line(object instance) => (int)_line.Value.GetValue(instance);
            private readonly Lazy<InstanceProperty> _line = new Lazy<InstanceProperty>(() => new InstanceProperty(I_INPUT_POSITION, INTEGER, "Line"), true);

            public int Column(object instance) => (int)_column.Value.GetValue(instance);
            private readonly Lazy<InstanceProperty> _column = new Lazy<InstanceProperty>(() => new InstanceProperty(I_INPUT_POSITION, INTEGER, "Column"), true);
        }

        #endregion

        public void Dispose() => ((IDisposable)Value).Dispose();
        public ValueTask DisposeAsync() => ((IAsyncDisposable)Value).DisposeAsync();
    }
}
