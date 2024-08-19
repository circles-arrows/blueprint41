using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41
{
    public class EntityCollection : Core.CollectionBase<Entity, DatastoreModel>
    {
        internal EntityCollection(DatastoreModel parent)
            : base(parent)
        {
        }

        public Entity New(string name, Entity? inherits = null)
        {
            return New(name, name, (string?)null, inherits);
        }
        public Entity New(string name, string prefix, Entity? inherits = null)
        {
            return New(name, name, prefix, inherits);
        }
        public Entity New(string name, string label, string? prefix, Entity? inherits = null)
        {
            Entity value = new Entity(Parent, name, label, prefix, inherits);
            collection.Add(name, value);

            return value;
        }
        public Entity New(string name, FunctionalId functionId, Entity? inherits = null)
        {
            return New(name, name, functionId, inherits);
        }
        public Entity New(string name, string label, FunctionalId functionId, Entity? inherits = null)
        {
            Entity value = new Entity(Parent, name, label, functionId, inherits);
            collection.Add(name, value);

            return value;
        }
    }
}
