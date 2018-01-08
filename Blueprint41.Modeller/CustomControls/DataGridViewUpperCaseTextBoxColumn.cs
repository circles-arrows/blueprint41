using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Blueprint41.Modeller
{
    public class DataGridViewUpperCaseTextBoxColumn : DataGridViewTextBoxColumn
    {
        public DataGridViewUpperCaseTextBoxColumn() : base()
        {
            CellTemplate = new DataGridViewUpperCaseTextBoxCell();
        }
    }

    public class DataGridViewUpperCaseTextBoxCell : DataGridViewTextBoxCell
    {
        public DataGridViewUpperCaseTextBoxCell() : base() { }
        public override Type EditType
        {
            get
            {
                return typeof(DataGridViewUpperCaseTextBoxEditingControl);
            }
        }
    }

    public class DataGridViewUpperCaseTextBoxEditingControl : DataGridViewTextBoxEditingControl
    {
        public DataGridViewUpperCaseTextBoxEditingControl() : base()
        {
            this.CharacterCasing = CharacterCasing.Upper;
        }
    }
}
