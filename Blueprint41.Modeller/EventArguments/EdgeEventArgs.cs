using Microsoft.Msagl.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Blueprint41.Modeller
{
    public class EdgeEventArgs : EventArgs
    {
        public Edge Edge { get; private set; }
        public EdgeEventArgs(Edge edge)
        {
            Edge = edge;
        }
    }
}
