using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Blueprint41.Modeller
{
    public partial class PrimitiveRelationshipEditor : UserControl
    {
        public DataGridView DataGridViewPrimitive
        {
            get { return this.dataGridViewPrimitive; }
        }

        public DataGridView DataGridViewInheritedPrimitive
        {
            get { return this.dataGridViewInheritedPrimitive; }
        }

        public DataGridView DataGridViewRelationship
        {
            get { return this.dataGridViewRelationship; }
        }

        public DataGridView DataGridViewInheritedRelationship
        {
            get { return this.dataGridViewInheritedRelationship; }
        }

        public CheckBox CheckBoxShowAllRelationship
        {
            get { return this.cbShowAll; }
        }

        public TabControl TabControl
        {
            get { return this.tabControl; }
        }

        public PrimitiveRelationshipEditor()
        {
            InitializeComponent();
        }
    }
}
