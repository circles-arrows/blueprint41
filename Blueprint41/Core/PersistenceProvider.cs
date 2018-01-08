using Blueprint41.Neo4j.Persistence;
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

        internal abstract Transaction NewTransaction(bool withTransaction);

        public abstract IEnumerable<TypeMapping> SupportedTypeMappings { get; }

        private Lazy<Dictionary<Type, Conversion>> convertToStoredType;
        internal Dictionary<Type, Conversion> ConvertToStoredTypeCache { get { return convertToStoredType.Value; } }

        private Lazy<Dictionary<Type, Conversion>> convertFromStoredType;
        internal Dictionary<Type, Conversion> ConvertFromStoredTypeCache { get { return convertFromStoredType.Value; } }


        public static PersistenceProvider CurrentPersistenceProvider { get; set; } = new Neo4JPersistenceProvider(null, null, null);

        public static bool IsNeo4j
        {
            get
            {
                if (CurrentPersistenceProvider == null)
                    return false;
 
                return (CurrentPersistenceProvider.GetType() == typeof(Neo4j.Persistence.Neo4JPersistenceProvider));
            }
        }
    }
}
