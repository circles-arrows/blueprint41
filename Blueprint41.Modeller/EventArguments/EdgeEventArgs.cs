using DrawingEdge = Microsoft.Msagl.Drawing.Edge;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Blueprint41.Modeller
{
    public class EdgeEventArgs : EventArgs
    {
        public DrawingEdge Edge { get; private set; }
        public EdgeEventArgs(DrawingEdge edge)
        {
            Edge = edge;
        }
    }
}
