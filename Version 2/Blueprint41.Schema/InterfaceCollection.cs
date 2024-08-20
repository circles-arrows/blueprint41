using System;
using System.Collections.Generic;

namespace Blueprint41
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
