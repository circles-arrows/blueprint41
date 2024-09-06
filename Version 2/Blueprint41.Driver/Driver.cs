#pragma warning disable S3881

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;

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

        public Session Session() => I_DRIVER.AsyncSession(this);
        public Session Session(Action<SessionConfigBuilder> configBuilder) => I_DRIVER.AsyncSession(this, configBuilder);


        public static void Configure<TDriverInterface>() => Configure<TDriverInterface>(new CustomTaskQueueOptions(10, 50), new CustomTaskQueueOptions(4, 20));
        public static void Configure<TDriverInterface>(CustomTaskQueueOptions mainQueue) => Configure<TDriverInterface>(mainQueue, CustomTaskQueueOptions.Disabled);
        public static void Configure<TDriverInterface>(CustomTaskQueueOptions mainQueue, CustomTaskQueueOptions subQueue)
        {
            lock (sync)
            {
                _taskScheduler = new CustomThreadSafeTaskScheduler(mainQueue, subQueue);
            }

            DriverTypeInfo.SearchAssembly = typeof(TDriverInterface).Assembly;

            if (typeof(TDriverInterface) != I_DRIVER.Type)
                throw new InvalidOperationException($"Please pass type of {I_DRIVER.Names} as generic argument TDriverInterface.");

            foreach (FieldInfo field in typeof(Driver).GetFields(BindingFlags.NonPublic | BindingFlags.Static).Where(item => typeof(DriverTypeInfo).IsAssignableFrom(item.FieldType) && item.Name == item.Name.ToUpperInvariant()))
            {
                DriverTypeInfo typeInfo = (DriverTypeInfo)field.GetValue(null)!;
                if (!typeInfo.Exists)
                    throw new TypeLoadException(typeInfo.Names);

                Dictionary<string, List<Member>> fieldGroups = new Dictionary<string, List<Member>>();

                foreach (FieldInfo member in typeInfo.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance).Where(item => item.FieldType.IsGenericType && item.FieldType.GenericTypeArguments.Length == 1 && item.FieldType.Name == "Lazy`1" && typeof(Member).IsAssignableFrom(item.FieldType.GenericTypeArguments[0]) && item.Name.StartsWith("_")))
                {
                    OneOfAttribute? oneOf = (OneOfAttribute?)member.GetCustomAttribute(typeof(OneOfAttribute));

                    Member memberInfo = (Member)((dynamic)member.GetValue(typeInfo)!).Value;
                    if (oneOf is null && !memberInfo.Exists)
                    {
                        throw new MissingMemberException($"Member {string.Join("or ", memberInfo.Names.Select(name => $"'{name}'"))} missing on type {typeInfo.Names}.");
                    }
                    else if (oneOf is not null)
                    {
                        List<Member>? group;
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
        public static Version AssemblyVersion => DriverTypeInfo.SearchAssembly?.GetName().Version ?? throw new InvalidOperationException("Please configure which Neo4j.Driver to use by running 'Driver.Configure<IDriver>();' first.");
        public static string NugetVersion => DriverTypeInfo.SearchAssembly?.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion ?? throw new InvalidOperationException("Please configure which Neo4j.Driver to use by running 'Driver.Configure<IDriver>();' first.");

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
        internal static readonly QueryPlanInfo                QUERY_PLAN                           = new QueryPlanInfo("Neo4j.Driver.IPlan");
        internal static readonly QueryProfileInfo             QUERY_PROFILE                        = new QueryProfileInfo("Neo4j.Driver.IProfiledPlan");
        internal static readonly IServerInfo                  SERVER_INFO                          = new IServerInfo("Neo4j.Driver.IServerInfo");
        internal static readonly IDatabaseInfo                DATABASE_INFO                        = new IDatabaseInfo("Neo4j.Driver.IDatabaseInfo");
        internal static readonly ICountersInfo                I_COUNTERS                           = new ICountersInfo("Neo4j.Driver.ICounters");
        internal static readonly INotificationInfo            I_NOTIFICATION                       = new INotificationInfo("Neo4j.Driver.INotification");
        internal static readonly IInputPositionInfo           I_INPUT_POSITION                     = new IInputPositionInfo("Neo4j.Driver.IInputPosition");
        internal static readonly ValueExtensionsInfo          VALUE_EXTENSIONS                     = new ValueExtensionsInfo("Neo4j.Driver.ValueExtensions");
        internal static readonly INodeInfo                    I_NODE                               = new INodeInfo("Neo4j.Driver.INode");
        internal static readonly IRelationshipInfo            I_RELATIONSHIP                       = new IRelationshipInfo("Neo4j.Driver.IRelationship");
        internal static readonly EncryptionLevelInfo          ENCRYPTION_LEVEL                     = new EncryptionLevelInfo("Neo4j.Driver.EncryptionLevel");
        internal static readonly ServerAddressInfo            SERVER_ADDRESS                       = new ServerAddressInfo("Neo4j.Driver.ServerAddress");
        internal static readonly TrustManagerInfo             TRUST_MANAGER                        = new TrustManagerInfo("Neo4j.Driver.TrustManager");
        internal static readonly ILoggerInfo                  I_LOGGER                             = new ILoggerInfo("Neo4j.Driver.ILogger");
        internal static readonly IServerAddressResolverInfo   I_SERVER_ADDRESS_RESOLVER            = new IServerAddressResolverInfo("Neo4j.Driver.IServerAddressResolver");

        internal static readonly DriverTypeInfo               I_AUTH_TOKEN                         = new DriverTypeInfo("Neo4j.Driver.IAuthToken");
        internal static readonly DriverTypeInfo               LOGGER_PROXY                         = new DriverTypeInfo(RuntimeCodeGen.GetLoggerProxyType);
        internal static readonly DriverTypeInfo               SERVER_ADDRESS_RESOLVER_PROXY        = new DriverTypeInfo(RuntimeCodeGen.GetServerAddressResolverProxyType);

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
            private const string LOGGER = "LoggerProxy";
            private const string RESOLVER = "ServerAddressResolverProxy";

            private static void GenerateTypes()
            {
                string assemblyName = "Blueprint41.Driver.RuntimeGeneration";

                AssemblyBuilder assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(new AssemblyName(assemblyName), AssemblyBuilderAccess.Run);
                ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule(assemblyName);

                #region Build ServerAddressResolverProxy

                TypeBuilder resolverBuilder = moduleBuilder.DefineType(
                        RESOLVER,
                        TypeAttributes.Public | TypeAttributes.Class | TypeAttributes.AutoClass | TypeAttributes.AnsiClass | TypeAttributes.Sealed | TypeAttributes.BeforeFieldInit | TypeAttributes.AutoLayout,
                        typeof(object),
                        new Type[] { I_SERVER_ADDRESS_RESOLVER.Type }
                    );

                FieldBuilder resolverField = BuildInitialization(resolverBuilder, typeof(ServerAddressResolver));

                BuildResolveMethod(resolverBuilder, resolverField);

                #endregion

                #region Build LoggerProxy

                TypeBuilder loggerBuilder = moduleBuilder.DefineType(
                        LOGGER,
                        TypeAttributes.Public | TypeAttributes.Class | TypeAttributes.AutoClass | TypeAttributes.AnsiClass | TypeAttributes.Sealed | TypeAttributes.BeforeFieldInit | TypeAttributes.AutoLayout,
                        typeof(object),
                        new Type[] { I_LOGGER.Type }
                    );

                FieldBuilder loggerField = BuildInitialization(loggerBuilder, typeof(ILogger));

                BuildLoggerMethod(loggerBuilder, loggerField, "Debug");
                BuildLoggerMethodEx(loggerBuilder, loggerField, "Error");
                BuildLoggerMethod(loggerBuilder, loggerField, "Info");
                BuildLoggerMethod(loggerBuilder, loggerField, "Trace");
                BuildLoggerMethodEx(loggerBuilder, loggerField, "Warn");

                BuildIsEnabledMethod(loggerBuilder, loggerField, "IsDebugEnabled");
                BuildIsEnabledMethod(loggerBuilder, loggerField, "IsTraceEnabled");

                #endregion


                // Build & load assembly
                _loggerProxyType = loggerBuilder.CreateType();
                _serverAddressResolverProxyType = resolverBuilder.CreateType();


                static FieldBuilder BuildInitialization(TypeBuilder builder, Type argument) // .ctor + field + static Get method
                {
                    ILGenerator il;

                    FieldBuilder field = builder.DefineField("_instance", argument, FieldAttributes.Private);

                    ConstructorBuilder ctor = builder.DefineConstructor(
                            MethodAttributes.Private | MethodAttributes.HideBySig | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName, 
                            CallingConventions.Standard, 
                            new Type[] { typeof(ILogger) }
                        );

                    ConstructorInfo? baseCtor = typeof(object).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, CallingConventions.Standard, new Type[0], null);
                    if (baseCtor is null)
                        throw new MissingMemberException();

                    il = ctor.GetILGenerator();
                    il.Emit(OpCodes.Ldarg_0);
                    il.Emit(OpCodes.Call, baseCtor);
                    il.Emit(OpCodes.Ldarg_0);
                    il.Emit(OpCodes.Ldarg_1);
                    il.Emit(OpCodes.Stfld, field);
                    il.Emit(OpCodes.Ret);

                    /*

                    // Methods
                    .method private hidebysig specialname rtspecialname 
                        instance void .ctor (
                            class [Blueprint41.Driver]Blueprint41.Driver.ILogger logger
                        ) cil managed 
                    {
                        // Method begins at RVA 0x20b6
                        // Header size: 1
                        // Code size: 14 (0xe)
                        .maxstack 8

                        IL_0000: ldarg.0
                        IL_0001: call instance void [System.Runtime]System.Object::.ctor()
                        IL_0006: ldarg.0
                        IL_0007: ldarg.1
                        IL_0008: stfld class [Blueprint41.Driver]Blueprint41.Driver.ILogger Blueprint41.Driver.RuntimeGeneration.LoggerProxy::_logger
                        IL_000d: ret
                    } // end of method LoggerProxy::.ctor 

                     */

                    MethodBuilder method = builder.DefineMethod(
                            "Get", 
                            MethodAttributes.Public | MethodAttributes.Static | MethodAttributes.HideBySig, 
                            CallingConventions.Standard, 
                            builder, 
                            new Type[] { argument }
                        );

                    il = method.GetILGenerator();
                    il.Emit(OpCodes.Ldarg_0);
                    il.Emit(OpCodes.Newobj, ctor);
                    il.Emit(OpCodes.Ret);

                    /*

                    .method public hidebysig static 
                        class Blueprint41.Driver.RuntimeGeneration.LoggerProxy Get (
                            class [Blueprint41.Driver]Blueprint41.Driver.ILogger logger
                        ) cil managed 
                    {
                        // Method begins at RVA 0x20c5
                        // Header size: 1
                        // Code size: 7 (0x7)
                        .maxstack 8

                        IL_0000: ldarg.0
                        IL_0001: newobj instance void Blueprint41.Driver.RuntimeGeneration.LoggerProxy::.ctor(class [Blueprint41.Driver]Blueprint41.Driver.ILogger)
                        IL_0006: ret
                    } // end of method LoggerProxy::Get

                     */

                    return field;
                }

                static void BuildResolveMethod(TypeBuilder builder, FieldBuilder field)
                {
                    // public ISet<neo4j.ServerAddress>? Resolve(neo4j.ServerAddress address)
                    MethodBuilder method = builder.DefineMethod(
                            "Resolve",
                            MethodAttributes.Public | MethodAttributes.Final | MethodAttributes.HideBySig | MethodAttributes.NewSlot | MethodAttributes.Virtual,
                            CallingConventions.HasThis,
                            typeof(ISet<>).MakeGenericType(SERVER_ADDRESS.Type),
                            new Type[] { SERVER_ADDRESS.Type }
                        );

                    ILGenerator il = method.GetILGenerator();

                    LocalBuilder intermediate = il.DeclareLocal(typeof(ServerAddress[]));                           // bp41.ServerAddress[] intermediate
                    LocalBuilder result = il.DeclareLocal(typeof(HashSet<>).MakeGenericType(SERVER_ADDRESS.Type));  // HashSet<neo4j.ServerAddress> result
                    LocalBuilder index = il.DeclareLocal(typeof(int));                                              // int index

                    var IL_0018 = il.DefineLabel();
                    var IL_001d = il.DefineLabel();
                    var IL_0023 = il.DefineLabel();
                    var IL_002d = il.DefineLabel();
                    var IL_0045 = il.DefineLabel();

                    ConstructorInfo? sa_ctor = typeof(ServerAddress).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[] { typeof(object) }, null);
                    if (sa_ctor is null)
                        throw new MissingMemberException();

                    MethodInfo? sa_resolve = typeof(ServerAddressResolver).GetMethod("Resolve", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[] { typeof(ServerAddress) }, null);
                    if (sa_resolve is null)
                        throw new MissingMemberException();

                    MethodInfo? sa_toArray = typeof(Enumerable).GetMethods(BindingFlags.Static | BindingFlags.Public).FirstOrDefault(item => item.Name == "ToArray")?.MakeGenericMethod(typeof(ServerAddress));
                    if (sa_toArray is null)
                        throw new MissingMemberException();

                    ConstructorInfo? sa_hs_ctor = typeof(HashSet<>).MakeGenericType(SERVER_ADDRESS.Type).GetConstructor(new Type[0]);
                    if (sa_hs_ctor is null)
                        throw new MissingMemberException();

                    MethodInfo? sa_inst = typeof(ServerAddress).GetMethod("get__instance", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[0], null);
                    if (sa_inst is null)
                        throw new MissingMemberException();

                    MethodInfo? sa_hs_add = typeof(HashSet<>).MakeGenericType(SERVER_ADDRESS.Type).GetMethod("Add");
                    if (sa_hs_add is null)
                        throw new MissingMemberException();

                    Type neo4j_sa = SERVER_ADDRESS.Type;

                    il.Emit(OpCodes.Ldarg_0);               //IL_0000: ldarg.0
                    il.Emit(OpCodes.Ldfld, field);          //IL_0001: ldfld            >>> ServerAddressResolverProxy._instance                                            >>> field
                    il.Emit(OpCodes.Ldarg_1);               //IL_0006: ldarg.1
                    il.Emit(OpCodes.Newobj, sa_ctor);       //IL_0007: newobj           >>> bp41.ServerAddress.ctor(object)                                                 >>> sa_ctor
                    il.Emit(OpCodes.Callvirt, sa_resolve);  //IL_000c: callvirt         >>> bp41.ServerAddressResolver.Resolve(bp41.ServerAddress)                          >>> sa_resolve
                    il.Emit(OpCodes.Dup);                   //IL_0011: dup
                    il.Emit(OpCodes.Brtrue_S, IL_0018);     //IL_0012: brtrue.s IL_0018

                    il.Emit(OpCodes.Pop);                   //IL_0014: pop
                    il.Emit(OpCodes.Ldnull);                //IL_0015: ldnull
                    il.Emit(OpCodes.Br_S, IL_001d);         //IL_0016: br.s IL_001d

                    il.MarkLabel(IL_0018);
                    il.Emit(OpCodes.Call, sa_toArray);      //IL_0018: call             >>> Enumerable.ToArray<bp41.ServerAddress>(this IEnumerable<bp41.ServerAddress>)    >>> sa_toArray

                    il.MarkLabel(IL_001d);
                    il.Emit(OpCodes.Stloc_0);               //IL_001d: stloc.0
                    il.Emit(OpCodes.Ldloc_0);               //IL_001e: ldloc.0
                    il.Emit(OpCodes.Brtrue_S, IL_0023);     //IL_001f: brtrue.s IL_0023

                    il.Emit(OpCodes.Ldnull);                //IL_0021: ldnull
                    il.Emit(OpCodes.Ret);                   //IL_0022: ret

                    il.MarkLabel(IL_0023);
                    il.Emit(OpCodes.Newobj, sa_hs_ctor);    //IL_0023: newobj           >>> HashSet<neo4j.ServerAddress>.ctor()                                             >>> sa_hs_ctor
                    il.Emit(OpCodes.Stloc_1);               //IL_0028: stloc.1
                    il.Emit(OpCodes.Ldc_I4_0);              //IL_0029: ldc.i4.0
                    il.Emit(OpCodes.Stloc_2);               //IL_002a: stloc.2
                    il.Emit(OpCodes.Br_S, IL_0045);         //IL_002b: br.s IL_0045

                    il.MarkLabel(IL_002d);
                                                            //loop start (head: IL_0045)
                    il.Emit(OpCodes.Ldloc_1);               //    IL_002d: ldloc.1
                    il.Emit(OpCodes.Ldloc_0);               //    IL_002e: ldloc.0
                    il.Emit(OpCodes.Ldloc_2);               //    IL_002f: ldloc.2
                    il.Emit(OpCodes.Ldelem_Ref);            //    IL_0030: ldelem.ref
                    il.Emit(OpCodes.Callvirt, sa_inst);     //    IL_0031: callvirt     >>> bp41.ServerAddress.get__instance()                                              >>> sa_inst
                    il.Emit(OpCodes.Castclass, neo4j_sa);   //    IL_0036: castclass    >>> neo4j.ServerAddress                                                             >>> neo4j_sa
                    il.Emit(OpCodes.Callvirt, sa_hs_add);   //    IL_003b: callvirt     >>> HashSet<neo4j.ServerAddress>.Add(neo4j.ServerAddress)                           >>> sa_hs_add
                    il.Emit(OpCodes.Pop);                   //    IL_0040: pop
                    il.Emit(OpCodes.Ldloc_2);               //    IL_0041: ldloc.2
                    il.Emit(OpCodes.Ldc_I4_1);              //    IL_0042: ldc.i4.1
                    il.Emit(OpCodes.Add);                   //    IL_0043: add
                    il.Emit(OpCodes.Stloc_2);               //    IL_0044: stloc.2

                    il.MarkLabel(IL_0045);
                    il.Emit(OpCodes.Ldloc_2);               //    IL_0045: ldloc.2
                    il.Emit(OpCodes.Ldloc_0);               //    IL_0046: ldloc.0
                    il.Emit(OpCodes.Ldlen);                 //    IL_0047: ldlen
                    il.Emit(OpCodes.Conv_I4);               //    IL_0048: conv.i4
                    il.Emit(OpCodes.Blt_S, IL_002d);        //    IL_0049: blt.s IL_002d
                                                            //end loop

                    il.Emit(OpCodes.Ldloc_1);               //IL_004b: ldloc.1
                    il.Emit(OpCodes.Ret);                   //IL_004c: ret

                    /*

                    .method public final hidebysig newslot virtual 
                        instance class [System]System.Collections.Generic.ISet`1<class [Neo4j.Driver]Neo4j.Driver.ServerAddress> Resolve (
                            class [Neo4j.Driver]Neo4j.Driver.ServerAddress address
                        ) cil managed 
                    {
                        // Method begins at RVA 0x20b4
                        // Header size: 12
                        // Code size: 77 (0x4d)
                        .maxstack 3
                        .locals init (
                            [0] class [Blueprint41.Driver]Blueprint41.Driver.ServerAddress[] intermediate,
                            [1] class [System.Core]System.Collections.Generic.HashSet`1<class [Neo4j.Driver]Neo4j.Driver.ServerAddress> result,
                            [2] int32 index
                        )

                        IL_0000: ldarg.0
                        IL_0001: ldfld class [Blueprint41.Driver]Blueprint41.Driver.ServerAddressResolver Blueprint41.Driver.RuntimeGeneration.ServerAddressResolverProxy::_instance
                        IL_0006: ldarg.1
                        IL_0007: newobj instance void [Blueprint41.Driver]Blueprint41.Driver.ServerAddress::.ctor(object)
                        IL_000c: callvirt instance class [System]System.Collections.Generic.ISet`1<class [Blueprint41.Driver]Blueprint41.Driver.ServerAddress> [Blueprint41.Driver]Blueprint41.Driver.ServerAddressResolver::Resolve(class [Blueprint41.Driver]Blueprint41.Driver.ServerAddress)
                        IL_0011: dup
                        IL_0012: brtrue.s IL_0018

                        IL_0014: pop
                        IL_0015: ldnull
                        IL_0016: br.s IL_001d

                        IL_0018: call !!0[] [System.Core]System.Linq.Enumerable::ToArray<class [Blueprint41.Driver]Blueprint41.Driver.ServerAddress>(class [mscorlib]System.Collections.Generic.IEnumerable`1<!!0>)

                        IL_001d: stloc.0
                        IL_001e: ldloc.0
                        IL_001f: brtrue.s IL_0023

                        IL_0021: ldnull
                        IL_0022: ret

                        IL_0023: newobj instance void class [System.Core]System.Collections.Generic.HashSet`1<class [Neo4j.Driver]Neo4j.Driver.ServerAddress>::.ctor()
                        IL_0028: stloc.1
                        IL_0029: ldc.i4.0
                        IL_002a: stloc.2
                        IL_002b: br.s IL_0045
                        // loop start (head: IL_0045)
                            IL_002d: ldloc.1
                            IL_002e: ldloc.0
                            IL_002f: ldloc.2
                            IL_0030: ldelem.ref
                            IL_0031: callvirt instance object [Blueprint41.Driver]Blueprint41.Driver.ServerAddress::get__instance()
                            IL_0036: castclass [Neo4j.Driver]Neo4j.Driver.ServerAddress
                            IL_003b: callvirt instance bool class [System.Core]System.Collections.Generic.HashSet`1<class [Neo4j.Driver]Neo4j.Driver.ServerAddress>::Add(!0)
                            IL_0040: pop
                            IL_0041: ldloc.2
                            IL_0042: ldc.i4.1
                            IL_0043: add
                            IL_0044: stloc.2

                            IL_0045: ldloc.2
                            IL_0046: ldloc.0
                            IL_0047: ldlen
                            IL_0048: conv.i4
                            IL_0049: blt.s IL_002d
                        // end loop

                        IL_004b: ldloc.1
                        IL_004c: ret
                    } // end of method ServerAddressResolverProxy::Resolve

                     */
                }

                static void BuildLoggerMethod(TypeBuilder builder, FieldBuilder field, string name) => PassThroughMethod(builder, field, name, typeof(void), new Type[] { typeof(string), typeof(object[]) });

                static void BuildLoggerMethodEx(TypeBuilder builder, FieldBuilder field, string name) => PassThroughMethod(builder, field, name, typeof(void), new Type[] { typeof(Exception), typeof(string), typeof(object[]) });

                static void BuildIsEnabledMethod(TypeBuilder builder, FieldBuilder field, string name) => PassThroughMethod(builder, field, name, typeof(bool),new Type[0]);

                static void PassThroughMethod(TypeBuilder builder, FieldBuilder field, string name, Type returns, params Type[] args)
                {
                    if (args.Length > 5)
                        throw new NotSupportedException();

                    MethodBuilder method = builder.DefineMethod(
                        name,
                        MethodAttributes.Public | MethodAttributes.Final | MethodAttributes.HideBySig | MethodAttributes.NewSlot | MethodAttributes.Virtual,
                        CallingConventions.HasThis,
                        returns,
                        args
                    );

                    MethodInfo? underlying = typeof(ILogger).GetMethod(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, args, null);
                    if (underlying is null)
                        throw new MissingMemberException();

                    ILGenerator il = method.GetILGenerator();

                    il.Emit(OpCodes.Ldarg_0);
                    il.Emit(OpCodes.Ldfld, field);
                    
                    if (args.Length >= 1)
                        il.Emit(OpCodes.Ldarg_1);

                    if (args.Length >= 2)
                        il.Emit(OpCodes.Ldarg_2);

                    if (args.Length >= 3)
                        il.Emit(OpCodes.Ldarg_3);

                    if (args.Length >= 4)
                        il.Emit(OpCodes.Ldarg_S, 4);

                    if (args.Length >= 5)
                        il.Emit(OpCodes.Ldarg_S, 5);

                    il.Emit(OpCodes.Callvirt, underlying);
                    il.Emit(OpCodes.Ret);

                    /*

                    .method public final hidebysig newslot virtual 
                        instance void Error (
                            class [System.Runtime]System.Exception cause,
                            string message,
                            object[] args
                        ) cil managed 
                    {
                        .param [3]
                            .custom instance void [System.Runtime]System.ParamArrayAttribute::.ctor() = (
                                01 00 00 00
                            )
                        // Method begins at RVA 0x20dc
                        // Header size: 1
                        // Code size: 15 (0xf)
                        .maxstack 8

                        IL_0000: ldarg.0
                        IL_0001: ldfld class [Blueprint41.Driver]Blueprint41.Driver.ILogger Blueprint41.Driver.RuntimeGeneration.LoggerProxy::_logger
                        IL_0006: ldarg.1
                        IL_0007: ldarg.2
                        IL_0008: ldarg.3
                        IL_0009: callvirt instance void [Blueprint41.Driver]Blueprint41.Driver.ILogger::Error(class [System.Runtime]System.Exception, string, object[])
                        IL_000e: ret
                    } // end of method LoggerProxy::Error

                     */
                }
            }

            internal static Type GetServerAddressResolverProxyType()
            {
                if (_serverAddressResolverProxyType is null)
                {
#pragma warning disable S2551 // Shared resources should not be used for locking
                    lock (typeof(RuntimeCodeGen))
                    {
                        if (_serverAddressResolverProxyType is null)
                            GenerateTypes();
                    }
#pragma warning restore S2551 // Shared resources should not be used for locking
                }
                return _serverAddressResolverProxyType ?? throw new TypeLoadException("ServerAddressResolverProxy");
            }
            private static Type? _serverAddressResolverProxyType = null;

            internal static Type GetLoggerProxyType()
            {
                if (_loggerProxyType is null)
                {
#pragma warning disable S2551 // Shared resources should not be used for locking
                    lock (typeof(RuntimeCodeGen))
                    {
                        if (_loggerProxyType is null)
                            GenerateTypes();
                    }
#pragma warning restore S2551 // Shared resources should not be used for locking
                }
                return _loggerProxyType ?? throw new TypeLoadException("LoggerProxy");
            }
            private static Type? _loggerProxyType = null;
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
            protected virtual string Name => string.Join("or ", Names.Select(name => $"'{name}'"));
            public string[] Names { get; private set; }
            public DriverTypeInfo[] Arguments { get; private set; }
            public bool IsEnum { get; private set; }

            public abstract bool Exists { get; }
            protected T Throw<T>() => throw new MissingMethodException(Parent.Names, Name);

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
            private T? ForEachType(Type? type, string name, Type? returnType, Type[] arguments, bool deepSearch)
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

                _getValue = new Lazy<Func<object?>>(() => DelegateHelper.CreateDelegate<Func<object?>>(PropertyInfo.GetGetMethod()!, null, DelegateHelper.CreateOptions.Downcasting), true);
                _getValueWithIndexer = new Lazy<Func<object?, object?>>(() => DelegateHelper.CreateDelegate<Func<object?, object?>>(PropertyInfo.GetGetMethod()!, null, DelegateHelper.CreateOptions.Downcasting), true);

                _setValue = new Lazy<Action<object?>>(() => DelegateHelper.CreateDelegate<Action<object?>>(PropertyInfo.GetSetMethod()!, null, DelegateHelper.CreateOptions.Downcasting), false);
                _setValueWithIndexer = new Lazy<Action<object?, object?>>(() => DelegateHelper.CreateDelegate<Action<object?, object?>>(PropertyInfo.GetSetMethod()!, null, DelegateHelper.CreateOptions.Downcasting), false);
            }
            internal StaticProperty(DriverTypeInfo parent, DriverTypeInfo returnType, string[] names, DriverTypeInfo? indexer = null) : base(parent, returnType, names, (indexer is null) ? new DriverTypeInfo[0] : new DriverTypeInfo[] { indexer })
            {
                _propertyInfo = ForEachType();

                _getValue = new Lazy<Func<object?>>(() => DelegateHelper.CreateDelegate<Func<object?>>(PropertyInfo.GetGetMethod()!, null, DelegateHelper.CreateOptions.Downcasting), true);
                _getValueWithIndexer = new Lazy<Func<object?, object?>>(() => DelegateHelper.CreateDelegate<Func<object?, object?>>(PropertyInfo.GetGetMethod()!, null, DelegateHelper.CreateOptions.Downcasting), true);

                _setValue = new Lazy<Action<object?>>(() => DelegateHelper.CreateDelegate<Action<object?>>(PropertyInfo.GetSetMethod()!, null, DelegateHelper.CreateOptions.Downcasting), false);
                _setValueWithIndexer = new Lazy<Action<object?, object?>>(() => DelegateHelper.CreateDelegate<Action<object?, object?>>(PropertyInfo.GetSetMethod()!, null, DelegateHelper.CreateOptions.Downcasting), false);
            }
            protected override PropertyInfo? Search(Type type, string name, Type? returnType, Type[] args)
            {
                return type.GetProperty(name, BindingFlags.Public | BindingFlags.Static, null, returnType, args, null);
            }
            public override bool Exists => (_propertyInfo.Value is not null);

            public object GetValue() => _getValue.Value.Invoke()!;
            public object GetValue(object? index) => _getValueWithIndexer.Value.Invoke(index)!;
            public void SetValue(object? value) => _setValue.Value.Invoke(value);
            public void SetValue(object? value, object? index) => _setValueWithIndexer.Value.Invoke(value, index);

            private PropertyInfo PropertyInfo => _propertyInfo.Value ?? Throw<PropertyInfo>();

            private readonly Lazy<Func<object?>> _getValue;
            private readonly Lazy<Func<object?, object?>> _getValueWithIndexer;
            private readonly Lazy<Action<object?>> _setValue;
            private readonly Lazy<Action<object?, object?>> _setValueWithIndexer;
            private readonly Lazy<PropertyInfo?> _propertyInfo;
        }
        internal sealed class InstanceProperty : Member<PropertyInfo>
        {
            internal InstanceProperty(DriverTypeInfo parent, DriverTypeInfo returnType, string name, DriverTypeInfo? indexer = null) : base(parent, returnType, name, (indexer is null) ? new DriverTypeInfo[0] : new DriverTypeInfo[] { indexer })
            {
                _propertyInfo = ForEachType();

                _getValue0 = new Lazy<Func<object, object?>>(() =>            DelegateHelper.CreateOpenInstanceDelegate<Func<object, object?>>(PropertyInfo.GetGetMethod()!, DelegateHelper.CreateOptions.Downcasting), true);
                _getValue1 = new Lazy<Func<object, object?, object?>>(() =>   DelegateHelper.CreateOpenInstanceDelegate<Func<object, object?, object?>>(PropertyInfo.GetGetMethod()!, DelegateHelper.CreateOptions.Downcasting), true);
                _setValue0 = new Lazy<Action<object, object?>>(() =>          DelegateHelper.CreateOpenInstanceDelegate<Action<object, object?>>(PropertyInfo.GetSetMethod()!, DelegateHelper.CreateOptions.Downcasting), false);
                _setValue1 = new Lazy<Action<object, object?, object?>>(() => DelegateHelper.CreateOpenInstanceDelegate<Action<object, object?, object?>>(PropertyInfo.GetSetMethod()!, DelegateHelper.CreateOptions.Downcasting), false);
            }
            internal InstanceProperty(DriverTypeInfo parent, DriverTypeInfo returnType, string[] names, DriverTypeInfo? indexer = null) : base(parent, returnType, names, (indexer is null) ? new DriverTypeInfo[0] : new DriverTypeInfo[] { indexer })
            {
                _propertyInfo = ForEachType();

                _getValue0 = new Lazy<Func<object, object?>>(() =>            DelegateHelper.CreateOpenInstanceDelegate<Func<object, object?>>(PropertyInfo.GetGetMethod()!, DelegateHelper.CreateOptions.Downcasting), true);
                _getValue1 = new Lazy<Func<object, object?, object?>>(() =>   DelegateHelper.CreateOpenInstanceDelegate<Func<object, object?, object?>>(PropertyInfo.GetGetMethod()!, DelegateHelper.CreateOptions.Downcasting), true);
                _setValue0 = new Lazy<Action<object, object?>>(() =>          DelegateHelper.CreateOpenInstanceDelegate<Action<object, object?>>(PropertyInfo.GetSetMethod()!, DelegateHelper.CreateOptions.Downcasting), false);
                _setValue1 = new Lazy<Action<object, object?, object?>>(() => DelegateHelper.CreateOpenInstanceDelegate<Action<object, object?, object?>>(PropertyInfo.GetSetMethod()!, DelegateHelper.CreateOptions.Downcasting), false);
            }
            protected override PropertyInfo? Search(Type type, string name, Type? returnType, Type[] args)
            {
                return type.GetProperty(name, BindingFlags.Public | BindingFlags.Instance, null, returnType, args, null);
            }
            public override bool Exists => (_propertyInfo.Value is not null);

            public object GetValue(object instance)                            => _getValue0.Value.Invoke(instance)!;
            public object GetValue(object instance, object? index)             => _getValue1.Value.Invoke(instance, index)!;
            public void SetValue(object instance, object? value)                => _setValue0.Value.Invoke(instance, value);
            public void SetValue(object instance, object? value, object? index) => _setValue1.Value.Invoke(instance, value, index);

            private PropertyInfo PropertyInfo => _propertyInfo.Value ?? Throw<PropertyInfo>();

            private readonly Lazy<Func<object, object?>>            _getValue0;
            private readonly Lazy<Func<object, object?, object?>>   _getValue1;
            private readonly Lazy<Action<object, object?>>          _setValue0;
            private readonly Lazy<Action<object, object?, object?>> _setValue1;
            private readonly Lazy<PropertyInfo?> _propertyInfo;
        }
        internal sealed class StaticMethod : Member<MethodInfo>
        {
            internal StaticMethod(DriverTypeInfo parent, DriverTypeInfo returnType, string name, params DriverTypeInfo[] arguments) : base(parent, returnType, name, arguments)
            {
                _methodInfo = ForEachType();

                _invoke0 = new Lazy<Func<object?>>(() =>                                              DelegateHelper.CreateDelegate<Func<object?>>(MethodInfo, null, DelegateHelper.CreateOptions.Downcasting), true);
                _invoke1 = new Lazy<Func<object?, object?>>(() =>                                     DelegateHelper.CreateDelegate<Func<object?, object?>>(MethodInfo, null, DelegateHelper.CreateOptions.Downcasting), true);
                _invoke2 = new Lazy<Func<object?, object?, object?>>(() =>                            DelegateHelper.CreateDelegate<Func<object?, object?, object?>>(MethodInfo, null, DelegateHelper.CreateOptions.Downcasting), true);
                _invoke3 = new Lazy<Func<object?, object?, object?, object?>>(() =>                   DelegateHelper.CreateDelegate<Func<object?, object?, object?, object?>>(MethodInfo, null, DelegateHelper.CreateOptions.Downcasting), true);
                _invoke4 = new Lazy<Func<object?, object?, object?, object?, object?>>(() =>          DelegateHelper.CreateDelegate<Func<object?, object?, object?, object?, object?>>(MethodInfo, null, DelegateHelper.CreateOptions.Downcasting), true);
                _invoke5 = new Lazy<Func<object?, object?, object?, object?, object?, object?>>(() => DelegateHelper.CreateDelegate<Func<object?, object?, object?, object?, object?, object?>>(MethodInfo, null, DelegateHelper.CreateOptions.Downcasting), true);
            }
            internal StaticMethod(DriverTypeInfo parent, DriverTypeInfo returnType, string[] names, params DriverTypeInfo[] arguments) : base(parent, returnType, names, arguments)
            {
                _methodInfo = ForEachType();

                _invoke0 = new Lazy<Func<object?>>(() =>                                              DelegateHelper.CreateDelegate<Func<object?>>(MethodInfo, null, DelegateHelper.CreateOptions.Downcasting), true);
                _invoke1 = new Lazy<Func<object?, object?>>(() =>                                     DelegateHelper.CreateDelegate<Func<object?, object?>>(MethodInfo, null, DelegateHelper.CreateOptions.Downcasting), true);
                _invoke2 = new Lazy<Func<object?, object?, object?>>(() =>                            DelegateHelper.CreateDelegate<Func<object?, object?, object?>>(MethodInfo, null, DelegateHelper.CreateOptions.Downcasting), true);
                _invoke3 = new Lazy<Func<object?, object?, object?, object?>>(() =>                   DelegateHelper.CreateDelegate<Func<object?, object?, object?, object?>>(MethodInfo, null, DelegateHelper.CreateOptions.Downcasting), true);
                _invoke4 = new Lazy<Func<object?, object?, object?, object?, object?>>(() =>          DelegateHelper.CreateDelegate<Func<object?, object?, object?, object?, object?>>(MethodInfo, null, DelegateHelper.CreateOptions.Downcasting), true);
                _invoke5 = new Lazy<Func<object?, object?, object?, object?, object?, object?>>(() => DelegateHelper.CreateDelegate<Func<object?, object?, object?, object?, object?, object?>>(MethodInfo, null, DelegateHelper.CreateOptions.Downcasting), true);
            }
            protected override MethodInfo? Search(Type type, string name, Type? returnType, Type[] args)
            {
                return GetMethodInfo(BindingFlags.Static, type, returnType!, name, args);
            }
            public override bool Exists => (_methodInfo.Value is not null);

            public object Invoke()                                                                     => _invoke0.Value.Invoke()!;
            public object Invoke(object? arg1)                                                         => _invoke1.Value.Invoke(arg1)!;
            public object Invoke(object? arg1, object? arg2)                                           => _invoke2.Value.Invoke(arg1, arg2)!;
            public object Invoke(object? arg1, object? arg2, object? arg3)                             => _invoke3.Value.Invoke(arg1, arg2, arg3)!;
            public object Invoke(object? arg1, object? arg2, object? arg3, object? arg4)               => _invoke4.Value.Invoke(arg1, arg2, arg3, arg4)!;
            public object Invoke(object? arg1, object? arg2, object? arg3, object? arg4, object? arg5) => _invoke5.Value.Invoke(arg1, arg2, arg3, arg4, arg5)!;

            private MethodInfo MethodInfo => _methodInfo.Value ?? Throw<MethodInfo>();

            private readonly Lazy<Func<object?>> _invoke0;
            private readonly Lazy<Func<object?, object?>> _invoke1;
            private readonly Lazy<Func<object?, object?, object?>> _invoke2;
            private readonly Lazy<Func<object?, object?, object?, object?>> _invoke3;
            private readonly Lazy<Func<object?, object?, object?, object?, object?>> _invoke4;
            private readonly Lazy<Func<object?, object?, object?, object?, object?, object?>> _invoke5;
            private readonly Lazy<MethodInfo?> _methodInfo;
        }
        internal sealed class InstanceMethod : Member<MethodInfo>
        {
            internal InstanceMethod(DriverTypeInfo parent, DriverTypeInfo returnType, string name, params DriverTypeInfo[] arguments) : base(parent, returnType, name, arguments)
            {
                _methodInfo = ForEachType();

                _invoke0 = new Lazy<Func<object, object?>>(() =>                                              DelegateHelper.CreateOpenInstanceDelegate<Func<object, object?>>(MethodInfo, DelegateHelper.CreateOptions.Downcasting), true);
                _invoke1 = new Lazy<Func<object, object?, object?>>(() =>                                     DelegateHelper.CreateOpenInstanceDelegate<Func<object, object?, object?>>(MethodInfo, DelegateHelper.CreateOptions.Downcasting), true);
                _invoke2 = new Lazy<Func<object, object?, object?, object?>>(() =>                            DelegateHelper.CreateOpenInstanceDelegate<Func<object, object?, object?, object?>>(MethodInfo, DelegateHelper.CreateOptions.Downcasting), true);
                _invoke3 = new Lazy<Func<object, object?, object?, object?, object?>>(() =>                   DelegateHelper.CreateOpenInstanceDelegate<Func<object, object?, object?, object?, object?>>(MethodInfo, DelegateHelper.CreateOptions.Downcasting), true);
                _invoke4 = new Lazy<Func<object, object?, object?, object?, object?, object?>>(() =>          DelegateHelper.CreateOpenInstanceDelegate<Func<object, object?, object?, object?, object?, object?>>(MethodInfo, DelegateHelper.CreateOptions.Downcasting), true);
                _invoke5 = new Lazy<Func<object, object?, object?, object?, object?, object?, object?>>(() => DelegateHelper.CreateOpenInstanceDelegate<Func<object, object?, object?, object?, object?, object?, object?>>(MethodInfo, DelegateHelper.CreateOptions.Downcasting), true);
            }
            internal InstanceMethod(DriverTypeInfo parent, DriverTypeInfo returnType, string[] names, params DriverTypeInfo[] arguments) : base(parent, returnType, names, arguments)
            {
                _methodInfo = ForEachType();

                _invoke0 = new Lazy<Func<object, object?>>(() =>                                              DelegateHelper.CreateOpenInstanceDelegate<Func<object, object?>>(MethodInfo, DelegateHelper.CreateOptions.Downcasting), true);
                _invoke1 = new Lazy<Func<object, object?, object?>>(() =>                                     DelegateHelper.CreateOpenInstanceDelegate<Func<object, object?, object?>>(MethodInfo, DelegateHelper.CreateOptions.Downcasting), true);
                _invoke2 = new Lazy<Func<object, object?, object?, object?>>(() =>                            DelegateHelper.CreateOpenInstanceDelegate<Func<object, object?, object?, object?>>(MethodInfo, DelegateHelper.CreateOptions.Downcasting), true);
                _invoke3 = new Lazy<Func<object, object?, object?, object?, object?>>(() =>                   DelegateHelper.CreateOpenInstanceDelegate<Func<object, object?, object?, object?, object?>>(MethodInfo, DelegateHelper.CreateOptions.Downcasting), true);
                _invoke4 = new Lazy<Func<object, object?, object?, object?, object?, object?>>(() =>          DelegateHelper.CreateOpenInstanceDelegate<Func<object, object?, object?, object?, object?, object?>>(MethodInfo, DelegateHelper.CreateOptions.Downcasting), true);
                _invoke5 = new Lazy<Func<object, object?, object?, object?, object?, object?, object?>>(() => DelegateHelper.CreateOpenInstanceDelegate<Func<object, object?, object?, object?, object?, object?, object?>>(MethodInfo, DelegateHelper.CreateOptions.Downcasting), true);
            }
            protected override MethodInfo? Search(Type type, string name, Type? returnType, Type[] args)
            {
                return GetMethodInfo(BindingFlags.Instance, type, returnType!, name, args);
            }
            public override bool Exists => (_methodInfo.Value is not null);

            public object Invoke(object instance)                                                                       => _invoke0.Value.Invoke(instance)!;
            public object Invoke(object instance, object? arg1)                                                         => _invoke1.Value.Invoke(instance, arg1)!;
            public object Invoke(object instance, object? arg1, object? arg2)                                           => _invoke2.Value.Invoke(instance, arg1, arg2)!;
            public object Invoke(object instance, object? arg1, object? arg2, object? arg3)                             => _invoke3.Value.Invoke(instance, arg1, arg2, arg3)!;
            public object Invoke(object instance, object? arg1, object? arg2, object? arg3, object? arg4)               => _invoke4.Value.Invoke(instance, arg1, arg2, arg3, arg4)!;
            public object Invoke(object instance, object? arg1, object? arg2, object? arg3, object? arg4, object? arg5) => _invoke5.Value.Invoke(instance, arg1, arg2, arg3, arg4, arg5)!;

            private MethodInfo MethodInfo => _methodInfo.Value ?? Throw<MethodInfo>();

            private readonly Lazy<Func<object, object?>> _invoke0;
            private readonly Lazy<Func<object, object?, object?>> _invoke1;
            private readonly Lazy<Func<object, object?, object?, object?>> _invoke2;
            private readonly Lazy<Func<object, object?, object?, object?, object?>> _invoke3;
            private readonly Lazy<Func<object, object?, object?, object?, object?, object?>> _invoke4;
            private readonly Lazy<Func<object, object?, object?, object?, object?, object?, object?>> _invoke5;
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

            public object Value
            {
                get
                {
                    if (value is null)
                        value = FieldInfo.GetRawConstantValue();

                    return value!;
                }
            }
            private object? value;
            public bool Test(object instance) => Value == instance;

            private FieldInfo FieldInfo => _fieldInfo.Value ?? Throw<FieldInfo>();
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
            protected override string Name => ".ctor";

            public TMember ForTypes(DriverTypeInfo returnType, params DriverTypeInfo[] arguments)
            {
                TMember? member;

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
            
            private ConstructorInfo ConstructorInfo => _constructorInfo.Value ?? Throw<ConstructorInfo>();
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
                public override bool Equals(object? obj)
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

            public Session AsyncSession(Driver driver) => new Session(_asyncSession1.Value.Invoke(driver._instance));
            private readonly Lazy<InstanceMethod> _asyncSession1 = new Lazy<InstanceMethod>(() => new InstanceMethod(I_DRIVER, I_ASYNC_SESSION, "AsyncSession"), true);

            public Session AsyncSession(Driver driver, Action<SessionConfigBuilder> configBuilder) => new Session(_asyncSession2.Value.Invoke(driver._instance, DelegateHelper.WrapDelegate(Generic.Of(typeof(Action<>), SESSION_CONFIG_BUILDER).Type, (object o) => configBuilder.Invoke(new SessionConfigBuilder(o)))));
            private readonly Lazy<InstanceMethod> _asyncSession2 = new Lazy<InstanceMethod>(() => new InstanceMethod(I_DRIVER, I_ASYNC_SESSION, "AsyncSession", Generic.Of(typeof(Action<>), SESSION_CONFIG_BUILDER)), true);

            public Task VerifyConnectivityAsync(Driver driver) => AsTask(_verifyConnectivityAsync.Value.Invoke(driver._instance));
            private readonly Lazy<InstanceMethod> _verifyConnectivityAsync = new Lazy<InstanceMethod>(() => new InstanceMethod(I_DRIVER, Type<Task>.Info, "VerifyConnectivityAsync"), true);

            public Task<bool> SupportsMultiDbAsync(Driver driver) => AsTask<bool>(_supportsMultiDbAsync.Value.Invoke(driver._instance));
            private readonly Lazy<InstanceMethod> _supportsMultiDbAsync = new Lazy<InstanceMethod>(() => new InstanceMethod(I_DRIVER, Type<Task<bool>>.Info, "SupportsMultiDbAsync"), true);
        }
        internal sealed class ConfigBuilderInfo : DriverTypeInfo
        {
            public ConfigBuilderInfo(params string[] names) : base(names) { }

            public void WithEncryptionLevel(object instance, object level) => _withEncryptionLevel.Value.Invoke(instance, level);
            private readonly Lazy<InstanceMethod> _withEncryptionLevel = new Lazy<InstanceMethod>(() => new InstanceMethod(CONFIG_BUILDER, CONFIG_BUILDER, "WithEncryptionLevel", ENCRYPTION_LEVEL), true);

            public void WithTrustManager(object instance, TrustManager manager) => _withTrustManager.Value.Invoke(instance, manager._instance);
            private readonly Lazy<InstanceMethod> _withTrustManager = new Lazy<InstanceMethod>(() => new InstanceMethod(CONFIG_BUILDER, CONFIG_BUILDER, "WithTrustManager", TRUST_MANAGER), true);

            public void WithLogger(object instance, ILogger logger) => _withLogger.Value.Invoke(instance, I_LOGGER.ConvertToNeo4jILogger(logger));
            private readonly Lazy<InstanceMethod> _withLogger = new Lazy<InstanceMethod>(() => new InstanceMethod(CONFIG_BUILDER, CONFIG_BUILDER, "WithLogger", I_LOGGER), true);

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

            public void WithResolver(object instance, ServerAddressResolver resolver) => _withResolver.Value.Invoke(instance, I_SERVER_ADDRESS_RESOLVER.ConvertToNeo4jIServerAddressResolver(resolver));
            private readonly Lazy<InstanceMethod> _withResolver = new Lazy<InstanceMethod>(() => new InstanceMethod(CONFIG_BUILDER, CONFIG_BUILDER, "WithResolver", I_SERVER_ADDRESS_RESOLVER), true);

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

            public void WithUserAgent(object instance, string userAgent) => _withUserAgent.Value.Invoke(instance, userAgent);
            private readonly Lazy<InstanceMethod> _withUserAgent = new Lazy<InstanceMethod>(() => new InstanceMethod(CONFIG_BUILDER, CONFIG_BUILDER, "WithUserAgent", Type<string>.Info), true);

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

            public bool HasPlan(object instance) => (bool)_hasPlan.Value.GetValue(instance);
            private readonly Lazy<InstanceProperty> _hasPlan = new Lazy<InstanceProperty>(() => new InstanceProperty(I_RESULT_SUMMARY, Type<bool>.Info, "HasPlan"), true);

            public bool HasProfile(object instance) => (bool)_hasProfile.Value.GetValue(instance);
            private readonly Lazy<InstanceProperty> _hasProfile = new Lazy<InstanceProperty>(() => new InstanceProperty(I_RESULT_SUMMARY, Type<bool>.Info, "HasProfile"), true);

            public QueryPlan Plan(object instance) => new QueryPlan(_Plan.Value.GetValue(instance));
            private readonly Lazy<InstanceProperty> _Plan = new Lazy<InstanceProperty>(() => new InstanceProperty(I_RESULT_SUMMARY, QUERY_PLAN, "Plan"), true);

            public QueryProfile Profile(object instance) => new QueryProfile(_Profile.Value.GetValue(instance));
            private readonly Lazy<InstanceProperty> _Profile = new Lazy<InstanceProperty>(() => new InstanceProperty(I_RESULT_SUMMARY, QUERY_PROFILE, "Profile"), true);

            public TimeSpan ResultAvailableAfter(object instance) => (TimeSpan)_ResultAvailableAfter.Value.GetValue(instance);
            private readonly Lazy<InstanceProperty> _ResultAvailableAfter = new Lazy<InstanceProperty>(() => new InstanceProperty(I_RESULT_SUMMARY, Type<TimeSpan>.Info, "ResultAvailableAfter"), true);

            public TimeSpan ResultConsumedAfter(object instance) => (TimeSpan)_ResultConsumedAfter.Value.GetValue(instance);
            private readonly Lazy<InstanceProperty> _ResultConsumedAfter = new Lazy<InstanceProperty>(() => new InstanceProperty(I_RESULT_SUMMARY, Type<TimeSpan>.Info, "ResultConsumedAfter"), true);

            public ServerInfo Server(object instance) => new ServerInfo(_Server.Value.GetValue(instance));
            private readonly Lazy<InstanceProperty> _Server = new Lazy<InstanceProperty>(() => new InstanceProperty(I_RESULT_SUMMARY, SERVER_INFO, "Server"), true);

            public DatabaseInfo Database(object instance) => new DatabaseInfo(_Database.Value.GetValue(instance));
            private readonly Lazy<InstanceProperty> _Database = new Lazy<InstanceProperty>(() => new InstanceProperty(I_RESULT_SUMMARY, DATABASE_INFO, "Database"), true);

        }
        internal sealed class QueryInfo : DriverTypeInfo
        {
            public QueryInfo(params string[] names) : base(names) { }

            public string Text(object instance) => (string)_text.Value.GetValue(instance);
            private readonly Lazy<InstanceProperty> _text = new Lazy<InstanceProperty>(() => new InstanceProperty(QUERY, Type<string>.Info, "Text"), true);

            public IDictionary<string, object> Parameters(object instance) => (IDictionary<string, object>)_parameters.Value.GetValue(instance);
            private readonly Lazy<InstanceProperty> _parameters = new Lazy<InstanceProperty>(() => new InstanceProperty(QUERY, Type<IDictionary<string, object>>.Info, "Parameters"), true);
        }
        internal sealed class QueryPlanInfo : DriverTypeInfo
        {
            public QueryPlanInfo(params string[] names) : base(names) { }

            public string OperatorType(object instance) => (string)_OperatorType.Value.GetValue(instance);
            private readonly Lazy<InstanceProperty> _OperatorType = new Lazy<InstanceProperty>(() => new InstanceProperty(QUERY_PLAN, Type<string>.Info, "OperatorType"), true);

            public IDictionary<string, object> Arguments(object instance) => (IDictionary<string, object>)_Arguments.Value.GetValue(instance);
            private readonly Lazy<InstanceProperty> _Arguments = new Lazy<InstanceProperty>(() => new InstanceProperty(QUERY_PLAN, Type<IDictionary<string, object>>.Info, "Arguments"), true);

            public IList<string> Identifiers(object instance) => (IList<string>)_Identifiers.Value.GetValue(instance);
            private readonly Lazy<InstanceProperty> _Identifiers = new Lazy<InstanceProperty>(() => new InstanceProperty(QUERY_PLAN, Type<IList<string>>.Info, "Identifiers"), true);

            public IList<QueryPlan> Children(object instance) => ToList<QueryPlan>(_Children.Value.GetValue(instance), item => new QueryPlan(item));
            private readonly Lazy<InstanceProperty> _Children = new Lazy<InstanceProperty>(() => new InstanceProperty(QUERY_PLAN, Generic.Of(typeof(IList<>), QUERY_PLAN), "Children"), true);
        }
        internal sealed class QueryProfileInfo : DriverTypeInfo
        {
            public QueryProfileInfo(params string[] names) : base(names) { }

            public string OperatorType(object instance) => (string)_OperatorType.Value.GetValue(instance);
            private readonly Lazy<InstanceProperty> _OperatorType = new Lazy<InstanceProperty>(() => new InstanceProperty(QUERY_PROFILE, Type<string>.Info, "OperatorType"), true);

            public IDictionary<string, object> Arguments(object instance) => (IDictionary<string, object>)_Arguments.Value.GetValue(instance);
            private readonly Lazy<InstanceProperty> _Arguments = new Lazy<InstanceProperty>(() => new InstanceProperty(QUERY_PROFILE, Type<IDictionary<string, object>>.Info, "Arguments"), true);

            public IList<string> Identifiers(object instance) => (IList<string>)_Identifiers.Value.GetValue(instance);
            private readonly Lazy<InstanceProperty> _Identifiers = new Lazy<InstanceProperty>(() => new InstanceProperty(QUERY_PROFILE, Type<IList<string>>.Info, "Identifiers"), true);

            public IList<QueryProfile> Children(object instance) => ToList<QueryProfile>(_Children.Value.GetValue(instance), item => new QueryProfile(item));
            private readonly Lazy<InstanceProperty> _Children = new Lazy<InstanceProperty>(() => new InstanceProperty(QUERY_PROFILE, Generic.Of(typeof(IList<>), QUERY_PROFILE), "Children"), true);

            public long DbHits(object instance) => (long)_DbHits.Value.GetValue(instance);
            private readonly Lazy<InstanceProperty> _DbHits = new Lazy<InstanceProperty>(() => new InstanceProperty(QUERY_PROFILE, Type<long>.Info, "DbHits"), true);

            public long Records(object instance) => (long)_Records.Value.GetValue(instance);
            private readonly Lazy<InstanceProperty> _Records = new Lazy<InstanceProperty>(() => new InstanceProperty(QUERY_PROFILE, Type<long>.Info, "Records"), true);

            public long PageCacheHits(object instance) => (long)_PageCacheHits.Value.GetValue(instance);
            private readonly Lazy<InstanceProperty> _PageCacheHits = new Lazy<InstanceProperty>(() => new InstanceProperty(QUERY_PROFILE, Type<long>.Info, "PageCacheHits"), true);

            public long PageCacheMisses(object instance) => (long)_PageCacheMisses.Value.GetValue(instance);
            private readonly Lazy<InstanceProperty> _PageCacheMisses = new Lazy<InstanceProperty>(() => new InstanceProperty(QUERY_PROFILE, Type<long>.Info, "PageCacheMisses"), true);

            public double PageCacheHitRatio(object instance) => (double)_PageCacheHitRatio.Value.GetValue(instance);
            private readonly Lazy<InstanceProperty> _PageCacheHitRatio = new Lazy<InstanceProperty>(() => new InstanceProperty(QUERY_PROFILE, Type<double>.Info, "PageCacheHitRatio"), true);
            
            public long Time(object instance) => (long)_Time.Value.GetValue(instance);
            private readonly Lazy<InstanceProperty> _Time = new Lazy<InstanceProperty>(() => new InstanceProperty(QUERY_PROFILE, Type<long>.Info, "Time"), true);

            public bool HasPageCacheStats(object instance) => (bool)_HasPageCacheStats.Value.GetValue(instance);
            private readonly Lazy<InstanceProperty> _HasPageCacheStats = new Lazy<InstanceProperty>(() => new InstanceProperty(QUERY_PROFILE, Type<bool>.Info, "HasPageCacheStats"), true);
        }
        internal sealed class IServerInfo : DriverTypeInfo
        {
            public IServerInfo(params string[] names) : base(names) { }

            public string Address(object instance) => (string)_Address.Value.GetValue(instance);
            private readonly Lazy<InstanceProperty> _Address = new Lazy<InstanceProperty>(() => new InstanceProperty(SERVER_INFO, Type<string>.Info, "Address"), true);

            public string ProtocolVersion(object instance) => (string)_ProtocolVersion.Value.GetValue(instance);
            private readonly Lazy<InstanceProperty> _ProtocolVersion = new Lazy<InstanceProperty>(() => new InstanceProperty(SERVER_INFO, Type<string>.Info, "ProtocolVersion"), true);

            public string Agent(object instance) => (string)_Agent.Value.GetValue(instance);
            private readonly Lazy<InstanceProperty> _Agent = new Lazy<InstanceProperty>(() => new InstanceProperty(SERVER_INFO, Type<string>.Info, "Agent"), true);
        }
        internal sealed class IDatabaseInfo : DriverTypeInfo
        {
            public IDatabaseInfo(params string[] names) : base(names) { }

            public string Name(object instance) => (string)_Name.Value.GetValue(instance);
            private readonly Lazy<InstanceProperty> _Name = new Lazy<InstanceProperty>(() => new InstanceProperty(DATABASE_INFO, Type<string>.Info, "Name"), true);

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

            public IReadOnlyDictionary<string, object> Values(object instance) => (IReadOnlyDictionary<string, object>)_values.Value.GetValue(instance);
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

            new public bool Equals(object instance, object? value) => (bool)_equals.Value.Invoke(instance, value);
            private readonly Lazy<InstanceMethod> _equals = new Lazy<InstanceMethod>(() => new InstanceMethod(BOOKMARKS, Type<bool>.Info, "Equals", Type<object>.Info));

            public int GetHashCode(object instance) => (int)_getHashCode.Value.Invoke(instance);
            private readonly Lazy<InstanceMethod> _getHashCode = new Lazy<InstanceMethod>(() => new InstanceMethod(BOOKMARKS, Type<int>.Info, "GetHashCode"));
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

            public bool EqualsINode(object instance, object? value) => (bool)_equalsINode.Value.Invoke(instance, value);
            private readonly Lazy<InstanceMethod> _equalsINode = new Lazy<InstanceMethod>(() => new InstanceMethod(I_NODE, Type<bool>.Info, "Equals", I_NODE));

            new public bool Equals(object instance, object? value) => (bool)_equals.Value.Invoke(instance, value);
            private readonly Lazy<InstanceMethod> _equals = new Lazy<InstanceMethod>(() => new InstanceMethod(Type<object>.Info, Type<bool>.Info, "Equals", Type<object>.Info));

            public int GetHashCode(object instance) => (int)_getHashCode.Value.Invoke(instance);
            private readonly Lazy<InstanceMethod> _getHashCode = new Lazy<InstanceMethod>(() => new InstanceMethod(Type<object>.Info, Type<int>.Info, "GetHashCode"));
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

            public bool EqualsIRelationship(object instance, object? value) => (bool)_equals.Value.Invoke(instance, value);
            private readonly Lazy<InstanceMethod> _equals = new Lazy<InstanceMethod>(() => new InstanceMethod(I_RELATIONSHIP, Type<bool>.Info, "Equals", I_RELATIONSHIP));

            new public bool Equals(object instance, object? value) => (bool)_equalsIRelationship.Value.Invoke(instance, value);
            private readonly Lazy<InstanceMethod> _equalsIRelationship = new Lazy<InstanceMethod>(() => new InstanceMethod(Type<object>.Info, Type<bool>.Info, "Equals", Type<object>.Info));

            public int GetHashCode(object instance) => (int)_getHashCode.Value.Invoke(instance);
            private readonly Lazy<InstanceMethod> _getHashCode = new Lazy<InstanceMethod>(() => new InstanceMethod(Type<object>.Info, Type<int>.Info, "GetHashCode"));
        }
        internal sealed class ValueExtensionsInfo : DriverTypeInfo
        {
            public ValueExtensionsInfo(params string[] names) : base(names) { }

            public T As<T>(object value) => (T)_as.Value.ForTypes(Type<T>.Info, Type<object>.Info).Invoke(value);
            public object As(DriverTypeInfo type, object value) => _as.Value.ForTypes(type, Type<object>.Info).Invoke(value);

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
        internal sealed class ServerAddressInfo : DriverTypeInfo
        {
            public ServerAddressInfo(params string[] names) : base(names) { }

            public ServerAddress From(string host, int port) => new ServerAddress(_fromHostAndPort.Value.Invoke(host, port));
            private readonly Lazy<StaticMethod> _fromHostAndPort = new Lazy<StaticMethod>(() => new StaticMethod(SERVER_ADDRESS, SERVER_ADDRESS, "From", Type<string>.Info, Type<int>.Info));

            public ServerAddress From(Uri uri) => new ServerAddress(_fromUri.Value.Invoke(uri));
            private readonly Lazy<StaticMethod> _fromUri = new Lazy<StaticMethod>(() => new StaticMethod(SERVER_ADDRESS, SERVER_ADDRESS, "From", Type<Uri>.Info));

            public bool EqualsServerAddress(object instance, ServerAddress? other) => (bool)_equalsServerAddress.Value.Invoke(instance, other?._instance);
            private readonly Lazy<InstanceMethod> _equalsServerAddress = new Lazy<InstanceMethod>(() => new InstanceMethod(SERVER_ADDRESS, Type<bool>.Info, "Equals", SERVER_ADDRESS));

            new public bool Equals(object instance, object? value) => (bool)_equals.Value.Invoke(instance, value);
            private readonly Lazy<InstanceMethod> _equals = new Lazy<InstanceMethod>(() => new InstanceMethod(SERVER_ADDRESS, Type<bool>.Info, "Equals", Type<object>.Info));

            public int GetHashCode(object instance) => (int)_getHashCode.Value.Invoke(instance);
            private readonly Lazy<InstanceMethod> _getHashCode = new Lazy<InstanceMethod>(() => new InstanceMethod(SERVER_ADDRESS, Type<int>.Info, "GetHashCode"));
        }
        internal sealed class TrustManagerInfo : DriverTypeInfo
        {
            public TrustManagerInfo(params string[] names) : base(names) { }

            public TrustManager CreateInsecure(bool verifyHostname = false) => new TrustManager( _createInsecure.Value.Invoke(verifyHostname));
            private readonly Lazy<StaticMethod> _createInsecure = new Lazy<StaticMethod>(() => new StaticMethod(TRUST_MANAGER, TRUST_MANAGER, "CreateInsecure", Type<bool>.Info));

            public TrustManager CreateChainTrust(bool verifyHostname = true, X509RevocationMode revocationMode = X509RevocationMode.NoCheck, X509RevocationFlag revocationFlag = X509RevocationFlag.ExcludeRoot, bool useMachineContext = false) => new TrustManager(_createChainTrust.Value.Invoke(verifyHostname, revocationMode, revocationFlag, useMachineContext));
            private readonly Lazy<StaticMethod> _createChainTrust = new Lazy<StaticMethod>(() => new StaticMethod(TRUST_MANAGER, TRUST_MANAGER, "CreateChainTrust", Type<bool>.Info, Type<X509RevocationMode>.Info, Type<X509RevocationFlag>.Info, Type<bool>.Info));

            public TrustManager CreatePeerTrust(bool verifyHostname = true, bool useMachineContext = false) => new TrustManager(_createPeerTrust.Value.Invoke(verifyHostname, useMachineContext));
            private readonly Lazy<StaticMethod> _createPeerTrust = new Lazy<StaticMethod>(() => new StaticMethod(TRUST_MANAGER, TRUST_MANAGER, "CreatePeerTrust", Type<bool>.Info, Type<bool>.Info));

            public TrustManager CreateCertTrust(IEnumerable<X509Certificate2> trusted, bool verifyHostname = true) => new TrustManager(_createCertTrust.Value.Invoke(trusted, verifyHostname));
            private readonly Lazy<StaticMethod> _createCertTrust = new Lazy<StaticMethod>(() => new StaticMethod(TRUST_MANAGER, TRUST_MANAGER, "CreateCertTrust", Type<IEnumerable<X509Certificate2>>.Info, Type<bool>.Info));

            new public bool Equals(object instance, object? value) => (bool)_equals.Value.Invoke(instance, value);
            private readonly Lazy<InstanceMethod> _equals = new Lazy<InstanceMethod>(() => new InstanceMethod(TRUST_MANAGER, Type<bool>.Info, "Equals", Type<object>.Info));

            public int GetHashCode(object instance) => (int)_getHashCode.Value.Invoke(instance);
            private readonly Lazy<InstanceMethod> _getHashCode = new Lazy<InstanceMethod>(() => new InstanceMethod(TRUST_MANAGER, Type<int>.Info, "GetHashCode"));
        }
        internal sealed class IServerAddressResolverInfo : DriverTypeInfo
        {
            public IServerAddressResolverInfo(params string[] names) : base(names) { }

            public object ConvertToNeo4jIServerAddressResolver(ServerAddressResolver instance) => _get.Value.Invoke(instance);
            private readonly Lazy<StaticMethod> _get = new Lazy<StaticMethod>(() => new StaticMethod(SERVER_ADDRESS_RESOLVER_PROXY, SERVER_ADDRESS_RESOLVER_PROXY, "Get", Type<ServerAddressResolver>.Info), true);
        }
        internal sealed class ILoggerInfo : DriverTypeInfo
        {
            public ILoggerInfo(params string[] names) : base(names) { }

            public object ConvertToNeo4jILogger(ILogger instance) => _get.Value.Invoke(instance);
            private readonly Lazy<StaticMethod> _get = new Lazy<StaticMethod>(() => new StaticMethod(LOGGER_PROXY, LOGGER_PROXY, "Get", Type<ILogger>.Info), true);
        }

        #endregion

        #region TaskScheduler

        internal static void RunBlocking(Func<Task> work, string description) => TaskScheduler.RunBlocking(work, description);
        internal static TResult RunBlocking<TResult>(Func<Task<TResult>> work, string description) => TaskScheduler.RunBlocking(work, description);
        internal static CustomTaskScheduler TaskScheduler
        {
            get
            {
                if (_taskScheduler is null)
                {
                    lock (sync)
                    {
                        if (_taskScheduler is null)
                        {
                            CustomTaskQueueOptions main = new CustomTaskQueueOptions(10, 50);
                            CustomTaskQueueOptions sub = new CustomTaskQueueOptions(4, 20);
                            _taskScheduler = new CustomThreadSafeTaskScheduler(main, sub);
                        }
                    }
                }

                return _taskScheduler;
            }
        }
        private static CustomTaskScheduler? _taskScheduler = null;
        private static readonly object sync = new object();

        #endregion

        public void Dispose() => ((IDisposable)_instance).Dispose();
        public ValueTask DisposeAsync() => ((IAsyncDisposable)_instance).DisposeAsync();
    }
}
