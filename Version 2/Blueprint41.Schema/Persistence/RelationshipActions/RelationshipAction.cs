using System;
using System.Collections.Generic;

using Blueprint41.Core;
using Blueprint41.Persistence.Provider;

namespace Blueprint41.Persistence
{
    internal abstract class RelationshipAction
    {
        protected RelationshipAction(RelationshipPersistenceProvider persistenceProvider, Relationship? relationship, OGM? inItem, OGM? outItem)
        {
            Relationship = relationship;
            InItem = inItem;
            OutItem = outItem;
            PersistenceProvider = persistenceProvider;
        }

        public RelationshipPersistenceProvider PersistenceProvider { get; internal set; }

        private Relationship? Relationship { get; set; }
        public OGM? InItem { get; private set; }
        public OGM? OutItem { get; private set; }
        public bool IsExecutedInMemory { get; set; } = false;

        public void ExecuteInMemory(EntityCollectionBase target)
        {
            if (!target.IsLoaded)
                return;

            //if (IsExecutedInMemory)
            //    return;

            if (Relationship is not null && Relationship.Name != target.Relationship.Name)
                return;

            OGM? parent = target.ParentItem(this);
            if (parent is not null && target.Parent != parent)
                return;

            InMemoryLogic(target);

            IsExecutedInMemory = true;
        }
        protected abstract void InMemoryLogic(EntityCollectionBase target);

        public void ExecuteInDatastore()
        {
            if (Relationship is not null)
            {
                InDatastoreLogic(Relationship);
            }
            else
            {
                /*
                         Parent Nullable       0 to Many
                    (Account)-[HAS_EXTERNAL_REF]->(ExternalReference)

                         0 to Many      NOT NULL
                    (Account)-[HAS_PARENT]-(Account)

                Transaction:
                    - Delete Account
                    - Delete External References
                    - Commit
                 */

                Entity? entity = InItem?.GetEntity();
                if (entity is not null)
                {
                    foreach (var relationship in entity.Parent.Relations)
                        InDatastoreLogic(relationship);
                }
            }
        }
        protected abstract void InDatastoreLogic(Relationship relationship);
    }
}
