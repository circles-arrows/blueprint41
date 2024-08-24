using System;
using System.Collections.Generic;
using System.Linq;

using Blueprint41.Core;
using Blueprint41.Refactoring.Schema;
using Blueprint41.Persistence.Translator;
using System.Diagnostics.CodeAnalysis;
using Blueprint41.Refactoring;

namespace Blueprint41.Persistence.Provider
{
    public abstract class PersistenceProvider
    {
        // Precision (roughly) 10.8
        internal const decimal DECIMAL_FACTOR = 100000000m;

        public TransactionLogger? TransactionLogger { get; private set; }
        public string? Uri { get; private set; }
        public string? Username { get; private set; }
        public string? Password { get; private set; }
        public string? Database { get; private set; }
        public AdvancedConfig? AdvancedConfig { get; private set; }

#pragma warning disable IDE0200
        private protected PersistenceProvider(DatastoreModel model, string? uri, string? username, string? password, string? database, AdvancedConfig? advancedConfig = null)
        {
            DatastoreModel = model;

            Uri = uri;
            Username = username;
            Password = password;
            Database = database;
            AdvancedConfig = advancedConfig;
            TransactionLogger = advancedConfig?.GetLogger();

            _nodePropertyFeatures = new Lazy<FeatureSupport>(() => new FeatureSupport()
            {
                Index = true,
                Exists = IsEnterpriseEdition || IsMemgraph,
                Unique = true,
                Key = !IsMemgraph && IsEnterpriseEdition && VersionGreaterOrEqual(5, 7),
                Type = !IsMemgraph && IsEnterpriseEdition && VersionGreaterOrEqual(5, 9),
            });
            _relationshipPropertyFeatures = new Lazy<FeatureSupport>(() => new FeatureSupport()
            {
                Index = !IsMemgraph && VersionGreaterOrEqual(4, 3),
                Exists = !IsMemgraph && IsEnterpriseEdition,
                Unique = !IsMemgraph && VersionGreaterOrEqual(5, 7),
                Key = !IsMemgraph && IsEnterpriseEdition && VersionGreaterOrEqual(5, 7),
                Type = !IsMemgraph && IsEnterpriseEdition && VersionGreaterOrEqual(5, 9),
            });

            //WARNING: do not refactor warning IDE0200, the wrong translator will be returned
            supportedTypeMappings = new Lazy<List<TypeMapping>>(delegate ()
            {
                return Translator.FilterSupportedTypeMappings(GetSupportedTypeMappings()).ToList();
            }, true);

            convertToStoredType = new Lazy<Dictionary<Type, Conversion?>>(
                    delegate ()
                    {
                        return SupportedTypeMappings.ToDictionary(item => item.ReturnType, item => Conversion.GetConverter(item.ReturnType, item.PersistedType));
                    },
                    true
                );

            convertFromStoredType = new Lazy<Dictionary<Type, Conversion?>>(
                    delegate ()
                    {
                        return SupportedTypeMappings.ToDictionary(item => item.ReturnType, item => Conversion.GetConverter(item.PersistedType, item.ReturnType));
                    },
                    true
                );
        }
#pragma warning restore IDE0200

        internal static PersistenceProvider Get(DatastoreModel model, string? uri, string? username, string? password, string? database, AdvancedConfig? advancedConfig = null)
        {
            switch (model.DatastoreTechnology)
            {
                case GDMS.Memgraph:
                    return new MemgraphPersistenceProvider(model, uri, username, password, database, advancedConfig);
                case GDMS.Neo4j:
                    return new Neo4jPersistenceProvider(model, uri, username, password, database, advancedConfig);
                default:
                    throw new NotImplementedException($"Support for {model.DatastoreTechnology} is available.");
            }
        }

        public string DBMSName { get; internal set; } = string.Empty;
        public string Version { get; internal set; } = "0.0.0";
        public int Major { get; internal set; } = 0;
        public int Minor { get; internal set; } = 0;
        public int? Revision { get; internal set; } = null;
        public bool IsAura { get; set; } = false;
        public bool IsEnterpriseEdition { get; set; } = false;

        public virtual FeatureSupport NodePropertyFeatures => _nodePropertyFeatures.Value;
        private readonly Lazy<FeatureSupport> _nodePropertyFeatures;

        public virtual FeatureSupport RelationshipPropertyFeatures => _relationshipPropertyFeatures.Value;
        private readonly Lazy<FeatureSupport> _relationshipPropertyFeatures;

        public bool VersionGreaterOrEqual(int major, int minor = 0, int revision = 0)
        {
            if (Major > major) return true;
            if (Major == major && Minor > minor) return true;
            if (Major == major && Minor == minor && Revision >= revision) return true;

            return false;
        }

        public bool HasFunction(string function)
        {
            return functions.Contains(function);
        }
        protected HashSet<string> functions = new HashSet<string>();

        public bool HasProcedure(string procedure)
        {
            return procedures.Contains(procedure);
        }
        protected HashSet<string> procedures = new HashSet<string>();

        private void FetchDatabaseInfo()
        {
            if (IsVoidProvider)
                return;

            using (NewTransaction(ReadWriteMode.ReadOnly))
            {
                var components = Transaction.RunningTransaction.Run("call dbms.components() yield name, versions, edition unwind versions as version return name, version, edition").First();

                DBMSName = components["name"].As<string>();
                Version = components["version"].As<string>();
                IsEnterpriseEdition = components["edition"].As<string>().ToLowerInvariant() == "enterprise";

                ParseVersion(Version);
                LoadDbmsFunctions();
                LoadDbmsProcedures();
            }
        }

        private void ParseVersion(string version)
        {
            string[] parts = version.Split(new[] { '.' });
            Major = int.Parse(parts[0]);
            Minor = int.Parse(parts[1].Replace("-aura", ""));
            IsAura = parts[1].Contains("-aura");

            if (parts.Length > 2) Revision = int.Parse(parts[2]);
        }

        private void LoadDbmsFunctions()
        {
            functions = new HashSet<string>(Transaction.RunningTransaction.Run(GetFunctions(DBMSName, Major))
                .Select(item => item["name"].As<string>()));
        }
        private void LoadDbmsProcedures()
        {
            procedures = new HashSet<string>(Transaction.RunningTransaction.Run(GetProcedures(DBMSName, Major))
                .Select(item => item["name"].As<string>()));
        }

        private QueryTranslator DetermineTranslator()
        {
            if (DBMSName.IndexOf("memgraph", StringComparison.InvariantCultureIgnoreCase) >= 0)
                return new MemgraphQueryTranslatorV1(DatastoreModel);

            return Major switch
            {
                4 => new Neo4jQueryTranslatorV4(DatastoreModel),
                5 => new Neo4jQueryTranslatorV5(DatastoreModel),
                _ => throw new NotSupportedException($"Neo4j v{Version} is not supported by this version of Blueprint41, please upgrade to a later version.")
            };
        }
        static string GetFunctions(string name, int version)
        {
            if (name.IndexOf("neo4j", StringComparison.InvariantCultureIgnoreCase) >= 0)
            {
                return version switch
                {
                    < 5 => "call dbms.functions() yield name return name",
                    >= 5 => "show functions yield name return name",
                };
            }
            else if (name.IndexOf("memgraph", StringComparison.InvariantCultureIgnoreCase) >= 0)
            {
                return "call mg.functions() yield name return name";
            }
            else
            {
                return string.Empty;
            }
        }
        static string GetProcedures(string name, int version)
        {
            if (name.IndexOf("neo4j", StringComparison.InvariantCultureIgnoreCase) >= 0)
            {
                return version switch
                {
                    < 5 => "call dbms.procedures() yield name as name",
                    >= 5 => "show procedures yield name return name",
                };
            }
            else if (name.IndexOf("memgraph", StringComparison.InvariantCultureIgnoreCase) >= 0)
            {
                return "call mg.procedures() yield name return name";
            }
            else
            {
                return string.Empty;
            }
        }

        public abstract Transaction NewTransaction(ReadWriteMode mode, OptimizeFor optimize = OptimizeFor.PartialSubGraphAccess);
        public abstract Session NewSession(ReadWriteMode mode, OptimizeFor optimize = OptimizeFor.PartialSubGraphAccess);

        public bool IsNeo4j => (DatastoreModel.DatastoreTechnology == GDMS.Neo4j);
        public bool IsMemgraph => (DatastoreModel.DatastoreTechnology == GDMS.Memgraph);
        internal bool IsVoidProvider => Uri is null;

        public virtual string ToToken(Bookmark consistency) => string.Empty;

        public virtual Bookmark FromToken(string consistencyToken) => Bookmark.NullBookmark;

        public IReadOnlyList<TypeMapping> SupportedTypeMappings => supportedTypeMappings.Value;
        private readonly Lazy<List<TypeMapping>> supportedTypeMappings;
        protected internal abstract List<TypeMapping> GetSupportedTypeMappings();

        internal Dictionary<Type, Conversion?> ConvertToStoredTypeCache
        { get { return convertToStoredType.Value; } }
        private readonly Lazy<Dictionary<Type, Conversion?>> convertToStoredType;

        internal Dictionary<Type, Conversion?> ConvertFromStoredTypeCache
        { get { return convertFromStoredType.Value; } }
        private readonly Lazy<Dictionary<Type, Conversion?>> convertFromStoredType;

        #region Conversion

        public object? ConvertToStoredType<TValue>(TValue value)
        {
            return ConvertToStoredTypeCacheVia<TValue>.Convert(this, value);
        }

        public object? ConvertToStoredType(Type returnType, object? value)
        {
            if (returnType is null)
                return value;

            Conversion? converter;
            if (!ConvertToStoredTypeCache.TryGetValue(returnType, out converter))
                return value;

            if (converter is null)
                return value;

            return converter.Convert(value);
        }

        public TReturnType ConvertFromStoredType<TReturnType>(object? value)
        {
            return (TReturnType)ConvertFromStoredTypeCacheVia<TReturnType>.Convert(this, value)!;
        }

        public object? ConvertFromStoredType(Type returnType, object? value)
        {
            if (returnType is null)
                return value;

            Conversion? converter;
            if (!ConvertFromStoredTypeCache.TryGetValue(returnType, out converter))
                return value;

            if (converter is null)
                return value;

            return converter.Convert(value);
        }

        public Conversion? GetConverterToStoredType(Type returnType)
        {
            if (returnType is null)
                return null;

            Conversion? converter;
            ConvertToStoredTypeCache.TryGetValue(returnType, out converter);

            return converter;
        }

        public Conversion? GetConverterFromStoredType(Type returnType)
        {
            if (returnType is null)
                return null;

            Conversion? converter;
            ConvertFromStoredTypeCache.TryGetValue(returnType, out converter);

            return converter;
        }

        public Type? GetStoredType<TReturnType>()
        {
            ConvertFromStoredTypeCacheVia<TReturnType>.Initialize(this);
            return ConvertFromStoredTypeCacheVia<TReturnType>.Converter?.FromType ?? typeof(TReturnType);
        }

        public Type? GetStoredType(Type returnType)
        {
            Conversion? converter;
            if (!ConvertFromStoredTypeCache.TryGetValue(returnType, out converter))
                return null;

            return converter?.FromType ?? returnType;
        }

        private class ConvertToStoredTypeCacheVia<TReturnType>
        {
            public static Conversion? Converter = null;
            public static bool IsInitialized = false;

            public static object? Convert(PersistenceProvider factory, object? value)
            {
                Initialize(factory);

                if (Converter is null)
                    return value;

                return Converter.Convert(value);
            }

            internal static void Initialize(PersistenceProvider factory)
            {
                if (!IsInitialized)
                {
                    lock (typeof(ConvertFromStoredTypeCacheVia<TReturnType>))
                    {
                        if (!IsInitialized)
                        {
                            factory.ConvertToStoredTypeCache.TryGetValue(typeof(TReturnType), out Converter);
                            IsInitialized = true;
                        }
                    }
                }
            }
        }

        private class ConvertFromStoredTypeCacheVia<TReturnType>
        {
            public static Conversion? Converter = null;
            public static bool IsInitialized = false;

            public static object? Convert(PersistenceProvider factory, object? value)
            {
                Initialize(factory);

                if (Converter is null)
                    return value;

                return Converter.Convert(value);
            }

            internal static void Initialize(PersistenceProvider factory)
            {
                if (!IsInitialized)
                {
                    lock (typeof(ConvertFromStoredTypeCacheVia<TReturnType>))
                    {
                        if (!IsInitialized)
                        {
                            factory.ConvertFromStoredTypeCache.TryGetValue(typeof(TReturnType), out Converter);
                            IsInitialized = true;
                        }
                    }
                }
            }
        }

        #endregion Conversion

        #region Factory

        internal DatastoreModel DatastoreModel { get; private set; }

        internal virtual NodePersistenceProvider NodePersistenceProvider => GetOrInit(ref _nodePersistenceProvider, delegate ()
        {
            return new NodePersistenceProvider(DatastoreModel);
        });
        private protected NodePersistenceProvider? _nodePersistenceProvider = null;

        internal virtual RelationshipPersistenceProvider RelationshipPersistenceProvider => GetOrInit(ref _relationshipPersistenceProvider, delegate ()
        {
            return new RelationshipPersistenceProvider(DatastoreModel);
        });
        private protected RelationshipPersistenceProvider? _relationshipPersistenceProvider = null;

        internal virtual RefactorTemplates Templates => GetOrInit(ref _templates, delegate ()
        {
            if (IsNeo4j)
            {
                if (Major >= 5 || IsVoidProvider)
                    return new RefactorTemplates_Neo4jV5(DatastoreModel);
                else if (Major == 4)
                    return new RefactorTemplates_Neo4jV4(DatastoreModel);
            }
            else if (IsMemgraph)
            {
                return new RefactorTemplates_MemgraphV1(DatastoreModel);
            }
            throw new NotSupportedException();
        });
        private protected RefactorTemplates? _templates = null;

        internal virtual SchemaInfo SchemaInfo => GetOrInit(ref _schemaInfo, delegate ()
        {
            if (IsNeo4j)
            {
                if (Major >= 5 || IsVoidProvider)
                    return new SchemaInfo_Neo4jV5(DatastoreModel);
                else if (Major == 4)
                    return new SchemaInfo_Neo4jV4(DatastoreModel);
            }
            else if (IsMemgraph)
            {
                return new SchemaInfo_MemgraphV1(DatastoreModel);
            }
            throw new NotSupportedException();
        });
        private protected SchemaInfo? _schemaInfo = null;

        internal virtual QueryTranslator Translator => GetOrInit(ref _translator, delegate ()
        {
            if (IsNeo4j)
            {
                if (Major >= 5 || IsVoidProvider)
                    return new Neo4jQueryTranslatorV5(DatastoreModel);
                else if (Major == 4)
                    return new Neo4jQueryTranslatorV4(DatastoreModel);
            }
            else if (IsMemgraph)
            {
                return new MemgraphQueryTranslatorV1(DatastoreModel);
            }
            throw new NotSupportedException();
        });
        private protected QueryTranslator? _translator = null;

        private protected T GetOrInit<T>([NotNull] ref T? value, Func<T> initializer)
            where T : class
        {
            if (value is not null)
                return value;

            lock (_syncObject)
            {
                if (value is null)
                    value = initializer.Invoke();
            }
            return value;
        }
        private static readonly object _syncObject = new object();

        #endregion Factory
    }
}