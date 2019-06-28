using Blueprint41.Modeller.Schemas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Model = Blueprint41.Modeller.Schemas.Modeller;

namespace Blueprint41.Modeller
{
    public partial class ManageFunctionalId : Form
    {
        public Model Model { get; private set; }
        
        private string SelectedFunctionalIdGuid;

        private bool IsNewFunctionalId = true;

        private void CreateGridViewColumns()
        {
            DataGridViewTextBoxColumn nameColumn = new DataGridViewTextBoxColumn();
            nameColumn.DataPropertyName = "Name";
            nameColumn.Name = "Name";
            dataGridViewFunctionalIds.Columns.Add(nameColumn);

            DataGridViewTextBoxColumn prefixColumn = new DataGridViewTextBoxColumn();
            prefixColumn.DataPropertyName = "Prefix";
            prefixColumn.Name = "Prefix";
            dataGridViewFunctionalIds.Columns.Add(prefixColumn);

            DataGridViewTextBoxColumn typeColumn = new DataGridViewTextBoxColumn();
            typeColumn.DataPropertyName = "Type";
            typeColumn.Name = "Type";
            dataGridViewFunctionalIds.Columns.Add(typeColumn);
        }

        private void InitializeDataBindings()
        {
            bindingSourceFunctionalIds.DataSource = this.Model.FunctionalIds.FunctionalId.Where(x => !string.IsNullOrEmpty(x.Name));
            dataGridViewFunctionalIds.DataSource = bindingSourceFunctionalIds;
            dataGridViewFunctionalIds.ClearSelection();
        }

        #region Event Handlers
        public ManageFunctionalId(Model model)
        {
            InitializeComponent();
            this.Model = model;
            cmbType.Items.Clear();
            cmbType.Items.AddRange(Enum.GetNames(typeof(IdFormat)));
            cmbType.SelectedIndex = 0;
            
            InitializeDataBindings();
            SetupForSelection();
        }
        
        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedIndexChanged();
        }
        
        private void SelectedIndexChanged()
        {
            //txtPrefix.Enabled = true;
            //if ((string)cmbType.SelectedItem == IdFormat.Numeric.ToString())
            //{
            //    txtPrefix.Enabled = false;
            //}
        }

        private void Save()
        {
            FunctionalId defaultFunctionalId = GetDefaultFunctionalId();

            string type = cmbType.SelectedItem.ToString();
            string prefix = txtPrefix.Text.Trim();
            string name = txtName.Text.Trim();

            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Name cannot be empty.", "Cannot Save Functiond Id", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return;
            }

            if (string.IsNullOrEmpty(prefix))
            {
                MessageBox.Show("Prefix cannot be empty.", "Cannot Save Functiond Id", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPrefix.Focus();
                return;
            }

            if (this.IsNewFunctionalId)
            {
                if (this.Model.FunctionalIds.FunctionalId.Any(functionalId => functionalId.Name == name))
                {
                    MessageBox.Show($"Name \"{name}\" already exists.", "Cannot Save Functiond Id", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtName.Focus();
                    return;
                }

                if (this.Model.FunctionalIds.FunctionalId.Any(functionalId => functionalId.Value == prefix))
                {
                    MessageBox.Show($"Prefix \"{prefix}\" already exists.", "Cannot Save Functiond Id", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtPrefix.Focus();
                    return;
                }

                FunctionalId funcId = new FunctionalId(Model);
                funcId.Type = type;
                funcId.Value = prefix;
                funcId.Name = name;
                funcId.Guid = Model.GenerateGuid(name).ToString();

                if (chkDefault.Checked)
                {
                    if (defaultFunctionalId != null)
                        defaultFunctionalId.IsDefault = false;
                }
                else
                {
                    if (defaultFunctionalId == null)
                    {
                        MessageBox.Show($"Model must have a default functional id.");
                        return;
                    }
                }

                funcId.IsDefault = chkDefault.Checked;

                this.Model.FunctionalIds.FunctionalId.Add(funcId);
            }
            else
            {
                if (this.Model.FunctionalIds.FunctionalId.Where(fi => fi.Name == name && fi.Guid != this.SelectedFunctionalIdGuid).Count() > 0)
                {
                    MessageBox.Show($"Name \"{name}\" already exists.", "Functional ID", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (this.Model.FunctionalIds.FunctionalId.Any(functionalId => functionalId.Value == prefix && functionalId.Guid != this.SelectedFunctionalIdGuid))
                {
                    MessageBox.Show($"Prefix \"{prefix}\" already exists.", "Cannot Save Functiond Id", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtPrefix.Focus();
                    return;
                }

                FunctionalId selectedFunctionalId = GetSelected();
                selectedFunctionalId.Type = type;
                selectedFunctionalId.Value = prefix;
                selectedFunctionalId.Name = name;

                if (chkDefault.Checked && !selectedFunctionalId.IsDefault)
                {
                    if (defaultFunctionalId != null)
                        defaultFunctionalId.IsDefault = false;
                }

                if (!chkDefault.Checked)
                {
                    if (defaultFunctionalId == null || (defaultFunctionalId?.Guid == selectedFunctionalId.Guid))
                    {
                        MessageBox.Show($"Model must have a default functional id.", "Functional ID", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }

                selectedFunctionalId.IsDefault = chkDefault.Checked;
            }
            
            InitializeDataBindings();
            SetupForSelection();
        }
        
        #endregion

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            this.IsNewFunctionalId = true;
            SetupForEdit();
            dataGridViewFunctionalIds.ClearSelection();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            this.IsNewFunctionalId = false;
            FunctionalId selectedFunctionalId = GetSelected();

            SetupForEdit();

            cmbType.SelectedItem = selectedFunctionalId.Type;
            txtPrefix.Text = selectedFunctionalId.Value;
            txtName.Text = selectedFunctionalId.Name;
            chkDefault.Checked = selectedFunctionalId.IsDefault;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            FunctionalId selectedFunctionalId = GetSelected();

            if (this.Model.Entities.Entity.Where(ent => ent.FunctionalId == selectedFunctionalId.Guid).Count() > 0)
            {
                MessageBox.Show($"Cannot delete functionalId \"{selectedFunctionalId.Name}\" because 1 or more entities are using it.", "Functional ID", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            DialogResult dialogResult = MessageBox.Show($"Are you want to delete functionalId: \"{selectedFunctionalId.Name}\"?", "Delete", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                this.Model.FunctionalIds.FunctionalId.Remove(selectedFunctionalId);

                InitializeDataBindings();
                SetupForSelection();
            }
        }

        private void SetupForEdit()
        {
            gbPropertiesPanel.Enabled = true;

            cmbType.SelectedItem = IdFormat.Hash.ToString();
            txtPrefix.Clear();
            txtName.Clear();

            btnSave.Enabled = true;
            btnCancel.Enabled = true;

            dataGridViewFunctionalIds.Enabled = false;
            btnNew.Enabled = false;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
        }

        private void SetupForSelection()
        {
            gbPropertiesPanel.Enabled = false;

            cmbType.SelectedItem = null;
            txtPrefix.Clear();
            txtName.Clear();
            chkDefault.Checked = false;

            btnSave.Enabled = false;
            btnCancel.Enabled = false;

            dataGridViewFunctionalIds.Enabled = true;
            dataGridViewFunctionalIds.ClearSelection();
            btnNew.Enabled = true;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
        }
        
        private void btnCancel_Click(object sender, EventArgs e)
        {
            SetupForSelection();
        }

        private FunctionalId GetSelected()
        {
            SelectedFunctionalIdGuid = dataGridViewFunctionalIds.SelectedRows[0].Cells["dgvtbcGuid"].Value as string;
            return Model.FunctionalIds.FunctionalId.Where(item => item.Guid == SelectedFunctionalIdGuid).SingleOrDefault();
        }

        private void dataGridViewFunctionalIds_SelectionChanged(object sender, EventArgs e)
        {
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
        }

        private FunctionalId GetDefaultFunctionalId()
        {
            return this.Model.FunctionalIds.FunctionalId.Where(x => x.IsDefault).SingleOrDefault();
        }
    }
}
