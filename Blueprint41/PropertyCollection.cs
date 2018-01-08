using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41
{
    public class PropertyCollection : Core.CollectionBase<Property, Entity>
    {
        internal PropertyCollection(Entity parent)
            :base(parent)
        {
        }
    }
}
