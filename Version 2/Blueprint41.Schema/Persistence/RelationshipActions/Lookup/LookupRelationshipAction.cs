using System;
using System.Collections.Generic;

using Blueprint41.Core;
using Blueprint41.Persistence;

namespace Blueprint41.Persistence
{
    /// <summary>
    /// Assigns (or clears) a lookup without having to load the previous owner(s) of the lookup value.
    /// Datastore: removes the existing relationship(s) on the owning side, removes the existing relationship(s) of the
    /// assigned item when the opposite side is also a lookup, then adds the new relationship.
    /// In memory: replays the change onto every loaded collection of the same relationship.
    /// </summary>
    internal class LookupRelationshipAction : RelationshipAction
    {
        internal LookupRelationshipAction(RelationshipPersistenceProvider persistenceProvider, Relationship relationship, Property parentProperty, Property? foreignProperty, OGM parent, OGM? item, Dictionary<string, object>? properties)
            : base(persistenceProvider, relationship, IsParentIn(foreignProperty) ? parent : item, IsParentIn(foreignProperty) ? item : parent)
        {
            ParentProperty = parentProperty;
            ForeignProperty = foreignProperty;
            Parent = parent;
            Item = item;
            Properties = properties;
            ParentIsIn = IsParentIn(foreignProperty);
        }

        private static bool IsParentIn(Property? foreignProperty) => !(foreignProperty is not null && foreignProperty.Direction == DirectionEnum.In);

        private Property ParentProperty { get; }
        private Property? ForeignProperty { get; }
        private OGM Parent { get; }
        private OGM? Item { get; }
        private Dictionary<string, object>? Properties { get; }
        private bool ParentIsIn { get; }

        protected override bool FilterByParent => false;

        protected override void InDatastoreLogic(Relationship relationship)
        {
            if (ParentIsIn)
                PersistenceProvider.Remove(relationship, Parent, (OGM?)null, null, false);
            else
                PersistenceProvider.Remove(relationship, (OGM?)null, Parent, null, false);

            if (Item is null)
                return;

            if (ForeignProperty is not null && ForeignProperty.PropertyType == PropertyType.Lookup)
            {
                if (ParentIsIn)
                    PersistenceProvider.Remove(relationship, (OGM?)null, Item, null, false);
                else
                    PersistenceProvider.Remove(relationship, Item, (OGM?)null, null, false);
            }

            PersistenceProvider.Add(relationship, InItem!, OutItem!, null, false, Properties);
        }

        protected override void InMemoryLogic(IReplayableCollection target)
        {
            if (Same(target.ParentProperty, ParentProperty))
            {
                if (target.Parent.Equals(Parent))
                {
                    Clear(target);
                    if (Item is not null)
                        target.Add(target.NewCollectionItem(target.Parent, Item, null, null));
                }
                else if (Item is not null && ForeignProperty is not null && ForeignProperty.PropertyType == PropertyType.Lookup)
                {
                    Remove(target, Item); // previous owner of the assigned item
                }
            }
            else if (ForeignProperty is not null && Same(target.ParentProperty, ForeignProperty))
            {
                if (Item is not null && target.Parent.Equals(Item))
                {
                    if (ForeignProperty.PropertyType == PropertyType.Lookup)
                        Clear(target);

                    if (target.IndexOf(Parent).Length == 0)
                        target.Add(target.NewCollectionItem(target.Parent, Parent, null, null));
                }
                else
                {
                    Remove(target, Parent); // previous lookup value of the parent
                }
            }
        }

        private static bool Same(Property? a, Property? b)
        {
            if (a is null || b is null)
                return false;

            return ReferenceEquals(a, b) || (a.Name == b.Name && a.Direction == b.Direction);
        }

        private static void Clear(IReplayableCollection target)
        {
            target.ForEach((index, item) =>
            {
                if (item is not null)
                    target.RemoveAt(index);
            });
        }

        private static void Remove(IReplayableCollection target, OGM value)
        {
            foreach (int index in target.IndexOf(value))
            {
                if (target.GetItem(index) is not null)
                    target.RemoveAt(index);
            }
        }
    }
}
