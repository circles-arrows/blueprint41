using Blueprint41.Modeller.Schemas;
using Microsoft.Msagl.Drawing;
using Microsoft.Msagl.GraphViewerGdi;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.Modeller.Schemas
{
    public partial class Entity
    {

        protected override void InitializeView()
        {
        }


        // This is a helper properties for relationships
        public string OutEntityReferenceGuid
        {
            get { return Guid; }
        }

        public string InEntityReferenceGuid
        {
            get { return Guid; }
        }
    }
}
