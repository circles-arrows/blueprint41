using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.Core
{
    internal class AddRelationshipAction : RelationshipAction
    {
        internal AddRelationshipAction(RelationshipPersistenceProvider persistenceProvider, Relationship relationship, OGM inItem, OGM outItem)
            : base(persistenceProvider, relationship, inItem, outItem)
        {
        }

        protected override void InDatastoreLogic(Relationship Relationship)
        {
            PersistenceProvider.Add(Relationship, InItem, OutItem, null, false);
        }

        protected override void InMemoryLogic(EntityCollectionBase target)
        {
            bool contains = false;
            target.ForEach(delegate (int index, CollectionItem item)
            {
                if (item.Item.Equals(target.ForeignItem(this)))
                    contains = true;
            });
            if (!contains)
                target.Add(target.NewCollectionItem(target.Parent, target.ForeignItem(this), null, null));
        }
    }
}
