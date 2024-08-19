using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.Model
{
    internal class AttributeCollection<TParent> : Core.CollectionBase<Property<TParent>, TParent>
    {
        internal AttributeCollection(TParent parent) : base(parent) { }
    }
}
