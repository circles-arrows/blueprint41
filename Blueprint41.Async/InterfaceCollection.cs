using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.Async
{
    public class InterfaceCollection : Core.CollectionBase<Interface, DatastoreModel>
    {
        internal InterfaceCollection(DatastoreModel parent)
            : base(parent)
        {
        }

        public Interface New(string name)
        {
            Interface value = new Interface(Parent, name);
            collection.Add(name, value);

            return value;
        }
    }
}
