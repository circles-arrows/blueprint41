using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.Core
{
    internal class TimeDependentAddUnmanagedRelationshipAction : TimeDependentRelationshipAction
    {
        public DateTime EndDate { get; set; }
        internal TimeDependentAddUnmanagedRelationshipAction(RelationshipPersistenceProvider persistenceProvider, Relationship relationship, OGM inItem, OGM outItem, DateTime? startDate, DateTime? endDate)
            : base(persistenceProvider, relationship, inItem, outItem, startDate)
        {
            EndDate = (endDate.HasValue) ? endDate.Value : DateTime.MaxValue;
        }

        protected override void InDatastoreLogic(Relationship Relationship)
        {
            PersistenceProvider.AddUnmanaged(Relationship, InItem, OutItem, Moment, EndDate);
        }

        protected override void InMemoryLogic(EntityCollectionBase target)
        {
            DateTime minStartDate = Moment;
            DateTime maxEndDate = EndDate;

            int[] indexes = target.IndexOf(target.ForeignItem(this));
            foreach (int index in indexes)
            {
                CollectionItem? item = target.GetItem(index);
                if (item != null)
                {
                    if (item.Overlaps(Moment))
                    {
                        if (minStartDate > item.StartDate)
                            minStartDate = item.StartDate ?? DateTime.MinValue;

                        if (maxEndDate < item.EndDate)
                            maxEndDate = item.EndDate ?? DateTime.MaxValue;

                        target.RemoveAt(index);
                    }
                }
            }

            target.Add(target.NewCollectionItem(target.Parent, target.ForeignItem(this), minStartDate, maxEndDate));
        }
    }
}
