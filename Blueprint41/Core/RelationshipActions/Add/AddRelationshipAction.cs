using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.Core
{
    internal class AddRelationshipAction : RelationshipAction
    {
        public IDictionary<string, object>? Properties { get; private set; }

        internal AddRelationshipAction(RelationshipPersistenceProvider persistenceProvider, Relationship relationship, OGM inItem, OGM outItem, ExpandoObject? relationshipProperties = null)
            : base(persistenceProvider, relationship, inItem, outItem)
        {
            Properties = relationshipProperties?.ToDictionary(item => item.Key, item => item.Value);
        }

        protected override void InDatastoreLogic(Relationship relationship)
        {
            PersistenceProvider.Add(relationship, Properties, InItem!, OutItem!, null, false);
        }

        protected override void InMemoryLogic(EntityCollectionBase target)
        {
            bool contains = target.IndexOf(target.ForeignItem(this)!).Length != 0;
            if (!contains)
                target.Add(target.NewCollectionItem(target.Parent, target.ForeignItem(this)!, null, null));
        }
    }
}
