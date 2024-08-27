using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Blueprint41.Core;
using config_builder             = Blueprint41.Persistence.ConfigBuilder;
using session_config_builder     = Blueprint41.Persistence.SessionConfigBuilder;
using transaction_config_builder = Blueprint41.Persistence.TransactionConfigBuilder;

namespace Blueprint41.Persistence
{
    public sealed class Driver
    {
        internal Driver(object value)
        {
            Value = value;
        }
        internal object Value { get; private set; }

        public static Driver Get(Uri uri, AuthToken authToken, Action<config_builder> configBuilder)
        {
            return GRAPH_DATABASE.Driver(uri, authToken, configBuilder);
        }

        public static void Configure<TDriverInterface>()
        {
            DriverTypeInfo.SearchAssembly = typeof(TDriverInterface).Assembly;

            if (typeof(TDriverInterface) != I_DRIVER.Type)
                throw new InvalidOperationException($"Please pass type of {I_DRIVER.Names} as generic argument TDriverInterface.");
        }

        internal static readonly IDriverInfo                  I_DRIVER                      = new IDriverInfo("Neo4j.Driver.IDriver");
        internal static readonly ConfigBuilderInfo            CONFIG_BUILDER                = new ConfigBuilderInfo("Neo4j.Driver.ConfigBuilder");
        internal static readonly GraphDatabaseInfo            GRAPH_DATABASE                = new GraphDatabaseInfo("Neo4j.Driver.GraphDatabase");
        internal static readonly IAuthTokenInfo               I_AUTH_TOKEN                  = new IAuthTokenInfo("Neo4j.Driver.IAuthToken");
        internal static readonly AuthTokensInfo               AUTH_TOKENS                   = new AuthTokensInfo("Neo4j.Driver.AuthTokens");
        internal static readonly IResultCursorInfo            I_RESULT_CURSOR               = new IResultCursorInfo("Neo4j.Driver.IResultCursor");
        internal static readonly IAsyncSessionInfo            I_ASYNC_SESSION               = new IAsyncSessionInfo("Neo4j.Driver.IAsyncSession");
        internal static readonly SessionConfigBuilderInfo     SESSION_CONFIG_BUILDER        = new SessionConfigBuilderInfo("Neo4j.Driver.SessionConfigBuilder");
        internal static readonly IAsyncTransactionInfo        I_ASYNC_TRANSACTION           = new IAsyncTransactionInfo("Neo4j.Driver.IAsyncTransaction");
        internal static readonly TransactionConfigBuilderInfo TRANSACTION_CONFIG_BUILDER    = new TransactionConfigBuilderInfo("Neo4j.Driver.TransactionConfigBuilder");
        internal static readonly IAsyncQueryRunnerInfo        I_ASYNC_QUERY_RUNNER          = new IAsyncQueryRunnerInfo("Neo4j.Driver.IAsyncQueryRunner");
        internal static readonly AccessModeInfo               ACCESS_MODE                   = new AccessModeInfo("Neo4j.Driver.AccessMode");
        internal static readonly BookmarksInfo                BOOKMARKS                     = new BookmarksInfo("Neo4j.Driver.Bookmarks");

        internal static readonly DriverTypeInfo               TASK                          = new DriverTypeInfo(typeof(Task));
        internal static readonly DriverTypeInfo               TASK_OF_BOOL                  = new DriverTypeInfo(typeof(Task<bool>));
        internal static readonly DriverTypeInfo               TASK_OF_I_ASYNC_SESSION       = new DriverTypeInfo(() => typeof(Task<>).MakeGenericType(I_ASYNC_SESSION.Type));
        internal static readonly DriverTypeInfo               TASK_OF_I_ASYNC_TRANSACTION   = new DriverTypeInfo(() => typeof(Task<>).MakeGenericType(I_ASYNC_TRANSACTION.Type));
        internal static readonly DriverTypeInfo               BOOKMARKS_ARRAY               = new DriverTypeInfo(() => BOOKMARKS.Type.MakeArrayType());
        
        internal static readonly DriverTypeInfo               VOID                          = new DriverTypeInfo(typeof(void));
        internal static readonly DriverTypeInfo               BOOLEAN                       = new DriverTypeInfo(typeof(bool));
        internal static readonly DriverTypeInfo               INTEGER                       = new DriverTypeInfo(typeof(int));
        internal static readonly DriverTypeInfo               BIGINTEGER                    = new DriverTypeInfo(typeof(long));
        internal static readonly DriverTypeInfo               STRING                        = new DriverTypeInfo(typeof(string));
        internal static readonly DriverTypeInfo               STRING_ARRAY                  = new DriverTypeInfo(typeof(string[]));
        internal static readonly DriverTypeInfo               TIMESPAN                      = new DriverTypeInfo(typeof(TimeSpan));
        internal static readonly DriverTypeInfo               NULLABLE_TIMESPAN             = new DriverTypeInfo(typeof(TimeSpan?));
        internal static readonly DriverTypeInfo               URI                           = new DriverTypeInfo(typeof(Uri));
        internal static readonly DriverTypeInfo               DICT_OF_STRING_AND_OBJECT     = new DriverTypeInfo(typeof(Dictionary<string, object>));
        internal static readonly DriverTypeInfo               I_DICT_OF_STRING_AND_OBJECT   = new DriverTypeInfo(typeof(IDictionary<string, object>));
        internal static readonly DriverTypeInfo               ACTION_OF_CONFIG_BUILDER      = new DriverTypeInfo(() => typeof(Action<>).MakeGenericType(CONFIG_BUILDER.Type));
        internal static readonly DriverTypeInfo               ACTION_OF_SESSION_BUILDER     = new DriverTypeInfo(() => typeof(Action<>).MakeGenericType(SESSION_CONFIG_BUILDER.Type));
        internal static readonly DriverTypeInfo               ACTION_OF_TRANSACTION_BUILDER = new DriverTypeInfo(() => typeof(Action<>).MakeGenericType(TRANSACTION_CONFIG_BUILDER.Type));

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
            protected static async Task<T> AsTask<T>(object value)
            {
                if (value is not Task)
                    throw new InvalidOperationException("Not a task.");

                Task task = (Task)value;
                await task.ConfigureAwait(false);

                // Harvest the result. Ugly but works
                return (T)((dynamic)task).Result;
            }
#nullable enable
        }
        internal sealed class StaticProperty
        {
            internal StaticProperty(DriverTypeInfo parent, DriverTypeInfo returnType, string name, DriverTypeInfo? indexer = null)
            {
                _propertyInfo = new Lazy<PropertyInfo>(delegate()
                {
                    PropertyInfo? propertyInfo = parent.Type.GetProperty(name, BindingFlags.Public | BindingFlags.Static, null, returnType.Type, (indexer is null) ? null : new Type[] { indexer.Type }, null);
                    if (propertyInfo is not null)
                        return propertyInfo;

                    throw new MissingMethodException();
                }, true);
            }
            internal StaticProperty(DriverTypeInfo parent, DriverTypeInfo returnType, string[] names, DriverTypeInfo? indexer = null)
            {
                _propertyInfo = new Lazy<PropertyInfo>(delegate ()
                {
                    foreach (string name in names)
                    {
                        PropertyInfo? propertyInfo = parent.Type.GetProperty(name, BindingFlags.Public | BindingFlags.Static, null, returnType.Type, (indexer is null) ? null : new Type[] { indexer.Type }, null);
                        if (propertyInfo is not null)
                            return propertyInfo;
                    }
                    throw new MissingMethodException();
                }, true);
            }

            public object GetValue(object? index = null)
            {
                if (index is null)
                    return _propertyInfo.Value.GetValue(null);
                else
                    return _propertyInfo.Value.GetValue(null, new object[] { index });
            }
            public void SetValue(object? value, object? index = null)
            {
                if (index is null)
                    _propertyInfo.Value.SetValue(null, value);
                else
                    _propertyInfo.Value.SetValue(null, value, new object[] { index });
            }
            private readonly Lazy<PropertyInfo> _propertyInfo;
        }
        internal sealed class InstanceProperty
        {
            internal InstanceProperty(DriverTypeInfo parent, DriverTypeInfo returnType, string name, DriverTypeInfo? indexer = null)
            {
                _propertyInfo = new Lazy<PropertyInfo>(delegate ()
                {
                    PropertyInfo? propertyInfo = parent.Type.GetProperty(name, BindingFlags.Public | BindingFlags.Instance, null, returnType.Type, (indexer is null) ? null : new Type[] { indexer.Type }, null);
                    if (propertyInfo is not null)
                        return propertyInfo;

                    throw new MissingMethodException();
                }, true);
            }
            internal InstanceProperty(DriverTypeInfo parent, DriverTypeInfo returnType, string[] names, DriverTypeInfo? indexer = null)
            {
                _propertyInfo = new Lazy<PropertyInfo>(delegate ()
                {
                    foreach (string name in names)
                    {
                        PropertyInfo? propertyInfo = parent.Type.GetProperty(name, BindingFlags.Public | BindingFlags.Instance, null, returnType.Type, (indexer is null) ? null : new Type[] { indexer.Type }, null);
                        if (propertyInfo is not null)
                            return propertyInfo;
                    }
                    throw new MissingMethodException();
                }, true);
            }

            public object GetValue(object instance, object? index = null)
            {
                if (index is null)
                    return _propertyInfo.Value.GetValue(instance);
                else
                    return _propertyInfo.Value.GetValue(instance, new object[] { index });
            }
            public void SetValue(object instance, object? value, object? index = null)
            {
                if (index is null)
                    _propertyInfo.Value.SetValue(instance, value);
                else
                    _propertyInfo.Value.SetValue(instance, value, new object[] { index });
            }
            private readonly Lazy<PropertyInfo> _propertyInfo;
        }
        internal sealed class StaticMethod
        {
            internal StaticMethod(DriverTypeInfo parent, DriverTypeInfo returnType, string name, params DriverTypeInfo[] arguments)
            {
                _methodInfo = new Lazy<MethodInfo>(delegate ()
                {
                    MethodInfo? methodInfo = parent.Type.GetMethod(name, BindingFlags.Public | BindingFlags.Static, null, arguments?.Select(info => info.Type).ToArray(), null);
                    if (methodInfo?.ReturnType == returnType.Type)
                        return methodInfo;

                    throw new MissingMethodException();
                }, true);
            }
            internal StaticMethod(DriverTypeInfo parent, DriverTypeInfo returnType, string[] names, params DriverTypeInfo[] arguments)
            {
                _methodInfo = new Lazy<MethodInfo>(delegate ()
                {
                    foreach (string name in names)
                    {
                        MethodInfo? methodInfo = parent.Type.GetMethod(name, BindingFlags.Public | BindingFlags.Static, null, arguments?.Select(info => info.Type).ToArray(), null);
                        if (methodInfo?.ReturnType == returnType.Type)
                            return methodInfo;
                    }
                    throw new MissingMethodException();
                }, true);
            }

            public object Invoke(params object[] arguments)
            {
                return _methodInfo.Value.Invoke(null, arguments);
            }
            private readonly Lazy<MethodInfo> _methodInfo;
        }
        internal sealed class InstanceMethod
        {
            internal InstanceMethod(DriverTypeInfo parent, DriverTypeInfo returnType, string name, params DriverTypeInfo[] arguments)
            {
                _methodInfo = new Lazy<MethodInfo>(delegate ()
                {
                    MethodInfo? methodInfo = parent.Type.GetMethod(name, BindingFlags.Public | BindingFlags.Instance, null, arguments?.Select(info => info.Type).ToArray(), null);
                    if (methodInfo?.ReturnType == returnType.Type)
                        return methodInfo;

                    throw new MissingMethodException();
                }, true);
            }
            internal InstanceMethod(DriverTypeInfo parent, DriverTypeInfo returnType, string[] names, params DriverTypeInfo[] arguments)
            {
                _methodInfo = new Lazy<MethodInfo>(delegate ()
                {
                    foreach (string name in names)
                    {
                        MethodInfo? methodInfo = parent.Type.GetMethod(name, BindingFlags.Public | BindingFlags.Instance, null, arguments?.Select(info => info.Type).ToArray(), null);
                        if (methodInfo?.ReturnType == returnType.Type)
                            return methodInfo;
                    }
                    throw new MissingMethodException();
                }, true);
            }

            public object Invoke(object instance, params object?[] arguments)
            {
                return _methodInfo.Value.Invoke(instance, arguments);
            }
            private readonly Lazy<MethodInfo> _methodInfo;
        }
        internal sealed class EnumField
        {
            internal EnumField(DriverTypeInfo parent, string name)
            {
                _fieldInfo = new Lazy<FieldInfo>(delegate ()
                {
                    FieldInfo? fieldInfo = parent.Type.GetFields().FirstOrDefault(fi => fi.IsLiteral && fi.Name == name);
                    if (fieldInfo is not null)
                        return fieldInfo;

                    throw new MissingMethodException();
                }, true);
            }
            internal EnumField(DriverTypeInfo parent, string[] names)
            {
                _fieldInfo = new Lazy<FieldInfo>(delegate ()
                {
                    foreach (string name in names)
                    {
                        FieldInfo? fieldInfo = parent.Type.GetFields().FirstOrDefault(fi => fi.IsLiteral && fi.Name == name);
                        if (fieldInfo is not null)
                            return fieldInfo;
                    }
                    throw new MissingMethodException();
                }, true);
            }

            public object Value => _fieldInfo.Value.GetRawConstantValue();
            public bool Test(object instance) => Value == instance;

            private readonly Lazy<FieldInfo> _fieldInfo;
        }

        internal sealed class IDriverInfo : DriverTypeInfo
        {
            public IDriverInfo(params string[] names) : base (names) { }

            public Task<object> AsyncSession(Driver driver) => AsTask<object>(_asyncSession1.Value.Invoke(driver.Value));
            private readonly Lazy<InstanceMethod> _asyncSession1 = new Lazy<InstanceMethod>(() => new InstanceMethod(I_DRIVER, TASK_OF_I_ASYNC_SESSION, "AsyncSession"), true);

            public Task<object> AsyncSession(Driver driver, Action<session_config_builder> configBuilder) => AsTask<object>(_asyncSession2.Value.Invoke(driver.Value, DelegateHelper.WrapDelegate(ACTION_OF_SESSION_BUILDER.Type, (object o) => configBuilder.Invoke(new session_config_builder(o)))));
            private readonly Lazy<InstanceMethod> _asyncSession2 = new Lazy<InstanceMethod>(() => new InstanceMethod(I_DRIVER, TASK_OF_I_ASYNC_SESSION, "AsyncSession", ACTION_OF_SESSION_BUILDER), true);

            public Task DisposeAsync(Driver driver) => AsTask(_disposeAsync.Value.Invoke(driver.Value));
            private readonly Lazy<InstanceMethod> _disposeAsync = new Lazy<InstanceMethod>(() => new InstanceMethod(I_DRIVER, TASK, "DisposeAsync", ACTION_OF_SESSION_BUILDER), true);

            public Task<bool> TryVerifyConnectivityAsync(Driver driver) => AsTask<bool>(_tryVerifyConnectivityAsync.Value.Invoke(driver.Value));
            private readonly Lazy<InstanceMethod> _tryVerifyConnectivityAsync = new Lazy<InstanceMethod>(() => new InstanceMethod(I_DRIVER, TASK_OF_BOOL, "TryVerifyConnectivityAsync", ACTION_OF_SESSION_BUILDER), true);

            public Task<bool> SupportsMultiDbAsync(Driver driver) => AsTask<bool>(_supportsMultiDbAsync.Value.Invoke(driver.Value));
            private readonly Lazy<InstanceMethod> _supportsMultiDbAsync = new Lazy<InstanceMethod>(() => new InstanceMethod(I_DRIVER, TASK_OF_BOOL, "SupportsMultiDbAsync", ACTION_OF_SESSION_BUILDER), true);
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

            public Driver Driver(Uri uri, AuthToken authToken, Action<config_builder> configBuilder) => new Driver(_driver.Value.Invoke(uri, authToken.Value, DelegateHelper.WrapDelegate(ACTION_OF_CONFIG_BUILDER.Type, (object o) => configBuilder.Invoke(new config_builder(o)))));
            private readonly Lazy<StaticMethod> _driver = new Lazy<StaticMethod>(() => new StaticMethod(GRAPH_DATABASE, I_DRIVER, "Driver", URI, I_AUTH_TOKEN, ACTION_OF_CONFIG_BUILDER), true);
        }
        internal sealed class IAuthTokenInfo : DriverTypeInfo
        {
            public IAuthTokenInfo(params string[] names) : base(names) { }
        }
        internal sealed class AuthTokensInfo : DriverTypeInfo
        {
            public AuthTokensInfo(params string[] names) : base(names) { }

            public object None { get => _none.Value.GetValue(); set => _none.Value.SetValue(value); }
            private readonly Lazy<StaticProperty> _none = new Lazy<StaticProperty>(() => new StaticProperty(AUTH_TOKENS, I_AUTH_TOKEN, "None"), true);

            public object Basic(string username, string password) => _basic1.Value.Invoke(username, password);
            private readonly Lazy<StaticMethod> _basic1 = new Lazy<StaticMethod>(()=> new StaticMethod(AUTH_TOKENS, I_AUTH_TOKEN, "Basic", STRING, STRING), true);

            public object Basic(string username, string password, string realm) => _basic2.Value.Invoke(username, password, realm);
            private readonly Lazy<StaticMethod> _basic2 = new Lazy<StaticMethod>(() => new StaticMethod(AUTH_TOKENS, I_AUTH_TOKEN, "Basic", STRING, STRING, STRING), true);

            public object Kerberos(string base64EncodedTicket) => _kerberos.Value.Invoke(base64EncodedTicket);
            private readonly Lazy<StaticMethod> _kerberos = new Lazy<StaticMethod>(() => new StaticMethod(AUTH_TOKENS, I_AUTH_TOKEN, "Kerberos", STRING), true);

            public object Custom(string principal, string credentials, string realm, string scheme) => _custom1.Value.Invoke(principal, credentials, realm, scheme);
            private readonly Lazy<StaticMethod> _custom1 = new Lazy<StaticMethod>(() => new StaticMethod(AUTH_TOKENS, I_AUTH_TOKEN, "Custom", STRING, STRING, STRING, STRING), true);

            public object Custom(string principal, string credentials, string realm, string scheme, Dictionary<string, object> parameters) => _custom2.Value.Invoke(principal, credentials, realm, scheme, parameters);
            private readonly Lazy<StaticMethod> _custom2 = new Lazy<StaticMethod>(() => new StaticMethod(AUTH_TOKENS, I_AUTH_TOKEN, "Custom", STRING, STRING, STRING, STRING, DICT_OF_STRING_AND_OBJECT), true);

            public object Bearer(string token) => _bearer.Value.Invoke(token);
            private readonly Lazy<StaticMethod> _bearer = new Lazy<StaticMethod>(() => new StaticMethod(AUTH_TOKENS, I_AUTH_TOKEN, "Bearer", STRING), true);
        }
        internal sealed class IResultCursorInfo : DriverTypeInfo
        {
            public IResultCursorInfo(params string[] names) : base(names) { }

            //IRecord Current { get; }
            //bool IsOpen { get; }
            //Task<string[]> KeysAsync();
            //Task<IResultSummary> ConsumeAsync();
            //Task<IRecord> PeekAsync();
            //Task<bool> FetchAsync();
        }
        internal sealed class IAsyncSessionInfo : DriverTypeInfo
        {
            public IAsyncSessionInfo(params string[] names) : base(names) { }

            public Bookmark[] LastBookmarks(object session) => ((object[])_lastBookmarks.Value.GetValue(session)).Select(item => Bookmark.FromToken(BOOKMARKS.Values(item))).ToArray();
            private readonly Lazy<InstanceProperty> _lastBookmarks = new Lazy<InstanceProperty>(() => new InstanceProperty(I_ASYNC_SESSION, BOOKMARKS_ARRAY, "LastBookmarks"), true);

            public Task<object> BeginTransactionAsync(object session) => AsTask<object>(_beginTransactionAsync1.Value.Invoke(session));
            private readonly Lazy<InstanceMethod> _beginTransactionAsync1 = new Lazy<InstanceMethod>(() => new InstanceMethod(I_ASYNC_SESSION, TASK_OF_I_ASYNC_TRANSACTION, "BeginTransactionAsync"), true);

            public Task<object> BeginTransactionAsync(object session, Action<transaction_config_builder> configBuilder) => AsTask<object>(_beginTransactionAsync2.Value.Invoke(session, DelegateHelper.WrapDelegate(TRANSACTION_CONFIG_BUILDER.Type, (object o) => configBuilder.Invoke(new transaction_config_builder(o)))));
            private readonly Lazy<InstanceMethod> _beginTransactionAsync2 = new Lazy<InstanceMethod>(() => new InstanceMethod(I_ASYNC_SESSION, TASK_OF_I_ASYNC_TRANSACTION, "BeginTransactionAsync", TRANSACTION_CONFIG_BUILDER), true);
        }
        internal sealed class SessionConfigBuilderInfo : DriverTypeInfo
        {
            public SessionConfigBuilderInfo(params string[] names) : base(names) { }

            public void WithDatabase(object instance, string database) => _withDatabase.Value.Invoke(instance, database);
            private readonly Lazy<InstanceMethod> _withDatabase = new Lazy<InstanceMethod>(() => new InstanceMethod(SESSION_CONFIG_BUILDER, SESSION_CONFIG_BUILDER, "WithDatabase", STRING), true);

            public void WithDefaultAccessMode(object instance, object defaultAccessMode) => _withDefaultAccessMode.Value.Invoke(instance, defaultAccessMode);
            private readonly Lazy<InstanceMethod> _withDefaultAccessMode = new Lazy<InstanceMethod>(() => new InstanceMethod(SESSION_CONFIG_BUILDER, SESSION_CONFIG_BUILDER, "WithDefaultAccessMode", ACCESS_MODE), true);

            public void WithBookmarks(object instance, params object[] bookmarks) => _withBookmarks.Value.Invoke(instance, bookmarks);
            private readonly Lazy<InstanceMethod> _withBookmarks = new Lazy<InstanceMethod>(() => new InstanceMethod(SESSION_CONFIG_BUILDER, SESSION_CONFIG_BUILDER, "WithBookmarks", BOOKMARKS_ARRAY), true);

            public void WithFetchSize(object instance, long size) => _withFetchSize.Value.Invoke(instance, size);
            private readonly Lazy<InstanceMethod> _withFetchSize = new Lazy<InstanceMethod>(() => new InstanceMethod(SESSION_CONFIG_BUILDER, SESSION_CONFIG_BUILDER, "WithFetchSize", BIGINTEGER), true);

            public void WithImpersonatedUser(object instance, string impersonatedUser) => _withImpersonatedUser.Value.Invoke(instance, impersonatedUser);
            private readonly Lazy<InstanceMethod> _withImpersonatedUser = new Lazy<InstanceMethod>(() => new InstanceMethod(SESSION_CONFIG_BUILDER, SESSION_CONFIG_BUILDER, "WithImpersonatedUser", STRING), true);
        }
        internal sealed class IAsyncTransactionInfo : DriverTypeInfo
        {
            public IAsyncTransactionInfo(params string[] names) : base(names) { }

            //Task CommitAsync();
            //Task RollbackAsync();
        }
        internal sealed class TransactionConfigBuilderInfo : DriverTypeInfo
        {
            public TransactionConfigBuilderInfo(params string[] names) : base(names) { }

            public void WithTimeout(object instance, TimeSpan? timeout) => _withTimeout.Value.Invoke(instance, timeout);
            private readonly Lazy<InstanceMethod> _withTimeout = new Lazy<InstanceMethod>(() => new InstanceMethod(TRANSACTION_CONFIG_BUILDER, TRANSACTION_CONFIG_BUILDER, "WithTimeout", NULLABLE_TIMESPAN), true);

            public void WithMetadata(object instance, IDictionary<string, object> metadata) => _withMetadata.Value.Invoke(instance, metadata);
            private readonly Lazy<InstanceMethod> _withMetadata = new Lazy<InstanceMethod>(() => new InstanceMethod(TRANSACTION_CONFIG_BUILDER, TRANSACTION_CONFIG_BUILDER, "WithMetadata", I_DICT_OF_STRING_AND_OBJECT), true);
        }
        internal sealed class IAsyncQueryRunnerInfo : DriverTypeInfo
        {
            public IAsyncQueryRunnerInfo(params string[] names) : base(names) { }

            //Task<IResultCursor> RunAsync(string query);
            //Task<IResultCursor> RunAsync(string query, IDictionary<string, object> parameters);
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
            private readonly Lazy<StaticMethod> _from = new Lazy<StaticMethod>(() => new StaticMethod(BOOKMARKS, BOOKMARKS, "From", STRING), true);
            
            public string[] Values(object instance) => (string[])_values.Value.GetValue(instance);
            private readonly Lazy<InstanceProperty> _values = new Lazy<InstanceProperty>(() => new InstanceProperty(BOOKMARKS, STRING_ARRAY, "Values"), true);
        }
    }
}
