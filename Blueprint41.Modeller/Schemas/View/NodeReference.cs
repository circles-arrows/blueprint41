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
    public partial class NodeReference
    {
        protected override void InitializeView()
        {
            OnLabelChanged += delegate (object sender, PropertyChangedEventArgs<string> e)
            {
                if (Model == null && Parent.Model == null)
                    return;

                Parent.CreateEdge();

                Model.AutoResize();
            };
        }
    }
}
