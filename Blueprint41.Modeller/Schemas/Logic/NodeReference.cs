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
        internal Relationship Parent
        {
            get { return m_Parent; }
            set
            {
                m_Parent = value;
                this.Model = m_Parent?.Model;
            }
        }
        private Relationship m_Parent = null;

        protected override void InitializeLogic()
        {
            OnLabelChanged += delegate (object sender, PropertyChangedEventArgs<string> e)
            {

            };
        }
    }
}
