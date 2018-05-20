using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using s = System.Drawing;
using g = Microsoft.Msagl.Drawing;

namespace Blueprint41.Modeller.Schemas
{
    internal static class Styles
    {
        public static readonly g.Color NODE_BGCOLOR_SELECTED = g.Color.Red;
        public static readonly g.Color NODE_BGCOLOR_VIRTUAL  = g.Color.White;
        public static readonly g.Color NODE_BGCOLOR_ABSTRACT = g.Color.LightGreen;
        public static readonly g.Color NODE_BGCOLOR_NORMAL   = g.Color.LightSkyBlue;
        public static readonly g.Color NODE_LINE_COLOR       = g.Color.LightGray;
        public static readonly g.Color RELATION_LINE_COLOR   = g.Color.LightGray;
        public static readonly g.Color RELATION_LABEL_COLOR  = g.Color.Black;

        public static readonly s.Color FORMS_WARNING  = s.Color.Red;
        public static readonly s.Color FORMS_SKY_BLUE = s.Color.LightSkyBlue;
    }
}
