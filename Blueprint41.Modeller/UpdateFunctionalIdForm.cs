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

            if (this.Model.FunctionalIds.FunctionalId.Where(fi => fi.Name == name).Count() > 0)
            {
                MessageBox.Show($"Name \"{name}\" already exists.");
                return;
            }

            FunctionalId funcId = new FunctionalId();
            funcId.Type = type;
            funcId.Value = prefix;
            funcId.Name = name;
            funcId.Guid = Model.GenerateGuid(name).ToString();

            this.Model.FunctionalIds.FunctionalId.Add(funcId);
            string displayName = string.Concat(string.Concat(name, " - "), prefix);
            ParentControl.FunctionalIdComboBox.InsertNonDataBoundItems(displayName, funcId.Guid);
            ParentControl.FunctionalIdComboBox.SelectedIndex = ParentControl.FunctionalIdComboBox.FindStringExact(displayName);
            this.Close();
        }

        #endregion
    }

    public enum IdFormat
    {
        Hash,
        Numeric
    }
}
