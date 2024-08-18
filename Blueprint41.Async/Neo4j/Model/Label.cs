using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.Async.Neo4j.Model
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
