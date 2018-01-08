using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.Core
{
    internal class ClearRelationshipsAction : RelationshipAction
    {
        public DirectionEnum Direction { get; private set; }
        internal ClearRelationshipsAction(RelationshipPersistenceProvider persistenceProvider, Relationship relationship, OGM inItem, OGM outItem)
            : base(persistenceProvider, relationship, inItem, outItem)
        {
            Direction = inItem != null ? DirectionEnum.In : DirectionEnum.Out;
        }

        protected override bool ActsOnSpecificParent() { return false; }

        protected override void InDatastoreLogic(Relationship Relationship)
        {
            PersistenceProvider.RemoveAll(Relationship, Direction, InItem ?? OutItem, null, false);
        }

        protected override void InMemoryLogic(EntityCollectionBase target)
        {
            target.ForEach(delegate (int index, CollectionItem item)
            {
                if (target.Direction == DirectionEnum.Out)
                {
                    if ((OutItem != null && item.Parent.Equals(OutItem)) || (InItem != null && item.Item.Equals(InItem)))
                        target.RemoveAt(index);
                }
                else if (target.Direction == DirectionEnum.In)
                {
                    if ((InItem != null && item.Parent.Equals(InItem)) || (OutItem != null && item.Item.Equals(OutItem)))
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
