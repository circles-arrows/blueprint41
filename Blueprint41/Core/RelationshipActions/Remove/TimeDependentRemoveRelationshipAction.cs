using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.Core
{
    internal class TimeDependentRemoveRelationshipAction : TimeDependentRelationshipAction
    {
        internal TimeDependentRemoveRelationshipAction(RelationshipPersistenceProvider persistenceProvider, Relationship relationship, OGM? inItem, OGM? outItem, DateTime? moment)
            : base(persistenceProvider, relationship, inItem, outItem, moment)
        {
        }

        protected override void InDatastoreLogic(Relationship relationship)
        {
            PersistenceProvider.Remove(relationship, InItem, OutItem, Moment, true);
        }

        protected override void InMemoryLogic(EntityCollectionBase target)
        {
            OGM? foreignItem = target.ForeignItem(this);
            if (foreignItem is null)
            {
                target.ForEach((index, item) =>
                {
                    if (item is not null)
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
            else
            {
                int[] indexes = target.IndexOf(foreignItem);
                foreach (int index in indexes)
                {
                    CollectionItem? item = target.GetItem(index);
                    if (item is not null)
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
                }
            }
        }
    }
}
