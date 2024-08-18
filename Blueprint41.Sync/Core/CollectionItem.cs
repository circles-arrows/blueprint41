using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Blueprint41.Sync.TypeConversion;

namespace Blueprint41.Sync.Core
{
    public abstract class CollectionItem
    {
        protected CollectionItem(OGM parent, OGM item, DateTime? startDate, DateTime? endDate)
        {
            if (parent is null)
                throw new ArgumentNullException("parent");

            if (item is null)
                throw new ArgumentNullException("item");

            Parent = parent;
            Item = item;
            StartDate = startDate ?? Conversion.MinDateTime;
            EndDate = endDate ?? Conversion.MaxDateTime;
        }

        public OGM Parent { get; private set; }
        public OGM Item { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; internal set; }

        public bool IsBefore(DateTime moment)
        {
            return EndDate < moment;
        }
        public bool Overlaps(DateTime moment)
        {
            return (StartDate <= moment && EndDate > moment);
        }
        public bool OverlapsOrIsAttached(DateTime moment)
        {
            return (StartDate <= moment && EndDate >= moment);
        }
        public bool Overlaps(DateTime? start, DateTime? end)
        {
            DateTime s = start ?? Conversion.MinDateTime;
            DateTime e = end ?? Conversion.MaxDateTime;
            return (StartDate <= e && EndDate > s);
        }
        public bool IsAfter(DateTime moment)
        {
            return StartDate >= moment;
        }
        internal bool IsAttached(DateTime? start, DateTime? end)
        {
            DateTime s = start ?? Conversion.MinDateTime;
            DateTime e = end ?? Conversion.MaxDateTime;
            return (EndDate == s || StartDate == e);
        }
        internal bool OverlapsOrIsAttached(DateTime? start, DateTime? end)
        {
            DateTime s = start ?? Conversion.MinDateTime;
            DateTime e = end ?? Conversion.MaxDateTime;
            return (StartDate <= e && EndDate >= s);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Parent, Item, StartDate, EndDate);
        }
        public override bool Equals(object? obj)
        {
            CollectionItem? other = obj as CollectionItem;
            if (other is null)
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
            if (item is null)
                throw new ArgumentNullException("item");

            Item = item;
        }

        new public TEntity Item { get; private set; }
    }
}
