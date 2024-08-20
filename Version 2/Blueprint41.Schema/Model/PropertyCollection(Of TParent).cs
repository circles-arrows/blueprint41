using System;
using System.Collections.Generic;

namespace Blueprint41.Model
{
    internal class AttributeCollection<TParent> : Core.CollectionBase<Property<TParent>, TParent>
    {
        internal AttributeCollection(TParent parent) : base(parent) { }
    }
}
