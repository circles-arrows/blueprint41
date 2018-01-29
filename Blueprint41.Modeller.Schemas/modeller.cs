using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Blueprint41.Modeller.Schemas
{
    public partial class modeller
    {
        public entity FindEntity(string guid)
        {
            if (string.IsNullOrWhiteSpace(guid))
                return null;

            if (entitiesByGuid == null)
                entitiesByGuid = entities.entity.ToDictionary(key => new Guid(key.guid), value => value);

            entity found;
            entitiesByGuid.TryGetValue(new Guid(guid), out found);
            return found;
        }

        private Dictionary<Guid, entity> entitiesByGuid = null;

        public void UpdateEntityGuid(entity entity, Guid? newId)
        {
            entity.mappingGuid = newId?.ToString();
        }
        public void UpdateRelationshipGuid(relationship relationship, Guid? newId)
        {
            relationship.mappingGuid = newId?.ToString();
        }
        public void UpdatePrimitivePropertyGuid(entity entity, primitive prop, Guid? newId)
        {
            prop.mappingGuid = newId?.ToString();
        }
        public void UpdateRecordGuid(entity entity, record prop, Guid? newId)
        {
            prop.mappingGuid = newId?.ToString();
        }
    }
}
