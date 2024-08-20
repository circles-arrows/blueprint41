using System;
using System.Collections.Generic;

namespace Blueprint41.Model
{
    internal class RelationshipTypeCollection : Core.CollectionBase<RelationshipType>
    {
        public RelationshipType New(string name, bool autoIndexing = false)
        {
            RelationshipType value = new RelationshipType(name, autoIndexing);
            collection.Add(name, value);

            return value;
        }
    }
}
