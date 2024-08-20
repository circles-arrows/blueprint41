using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Blueprint41
{
    public class EntityPropertyCollection<TProperty, TParent> : IEnumerable<TProperty>
        where TProperty : Property
    {
        internal EntityPropertyCollection(TParent parent, PropertyCollection properties)
           : base()
        {
            Parent = parent;
            _properties = properties;
        }
        private readonly PropertyCollection _properties;

        public TParent Parent { get; private set; }

        internal void Add(string name, Property value)
        {
            if (typeof(TParent) == typeof(Relationship))
            {
                if (name == "CreationDate")
                    throw new NotSupportedException("The property 'CreationDate' is mandatory for relationships and already exists, you do not need to add it manually.");

                if (name == "StartDate" || name == "EndData")
                    throw new NotSupportedException("Please use the 'AddTimeDependance()' method instead to add a 'StartDate' and 'EndDate' property.");
            }

            _properties.Add(name, value);
        }
        internal void Remove(string name)
        {
            if (typeof(TParent) == typeof(Relationship))
            {
                if (name == "CreationDate")
                    throw new NotSupportedException("The property 'CreationDate' is mandatory for relationships, you cannot remove it.");

                if (name == "StartDate" || name == "EndData")
                    throw new NotSupportedException("Please use the 'Refactor.RemoveTimeDependance()' method instead to remove the 'StartDate' and 'EndDate' property.");
            }

            _properties.Remove(name);
        }

        public TProperty this[string name]
        {
            get
            {
                return (TProperty)_properties[name];
            }
        }
        public bool Contains(string name)
        {
            return _properties.Contains(name);
        }
        public int Count
        {
            get
            {
                return _properties.Count;
            }
        }

        #region IEnumerable<NodeType>

        IEnumerator IEnumerable.GetEnumerator() => _properties.Cast<TProperty>().GetEnumerator();
        IEnumerator<TProperty> IEnumerable<TProperty>.GetEnumerator() => _properties.Cast<TProperty>().GetEnumerator();

        #endregion
    }


    public class PropertyCollection : Core.CollectionBase<Property, IEntity>
    {
        internal PropertyCollection(IEntity parent)
            : base(parent)
        {
        }
    }
}
