using System;
using System.Collections.Generic;

using Blueprint41.Core;

namespace Blueprint41.Dynamic
{
    public class DynamicEntityCollection : EntityCollection<DynamicEntity>
    {
        public DynamicEntityCollection(OGM parent, Property property) : base(parent, property)
        {

        }
    }
}
