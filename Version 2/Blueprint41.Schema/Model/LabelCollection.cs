using System;
using System.Collections.Generic;

namespace Blueprint41.Model
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
