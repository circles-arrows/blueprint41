using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.Core
{
    internal abstract class RelationshipAction
    {
        protected RelationshipAction(RelationshipPersistenceProvider persistenceProvider, Relationship? relationship, OGM inItem, OGM outItem)
        {
            Relationship = relationship;
            InItem = inItem;
            OutItem = outItem;
            PersistenceProvider = persistenceProvider;
        }

        public RelationshipPersistenceProvider PersistenceProvider { get; internal set; }

        private Relationship? Relationship { get; set; }
        public OGM InItem { get; private set; }
        public OGM OutItem { get; private set; }

        protected virtual bool ActsOnSpecificParent() { return true; }

        public void ExecuteInMemory(Core.EntityCollectionBase target)
        {
            if (!target.IsLoaded)
                return;

            if (Relationship is not null)
            {
                if (Relationship.Name != target.Relationship.Name)
                    return;

                if (ActsOnSpecificParent())
                    if (target.Parent != target.ParentItem(this))
                        return;
            }
            else
            {
                Entity entity = InItem.GetEntity();
                bool shouldRun = false;
                if (target.ParentEntity == entity)
                {
                    shouldRun = true;
                    if (target.ForeignProperty is not null && !target.ForeignProperty.Nullable)
                        return;
                }
                if (target.ForeignEntity == entity)
                {
                    shouldRun = true;
                    if (target.ParentProperty is not null && !target.ParentProperty.Nullable)
                        return;
                }
                if (!shouldRun)
                    return;
            }
            InMemoryLogic(target);
        }
        protected abstract void InMemoryLogic(Core.EntityCollectionBase target);

        public void ExecuteInDatastore()
        {
            if (Relationship is not null)
            {
                InDatastoreLogic(Relationship);
            }
            else
            {
                Entity entity = InItem.GetEntity();
                foreach (var relationship in entity.Parent.Relations)
                {
                    bool shouldRun = false;
                    if (entity.IsSelfOrSubclassOf(relationship.InEntity))
                    {
                        shouldRun = true;
                        if (relationship.OutProperty is not null && !relationship.OutProperty.Nullable)
                            continue;
                    }
                    if (entity.IsSelfOrSubclassOf(relationship.OutEntity))
                    {
                        shouldRun = true;
                        if (relationship.InProperty is not null && !relationship.InProperty.Nullable)
                            continue;
                    }
                    if (!shouldRun)
                        continue;

                    InDatastoreLogic(relationship);
                }
            }
        }
        protected abstract void InDatastoreLogic(Relationship relationship);
    }
}
