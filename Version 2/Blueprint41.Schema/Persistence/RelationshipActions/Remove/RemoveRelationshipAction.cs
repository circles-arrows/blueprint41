using System;
using System.Collections.Generic;

using Blueprint41.Core;
using Blueprint41.Persistence.Provider;

namespace Blueprint41.Persistence
{
    internal class RemoveRelationshipAction : RelationshipAction
    {
        internal RemoveRelationshipAction(RelationshipPersistenceProvider persistenceProvider, Relationship relationship, OGM? inItem, OGM? outItem)
            : base(persistenceProvider, relationship, inItem, outItem)
        {
        }

        protected override void InDatastoreLogic(Relationship relationship)
        {
            PersistenceProvider.Remove(relationship, InItem, OutItem, null, false);
        }

        protected override void InMemoryLogic(EntityCollectionBase target)
        {
            OGM? foreignItem = target.ForeignItem(this);
            if (foreignItem is null)
            {
                target.ForEach((index, item) =>
                {
                    if (item is not null)
                        target.RemoveAt(index);
                });
            }
            else
            {
                int[] indexes = target.IndexOf(foreignItem);
                foreach (int index in indexes)
                {
                    CollectionItem? item = target.GetItem(index);
                    if (item is not null)
                        target.RemoveAt(index);
                }
            }
        }
    }
}
