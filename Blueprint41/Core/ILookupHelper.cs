using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.Core
{
    public interface ILookupHelper<TInterface>
    {
        TInterface GetOriginalItem(DateTime? moment);
        TInterface GetItem(DateTime? moment);
        void SetItem(TInterface item, DateTime? moment);
        bool IsNull(bool isUpdate);
        void ClearLookup(DateTime? moment);
    }
}
