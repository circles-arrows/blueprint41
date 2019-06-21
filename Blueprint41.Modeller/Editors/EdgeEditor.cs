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

namespace Blueprint41.Modeller.Editors
{
    public partial class EdgeEditor : Form
    {
        public EdgeEditor()
        {
            InitializeComponent();
            Load += EdgeEditor_Load;
        }

        public string SourceName
        {
            get { return lblSourceName.Text; }
            set { lblSourceName.Text = value; }
        }

        public Model Model { get; set; }

        public PropertyType SourceType => (PropertyType)cmbSourcePropertyType.SelectedItem;
        public PropertyType TargetType => (PropertyType)cmbTargetPropertyType.SelectedItem;

        public string TargetName
        {
            get { return lblTargetName.Text; }
            set { lblTargetName.Text = value; }
        }

        private void EdgeEditor_Load(object sender, EventArgs e)
        {
            btnOk.Click += BtnOk_Click;
            btnCancel.Click += BtnCancel_Click;

            foreach (PropertyType item in Enum.GetValues(typeof(PropertyType)))
            {
                // skip the property type none on source
                if (item != PropertyType.None)
                    cmbSourcePropertyType.Items.Add(item);

                cmbTargetPropertyType.Items.Add(item);
            }

            cmbSourcePropertyType.SelectedIndex = 0;
            cmbTargetPropertyType.SelectedIndex = 0;

            cbAutoLabel.CheckStateChanged += CbAutoLabel_CheckStateChanged;
            cmbSourcePropertyType.SelectedIndexChanged += CmbSourcePropertyType_SelectedIndexChanged;
            cmbTargetPropertyType.SelectedIndexChanged += CmbSourcePropertyType_SelectedIndexChanged;

            Apply();
        }

        private void CmbSourcePropertyType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Apply();
        }

        private void CbAutoLabel_CheckStateChanged(object sender, EventArgs e)
        {
            Apply();
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            Relationship.Name = txtRelationshipName.Text;
            Relationship.Type = txtNeo4jName.Text;

            DialogResult = DialogResult.OK;
            Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        Schemas.Relationship relationship;
        public Schemas.Relationship Relationship
        {
            get { return relationship; }
        }

        void Apply()
        {
            relationship = new Schemas.Relationship(Model);


            string neo4jType;
            string relationshipName;

            switch (SourceType)
            {
                case PropertyType.None:
                    throw new NotSupportedException(string.Format("Source Property Type {0} is currently not supported", SourceType));
                case PropertyType.Lookup:
                    switch (TargetType)
                    {
                        case PropertyType.None:
                            relationshipName = string.Concat(SourceName.ToUpper(), "_HAS_", TargetName.ToUpper());
                            neo4jType = string.Concat("HAS_", TargetName.ToUpper());

                            relationship.InProperty = TargetName;
                            relationship.InPropertyType = PropertyType.Lookup.ToString();

                            break;
                        case PropertyType.Lookup:
                            relationshipName = string.Concat(SourceName.ToUpper(), "_IS_", TargetName.ToUpper());
                            neo4jType = string.Concat("IS_", TargetName.ToUpper());

                            relationship.InProperty = TargetName;
                            relationship.InPropertyType = PropertyType.Lookup.ToString();

                            relationship.OutProperty = SourceName;
                            relationship.OutPropertyType = PropertyType.Lookup.ToString();
                            break;
                        case PropertyType.Collection:
                            relationshipName = string.Concat(SourceName.ToUpper(), "_VALID_FOR_", TargetName.ToUpper());
                            neo4jType = string.Concat("VALID_FOR_", TargetName.ToUpper());

                            relationship.InProperty = TargetName;
                            relationship.InPropertyType = PropertyType.Lookup.ToString();

                            relationship.OutProperty = SourceName.ToPlural();
                            relationship.OutPropertyType = PropertyType.Collection.ToString();
                            break;
                        default:
                            throw new NotSupportedException(string.Format("Target Property Type value {0} is currently not supported", TargetType));
                    }
                    break;
                case PropertyType.Collection:
                    switch (TargetType)
                    {
                        case PropertyType.None:
                            relationshipName = string.Concat(SourceName.ToUpper(), "_CONTAINS_", TargetName.ToUpper());
                            neo4jType = string.Concat("CONTAINS_", TargetName.ToUpper());

                            relationship.InProperty = TargetName.ToPlural();
                            relationship.InPropertyType = PropertyType.Collection.ToString();
                            break;
                        case PropertyType.Lookup:
                            relationshipName = string.Concat(SourceName.ToUpper(), "_CONTAINS_", TargetName.ToUpper());
                            neo4jType = string.Concat("CONTAINS_", TargetName.ToUpper());

                            relationship.InProperty = TargetName.ToPlural();
                            relationship.InPropertyType = PropertyType.Collection.ToString();

                            relationship.OutProperty = SourceName;
                            relationship.OutPropertyType = PropertyType.Lookup.ToString();
                            break;
                        case PropertyType.Collection:
                            relationshipName = string.Concat(SourceName.ToUpper(), "_CONTAINS_", TargetName.ToUpper());
                            neo4jType = string.Concat("CONTAINS_", TargetName.ToUpper());

                            relationship.InProperty = TargetName.ToPlural();
                            relationship.InPropertyType = PropertyType.Collection.ToString();

                            relationship.OutProperty = SourceName.ToPlural();
                            relationship.OutPropertyType = PropertyType.Collection.ToString();
                            break;
                        default:
                            throw new NotSupportedException(string.Format("Target Property Type value {0} is currently not supported", TargetType));
                    }
                    break;
                default:
                    throw new NotSupportedException(string.Format("Source Property Type {0} is currently not supported", SourceType));
            }


            if (cbAutoLabel.Checked == false)
                return;

            txtRelationshipName.Text = relationshipName;
            txtNeo4jName.Text = neo4jType;
        }

        private void TxtRelationshipName_KeyDown(object sender, KeyEventArgs e)
        {
            cbAutoLabel.Checked = false;
        }

        private void txtNeo4jName_KeyDown(object sender, EventArgs e)
        {
            cbAutoLabel.Checked = false;
        }
    }
}
