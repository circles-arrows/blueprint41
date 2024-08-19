using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
