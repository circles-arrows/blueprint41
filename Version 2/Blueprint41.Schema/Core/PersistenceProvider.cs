using System;
using System.Collections.Generic;
using System.Linq;

using Blueprint41.Providers;
using Blueprint41.Refactoring.Schema;

namespace Blueprint41.Core
{
    public abstract class PersistenceProvider
    {
        // Precision (roughly) 10.8
        internal const decimal DECIMAL_FACTOR = 100000000m;

#pragma warning disable IDE0200
        protected PersistenceProvider()
        {
            //WARNING: do not refactor warning IDE0200, the wrong translator will be returned
            supportedTypeMappings = new Lazy<List<TypeMapping>>(delegate ()
            {
                return Translator.FilterSupportedTypeMappings(GetSupportedTypeMappings()).ToList();
            },
                    true
                );

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

        public abstract Session NewSession(bool readWriteMode);
        public abstract Transaction NewTransaction(bool readWriteMode);

        public bool IsNeo4j { get; private set; } = false;
        public bool IsMemgraph { get; private set; } = false;
        public abstract FeatureSupport NodePropertyFeatures { get; }
        public abstract FeatureSupport RelationshipPropertyFeatures { get; }

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

        internal DatastoreModel DatastoreModel
        {
            get => _datastoreModel ?? throw new NotSupportedException("Supply this PersistenceProvider to the constructor of a DatastoreModel first.");
            set => _datastoreModel = value;
        }
        private DatastoreModel? _datastoreModel = null;

        internal NodePersistenceProvider NodePersistenceProvider
        {
            get
            {
                if (_nodePersistenceProvider is null)
                    _nodePersistenceProvider = Translator.GetNodePersistenceProvider();

                return _nodePersistenceProvider;
            }
        }
        private NodePersistenceProvider? _nodePersistenceProvider = null;

        internal RelationshipPersistenceProvider RelationshipPersistenceProvider
        {
            get
            {
                if (_relationshipPersistenceProvider is null)
                    _relationshipPersistenceProvider = Translator.GetRelationshipPersistenceProvider();

                return _relationshipPersistenceProvider;
            }
        }
        private RelationshipPersistenceProvider? _relationshipPersistenceProvider = null;
        
        internal RefactorTemplates Templates => Translator.GetTemplates();
        internal SchemaInfo SchemaInfo => Translator.GetSchemaInfo();
        internal abstract QueryTranslator Translator { get; }

        #endregion Factory



        public string Version { get; internal set; } = "0.0.0";
        public int Major { get; internal set; } = 0;
        public int Minor { get; internal set; } = 0;
        public int? Revision { get; internal set; } = null;
        public bool IsAura { get; set; } = false;
        public bool IsEnterpriseEdition { get; set; } = false;

        //public override FeatureSupport NodePropertyFeatures => _nodePropertyFeatures.Value;
        //private readonly Lazy<FeatureSupport> _nodePropertyFeatures;

        //public override FeatureSupport RelationshipPropertyFeatures => _relationshipPropertyFeatures.Value;
        //private readonly Lazy<FeatureSupport> _relationshipPropertyFeatures;

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

        public static PersistenceProvider VoidProvider => throw new NotImplementedException();
    }
}