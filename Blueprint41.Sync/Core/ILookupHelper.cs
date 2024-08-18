using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.Sync.Core
{
    public interface ILookupHelper<TInterface>
        where TInterface : class, OGM
    {
        TInterface? GetOriginalItem(DateTime? moment);
        TInterface? GetItem(DateTime? moment);
        IEnumerable<CollectionItem<TInterface>> GetItems(DateTime? from, DateTime? till);
        void AddItem(TInterface item, DateTime? moment, Dictionary<string, object>? properties = null);
        void SetItem(TInterface? item, DateTime? moment, Dictionary<string, object>? properties = null);
        bool IsNull(bool isUpdate);
        void ClearLookup(DateTime? moment);
    }
}
