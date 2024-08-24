using System;
using System.Collections.Generic;

using Blueprint41.Core;
using Blueprint41.Persistence.Provider;

namespace Blueprint41.Persistence
{
    internal class TimeDependentAddUnmanagedRelationshipAction : TimeDependentRelationshipAction
    {
        public DateTime EndDate { get; set; }
        internal TimeDependentAddUnmanagedRelationshipAction(RelationshipPersistenceProvider persistenceProvider, Relationship relationship, OGM inItem, OGM outItem, DateTime? startDate, DateTime? endDate, Dictionary<string, object>? properties)
            : base(persistenceProvider, relationship, inItem, outItem, startDate)
        {
            EndDate = endDate.HasValue ? endDate.Value : DateTime.MaxValue;
            Properties = properties;
        }

        public Dictionary<string, object>? Properties { get; private set; }

        protected override void InDatastoreLogic(Relationship relationship)
        {
            PersistenceProvider.AddUnmanaged(relationship, InItem!, OutItem!, Moment, EndDate, Properties);
        }

        protected override void InMemoryLogic(EntityCollectionBase target)
        {
            DateTime minStartDate = Moment;
            DateTime maxEndDate = EndDate;

            int[] indexes = target.IndexOf(target.ForeignItem(this)!);
            foreach (int index in indexes)
            {
                CollectionItem? item = target.GetItem(index);
                if (item is not null)
                {
                    if (item.Overlaps(Moment))
                    {
                        if (minStartDate > item.StartDate)
                            minStartDate = item.StartDate;

                        if (maxEndDate < item.EndDate)
                            maxEndDate = item.EndDate;

                        target.RemoveAt(index);
                    }
                }
            }

            target.Add(target.NewCollectionItem(target.Parent, target.ForeignItem(this)!, minStartDate, maxEndDate));
        }
    }
}
