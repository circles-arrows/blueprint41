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
        #region Entities
        public void UpdateEntityMappingGuid(entity entity, Guid? newId)
        {
            entity.mappingGuid = newId?.ToString();
            UpdateGuidReferences(entity.guid, newId?.ToString());
            entity.guid = newId?.ToString();
        }

        public void RemoveMapping<T>(T item)
        {
            
        }
        public void RemoveEntityMapping(entity entity)
        {
            entity.mappingGuid = null;
        }
        #endregion
        #region Primitives
        public void UpdatePrimitiveMappingGuid(primitive primitive, Guid? newId)
        {
            primitive.mappingGuid = newId?.ToString();
            primitive.guid = newId?.ToString();
        }
        public void RemoveMapping(primitive primitive)
        {
            primitive.mappingGuid = null;
        }
        #endregion
        #region Relationships
        public void RemoveRelationshipMapping(relationship relationship)
        {
            relationship.mappingGuid = null;
        }
        public void UpdateRelationshipMappingGuid(relationship relationship, Guid? newId)
        {
            relationship.mappingGuid = newId?.ToString();
            relationship.guid = newId?.ToString();
        }
        #endregion
        #region Records
       public void RemoveRecordMapping(record record)
        {
            record.mappingGuid = null;
        }
        public void UpdateRecordMappingGuid(record record, Guid? newId)
        {
            record.mappingGuid = newId?.ToString();
            record.guid = newId?.ToString();
        }
        #endregion
        private void UpdateGuidReferences(string oldGuid, string newGuid)
        {
            foreach (var entity in entities.entity.Where(x => x.inherits == oldGuid))
            {
                entity.inherits = newGuid;
            }
            foreach (var relationship in relationships.relationship)
            {
                nodeReference source = relationship.source;
                nodeReference target = relationship.target;
                if (source.referenceGuid == oldGuid)
                    source.referenceGuid = newGuid;
                if (target.referenceGuid == oldGuid)
                    target.referenceGuid = newGuid;
            }
            foreach (var subModel in submodels.submodel)
            {
                foreach (var node in subModel.node.Where(x => x.entityGuid == oldGuid))
                {
                    node.entityGuid = newGuid;
                }
            }
        }

        public void ClearStaticData()
        {
            foreach (var item in entities.entity)
            {
                if (!item.isStaticData)
                {
                    item.isStaticData = false;
                    item.staticData = null;
                }
            }
        }

    }
}
