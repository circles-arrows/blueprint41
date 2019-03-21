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
    public partial class UpdateFunctionalIdForm : Form
    {
        public Model Model { get; private set; }
        public Entity Entity { get; private set; }
        public FunctionalId FunctionalId { get; private set; }
        public EntityEditor ParentControl { get; private set; }

        // If IsUpdate is false, then it is a create
        public bool IsUpdate { get; private set; }

        public UpdateFunctionalIdForm(Model model, EntityEditor parentControl)
        {
            InitializeComponent();
            this.Model = model;
            this.ParentControl = parentControl;

            cmbType.Items.Clear();
            cmbType.Items.AddRange(Enum.GetNames(typeof(IdFormat)));
            cmbType.SelectedIndex = 0;

            txtName.Text = parentControl.Entity.Label;
        }

        #region Event Handlers

        private void btnSave_Click(object sender, EventArgs e)
        {
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

            this.Model.FunctionalIds.FunctionalId.Add(funcId);
            string displayName = string.Concat(string.Concat(name, " - "), prefix);
            ParentControl.RefreshEntity();
            ParentControl.FunctionalIdComboBox.SelectedIndex = ParentControl.FunctionalIdComboBox.FindStringExact(displayName);
            this.Close();

            MessageBox.Show("Functional Id saved in memory.", "Saved!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion
    }

    public enum IdFormat
    {
        Hash,
        Numeric
    }
}
