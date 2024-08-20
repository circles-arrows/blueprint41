using System;
using System.Collections.Generic;

namespace Blueprint41
{
    public class EnumerationCollection : Core.CollectionBase<Enumeration, DatastoreModel>
    {
        internal EnumerationCollection(DatastoreModel parent)
            : base(parent)
        {
        }

        public Enumeration New(string name)
        {
            Enumeration value = new Enumeration(Parent, name);
            collection.Add(name, value);

            return value;
        }
    }
}
