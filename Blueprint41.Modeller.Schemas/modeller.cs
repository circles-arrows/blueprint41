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

        public void UpdateEntityGuid(entity entity, Guid newId)
        {
            Guid oldId = Guid.Parse(entity.guid);

            // TODO: Fix this to more robust real refactoring solution....
            // TODO: Why? because this can update conflicting guids when it shouldn't...
            GenericQuickHackToRefactorGuidsAllOverThePlace(newId, oldId);
        }
        public void UpdateRelationshipGuid(relationship relationship, Guid newId)
        {
            Guid oldId = Guid.Parse(relationship.guid);

            // TODO: Fix this to more robust real refactoring solution....
            // TODO: Why? because this can update conflicting guids when it shouldn't...
            GenericQuickHackToRefactorGuidsAllOverThePlace(newId, oldId);
        }
        public void UpdatePrimitivePropertyGuid(entity entity, primitive prop, Guid newId)
        {
            Guid oldId = Guid.Parse(prop.guid);

            // TODO: Fix this to more robust real refactoring solution....
            // TODO: Why? because this can update conflicting guids when it shouldn't...
            GenericQuickHackToRefactorGuidsAllOverThePlace(newId, oldId);
        }
        public void UpdateRecordGuid(entity entity, record prop, Guid newId)
        {
            Guid oldId = Guid.Parse(prop.guid);

            // TODO: Fix this to more robust real refactoring solution....
            // TODO: Why? because this can update conflicting guids when it shouldn't...
            GenericQuickHackToRefactorGuidsAllOverThePlace(newId, oldId);
        }

        private void GenericQuickHackToRefactorGuidsAllOverThePlace(Guid newId, Guid oldId)
        {
            foreach (XElement element in Untyped.Document.Descendants())
            {
                foreach (XAttribute attr in element.Attributes())
                {
                    if (attr.Value != null)
                    {
                        Guid actual;
                        if (Guid.TryParse(attr.Value, out actual))
                        {
                            if (actual == oldId)
                                attr.Value = newId.ToString();
                        }
                    }
                }
            }
        }
    }
}
