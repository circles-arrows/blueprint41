using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41
{
    public class RelationshipCollection : Core.CollectionBase<Relationship, DatastoreModel>
    {
        internal RelationshipCollection(DatastoreModel parent)
            : base(parent)
        {
        }

        public Relationship New(Entity inEntity, Entity outEntity, string name, string neo4jRelationshipType = null)
        {
            Relationship value = new Relationship(Parent,name, neo4jRelationshipType, inEntity, outEntity);

            this.collection.Add(value.Name, value);

            return value;
        }
   }
}
