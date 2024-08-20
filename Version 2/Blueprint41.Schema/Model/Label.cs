using System;
using System.Collections.Generic;

namespace Blueprint41.Model
{
    public class Label
    {
        internal Label(string name)
        {
            Name = name;
        }

        #region Properties

        public string Name { get; internal set; }

        #endregion
    }
}
