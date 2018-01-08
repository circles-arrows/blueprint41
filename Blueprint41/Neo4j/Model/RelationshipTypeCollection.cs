using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.Neo4j.Model
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
