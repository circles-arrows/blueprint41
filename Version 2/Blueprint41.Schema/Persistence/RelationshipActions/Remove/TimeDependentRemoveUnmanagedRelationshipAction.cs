using System;
using System.Collections.Generic;

using Blueprint41.Core;
using Blueprint41.Persistence.Provider;

namespace Blueprint41.Persistence
{
    internal class TimeDependentRemoveUnmanagedRelationshipAction : TimeDependentRelationshipAction
    {
        internal TimeDependentRemoveUnmanagedRelationshipAction(RelationshipPersistenceProvider persistenceProvider, Relationship relationship, OGM inItem, OGM outItem, DateTime? startDate)
            : base(persistenceProvider, relationship, inItem, outItem, startDate)
        {
        }

        protected override void InDatastoreLogic(Relationship relationship)
        {
            PersistenceProvider.RemoveUnmanaged(relationship, InItem!, OutItem!, Moment);
        }

        protected override void InMemoryLogic(EntityCollectionBase target)
        {
            int[] indexes = target.IndexOf(target.ForeignItem(this)!);
            foreach (int index in indexes)
            {
                CollectionItem? item = target.GetItem(index);
                if (item is not null)
                {
                    if (item.StartDate == Moment)
                        target.RemoveAt(index);
                }
            }
        }
    }
}
