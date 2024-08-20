using System;
using System.Collections.Generic;

using Blueprint41.Core;

namespace Blueprint41.Providers
{
    internal class ClearRelationshipsAction : RelationshipAction
    {
        internal ClearRelationshipsAction(RelationshipPersistenceProvider persistenceProvider, OGM item)
            : base(persistenceProvider, null, item, item) { }

        protected override void InDatastoreLogic(Relationship relationship)
        {
            Entity entity = InItem!.GetEntity();
            if (entity.IsSelfOrSubclassOf(relationship.InEntity))
            {
                if (relationship.OutProperty is null || relationship.OutProperty.Nullable || relationship.OutProperty.PropertyType == PropertyType.Collection)
                    PersistenceProvider.Remove(relationship, InItem, (OGM?)null, null, false);
            }
            if (entity.IsSelfOrSubclassOf(relationship.OutEntity))
            {
                if (relationship.InProperty is null || relationship.InProperty.Nullable || relationship.InProperty.PropertyType == PropertyType.Collection)
                    PersistenceProvider.Remove(relationship, (OGM?)null, OutItem, null, false);
            }
        }

        protected override void InMemoryLogic(EntityCollectionBase target)
        {
            target.ForEach(delegate (int index, CollectionItem item)
            {
                if (target.Direction == DirectionEnum.Out)
                {
                    if (OutItem is not null && item.Parent.Equals(OutItem) || InItem is not null && item.Item.Equals(InItem))
                        target.RemoveAt(index);
                }
                else if (target.Direction == DirectionEnum.In)
                {
                    if (InItem is not null && item.Parent.Equals(InItem) || OutItem is not null && item.Item.Equals(OutItem))
                        target.RemoveAt(index);
                }
                else
                {
                    throw new NotSupportedException("Please contact developer.");
                }
            });
        }
    }
}

/*

InMemoryLogic  is still broken, it now deletes both ParentOrg & ChildOrgs if you issue a Clear on either on.
               it should clear 1 and remove related items only for the other side. Not clear the complete other side.
               Example: if you clear child orgs property, then the parent property pointing back from these children should be cleared also.
                        however, now the parent property of the parent node itself is also cleared!!!

Constructor needs to take both in and out as argument, one of the 2 should always be NULL

p.ParentOrg (p = parent, p = out)
    |
    |
    O Has_Parent
    |
    ^
c.ChildOrgs  (c = parent, c = in)

Check that caller of ClearRelationshipsActions feed in the right InItem and OutItem



Fix InDatastoreLogic!!!! this is not working well at all now!!!

*/
