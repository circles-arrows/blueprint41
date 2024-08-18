using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.Async
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
