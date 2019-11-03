using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.Core
{
    internal class TimeDependentClearRelationshipsAction : TimeDependentRelationshipAction
    {
        public DirectionEnum Direction { get; private set; }
        internal TimeDependentClearRelationshipsAction(RelationshipPersistenceProvider persistenceProvider, Relationship relationship, OGM? inItem, OGM? outItem, DateTime? moment)
            : base(persistenceProvider, relationship, inItem!, outItem!, moment)
        {
            Direction = inItem != null ? DirectionEnum.In : DirectionEnum.Out;
        }

        new public OGM? InItem => base.InItem; 
        new public OGM? OutItem => base.OutItem;

        protected override bool ActsOnSpecificParent() { return false; }

        protected override void InDatastoreLogic(Relationship Relationship)
        {
            PersistenceProvider.RemoveAll(Relationship, Direction, (InItem ?? OutItem)!, Moment, true);
        }

        protected override void InMemoryLogic(EntityCollectionBase target)
        {
            target.ForEach(delegate (int index, CollectionItem item)
            {
                bool processItem = false;
                if (target.Direction == DirectionEnum.Out)
                {
                    if ((OutItem != null && item.Parent.Equals(OutItem)) || (InItem != null && item.Item.Equals(InItem)))
                        processItem = true;
                }
                else if(target.Direction == DirectionEnum.In)
                {
                    if ((InItem != null && item.Parent.Equals(InItem)) || (OutItem != null && item.Item.Equals(OutItem)))
                        processItem = true;
                }
                else
                {
                    throw new NotSupportedException("Please contact developer.");
                }
                if (processItem)
                {
                    if (item.IsAfter(Moment))
                    {
                        target.RemoveAt(index);
                    }
                    else if (item.Overlaps(Moment))
                    {
                        target.SetItem(index, target.NewCollectionItem(target.Parent, item.Item, item.StartDate, Moment));
                    }
                }
            });
        }
    }
}

/*

Relationship: FOR_SITE
InItem: Vessel
OutItem: null

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
