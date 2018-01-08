using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.Core
{
    internal class TimeDependentRemoveUnmanagedRelationshipAction : TimeDependentRelationshipAction
    {
        internal TimeDependentRemoveUnmanagedRelationshipAction(RelationshipPersistenceProvider persistenceProvider, Relationship relationship, OGM inItem, OGM outItem, DateTime? startDate)
            : base(persistenceProvider, relationship, inItem, outItem, startDate)
        {
        }

        protected override void InDatastoreLogic(Relationship Relationship)
        {
            PersistenceProvider.RemoveUnmanaged(Relationship, InItem, OutItem, Moment);
        }

        protected override void InMemoryLogic(EntityCollectionBase target)
        {
            target.ForEach(delegate (int index, CollectionItem item)
            {
                if (item.Item.Equals(target.ForeignItem(this)))
                {
                    if (item.StartDate == Moment)
                        target.RemoveAt(index);
                }
            });
        }
    }
}
