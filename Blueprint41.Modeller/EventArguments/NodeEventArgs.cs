using Microsoft.Msagl.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrawingNode = Microsoft.Msagl.Drawing.Node;

namespace Blueprint41.Modeller
{
    public class NodeEventArgs : EventArgs
    {
        public IViewerNode Node { get; private set; }
        public Microsoft.Msagl.Core.Geometry.Point Center { get; private set; }
        //public NodeEventArgs(DrawingNode node, Microsoft.Msagl.Core.Geometry.Point center)
        //{
        //    Node = node;
        //    Center = center;
        //}

        public NodeEventArgs(IViewerNode node, Microsoft.Msagl.Core.Geometry.Point center)
        {
            Node = node;
            Center = center;
        }
    }
}
