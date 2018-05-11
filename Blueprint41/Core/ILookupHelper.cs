using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.Core
{
    public interface ILookupHelper<TInterface>
        where TInterface : OGM
    {
        TInterface GetOriginalItem(DateTime? moment);
        TInterface GetItem(DateTime? moment);
        IEnumerable<CollectionItem<TInterface>> GetItems(DateTime? from, DateTime? till);
        void SetItem(TInterface item, DateTime? moment);
        bool IsNull(bool isUpdate);
        void ClearLookup(DateTime? moment);
    }
}
