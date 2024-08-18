using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.Async.Neo4j.Model
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
