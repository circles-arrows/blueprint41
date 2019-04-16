using System.Windows.Forms;

namespace Blueprint41.Modeller
{
    public partial class PrimitiveRelationshipEditor : UserControl
    {
        public DataGridView DataGridViewPrimitive
        {
            get { return this.dataGridViewPrimitive; }
        }

        public DataGridView DataGridViewRelationship
        {
            get { return this.dataGridViewRelationship; }
        }

        public CheckBox CheckBoxShowAllRelationship
        {
            get { return this.cbShowAll; }
        }

        public CheckBox CheckBoxShowFromCurrentModel
        {
            get { return this.cbShowFromCurrentSubModel; }
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
