using System;
using System.Collections.Generic;

using Blueprint41.Core;
using Blueprint41.Persistence;

namespace Blueprint41.Persistence
{
    /// <summary>
    /// Time dependent variant of <see cref="LookupRelationshipAction"/>.
    /// Ends the existing relationship(s) of the parent (and of the assigned item when the opposite side is also a lookup)
    /// at the given moment, then adds the new relationship starting at that moment.
    /// </summary>
    internal class TimeDependentLookupRelationshipAction : TimeDependentRelationshipAction
    {
        internal TimeDependentLookupRelationshipAction(RelationshipPersistenceProvider persistenceProvider, Relationship relationship, Property parentProperty, Property? foreignProperty, OGM parent, OGM? item, DateTime? moment, Dictionary<string, object>? properties)
            : base(persistenceProvider, relationship, IsParentIn(foreignProperty) ? parent : item, IsParentIn(foreignProperty) ? item : parent, moment)
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
        private bool ForeignIsLookup => ForeignProperty is not null && ForeignProperty.PropertyType == PropertyType.Lookup;

        protected override bool FilterByParent => false;

        protected override void InDatastoreLogic(Relationship relationship)
        {
            if (ParentIsIn)
                PersistenceProvider.Remove(relationship, Parent, (OGM?)null, Moment, true);
            else
                PersistenceProvider.Remove(relationship, (OGM?)null, Parent, Moment, true);

            if (Item is null)
                return;

            if (ForeignIsLookup)
            {
                if (ParentIsIn)
                    PersistenceProvider.Remove(relationship, (OGM?)null, Item, Moment, true);
                else
                    PersistenceProvider.Remove(relationship, Item, (OGM?)null, Moment, true);
            }

            PersistenceProvider.Add(relationship, InItem!, OutItem!, Moment, true, Properties);
        }

        protected override async System.Threading.Tasks.Task InDatastoreLogicAsync(Relationship relationship)
        {
            if (ParentIsIn)
                await PersistenceProvider.RemoveAsync(relationship, Parent, (OGM?)null, Moment, true);
            else
                await PersistenceProvider.RemoveAsync(relationship, (OGM?)null, Parent, Moment, true);

            if (Item is null)
                return;

            if (ForeignIsLookup)
            {
                if (ParentIsIn)
                    await PersistenceProvider.RemoveAsync(relationship, (OGM?)null, Item, Moment, true);
                else
                    await PersistenceProvider.RemoveAsync(relationship, Item, (OGM?)null, Moment, true);
            }

            await PersistenceProvider.AddAsync(relationship, InItem!, OutItem!, Moment, true, Properties);
        }

        protected override void InMemoryLogic(IReplayableCollection target)
        {
            if (Same(target.ParentProperty, ParentProperty))
            {
                if (target.Parent.Equals(Parent))
                {
                    End(target, null);
                    if (Item is not null)
                        AddTo(target, Item);
                }
                else if (Item is not null && ForeignIsLookup)
                {
                    End(target, Item);
                }
            }
            else if (ForeignProperty is not null && Same(target.ParentProperty, ForeignProperty))
            {
                if (Item is not null && target.Parent.Equals(Item))
                {
                    if (ForeignIsLookup)
                        End(target, null);

                    AddTo(target, Parent);
                }
                else
                {
                    End(target, Parent);
                }
            }
        }

        private static bool Same(Property? a, Property? b)
        {
            if (a is null || b is null)
                return false;

            return ReferenceEquals(a, b) || (a.Name == b.Name && a.Direction == b.Direction);
        }

        private void End(IReplayableCollection target, OGM? value)
        {
            if (value is null)
            {
                target.ForEach((index, item) =>
                {
                    if (item is not null)
                        EndItem(target, index, item);
                });
            }
            else
            {
                foreach (int index in target.IndexOf(value))
                {
                    CollectionItem? item = target.GetItem(index);
                    if (item is not null)
                        EndItem(target, index, item);
                }
            }
        }

        private void EndItem(IReplayableCollection target, int index, CollectionItem item)
        {
            if (item.IsAfter(Moment))
                target.RemoveAt(index);
            else if (item.Overlaps(Moment))
                target.SetItem(index, target.NewCollectionItem(target.Parent, item.Item, item.StartDate, Moment));
        }

        private void AddTo(IReplayableCollection target, OGM value)
        {
            bool wasUpdated = false;
            foreach (int index in target.IndexOf(value))
            {
                CollectionItem? item = target.GetItem(index);
                if (item is null)
                    continue;

                if (item.IsAfter(Moment))
                {
                    target.RemoveAt(index);
                }
                else if (item.OverlapsOrIsAttached(Moment))
                {
                    target.SetItem(index, target.NewCollectionItem(target.Parent, item.Item, item.StartDate, null));
                    wasUpdated = true;
                }
            }

            if (!wasUpdated)
                target.Add(target.NewCollectionItem(target.Parent, value, Moment, null));
        }
    }
}
