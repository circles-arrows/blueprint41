using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.Async.Core
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
