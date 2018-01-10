using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.Neo4j.Model
{
    internal class LabelCollection : Core.CollectionBase<Label>
    {
        public Label New(string name)
        {
            Label value = new Label(name);
            Add(name, value);

            return value;
        }
    }
}
