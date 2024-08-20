using System;
using System.Collections.Generic;

namespace Blueprint41.Model
{
    internal class RelationshipType
    {
        internal RelationshipType(string name, bool autoIndexing)
        {
            Name = name;
            AutoIndexing = autoIndexing;
        }

        #region Properties

        public string Name { get; private set; }
        public bool AutoIndexing { get; private set; }

        #endregion
    }
}
