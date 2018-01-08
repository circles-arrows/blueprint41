using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41
{
    public class InstanceCollection : IEnumerable<Instance>
    {
        internal InstanceCollection(Entity parent)
             : base()
        {
            Parent = parent;
        }

        public Entity Parent { get; private set; }

        protected List<Instance> collection = new List<Instance>();
        internal void Add(Instance value)
        {
            collection.Add(value);
        }

        public Instance First(Predicate<dynamic> predicate)
        {
            return collection.First(item => predicate.Invoke(item.Values));
        }
        public Instance FirstOrDefault(Predicate<dynamic> predicate)
        {
            return collection.FirstOrDefault(item => predicate.Invoke(item.Values));
        }

        #region IEnumerable<Node>

        IEnumerator IEnumerable.GetEnumerator()
        {
            return collection.GetEnumerator();
        }

        IEnumerator<Instance> IEnumerable<Instance>.GetEnumerator()
        {
            return collection.GetEnumerator();
        }

        #endregion
    }
}
