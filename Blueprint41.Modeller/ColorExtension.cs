using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using s = System.Drawing;
using g = Microsoft.Msagl.Drawing;

namespace Blueprint41.Modeller
{
    internal static class ColorExtension
    {
        public static g.Color ToMsAgl(this s.Color @this)
        {
            return new g.Color(@this.A, @this.R, @this.G, @this.B);
        }
    }
}
