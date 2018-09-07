using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Blueprint41.Modeller.Schemas;

namespace Blueprint41.Modeller
{
    public partial class GuidDiscrepancyForm : Form
    {
        public Schemas.Modeller Model { get; internal set; }
        public IEnumerable<Relationship> RelationshipDiscrepancies { get; internal set; }

        public GuidDiscrepancyForm()
        {
            InitializeComponent();
            Load += GuidDiscrepancyForm_Load;
        }

        private void GuidDiscrepancyForm_Load(object sender, EventArgs e)
        {
            CreateColumns();
            dataGridView.DataError += DataGridView_DataError;
            dataGridView.DataSource = RelationshipDiscrepancies.ToList();

            BindingSource source = new BindingSource();
            source.DataSource = Model.Entities.Entity.OrderBy(x => x.Label);

            (dataGridView.Columns["In Entity"] as DataGridViewComboBoxColumn).DisplayMember = "Label";
            (dataGridView.Columns["In Entity"] as DataGridViewComboBoxColumn).ValueMember = "InEntityReferenceGuid";
            (dataGridView.Columns["In Entity"] as DataGridViewComboBoxColumn).DataSource = source;

            (dataGridView.Columns["Out Entity"] as DataGridViewComboBoxColumn).DisplayMember = "Label";
            (dataGridView.Columns["Out Entity"] as DataGridViewComboBoxColumn).ValueMember = "OutEntityReferenceGuid";
            (dataGridView.Columns["Out Entity"] as DataGridViewComboBoxColumn).DataSource = source;

            
        }

        private void DataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void CreateColumns()
        {
            dataGridView.AutoGenerateColumns = false;
            dataGridView.AutoSize = true;

            DataGridViewColumn sourcePropertyLabelColumn = new DataGridViewTextBoxColumn();
            sourcePropertyLabelColumn.DataPropertyName = "InEntity";
            sourcePropertyLabelColumn.Name = "In Label";
            sourcePropertyLabelColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            sourcePropertyLabelColumn.ReadOnly = true;
            dataGridView.Columns.Add(sourcePropertyLabelColumn);

            DataGridViewComboBoxColumn sourceComboColumn = new DataGridViewComboBoxColumn();
            sourceComboColumn.Name = "In Entity";
            sourceComboColumn.DataPropertyName = "InEntityReferenceGuid";
            dataGridView.Columns.Add(sourceComboColumn);

            //DataGridViewColumn propertyNameColumn = new DataGridViewTextBoxColumn();
            //propertyNameColumn.DataPropertyName = "InProperty";
            //propertyNameColumn.Name = "Property";
            //propertyNameColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            //propertyNameColumn.ReadOnly = true;
            //dataGridView.Columns.Add(propertyNameColumn);

            //DataGridViewComboBoxColumn sourceTypeColumn = new DataGridViewComboBoxColumn();
            //sourceTypeColumn.Items.Add("");
            //sourceTypeColumn.Items.Add(PropertyType.Lookup.ToString());
            //sourceTypeColumn.Items.Add(PropertyType.Collection.ToString());
            //sourceTypeColumn.DataPropertyName = "InPropertyType";
            //sourceTypeColumn.Name = "Type";
            //sourceTypeColumn.ReadOnly = true;
            //dataGridView.Columns.Add(sourceTypeColumn);

            //DataGridViewCheckBoxColumn sourceNullableColumn = new DataGridViewCheckBoxColumn();
            //sourceNullableColumn.DataPropertyName = "InNullable";
            //sourceNullableColumn.Name = "Optional";
            //sourceNullableColumn.ReadOnly = true;
            //dataGridView.Columns.Add(sourceNullableColumn);

            DataGridViewColumn relationshipNameColumn = new DataGridViewTextBoxColumn();
            relationshipNameColumn.Name = "Relationship Name";
            relationshipNameColumn.DataPropertyName = "Name";
            relationshipNameColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            relationshipNameColumn.ReadOnly = true;
            dataGridView.Columns.Add(relationshipNameColumn);

            DataGridViewColumn targetPropertyLabelColumn = new DataGridViewTextBoxColumn();
            targetPropertyLabelColumn.DataPropertyName = "OUtEntity";
            targetPropertyLabelColumn.Name = "Out Label";
            targetPropertyLabelColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            targetPropertyLabelColumn.ReadOnly = true;
            dataGridView.Columns.Add(targetPropertyLabelColumn);

            DataGridViewComboBoxColumn targetComboColumn = new DataGridViewComboBoxColumn();
            targetComboColumn.Name = "Out Entity";
            targetComboColumn.DataPropertyName = "OutEntityReferenceGuid";
            dataGridView.Columns.Add(targetComboColumn);

            //DataGridViewColumn targetPropertyNameColumn = new DataGridViewTextBoxColumn();
            //targetPropertyNameColumn.DataPropertyName = "OutProperty";
            //targetPropertyNameColumn.Name = "Property";
            //targetPropertyNameColumn.ReadOnly = true;
            //dataGridView.Columns.Add(targetPropertyNameColumn);

            //DataGridViewComboBoxColumn targetTypeColumn = new DataGridViewComboBoxColumn();
            //targetTypeColumn.Items.Add("");
            //targetTypeColumn.Items.Add(PropertyType.Lookup.ToString());
            //targetTypeColumn.Items.Add(PropertyType.Collection.ToString());
            //targetTypeColumn.DataPropertyName = "OutPropertyType";
            //targetTypeColumn.Name = "Type";
            //targetTypeColumn.ReadOnly = true;
            //dataGridView.Columns.Add(targetTypeColumn);

            //DataGridViewCheckBoxColumn targetNullableColumn = new DataGridViewCheckBoxColumn();
            //targetNullableColumn.DataPropertyName = "OutNullable";
            //targetNullableColumn.Name = "Optional";
            //targetNullableColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            //targetNullableColumn.ReadOnly = true;
            //dataGridView.Columns.Add(targetNullableColumn);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}
