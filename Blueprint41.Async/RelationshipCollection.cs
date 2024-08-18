using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.Async
{
    public class RelationshipCollection : Core.CollectionBase<Relationship, DatastoreModel>
    {
        internal RelationshipCollection(DatastoreModel parent)
            : base(parent)
        {
        }

        public Relationship New(Entity inEntity, Entity outEntity, string name, string? neo4jRelationshipType = null)
        {
            Relationship value = new Relationship(Parent,name, neo4jRelationshipType, inEntity, null, outEntity, null);

            this.collection.Add(value.Name, value);

            return value;
        }
        public Relationship New(Interface inInterface, Entity outEntity, string name, string? neo4jRelationshipType = null)
        {
            Relationship value = new Relationship(Parent, name, neo4jRelationshipType, null, inInterface, outEntity, null);

            this.collection.Add(value.Name, value);

            return value;
        }
        public Relationship New(Entity inEntity, Interface outInterface, string name, string? neo4jRelationshipType = null)
        {
            Relationship value = new Relationship(Parent, name, neo4jRelationshipType, inEntity, null, null, outInterface);

            this.collection.Add(value.Name, value);

            return value;
        }
        public Relationship New(Interface inInterface, Interface outInterface, string name, string? neo4jRelationshipType = null)
        {
            Relationship value = new Relationship(Parent, name, neo4jRelationshipType, null, inInterface, null, outInterface);

            this.collection.Add(value.Name, value);

            return value;
        }
    }
}
