using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blueprint41.Core
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
    public interface ILookupHelperAsync<TInterface>
    where TInterface : class, OGM
    {
        Task<TInterface?> GetOriginalItemAsync(DateTime? moment);
        Task<TInterface?> GetItemAsync(DateTime? moment);
        Task<IEnumerable<CollectionItem<TInterface>>> GetItemsAsync(DateTime? from, DateTime? till);
        Task AddItemAsync(TInterface item, DateTime? moment, Dictionary<string, object>? properties = null);
        Task SetItemAsync(TInterface? item, DateTime? moment, Dictionary<string, object>? properties = null);
        bool IsNull(bool isUpdate);
        Task ClearLookupAsync(DateTime? moment);
    }
}
