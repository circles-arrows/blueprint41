﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Blueprint41.Neo4j.Model;
using Blueprint41.Neo4j.Persistence.Void;
using Blueprint41.Neo4j.Schema;

namespace Blueprint41.Core
{
    public abstract class PersistenceProvider
    {
        protected PersistenceProvider()
        {
            convertToStoredType = new Lazy<Dictionary<Type, Conversion?>>(
            delegate ()
            {
                return SupportedTypeMappings.ToDictionary(item => item.ReturnType, item => Conversion.GetConverter(item.ReturnType, item.PersistedType));

            }, true);

            convertFromStoredType = new Lazy<Dictionary<Type, Conversion?>>(
            delegate ()
            {
                return SupportedTypeMappings.ToDictionary(item => item.ReturnType, item => Conversion.GetConverter(item.PersistedType, item.ReturnType));

            }, true);

            _nodePersistenceProvider = new Lazy<NodePersistenceProvider>(() => Translator.GetNodePersistenceProvider());
            _relationshipPersistenceProvider = new Lazy<RelationshipPersistenceProvider>(() => Translator.GetRelationshipPersistenceProvider());
            _templates = new Lazy<RefactorTemplates>(() => Translator.GetTemplates());
        }

        internal NodePersistenceProvider NodePersistenceProvider => _nodePersistenceProvider.Value;
        private readonly Lazy<NodePersistenceProvider> _nodePersistenceProvider;

        internal RelationshipPersistenceProvider RelationshipPersistenceProvider => _relationshipPersistenceProvider.Value;
        private readonly Lazy<RelationshipPersistenceProvider> _relationshipPersistenceProvider;

        public abstract Transaction NewTransaction(bool withTransaction);
        public virtual string ToToken(Bookmark consistency) => string.Empty;
        public virtual Bookmark FromToken(string consistencyToken) => Bookmark.NullBookmark;

        public abstract List<TypeMapping> SupportedTypeMappings { get; }

        private Lazy<Dictionary<Type, Conversion?>> convertToStoredType;
        internal Dictionary<Type, Conversion?> ConvertToStoredTypeCache { get { return convertToStoredType.Value; } }

        private Lazy<Dictionary<Type, Conversion?>> convertFromStoredType;
        internal Dictionary<Type, Conversion?> ConvertFromStoredTypeCache { get { return convertFromStoredType.Value; } }

        public static PersistenceProvider CurrentPersistenceProvider { get; set; } = new Neo4jPersistenceProvider(null, null, null);
        public static bool IsConfigured => ((CurrentPersistenceProvider as Neo4jPersistenceProvider)?.Uri is not null); 

        public static bool IsNeo4j
        {
            get
            {
                if (CurrentPersistenceProvider is null)
                    return false;

                return CurrentPersistenceProvider.GetType().IsSubclassOfOrSelf(typeof(Neo4jPersistenceProvider));
            }
        }

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

        #endregion

        #region Factory: Refactoring Templates

        internal RefactorTemplates Templates => _templates.Value;
        private readonly Lazy<RefactorTemplates> _templates;

        #endregion

        #region Factory: Query Translator

        internal abstract QueryTranslator Translator { get; }
        
        #endregion

        #region Factory: Schema Info

        internal SchemaInfo GetSchemaInfo(DatastoreModel datastoreModel) => Translator.GetSchemaInfo(datastoreModel);

        #endregion
    }
}
