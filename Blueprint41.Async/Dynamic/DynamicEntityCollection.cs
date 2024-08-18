using Blueprint41.Async.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.Async.Dynamic
{
    public class DynamicEntityCollection : EntityCollection<DynamicEntity>
    {
        public DynamicEntityCollection(OGM parent, Property property) : base(parent, property)
        {

        }
    }
}
