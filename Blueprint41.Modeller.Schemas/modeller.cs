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

    #region PrepareForComparison

    public partial class modeller
    {

        public void PrepareForComparison()
        {
            foreach (entity entity in entities.entity)
            {
                if (!string.IsNullOrEmpty(entity.inherits))
                    entity.Inherits = FindEntity(entity.inherits);

                entity.Key = entity.primitive.FirstOrDefault(item => item.isKey);

                if (entity.staticData != null)
                {
                    foreach (record record in entity.staticData.records.record)
                    {
                        record.Entity = entity;

                        foreach (property property in record.property)
                            property.Primitive = record.Entity.FindPrimitive(property.propertyGuid);

                        record.Key = record.property.Where(item => item.Primitive != null).FirstOrDefault(item => item.Primitive.isKey);
                    }
                }
            }
        }

        public partial class entitiesLocalType { }
        public partial class relationshipsLocalType { }
        public partial class submodelsLocalType { }
        public partial class functionalIdsLocalType { }


    }

    public partial class nodeReference
    {
    }

    public partial class relationship
    {

    }
    public partial class primitive
    {

    }
    public partial class staticData
    {

    }
    public partial class records
    {

    }

    public partial class record
    {
        public entity Entity { get; internal set; }
        public property Key { get; internal set; }
    }

    public partial class property
    {
        public primitive Primitive { get; internal set; }
    }
    public partial class entity
    {
        private primitive key = null;
        public primitive Key
        {
            get
            {
                if (key == null)
                    key = Inherits?.Key;

                return key;
            }
            internal set
            {
                key = value;
            }
        }
        public entity Inherits { get; internal set; }

        public primitive FindPrimitive(string guid)
        {
            if (string.IsNullOrWhiteSpace(guid))
                return null;

            if (primitivesByGuid == null)
                primitivesByGuid = primitive.ToDictionary(key => new Guid(key.guid), value => value);

            primitive found;
            primitivesByGuid.TryGetValue(new Guid(guid), out found);

            if (found == null && Inherits != null)
                found = Inherits.FindPrimitive(guid);

            return found;
        }

        private Dictionary<Guid, primitive> primitivesByGuid = null;
    }

    public partial class submodel
    {
        public partial class nodeLocalType
        {

        }
    }
    public partial class functionalIds
    {

    }
    public partial class functionalId
    {

    }


    #endregion
}
