using System;
using System.Collections.Generic;
using System.Text;

namespace Blueprint41.Core
{
    public interface IInternalListAccess
    {
        void EnsureLoaded();
        void Add(CollectionItem item);
        CollectionItem? GetItem(int index);
        void SetItem(int index, CollectionItem item);
        void RemoveAt(int index);
        int[] IndexOf(OGM item);
    }
}
