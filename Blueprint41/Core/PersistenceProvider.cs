using Blueprint41.Neo4j.Persistence;
using Blueprint41.Neo4j.Refactoring;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.Core
{
    public abstract class PersistenceProvider
    {
        protected PersistenceProvider()
        {
            convertToStoredType = new Lazy<Dictionary<Type, Conversion>>(
            delegate ()
            {
                return SupportedTypeMappings.ToDictionary(item => item.ReturnType, item => Conversion.GetConverter(item.ReturnType, item.PersistedType));

            }, true);

            convertFromStoredType = new Lazy<Dictionary<Type, Conversion>>(
            delegate ()
            {
                return SupportedTypeMappings.ToDictionary(item => item.ReturnType, item => Conversion.GetConverter(item.PersistedType, item.ReturnType));

            }, true);
        }
        internal abstract NodePersistenceProvider GetNodePersistenceProvider();
        internal abstract RelationshipPersistenceProvider GetRelationshipPersistenceProvider();

        public abstract Transaction NewTransaction(bool withTransaction);

        public abstract IEnumerable<TypeMapping> SupportedTypeMappings { get; }

        private Lazy<Dictionary<Type, Conversion>> convertToStoredType;
        internal Dictionary<Type, Conversion> ConvertToStoredTypeCache { get { return convertToStoredType.Value; } }

        private Lazy<Dictionary<Type, Conversion>> convertFromStoredType;
        internal Dictionary<Type, Conversion> ConvertFromStoredTypeCache { get { return convertFromStoredType.Value; } }

        public static PersistenceProvider CurrentPersistenceProvider { get; private set; } = new Neo4JPersistenceProvider(null, null, null);

        private static GraphFeatures targetFeatures;
        public static GraphFeatures TargetFeatures
        {
            get
            {
                if ((object)targetFeatures == null)
                    throw new InvalidOperationException("Should call Initialize method.");

                return targetFeatures;
            }
        }

        public static bool IsNeo4j
        {
            get
            {
                if (CurrentPersistenceProvider == null)
                    return false;

                return CurrentPersistenceProvider.GetType().IsSubclassOfOrSelf(typeof(Neo4j.Persistence.Neo4JPersistenceProvider));
            }
        }

        public static bool IsGremlin
        {
            get
            {
                if (CurrentPersistenceProvider == null)
                    return false;

                return CurrentPersistenceProvider.GetType().IsSubclassOfOrSelf(typeof(Gremlin.GremlinPersistenceProvider));
            }
        }

        public static void Initialize(Type type, params object[] connectionArgs)
        {
            if (type.IsSubclassOf(typeof(DatastoreModel)) == false)
                throw new NotSupportedException($"{type} should be a subclass of DatastoreModel");

            DatastoreModel model = (DatastoreModel)Activator.CreateInstance(type);
            targetFeatures = model.TargetFeatures;
            CurrentPersistenceProvider = (PersistenceProvider)Activator.CreateInstance(targetFeatures.PersistenceProviderType, connectionArgs);
        }
    }
}
