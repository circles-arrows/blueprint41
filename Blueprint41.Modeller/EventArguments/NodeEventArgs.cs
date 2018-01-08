using Microsoft.Msagl.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.Modeller
{
    public class NodeEventArgs : EventArgs
    {
        public Node Node { get; private set; }
        public Microsoft.Msagl.Point Center { get; private set; }
        public NodeEventArgs(Node node, Microsoft.Msagl.Point center)
        {
            Node = node;
            Center = center;
        }
    }
}
