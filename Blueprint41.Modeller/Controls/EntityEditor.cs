using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Blueprint41.Modeller.Schemas;
using Model = Blueprint41.Modeller.Schemas.Modeller;
using System.Collections.Specialized;
using System.Collections;

namespace Blueprint41.Modeller
{
    public partial class EntityEditor : UserControl
    {
        public event EventHandler<EventArgs> EntityTypeChanged;

        private bool isFromCmbInherits = false;

        private bool showAllRelationships = false;

        public Entity Entity { get; private set; }

        public Model StorageModel { get; private set; }

        public string EntityNodeName
        {
            get
            {
                if (StorageModel == null || string.IsNullOrEmpty(StorageModel.Type))
                    return "Entity";

                ModellerType currentModeller = (ModellerType)Enum.Parse(typeof(ModellerType), StorageModel.Type);
                return currentModeller == ModellerType.Blueprint41 ? "Entity" : "Node";
            }
        }

        public DataGridViewComboBoxColumn SourceEntitiesColumn { get; private set; }
        public DataGridViewComboBoxColumn TargetEntitiesColumn { get; private set; }

        private ObservableCollection<GridPrimitiveItem> primitiveObservable;
        private Dictionary<string, Primitive> primitiveLookUp
        {
            get { return Entity.Primitive.ToDictionary(x => x.Name); }
        }

        private ObservableCollection<Relationship> relationshipsObservable;

        private Dictionary<string, Relationship> relationshipLookUp
        {
            get { return Entity.GetRelationships(RelationshipDirection.Both, false).ToDictionary(x => x.Name); }
        }

        public DataTable FunctionalIdDataTable { get; set; }

        public bool IsEditable
        {
            get { return gbProperties.Enabled && pre.Enabled; }
        }

        public ComboBox FunctionalIdComboBox
        {
            get
            {
                return this.cmbFunctionalId;
            }
            set
            {
                this.cmbFunctionalId = value;
            }
        }

        public int DataGridMaxWidth
        {
            get
            {
                int propertiesWidth = pre.DataGridViewPrimitive.Columns.GetColumnsWidth(DataGridViewElementStates.None);
                int relationshipsWidth = pre.DataGridViewRelationship.Columns.GetColumnsWidth(DataGridViewElementStates.None);

                if (pre.TabControl.SelectedIndex == 0)
                    return propertiesWidth + pre.DataGridViewPrimitive.RowHeadersWidth;

                return relationshipsWidth + pre.DataGridViewRelationship.RowHeadersWidth;
            }
        }

        public EntityEditor()
        {
            InitializeComponent();
            //CreateGridColumnsForPrimitiveProperties(pre.DataGridViewPrimitive);
            //CreateGridColumnsForRelationships();
            CreateToolTipForShowAllRelationshipsCheckbox();

            pre.DataGridViewPrimitive.MultiSelect = false;
            pre.DataGridViewRelationship.MultiSelect = false;
            pre.DataGridViewPrimitive.DefaultCellStyle.SelectionBackColor = Styles.FORMS_SKY_BLUE;
            pre.DataGridViewRelationship.DefaultCellStyle.SelectionBackColor = Styles.FORMS_SKY_BLUE;

            pre.DataGridViewPrimitive.CellMouseClick += DataGridViewPrimitive_CellMouseClick;
            pre.DataGridViewPrimitive.DataSourceChanged += DataGridViewPrimitive_DataSourceChanged;
            pre.DataGridViewPrimitive.UserDeletingRow += DataGridViewPrimitive_UserDeletingRow;
            pre.DataGridViewPrimitive.CellValueChanged += dataGridViewPrimitiveProperties_CellValueChanged;
            pre.DataGridViewPrimitive.DefaultValuesNeeded += dataGridViewPrimitiveProperties_DefaultValuesNeeded;
            pre.DataGridViewPrimitive.KeyDown += dataGridViewPrimitiveProperties_KeyDown;
            pre.DataGridViewPrimitive.RowLeave += DataGridViewPrimitive_RowLeave;
            pre.DataGridViewPrimitive.CellLeave += DataGridViewPrimitive_CellLeave;

            pre.DataGridViewRelationship.CellMouseClick += DataGridViewPrimitive_CellMouseClick;
            pre.DataGridViewRelationship.DataSourceChanged += DataGridViewRelationship_DataSourceChanged;
            pre.DataGridViewRelationship.UserDeletingRow += DataGridViewRelationship_UserDeletingRow;
            pre.DataGridViewRelationship.Leave += DataGridViewRelationship_Leave;
            pre.DataGridViewRelationship.CellValueChanged += DataGridViewRelationships_CellValueChanged;

            pre.CheckBoxShowAllRelationship.CheckedChanged += checkBoxShowAllRelationships_CheckedChanged;
            pre.CheckBoxShowFromCurrentModel.CheckedChanged += checkBoxShowFromCurrentModel_CheckedChanged;

            pre.Enabled = false;
            gbProperties.Enabled = false;
        }

        private void DataGridViewPrimitive_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            ValidatePrimitiveProperties(e.RowIndex, e.ColumnIndex);
        }

        private void DataGridViewPrimitive_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            ValidatePrimitiveProperties(e.RowIndex, e.ColumnIndex);
        }

        #region Primitive Event Handlers

        private void DataGridViewPrimitive_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            if (e.Row.DataBoundItem is GridPrimitiveItem myPrim && primitiveLookUp.ContainsKey(myPrim.Item.Name) == false)
                e.Cancel = true;
        }

        private void DataGridViewPrimitive_DataSourceChanged(object sender, EventArgs e)
        {
            DataGridView dataGrid = (DataGridView)sender;

            for (int x = 0; x < dataGrid.RowCount; x++)
            {
                DataGridViewRow row = dataGrid.Rows[x];

                if (row.DataBoundItem is GridPrimitiveItem myPrim && primitiveLookUp.ContainsKey(myPrim.Item.Name) == false)
                {
                    row.ReadOnly = true;
                    row.DefaultCellStyle.BackColor = Color.DarkGray;
                    row.DefaultCellStyle.SelectionBackColor = Color.DarkGray;
                    row.DefaultCellStyle.ForeColor = Color.Black;
                    row.DefaultCellStyle.SelectionForeColor = Color.Black;
                }
            }
        }

        private void DataGridViewPrimitive_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (ParentForm is MainForm main)
                main.DefaultOrExpandPropertiesWidth(true);
        }

        private void dataGridViewPrimitiveProperties_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            e.Row.Cells["Type"].Value = "string";
            e.Row.Cells["Index"].Value = PropertyIndex.None.ToString();
        }

        private void dataGridViewPrimitiveProperties_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            ValidatePrimitiveProperties(e.RowIndex, e.ColumnIndex);
        }

        private void ValidatePrimitiveProperties(int rowIndex, int columnIndex)
        {
            if (columnIndex == 1)
            {
                DataGridViewRow currentRow = pre.DataGridViewPrimitive.Rows[rowIndex];

                DataGridViewTextBoxCell textBox = (DataGridViewTextBoxCell)pre.DataGridViewPrimitive.Rows[rowIndex].Cells[1];

                if (!textBox.IsInEditMode)
                    return;

                if (currentRow.IsNewRow && textBox.IsInEditMode && string.IsNullOrEmpty(textBox.EditedFormattedValue?.ToString()))
                    return;

                if (currentRow.IsNewRow == false && textBox.IsInEditMode && textBox.Value?.ToString() == textBox.EditedFormattedValue?.ToString().Trim())
                    return;

                //string propertyNameValue = textBox.Value?.ToString() ?? textBox.EditedFormattedValue?.ToString();
                string propertyNameValue = textBox.EditedFormattedValue?.ToString();

                string newName = propertyNameValue?.Replace(" ", string.Empty);
                ValidatePrimitivePropertyName(Entity, newName, out string validateName, 1, true);

                pre.DataGridViewPrimitive.CellValueChanged -= dataGridViewPrimitiveProperties_CellValueChanged;
                pre.DataGridViewPrimitive.RowLeave -= DataGridViewPrimitive_RowLeave;
                pre.DataGridViewPrimitive.CellLeave -= DataGridViewPrimitive_CellLeave;
                textBox.Value = validateName;

                pre.DataGridViewPrimitive.CellValueChanged += dataGridViewPrimitiveProperties_CellValueChanged;
                pre.DataGridViewPrimitive.RowLeave += DataGridViewPrimitive_RowLeave;
                pre.DataGridViewPrimitive.CellLeave += DataGridViewPrimitive_CellLeave;
            }
        }

        #endregion

        #region Relationship Event Handlers

        private void DataGridViewRelationship_DataSourceChanged(object sender, EventArgs e)
        {
            DataGridView dataGrid = (DataGridView)sender;
            Dictionary<string, Relationship> lookUp = relationshipLookUp;

            for (int x = 0; x < dataGrid.RowCount; x++)
            {
                DataGridViewRow row = dataGrid.Rows[x];

                if (row.DataBoundItem is Relationship rel && lookUp.ContainsKey(rel.Name) == false)
                {
                    row.ReadOnly = true;
                    row.DefaultCellStyle.BackColor = Color.DarkGray;
                    row.DefaultCellStyle.SelectionBackColor = Color.DarkGray;
                    row.DefaultCellStyle.ForeColor = Color.Black;
                    row.DefaultCellStyle.SelectionForeColor = Color.Black;
                }
            }
        }

        private void DataGridViewRelationship_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            if (e.Row.DataBoundItem is Relationship rel && rel.Name != null && relationshipLookUp.ContainsKey(rel.Name) == false)
                e.Cancel = true;
        }

        private void DataGridViewRelationships_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            bool hasErrors = false;

            DataGridViewRow row = pre.DataGridViewRelationship.Rows[e.RowIndex];

            int columnIndex = 0;
            while (columnIndex < 7)
            {
                DataGridViewCell cell = row.Cells[columnIndex];

                if (string.IsNullOrEmpty(cell.Value?.ToString()))
                {
                    cell.ErrorText = "Required";
                    hasErrors = true;
                }
                else
                    cell.ErrorText = null;

                columnIndex++;
            }

            if (hasErrors)
                return;

            if (e.ColumnIndex == 0)
            {
                DataGridViewComboBoxCell cb = (DataGridViewComboBoxCell)pre.DataGridViewRelationship.Rows[e.RowIndex].Cells[0];
                relationshipsObservable[e.RowIndex].Source.ReferenceGuid = StorageModel.Entities.Entity.Where(x => x.Label == (string)cb.Value).SingleOrDefault()?.Guid;
            }

            if (e.ColumnIndex == 1 || e.ColumnIndex == 7)
            {
                DataGridViewTextBoxCell textBox = (DataGridViewTextBoxCell)pre.DataGridViewRelationship.Rows[e.RowIndex].Cells[e.ColumnIndex];
                string textBoxValue = textBox.Value?.ToString();
                textBoxValue = textBoxValue?.Replace(" ", string.Empty);

                if (e.ColumnIndex == 7)
                {
                    DataGridViewComboBoxCell cb = (DataGridViewComboBoxCell)pre.DataGridViewRelationship.Rows[e.RowIndex].Cells[8];

                    if (cb.Value == null)
                    {
                        textBox.Value = null;
                        return;
                    }
                }

                if (string.IsNullOrEmpty(textBoxValue))
                {
                    ShowMessageAndResetTextBoxValue("Property name cannot be empty.", textBox);
                    return;
                }

                textBox.Value = textBoxValue;
            }

            if (e.ColumnIndex == 6)
            {
                DataGridViewComboBoxCell cb = (DataGridViewComboBoxCell)pre.DataGridViewRelationship.Rows[e.RowIndex].Cells[6];
                relationshipsObservable[e.RowIndex].Target.ReferenceGuid = StorageModel.Entities.Entity.Where(x => x.Label == (string)cb.Value).SingleOrDefault()?.Guid;
            }

            if (e.ColumnIndex == 8)
            {
                DataGridViewComboBoxCell cb = (DataGridViewComboBoxCell)pre.DataGridViewRelationship.Rows[e.RowIndex].Cells[e.ColumnIndex];
                if (cb.Value == null)
                {
                    DataGridViewTextBoxCell textBox = (DataGridViewTextBoxCell)pre.DataGridViewRelationship.Rows[e.RowIndex].Cells[7];
                    textBox.Value = string.Empty;
                }
            }

            if (e.ColumnIndex == 1 || e.ColumnIndex == 4 || e.ColumnIndex == 5 || e.ColumnIndex == 7)
            {
                DataGridViewTextBoxCell textBox = (DataGridViewTextBoxCell)pre.DataGridViewRelationship.Rows[e.RowIndex].Cells[e.ColumnIndex];
                string newName = ((string)textBox.Value)?.Replace(" ", string.Empty);
                textBox.Value = newName;

                if (!string.IsNullOrEmpty(newName) && e.ColumnIndex == 5)
                    relationshipsObservable[e.RowIndex].RenameEdge();
            }
        }

        private void DataGridViewRelationship_Leave(object sender, EventArgs e)
        {
            List<DataGridViewRow> rows = new List<DataGridViewRow>();

            foreach (DataGridViewRow row in pre.DataGridViewRelationship.Rows)
            {
                int columnIndex = 0;
                while (columnIndex < 7)
                {
                    DataGridViewCell cell = row.Cells[columnIndex];

                    if (string.IsNullOrEmpty(cell.Value?.ToString()))
                    {
                        if (!row.IsNewRow)
                        {
                            if (!rows.Contains(row))
                                rows.Add(row);

                            cell.ErrorText = "Required";
                        }
                    }
                    else
                        cell.ErrorText = null;

                    columnIndex++;
                }
            }

            if (rows.Count > 0)
            {
                rows.ForEach(row =>
                {
                    if (!row.IsNewRow)
                        pre.DataGridViewRelationship.Rows.Remove(row);
                });

                pre.DataGridViewRelationship.CancelEdit();
            }
        }

        #endregion

        private void CreateToolTipForShowAllRelationshipsCheckbox()
        {
            // Create the ToolTip and associate with the Form container.
            ToolTip toolTip1 = new ToolTip();

            // Set up the delays for the ToolTip.
            toolTip1.AutoPopDelay = 5000;
            toolTip1.InitialDelay = 100;
            toolTip1.ReshowDelay = 500;

            // Set up the ToolTip text
            toolTip1.SetToolTip(pre.CheckBoxShowAllRelationship, "Also shows relationships to entities outside the current submodel.");
        }

        private void CreateGridColumnsForPrimitiveProperties(DataGridView dataGridView, ModellerType modellerType, bool readOnly = false)
        {
            // Initialize the DataGridView.
            dataGridView.AutoGenerateColumns = false;
            dataGridView.AutoSize = true;

            DataGridViewColumn entityNameColumn = new DataGridViewTextBoxColumn();
            entityNameColumn.DataPropertyName = "EntityName";
            entityNameColumn.Name = $"{EntityNodeName} Name";
            entityNameColumn.ReadOnly = true;
            entityNameColumn.DefaultCellStyle.BackColor = readOnly ? Color.LightGray : Color.White;
            entityNameColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            entityNameColumn.DefaultCellStyle.Font = new Font(dataGridView.Font, FontStyle.Bold);
            entityNameColumn.Resizable = DataGridViewTriState.False;
            entityNameColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView.Columns.Add(entityNameColumn);

            DataGridViewColumn nameColumn = new DataGridViewTextBoxColumn();
            nameColumn.DataPropertyName = "Name";
            nameColumn.Name = "Name";
            nameColumn.ReadOnly = readOnly;
            nameColumn.DefaultCellStyle.BackColor = readOnly ? Color.LightGray : Color.White;
            dataGridView.Columns.Add(nameColumn);

            DataGridViewColumn keyColumn = new DataGridViewCheckBoxColumn();
            if (readOnly)
                keyColumn = new DataGridViewTextBoxColumn();

            keyColumn.DataPropertyName = "IsKey";
            keyColumn.Name = "Is Key";
            keyColumn.ReadOnly = readOnly;
            keyColumn.DefaultCellStyle.BackColor = readOnly ? Color.LightGray : Color.White;
            dataGridView.Columns.Add(keyColumn);

            DataGridViewColumn nullableColumn = new DataGridViewCheckBoxColumn();
            if (readOnly)
                nullableColumn = new DataGridViewTextBoxColumn();

            nullableColumn.DataPropertyName = "Nullable";
            nullableColumn.Name = "Optional";
            nullableColumn.ReadOnly = readOnly;
            nullableColumn.DefaultCellStyle.BackColor = readOnly ? Color.LightGray : Color.White;
            dataGridView.Columns.Add(nullableColumn);

            DataGridViewColumn typeColumn = new DataGridViewTextBoxColumn();
            if (!readOnly)
            {
                typeColumn = new DataGridViewComboBoxColumn();
                ((DataGridViewComboBoxColumn)typeColumn).Items.Add("string");
                ((DataGridViewComboBoxColumn)typeColumn).Items.Add("bool");
                ((DataGridViewComboBoxColumn)typeColumn).Items.Add("long");
                ((DataGridViewComboBoxColumn)typeColumn).Items.Add("int");
                ((DataGridViewComboBoxColumn)typeColumn).Items.Add("float");
                ((DataGridViewComboBoxColumn)typeColumn).Items.Add("sbyte");

                if (modellerType == ModellerType.Blueprint41)
                {
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("short");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("DateTime");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("decimal");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("double");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("char");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Guid");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("object");
                }

                ((DataGridViewComboBoxColumn)typeColumn).Items.Add("List<string>");
                ((DataGridViewComboBoxColumn)typeColumn).Items.Add("List<bool>");
                ((DataGridViewComboBoxColumn)typeColumn).Items.Add("List<long>");
                ((DataGridViewComboBoxColumn)typeColumn).Items.Add("List<int>");
                ((DataGridViewComboBoxColumn)typeColumn).Items.Add("List<float>");
                ((DataGridViewComboBoxColumn)typeColumn).Items.Add("List<sbyte>");

                if (modellerType == ModellerType.Blueprint41)
                {
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("List<short>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("List<DateTime>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("List<decimal>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("List<double>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("List<char>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("List<Guid>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("List<object>");
                }

                if (modellerType == ModellerType.Blueprint41)
                {
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<string,string>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<string,DateTime>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<string,bool>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<string,long>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<string,short>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<string,int>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<string,decimal>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<string,float>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<string,double>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<string,char>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<string,sbyte>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<string,Guid>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<string,object>");

                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<DateTime,string>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<DateTime,DateTime>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<DateTime,bool>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<DateTime,long>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<DateTime,short>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<DateTime,int>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<DateTime,decimal>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<DateTime,float>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<DateTime,double>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<DateTime,char>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<DateTime,sbyte>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<DateTime,Guid>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<DateTime,object>");

                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<bool,string>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<bool,DateTime>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<bool,bool>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<bool,long>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<bool,short>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<bool,int>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<bool,decimal>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<bool,float>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<bool,double>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<bool,char>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<bool,sbyte>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<bool,Guid>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<bool,object>");

                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<long,string>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<long,DateTime>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<long,bool>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<long,long>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<long,short>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<long,int>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<long,decimal>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<long,float>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<long,double>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<long,char>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<long,sbyte>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<long,Guid>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<long,object>");

                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<short,string>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<short,DateTime>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<short,bool>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<short,long>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<short,short>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<short,int>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<short,decimal>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<short,float>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<short,double>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<short,char>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<short,sbyte>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<short,Guid>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<short,object>");

                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<int,string>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<int,DateTime>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<int,bool>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<int,long>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<int,short>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<int,int>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<int,decimal>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<int,float>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<int,double>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<int,char>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<int,sbyte>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<int,Guid>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<int,object>");

                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<decimal,string>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<decimal,DateTime>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<decimal,bool>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<decimal,long>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<decimal,short>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<decimal,int>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<decimal,decimal>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<decimal,float>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<decimal,double>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<decimal,char>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<decimal,sbyte>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<decimal,Guid>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<decimal,object>");

                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<float,string>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<float,DateTime>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<float,bool>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<float,long>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<float,short>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<float,int>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<float,decimal>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<float,float>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<float,double>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<float,char>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<float,sbyte>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<float,Guid>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<float,object>");

                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<double,string>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<double,DateTime>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<double,bool>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<double,long>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<double,short>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<double,int>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<double,decimal>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<double,float>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<double,double>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<double,char>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<double,sbyte>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<double,Guid>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<double,object>");

                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<char,string>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<char,DateTime>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<char,bool>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<char,long>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<char,short>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<char,int>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<char,decimal>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<char,float>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<char,double>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<char,char>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<char,sbyte>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<char,Guid>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<char,object>");

                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<sbyte,string>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<sbyte,DateTime>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<sbyte,bool>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<sbyte,long>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<sbyte,short>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<sbyte,int>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<sbyte,decimal>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<sbyte,float>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<sbyte,double>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<sbyte,char>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<sbyte,sbyte>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<sbyte,Guid>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<sbyte,object>");

                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<Guid,string>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<Guid,DateTime>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<Guid,bool>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<Guid,long>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<Guid,short>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<Guid,int>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<Guid,decimal>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<Guid,float>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<Guid,double>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<Guid,char>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<Guid,sbyte>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<Guid,Guid>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<Guid,object>");

                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<object,string>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<object,DateTime>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<object,bool>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<object,long>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<object,short>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<object,int>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<object,decimal>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<object,float>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<object,double>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<object,char>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<object,sbyte>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<object,Guid>");
                    ((DataGridViewComboBoxColumn)typeColumn).Items.Add("Dictionary<object,object>");
                }
            }

            typeColumn.DataPropertyName = "Type";
            typeColumn.Name = "Type";
            typeColumn.ReadOnly = readOnly;
            typeColumn.DefaultCellStyle.BackColor = readOnly ? Color.LightGray : Color.White;
            dataGridView.Columns.Add(typeColumn);

            DataGridViewColumn indexTypeColumn = new DataGridViewTextBoxColumn();
            if (!readOnly)
            {
                indexTypeColumn = new DataGridViewComboBoxColumn();
                ((DataGridViewComboBoxColumn)indexTypeColumn).Items.Add(PropertyIndex.None.ToString());
                ((DataGridViewComboBoxColumn)indexTypeColumn).Items.Add(PropertyIndex.Indexed.ToString());
                ((DataGridViewComboBoxColumn)indexTypeColumn).Items.Add(PropertyIndex.Unique.ToString());
            }

            indexTypeColumn.DataPropertyName = "Index";
            indexTypeColumn.Name = "Index";
            indexTypeColumn.ReadOnly = readOnly;
            indexTypeColumn.DefaultCellStyle.BackColor = readOnly ? Color.LightGray : Color.White;
            dataGridView.Columns.Add(indexTypeColumn);
        }

        private void CreateGridColumnsForRelationships(ModellerType type)
        {
            // Initialize the DataGridView.
            pre.DataGridViewRelationship.AutoGenerateColumns = false;
            pre.DataGridViewRelationship.AutoSize = true;

            SourceEntitiesColumn = new DataGridViewComboBoxColumn();
            SourceEntitiesColumn.DataPropertyName = "InEntity";
            SourceEntitiesColumn.Name = "IN Entity";
            pre.DataGridViewRelationship.Columns.Add(SourceEntitiesColumn);

            DataGridViewColumn sourceNameColumn = new DataGridViewTextBoxColumn();
            sourceNameColumn.DataPropertyName = "InProperty";
            sourceNameColumn.Name = "Property";
            sourceNameColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            pre.DataGridViewRelationship.Columns.Add(sourceNameColumn);

            DataGridViewComboBoxColumn sourceTypeColumn = new DataGridViewComboBoxColumn();
            sourceTypeColumn.Items.Add("");
            sourceTypeColumn.Items.Add(PropertyType.Lookup.ToString());
            sourceTypeColumn.Items.Add(PropertyType.Collection.ToString());
            sourceTypeColumn.DataPropertyName = "InPropertyType";
            sourceTypeColumn.Name = "Type";
            pre.DataGridViewRelationship.Columns.Add(sourceTypeColumn);

            DataGridViewCheckBoxColumn sourceNullableColumn = new DataGridViewCheckBoxColumn();
            sourceNullableColumn.DataPropertyName = "InNullable";
            sourceNullableColumn.Name = "Optional";
            pre.DataGridViewRelationship.Columns.Add(sourceNullableColumn);

            DataGridViewUpperCaseTextBoxColumn relationshipNameColumn = new DataGridViewUpperCaseTextBoxColumn();
            relationshipNameColumn.DataPropertyName = "Name";
            relationshipNameColumn.Name = "Relationship Name";
            relationshipNameColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            pre.DataGridViewRelationship.Columns.Add(relationshipNameColumn);

            DataGridViewUpperCaseTextBoxColumn neo4jNameColumn = new DataGridViewUpperCaseTextBoxColumn();
            neo4jNameColumn.DataPropertyName = "Type";
            neo4jNameColumn.Name = "Neo4j Name";
            neo4jNameColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            pre.DataGridViewRelationship.Columns.Add(neo4jNameColumn);

            TargetEntitiesColumn = new DataGridViewComboBoxColumn();
            TargetEntitiesColumn.DataPropertyName = "OutEntity";
            TargetEntitiesColumn.Name = "OUT Entity";
            pre.DataGridViewRelationship.Columns.Add(TargetEntitiesColumn);

            DataGridViewColumn targetNameColumn = new DataGridViewTextBoxColumn();
            targetNameColumn.DataPropertyName = "OutProperty";
            targetNameColumn.Name = "Property";
            pre.DataGridViewRelationship.Columns.Add(targetNameColumn);

            DataGridViewComboBoxColumn targetTypeColumn = new DataGridViewComboBoxColumn();
            targetTypeColumn.Items.Add("");
            targetTypeColumn.Items.Add(PropertyType.Lookup.ToString());
            targetTypeColumn.Items.Add(PropertyType.Collection.ToString());
            targetTypeColumn.DataPropertyName = "OutPropertyType";
            targetTypeColumn.Name = "Type";
            pre.DataGridViewRelationship.Columns.Add(targetTypeColumn);

            DataGridViewCheckBoxColumn targetNullableColumn = new DataGridViewCheckBoxColumn();
            targetNullableColumn.DataPropertyName = "OutNullable";
            targetNullableColumn.Name = "Optional";
            targetNullableColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            pre.DataGridViewRelationship.Columns.Add(targetNullableColumn);
        }

        private void InitializeGridColumnsForType()
        {
            bool success = Enum.TryParse<ModellerType>(StorageModel.Type, out ModellerType modellerType);
            modellerType = success ? modellerType : ModellerType.Blueprint41;

            pre.DataGridViewPrimitive.Columns.Clear();
            pre.DataGridViewRelationship.Columns.Clear();

            CreateGridColumnsForPrimitiveProperties(pre.DataGridViewPrimitive, modellerType, false);
            CreateGridColumnsForRelationships(modellerType);
        }

        private class GridPrimitiveItem
        {
            public GridPrimitiveItem()
            {
                Item = new Primitive();
            }

            public GridPrimitiveItem(string entityName, Primitive item)
            {
                Item = item;
                EntityName = entityName;
            }

            public Primitive Item { get; }
            public string EntityName { get; set; }

            public string Name
            {
                get { return Item.Name; }
                set { Item.Name = value; }
            }

            public bool IsKey
            {
                get { return Item.IsKey; }
                set { Item.IsKey = value; }
            }

            public bool Nullable
            {
                get { return Item.Nullable; }
                set { Item.Nullable = value; }
            }

            public string Type
            {
                get { return Item.Type; }
                set { Item.Type = value; }
            }

            public string Index
            {
                get { return Item.Index; }
                set { Item.Index = value; }
            }
        }

        private Collection<GridPrimitiveItem> GetPrimitivesOfSelfAndBaseTypes(Entity entity)
        {
            Collection<GridPrimitiveItem> inheritedPrimitives = new Collection<GridPrimitiveItem>();
            Entity current = entity;

            if (current == null)
                return null;
            do
            {
                foreach (Primitive primitive in current.Primitive)
                    inheritedPrimitives.Add(new GridPrimitiveItem(current.Name, primitive));

                current = current.ParentEntity;

            } while (current != null);

            return inheritedPrimitives;
        }

        private Collection<Relationship> GetRelationShipsOfSelfAndBaseWithinSubmodel(Entity entity, bool showAll = false)
        {
            Collection<Relationship> inheritedRelationships = new Collection<Relationship>();
            Entity current = entity;

            if (current == null)
                return null;

            if (showAll)
                foreach (Relationship item in StorageModel.GetRelationships(entity, true))
                    inheritedRelationships.Add(item);
            else
                foreach (Relationship item in current.GetRelationships(StorageModel.DisplayedSubmodel, RelationshipDirection.Both, true))
                    inheritedRelationships.Add(item);

            return inheritedRelationships;
        }

        private void Assign()
        {
            cmbInherits.DataBindings.Clear();

            // Primitive
            UpdatePrimitiveGridView();

            // Relationships
            UpdateRelationshipGridView();

            bindingSourceEntities.DataSource = null;
            bindingSourceEntities.DataSource = StorageModel.Entities.Entity.OrderBy(x => x.Label);

            SourceEntitiesColumn.DataSource = null;
            SourceEntitiesColumn.DataSource = bindingSourceEntities;
            SourceEntitiesColumn.DisplayMember = "Label";
            SourceEntitiesColumn.ValueMember = "Label";

            TargetEntitiesColumn.DataSource = null;
            TargetEntitiesColumn.DataSource = bindingSourceEntities;
            TargetEntitiesColumn.DisplayMember = "Label";
            TargetEntitiesColumn.ValueMember = "Label";

            List<EntityComboBoxItem> entityItems = StorageModel.Entities.Entity.Where(e => e.Abstract && e.Label != Entity.Label).Select(x => new EntityComboBoxItem() { Display = x.Label, Value = x }).ToList();
            bindingSource.DataSource = Entity;
            cmbInherits.SetDataSource(ref entityItems, true);

            EntityComboBoxItem inherited = entityItems.Where(item => (item.Value as Entity)?.Guid == Entity.Inherits).FirstOrDefault();

            if (inherited != null)
                cmbInherits.SelectedItem = inherited;

            cmbInherits.SelectedIndexChanged += CmbInherits_SelectedIndexChanged;

            // FunctionalId Combobox
            List<EntityComboBoxItem> functionalIDs = StorageModel.FunctionalIds.FunctionalId.Where(x => x.Guid == Entity.Guid || !string.IsNullOrEmpty(x.Name)).OrderBy(x => x.Name)
                                                    .Select(x => new EntityComboBoxItem() { Display = string.Concat(x.Name ?? Entity.Label, " - ", x.Value), Value = x.Guid }).ToList();

            cmbFunctionalId.Enabled = !Entity.Virtual;
            cmbFunctionalId.SetDataSource(ref functionalIDs, true);

            FunctionalId entityFunctionalId = StorageModel.FunctionalIds.FunctionalId.Where(item => item.Guid == Entity.FunctionalId).SingleOrDefault();

            if (entityFunctionalId != null)
            {
                string displayName = string.Concat(entityFunctionalId.Name ?? Entity.Label, " - ", entityFunctionalId.Value);
                cmbFunctionalId.SelectedIndex = cmbFunctionalId.FindStringExact(displayName);
            }
            else
            {
                cmbFunctionalId.SelectedIndex = 0;
            }

            cmbFunctionalId.SelectedIndexChanged += cmbFunctionalId_SelectedIndexChanged;
            Entity.OnLabelChangeCancelled += Entity_OnLabelChangeCancelled;
            Entity.OnNameChangeCancelled += Entity_OnNameChangeCancelled;
        }

        void RemoveRelationships(Relationship item)
        {
            DialogResult dialogResult;

            if (item.InEntity == null || item.OutEntity == null ||
                string.IsNullOrEmpty(item.Name) || string.IsNullOrEmpty(item.Type) ||
                string.IsNullOrEmpty(item.InProperty))
                dialogResult = DialogResult.Yes;
            else
                dialogResult = MessageBox.Show($"Are you sure you want to delete the relationship '{item.Source.Label}->[{item.Name}]->{item.Target.Label}' from storage?", "WARNING!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dialogResult == DialogResult.Yes)
            {
                if (StorageModel.Relationships.Relationship.Contains(item))
                    StorageModel.RemoveRelationship(item);
            }
            else
                UpdateRelationshipGridView();
        }

        void RemovePrimitive(Primitive item)
        {
            DialogResult dialogResult;

            if (string.IsNullOrEmpty(item.Name))
                dialogResult = DialogResult.Yes;
            else
                dialogResult = MessageBox.Show($"Are you sure you want to delete '{item.Name}' from storage?", "WARNING!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dialogResult == DialogResult.Yes)
            {
                RemoveRecordPropertyAndFromChildEntity(item);
                Entity.Primitive.Remove(item);
            }
            else
                UpdatePrimitiveGridView();
        }

        private void RemoveRecordPropertyAndFromChildEntity(Primitive item)
        {
            RemoveRecordProperties(Entity, item);

            List<Entity> childEntities = Entity.GetChildStaticEntities();
            foreach (Entity e in childEntities)
                RemoveRecordProperties(e, item);
        }

        private void RemoveRecordProperties(Entity entity, Primitive item)
        {
            if (entity.IsStaticData == false)
                return;

            foreach (Record record in entity.StaticData.Records.Record)
                foreach (Property prop in record.Property.ToList())
                    if (prop.PropertyGuid == item.Guid)
                        record.Property.Remove(prop);
        }

        private void DataGridViewRelationship_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (pre.DataGridViewRelationship.IsCurrentCellDirty)
            {
                //pre.DataGridViewRelationship.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void CmbInherits_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbInherits.SelectedItem == null)
                return;

            if (cmbInherits.SelectedItem is EntityComboBoxItem item && item.Value is Entity entity && entity.Guid == Entity.Inherits)
                return;

            // why remove all the edges?
            //StorageModel.RemoveAllEdges(Entity);

            this.isFromCmbInherits = true;
            if (cmbInherits.SelectedItem is EntityComboBoxItem cmbitem)
                Entity.Inherits = (cmbitem.Value as Entity)?.Guid;
        }

        private void BaseEntityBinding_BindingComplete(object sender, BindingCompleteEventArgs e)
        {
            //TODO: Draw all inherited relationships (if ShowInheritedRelationships flag is toggled)
            if (this.isFromCmbInherits)
            {
                StorageModel.CreateAllEdges(Entity);
                this.isFromCmbInherits = false;
            }
        }

        private void Entity_OnLabelChangeCancelled(object sender, PropertyChangedEventArgs<string> e)
        {
            txtLabel.Text = Entity.Label;
        }

        private void Entity_OnNameChangeCancelled(object sender, PropertyChangedEventArgs<string> e)
        {
            txtName.Text = Entity.Name;
        }

        public void Show(Entity model, Model modeller)
        {
            Entity = model;
            StorageModel = modeller;

            InitializeGridColumnsForType();

            ClearDataSourceAndHandlers();
            gbProperties.Enabled = true;
            pre.Enabled = true;
            pre.SetToReadOnly(false);

            Assign();

            bool success = Enum.TryParse<ModellerType>(StorageModel.Type, out ModellerType modellerType);
            modellerType = success ? modellerType : ModellerType.Blueprint41;

            EnableDisableControlsForType(modellerType);
        }

        public void EnableDisableControlsForType(ModellerType modellerType)
        {
            lblEntityName.Text = $"{EntityNodeName} Name:";

            if (modellerType == ModellerType.Blueprint41)
            {
                cmbInherits.Visible = true;
                cmbFunctionalId.Visible = true;
                btnAddFunctionalId.Visible = true;
                chkIsAbstract.Visible = true;
                chkIsVirtual.Visible = true;
                chkIsStaticData.Visible = true;

                lblBaseEntity.Visible = true;
                lblFunctionaId.Visible = true;
                lblAbstract.Visible = true;
                lblVirtual.Visible = true;
                lblStaticData.Visible = true;
            }
            else
            {
                cmbInherits.Visible = false;
                cmbFunctionalId.Visible = false;
                btnAddFunctionalId.Visible = false;
                chkIsAbstract.Visible = false;
                chkIsVirtual.Visible = false;
                chkIsStaticData.Visible = false;

                lblBaseEntity.Visible = false;
                lblFunctionaId.Visible = false;
                lblAbstract.Visible = false;
                lblVirtual.Visible = false;
                lblStaticData.Visible = false;
            }
        }

        public void RefreshEntity()
        {
            Show(Entity, StorageModel);
        }

        public void CloseEditor()
        {
            gbProperties.Enabled = false;
            ClearDataSourceAndHandlers();
            Entity = null;
            bindingSource.DataSource = typeof(Entity);
            cmbInherits.DataSource = null;
            cmbInherits.SelectedIndex = -1;
            cmbFunctionalId.DataSource = null;
            cmbFunctionalId.SelectedIndex = -1;
            btnEditStaticData.Visible = false;
            pre.SetToReadOnly();
        }

        public void ClearDataSourceAndHandlers()
        {
            bindingSourcePrimitiveProperties.DataSource = null;
            bindingSourceInheritedPrimitiveProperties.DataSource = null;
            bindingSourceCollectionProperties.DataSource = null;
            bindingSourceInheritedRelationships.DataSource = null;
            cmbFunctionalId.SelectedIndexChanged -= cmbFunctionalId_SelectedIndexChanged;
            cmbInherits.SelectedIndexChanged -= CmbInherits_SelectedIndexChanged;

            if (Entity != null)
            {
                Entity.OnLabelChangeCancelled -= Entity_OnLabelChangeCancelled;
                Entity.OnNameChangeCancelled -= Entity_OnNameChangeCancelled;
            }
        }

        internal void Reload()
        {
            if (Entity == null)
                return;

            ClearDataSourceAndHandlers();
            Assign();
        }

        //private void btnApply_Click(object sender, EventArgs e)
        //{

        //    //Entity.Complex.Clear();
        //    //foreach (Complex item in (IEnumerable<Complex>)bindingSourceCollectionProperties.DataSource)
        //    //{
        //    //    Entity relatedEntity = StorageModel.Entities.Entity.First(search => search.Name == item.Entity);
        //    //    //if (!relatedEntity.complex.Any(search => search.entity == relatedEntity.name))
        //    //    //    relatedEntity.complex.Add(new complex() {
        //    //    //        name = Entity.name,
        //    //    //        entity = Entity.name,
        //    //    //        type = "Lookup",
        //    //    //        role = "None"
        //    //    //    });

        //    //    Entity.Complex.Add(item);

        //    //    StorageModel.Relationships.Relationship.Add(new Relationship()
        //    //    {
        //    //        Name = Entity.Name.ToUpper() + "_" + relatedEntity.Name.ToUpper(),
        //    //        Type = Entity.Name.ToUpper() + "_" + relatedEntity.Name.ToUpper(),
        //    //        Source = new NodeReference()
        //    //        {
        //    //            Name = Entity.Name,
        //    //            Label = Entity.Label,
        //    //            Complex = item.Name
        //    //        },
        //    //        Target = new NodeReference()
        //    //        {
        //    //            Name = relatedEntity.Name,
        //    //            Label = relatedEntity.Label
        //    //        }
        //    //    });
        //    //}

        //    //if (ApplyChangesButtonClicked != null)
        //    //    ApplyChangesButtonClicked(this, new EventArgs());

        //    //CloseEditor();
        //}

        private void ShowMessageAndResetTextBoxValue(string message, DataGridViewTextBoxCell textBox, string propName = "PropertyName")
        {
            MessageBox.Show(message);
            textBox.Value = propName;
        }

        private void btnEditStaticData_Click(object sender, EventArgs e)
        {
            ManageEntityStaticDataForm form = new ManageEntityStaticDataForm(StorageModel, Entity);
            form.ShowDialog();
        }

        private void lnkAddFunctionalID_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ManageFunctionalId form = new ManageFunctionalId(StorageModel);
            form.ShowDialog();
        }

        private void cmbFunctionalId_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbFunctionalId.SelectedItem == null)// || Entity.Abstract) -- TODO: Ask reason for this condition
                return;

            StorageModel.Entities.Entity.Where(x => x.Guid == Entity.Guid).SingleOrDefault().FunctionalId = (cmbFunctionalId.SelectedItem as EntityComboBoxItem)?.Value?.ToString();
        }

        private void txtLabel_Leave(object sender, EventArgs e)
        {
            txtLabel.Text = txtLabel.Text.Replace(" ", string.Empty);
        }

        private void txtName_Leave(object sender, EventArgs e)
        {
            txtName.Text = txtName.Text.Replace(" ", string.Empty);
        }

        private void chkIsVirtual_CheckedChanged(object sender, EventArgs e)
        {
            if (Entity == null)
                return;

            Entity.FunctionalId = null;
            cmbFunctionalId.SelectedItem = null;

            Entity.Virtual = chkIsVirtual.Checked;

            CheckAbstract();
            UncheckStaticData();
            EntityTypeChanged?.Invoke(sender, new EventArgs());
        }

        private void dataGridViewPrimitiveProperties_KeyDown(object sender, KeyEventArgs e)
        {
            // Handle Ctrl + V to paste properties
            if (e.Control && e.KeyCode == Keys.V)
                GetClipBoardDataAndAdd();
        }

        private void GetClipBoardDataAndAdd()
        {
            var clipBoardObject = Clipboard.GetText();
            DataObject dObj = new DataObject(DataFormats.UnicodeText, clipBoardObject);
            List<string> names = dObj.GetText().Split(new string[] { Environment.NewLine }, StringSplitOptions.None).ToList();
            foreach (string name in names)
            {
                Primitive prim = new Primitive(Entity.Model);
                ValidatePrimitivePropertyName(Entity, name, out string propName, 1, true);
                prim.Name = propName;
                prim.IsKey = false;
                prim.Nullable = true;
                prim.Type = "string";
                prim.Index = "None";

                Entity.Primitive.Add(prim);
            }

            Entity.CleanPrimitive();

            Reload();
        }

        private bool CheckInheritedPropertyExists(Entity entity, string propertyName)
        {
            Entity parentEntity = entity.ParentEntity;

            if (parentEntity == null)
                return false;

            if (parentEntity.Primitive.Where(x => x.Name?.ToLower() == propertyName.ToLower()).Count() > 0)
                return true;

            return CheckInheritedPropertyExists(parentEntity, propertyName);
        }

        private bool CheckIfPropertyExists(Entity entity, string propName)
        {
            int count = Entity.Primitive.Where(x => x.Name?.ToLower() == propName.ToLower()).Count();
            return count > 0;
        }

        private bool CheckIfReservedKeyword(string propertyName)
        {
            return Keywords.Instance.Contains(propertyName);
        }

        private void ValidatePrimitivePropertyName(Entity entity, string propertyName, out string newPropertyName, int count = 1, bool showMessage = false)
        {
            string tempName = propertyName;
            bool reValidate = false;

            if (string.IsNullOrEmpty(tempName))
            {
                if (showMessage)
                    MessageBox.Show($"Property name cannot be empty.", "Property Name", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                tempName = "PropertyName";
                reValidate = true;
            }

            if (CheckIfReservedKeyword(tempName))
            {
                if (showMessage)
                    MessageBox.Show($"Property \"{tempName}\" is a reserved keyword.", "Property Name", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                tempName = $"{tempName}{count}";
                reValidate = true;
                count++;
            }

            if (CheckInheritedPropertyExists(entity, tempName))
            {
                if (showMessage)
                    MessageBox.Show($"Property \"{tempName}\" already exists in base entity.", "Property Name", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                tempName = $"{tempName}{count}";
                reValidate = true;
                count++;
            }

            if (CheckIfPropertyExists(entity, tempName))
            {
                if (showMessage)
                    MessageBox.Show($"Property \"{tempName}\" already exists in entity.", "Property Name", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                tempName = $"{tempName}{count}";
                reValidate = true;
                count++;
            }

            newPropertyName = tempName;

            if (reValidate == true)
                ValidatePrimitivePropertyName(entity, tempName, out newPropertyName, count, false);
        }

        private void UpdatePrimitiveGridView()
        {
            bindingSourcePrimitiveProperties.DataSource = null;
            pre.DataGridViewPrimitive.DataSource = null;

            primitiveObservable = new ObservableCollection<GridPrimitiveItem>(GetPrimitivesOfSelfAndBaseTypes(Entity));
            primitiveObservable.CollectionChanged += delegate (object sender, NotifyCollectionChangedEventArgs e)
            {
                switch (e.Action)
                {
                    case NotifyCollectionChangedAction.Remove:
                    case NotifyCollectionChangedAction.Reset:
                        {
                            foreach (GridPrimitiveItem item in e.OldItems)
                                RemovePrimitive(item.Item);

                        }
                        break;
                    case NotifyCollectionChangedAction.Add:
                        {
                            foreach (GridPrimitiveItem item in e.NewItems)
                            {
                                item.EntityName = Entity.Name;
                                Entity.Primitive.Add(item.Item);
                            }

                        }
                        break;
                    case NotifyCollectionChangedAction.Replace:
                    case NotifyCollectionChangedAction.Move:
                        throw new NotSupportedException();
                }
            };

            bindingSourcePrimitiveProperties.DataSource = primitiveObservable;
            pre.DataGridViewPrimitive.DataSource = bindingSourcePrimitiveProperties;
        }

        public void UpdateRelationshipGridView()
        {
            bindingSourceCollectionProperties.DataSource = null;
            pre.DataGridViewRelationship.DataSource = null;

            relationshipsObservable = new ObservableCollection<Relationship>(GetRelationShipsOfSelfAndBaseWithinSubmodel(Entity, showAllRelationships));
            relationshipsObservable.CollectionChanged += delegate (object sender, NotifyCollectionChangedEventArgs e)
            {
                switch (e.Action)
                {
                    case NotifyCollectionChangedAction.Add:
                        {
                            int index = e.NewStartingIndex;
                            foreach (Relationship item in e.NewItems)
                            {
                                item.Model = StorageModel;
                                StorageModel.Relationships.Relationship.Add(item);
                            }
                        }
                        break;
                    case NotifyCollectionChangedAction.Remove:
                        {
                            int oldItemIndex = 0;
                            foreach (int index in Enumerable.Range(e.OldStartingIndex, e.OldItems.Count).Reverse())
                            {
                                Relationship item = (Relationship)e.OldItems[oldItemIndex++];
                                RemoveRelationships(item);
                            }
                        }
                        break;
                    case NotifyCollectionChangedAction.Reset:
                        {
                            foreach (Relationship item in e.OldItems)
                                RemoveRelationships(item);
                        }
                        break;
                    case NotifyCollectionChangedAction.Replace:
                    case NotifyCollectionChangedAction.Move:
                        throw new NotSupportedException();
                }
            };

            bindingSourceCollectionProperties.DataSource = relationshipsObservable;
            pre.DataGridViewRelationship.DataSource = bindingSourceCollectionProperties;
        }

        private void btnAddFunctionalId_Click(object sender, EventArgs e)
        {
            UpdateFunctionalIdForm form = new UpdateFunctionalIdForm(StorageModel, this);
            form.ShowDialog(this);
        }

        private void chkIsStaticData_CheckedChanged(object sender, EventArgs e)
        {
            if (Entity == null)
                return;

            if (!chkIsStaticData.Checked && Entity.IsStaticData && Entity.StaticData.Records.Record.Count != 0)
            {
                DialogResult result = MessageBox.Show($"This will delete all the existing '{Entity.Label}' static data. Do you wish to proceed?", "Warning", System.Windows.Forms.MessageBoxButtons.YesNo);
                if (result != DialogResult.Yes)
                {
                    chkIsStaticData.Checked = !chkIsStaticData.Checked;
                    return;
                }
            }

            Entity.IsStaticData = chkIsStaticData.Checked;
            btnEditStaticData.Visible = Entity.IsStaticData;

            UncheckVirtual();
            UncheckAbstract();
            EntityTypeChanged?.Invoke(sender, new EventArgs());
        }

        private void checkBoxShowAllRelationships_CheckedChanged(object sender, EventArgs e)
        {
            pre.CheckBoxShowFromCurrentModel.CheckedChanged -= checkBoxShowFromCurrentModel_CheckedChanged;

            showAllRelationships = pre.CheckBoxShowAllRelationship.Checked;
            pre.CheckBoxShowFromCurrentModel.Checked = !showAllRelationships;

            pre.CheckBoxShowFromCurrentModel.CheckedChanged += checkBoxShowFromCurrentModel_CheckedChanged;

            Reload();
        }

        private void checkBoxShowFromCurrentModel_CheckedChanged(object sender, EventArgs e)
        {
            pre.CheckBoxShowAllRelationship.CheckedChanged -= checkBoxShowAllRelationships_CheckedChanged;

            showAllRelationships = !pre.CheckBoxShowFromCurrentModel.Checked;
            pre.CheckBoxShowAllRelationship.Checked = showAllRelationships;

            pre.CheckBoxShowAllRelationship.CheckedChanged += checkBoxShowAllRelationships_CheckedChanged;

            Reload();
        }

        private void UncheckStaticData()
        {
            if (chkIsStaticData.Checked == false)
                return;

            chkIsStaticData.CheckedChanged -= chkIsStaticData_CheckedChanged;
            chkIsStaticData.Checked = false;
            chkIsStaticData.CheckedChanged += chkIsStaticData_CheckedChanged;

            if (!chkIsStaticData.Checked && Entity.IsStaticData && Entity.StaticData.Records.Record.Count != 0)
            {
                DialogResult result = MessageBox.Show($"This will delete all the existing '{Entity.Label}' static data. Do you wish to proceed?", "Warning", System.Windows.Forms.MessageBoxButtons.YesNo);
                if (result != System.Windows.Forms.DialogResult.Yes)
                {
                    chkIsStaticData.Checked = !chkIsStaticData.Checked;
                    return;
                }
            }
            Entity.IsStaticData = chkIsStaticData.Checked;
            btnEditStaticData.Visible = Entity.IsStaticData;

        }

        private void UncheckVirtual()
        {
            if (chkIsVirtual.Checked == false)
                return;

            chkIsVirtual.CheckedChanged -= chkIsVirtual_CheckedChanged;
            chkIsVirtual.Checked = false;
            Entity.Virtual = chkIsVirtual.Checked;
            chkIsVirtual.CheckedChanged += chkIsVirtual_CheckedChanged;
        }

        private void UncheckAbstract()
        {
            if (chkIsAbstract.Checked == false)
                return;

            chkIsAbstract.CheckedChanged -= chkIsAbstract_CheckedChanged;
            chkIsAbstract.Checked = false;
            Entity.Abstract = chkIsAbstract.Checked;
            chkIsAbstract.CheckedChanged += chkIsAbstract_CheckedChanged;
        }

        private void CheckAbstract()
        {
            if (chkIsAbstract.Checked == true)
                return;

            chkIsAbstract.CheckedChanged -= chkIsAbstract_CheckedChanged;
            chkIsAbstract.Checked = true;
            Entity.Abstract = chkIsAbstract.Checked;
            chkIsAbstract.CheckedChanged += chkIsAbstract_CheckedChanged;
        }

        private void chkIsAbstract_CheckedChanged(object sender, EventArgs e)
        {
            if (Entity == null)
                return;

            Entity.Abstract = chkIsAbstract.Checked;

            UncheckVirtual();
            UncheckStaticData();
            EntityTypeChanged?.Invoke(sender, new EventArgs());
        }

        private void btnPin_Click(object sender, EventArgs e)
        {
            if (ParentForm is MainForm parentForm)
                parentForm.ShowHideToolStripMenu(true);
        }
    }

    public static class EntityEditorExtensionMethods
    {
        public static void InsertNonDataBoundItems(this ComboBox cmb, string text, string value)
        {
            cmb.Items.Add(new ComboxBoxItem(text, value));
            cmb.DisplayMember = "Name";
            cmb.ValueMember = "Value";
        }
    }

    public class ComboxBoxItem
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public ComboxBoxItem()
        {

        }

        public ComboxBoxItem(string name, string value)
        {
            this.Name = name;
            this.Value = value;
        }
    }
}
