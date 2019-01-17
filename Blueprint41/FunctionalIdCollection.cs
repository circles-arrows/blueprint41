using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41
{
    public class FunctionalIdCollection : Core.CollectionBase<FunctionalId, DatastoreModel>
    {
        internal FunctionalIdCollection(DatastoreModel parent) : base(parent) { }

        private FunctionalId m_Default = null;
        public FunctionalId Default
        {
            get { return m_Default; }
            set
            {
                foreach (Entity entity in Parent.Entities)
                {
                    var materialise = entity.FunctionalId;
                }

                m_Default = value;
            }
        }

        public FunctionalId New(Entity entity, string prefix, IdFormat format = IdFormat.Hash, int startFrom = 0)
        {
            if (Parent.TargetFeatures.FunctionalId == false)
                throw new NotSupportedException("The current graph database does not support this feature");

            return New(entity.Label.Name, prefix, format, startFrom);
        }
        public FunctionalId New(string label, string prefix, IdFormat format = IdFormat.Hash, int startFrom = 0)
        {
            if (Parent.TargetFeatures.FunctionalId == false)
                throw new NotSupportedException("The current graph database does not support this feature");

            if (collection.Any(item => item.Value.Prefix == prefix))
                throw new InvalidOperationException(string.Format("You cannot have multiple FunctionalIds that have the same prefix '{0}'.", prefix));

            FunctionalId value = new FunctionalId(Parent, label, prefix, format, startFrom);
            collection.Add(label, value);

            return value;
        }
    }
}
