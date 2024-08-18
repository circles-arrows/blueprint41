using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.Sync
{
    public class SubModelCollection : Core.CollectionBase<SubModel, DatastoreModel>
    {
        internal SubModelCollection(DatastoreModel parent)
            : base(parent)
        {
            New("Main", 0, false, false);
        }

        public SubModel New(string name)
        {
            int chapter = (collection.Count == 0) ? 0 : collection.Max(item => item.Value.Chapter) + 1;

            return New(name, chapter, true, false);
        }
        public SubModel New(string name, int chapter)
        {
            return New(name, chapter, true, false);
        }
        public SubModel New(string name, int chapter, bool isDraft, bool isLaboratory)
        {
            SubModel value = new SubModel(Parent, name, chapter, isDraft, isLaboratory);
            collection.Add(name, value);

            return value;
        }

        new public void Remove(string name)
        {
            if (name == "Main")
                throw new InvalidOperationException("You cannot remove the 'Main' SubModel.");

            base.Remove(name);
        }
    }
}
