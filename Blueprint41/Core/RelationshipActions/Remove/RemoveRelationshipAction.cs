using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.Core
{
    internal class RemoveRelationshipAction : RelationshipAction
    {
        internal RemoveRelationshipAction(RelationshipPersistenceProvider persistenceProvider, Relationship relationship, OGM inItem, OGM outItem)
            : base(persistenceProvider, relationship, inItem, outItem)
        {
        }

        protected override void InDatastoreLogic(Relationship Relationship)
        {
            PersistenceProvider.Remove(Relationship, InItem, OutItem, null, false);
        }

        protected override void InMemoryLogic(EntityCollectionBase target)
        {
            target.ForEach(delegate (int index, CollectionItem item)
            {
                if (item.Item.Equals(target.ForeignItem(this)))
                    target.RemoveAt(index);
            });
        }
    }
}
