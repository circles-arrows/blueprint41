using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Blueprint41.Persistence;

namespace Blueprint41.Core
{
    internal interface IReplayableCollection : IInternalListAccess, IItteratable<CollectionItem>
    {
        bool IsLoaded { get; }
        void InitialLoad(IEnumerable<CollectionItem> items);

        OGM Parent { get; }
        Entity ParentEntity { get; }
        Property? ParentProperty { get; }
        Entity ForeignEntity { get; }
        Property? ForeignProperty { get; }

        Relationship Relationship { get; }
        DirectionEnum Direction { get; }

        OGM InItem(CollectionItem item);
        OGM InItem(OGM foreign);
        OGM OutItem(CollectionItem item);
        OGM OutItem(OGM foreign);
        OGM? ParentItem(RelationshipAction action);
        OGM? ForeignItem(RelationshipAction action);

        CollectionItem NewCollectionItem(OGM parent, OGM item, DateTime? startDate, DateTime? endDate);

        Transaction? Transaction { get; set; }
    }
}
