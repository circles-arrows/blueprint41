using Blueprint41.Sync.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.Sync.Dynamic
{
    public class DynamicEntityCollection : EntityCollection<DynamicEntity>
    {
        public DynamicEntityCollection(OGM parent, Property property) : base(parent, property)
        {

        }
    }
}
