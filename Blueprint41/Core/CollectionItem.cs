using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.Core
{
    public abstract class CollectionItem
    {
        protected CollectionItem(OGM parent, OGM item, DateTime? startDate, DateTime? endDate)
        {
            if (parent == null)
                throw new ArgumentNullException("parent");

            if (item == null)
                throw new ArgumentNullException("item");

            Parent = parent;
            Item = item;
            StartDate = startDate;
            EndDate = endDate;
        }

        public OGM Parent { get; private set; }
        public OGM Item { get; private set; }
        public DateTime? StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }

        public bool IsBefore(DateTime moment)
        {
            return (EndDate.HasValue && EndDate <= moment);
        }
        public bool Overlaps(DateTime moment)
        {
            return ((!StartDate.HasValue || StartDate <= moment)) && (!EndDate.HasValue || EndDate > moment);
        }
        public bool Overlaps(DateTime? start, DateTime? end)
        {
            return ((!StartDate.HasValue || !end.HasValue || StartDate <= end)) && (!EndDate.HasValue || !start.HasValue || EndDate > start);
        }
        public bool IsAfter(DateTime moment)
        {
            return (!StartDate.HasValue || StartDate.HasValue && StartDate > moment);
        }

        public override int GetHashCode()
        {
            return 
                (Parent?.GetHashCode() ?? 0) ^ 
                (Item?.GetHashCode() ?? 0) ^
                (StartDate?.GetHashCode() ?? 0) ^ 
                (EndDate?.GetHashCode() ?? 0);
        }

        public override bool Equals(object? obj)
        {
            CollectionItem? other = obj as CollectionItem;
            if (other is null)
                return false;

            if (!this.StartDate.HasValue && !other.StartDate.HasValue && !this.EndDate.HasValue && !other.EndDate.HasValue)
                return other.Parent.Equals(this.Parent) && other.Item.Equals(this.Item);

            if (!this.StartDate.HasValue && !other.StartDate.HasValue)
            {
                if (!this.EndDate.HasValue || !other.EndDate.HasValue)
                    return false;

                return other.Parent.Equals(this.Parent) && other.Item.Equals(this.Item) && other.EndDate.Equals(this.EndDate);
            }

            if (!this.EndDate.HasValue && !other.EndDate.HasValue)
            {
                if (!this.StartDate.HasValue || !other.StartDate.HasValue)
                    return false;

                return other.Parent.Equals(this.Parent) && other.Item.Equals(this.Item) && other.StartDate.Equals(this.StartDate);
            }

            if (!this.StartDate.HasValue || !other.StartDate.HasValue)
                return false;

            if (!this.EndDate.HasValue || !other.EndDate.HasValue)
                return false;

            return
                other.Parent.Equals(this.Parent) &&
                other.Item.Equals(this.Item) &&
                other.StartDate.Equals(this.StartDate) &&
                other.EndDate.Equals(this.EndDate);
        }
    }
    sealed public class CollectionItem<TEntity> : CollectionItem
         where TEntity : OGM
    {
        public CollectionItem(OGM parent, TEntity item) 
            : this(parent, item, null, null)
        {
        }
        public CollectionItem(OGM parent, TEntity item, DateTime? startDate, DateTime? endDate)
            : base(parent, item, startDate, endDate)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            Item = item;
        }

        new public TEntity Item { get; private set; }
    }
}
