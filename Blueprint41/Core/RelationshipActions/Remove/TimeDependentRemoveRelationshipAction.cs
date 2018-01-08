using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.Core
{
    internal class TimeDependentRemoveRelationshipAction : TimeDependentRelationshipAction
    {
        internal TimeDependentRemoveRelationshipAction(RelationshipPersistenceProvider persistenceProvider, Relationship relationship, OGM inItem, OGM outItem, DateTime? moment)
            : base(persistenceProvider, relationship, inItem, outItem, moment)
        {
        }

        protected override void InDatastoreLogic(Relationship Relationship)
        {
            PersistenceProvider.Remove(Relationship, InItem, OutItem, Moment, true);
        }

        protected override void InMemoryLogic(EntityCollectionBase target)
        {
            target.ForEach(delegate (int index, CollectionItem item)
            {
                if (item.Item.Equals(target.ForeignItem(this)))
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
