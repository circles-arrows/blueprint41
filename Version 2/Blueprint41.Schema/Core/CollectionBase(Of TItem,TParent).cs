using System;
using System.Collections.Generic;

namespace Blueprint41.Core
{
    public abstract class CollectionBase<TItem, TParent> : CollectionBase<TItem>
    {
        internal CollectionBase(TParent parent)
             : base()
        {
            Parent = parent;
        }

        public TParent Parent { get; private set; }
    }
}
