using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        internal void Add(string name, Property value) => _properties.Add(name, value);
        internal void Remove(string name) => _properties.Remove(name);
        
        public TProperty this[string name] => (TProperty)_properties[name];
        public bool Contains(string name) => _properties.Contains(name);
        public int Count => _properties.Count;

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
