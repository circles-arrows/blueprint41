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

        public PropertyType SourceType => GetSourceMutliplicity();
        public bool SourceNullable => rbSourceLookupOptional.Checked || rbSourceCollection.Checked;

        public PropertyType TargetType => GetTargetMutliplicity();
        public bool TargetNullable => rbTargetLookupOptional.Checked || rbTargetCollection.Checked;

        public string TargetName
        {
            get { return lblTargetName.Text; }
            set { lblTargetName.Text = value; }
        }

        private void EdgeEditor_Load(object sender, EventArgs e)
        {
            btnOk.Click += BtnOk_Click;
            btnCancel.Click += BtnCancel_Click;
            rbSourceLookupOptional.Checked = true;
            rbTargetNone.Checked = true;

            cbAutoLabel.CheckStateChanged += CbAutoLabel_CheckStateChanged;

            rbSourceLookupOptional.CheckedChanged += UpdateCardinalityMessage;
            rbSourceLookupRequired.CheckedChanged += UpdateCardinalityMessage;
            rbSourceCollection.CheckedChanged += UpdateCardinalityMessage;

            rbTargetNone.CheckedChanged += UpdateCardinalityMessage;
            rbTargetLookupOptional.CheckedChanged += UpdateCardinalityMessage;
            rbTargetLookupRequired.CheckedChanged += UpdateCardinalityMessage;
            rbTargetCollection.CheckedChanged += UpdateCardinalityMessage;

            Apply();
            ShowHideControls();
        }

        void ShowHideControls()
        {
            bool visible = Model.ModellerType == ModellerType.Blueprint41;

            txtNeo4jName.Visible = visible;
            lblNeo4jName.Visible = visible;
        }

        private void UpdateCardinalityMessage(object sender, EventArgs e)
        {
            Apply();
        }

        public string GetRelationshipCardinality()
        {
            string cardinality = "must have only one relationship";

            if (SourceNullable)
            {
                if (SourceType == PropertyType.Collection)
                    cardinality = "can have zero or more relationships";
                else
                    cardinality = "can have zero or one relationship";
            }
            else
            {
                if (SourceType == PropertyType.Collection)
                    cardinality = "must have at least one or more relationships";
            }

            return string.Format("■ {0} {1} to {2}.", SourceName, cardinality, TargetName);
        }

        PropertyType GetSourceMutliplicity()
        {
            if (rbSourceCollection.Checked)
                return PropertyType.Collection;

            if (rbSourceLookupOptional.Checked || rbSourceLookupRequired.Checked)
                return PropertyType.Lookup;

            return PropertyType.Lookup;
        }

        PropertyType GetTargetMutliplicity()
        {
            if (rbTargetCollection.Checked)
                return PropertyType.Collection;

            if (rbTargetLookupOptional.Checked || rbTargetLookupRequired.Checked)
                return PropertyType.Lookup;

            return PropertyType.None;
        }

        private void CbAutoLabel_CheckStateChanged(object sender, EventArgs e)
        {
            Apply();
        }

        private string GenerateRelationshipName(string name, Func<string, bool> checkIfRelationShipExists)
        {
            string newName = name;

            int count = 0;
            bool isExists = true;

            while (isExists)
            {
                count++;
                newName = $"{name}{count}";
                isExists = checkIfRelationShipExists(newName);
            }

            return newName;
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            string relationshipType = Model.ModellerType == ModellerType.Neo4j ? txtRelationshipName.Text : txtNeo4jName.Text;
            string relationshipName = txtRelationshipName.Text;

            if (Model.Relationships.Relationship.Any(item => item.Name == relationshipName))
            {
                MessageBox.Show("Relationship name already exist.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                relationshipName = GenerateRelationshipName(relationshipName, name => Model.Relationships.Relationship.Any(item => item.Name == name));
                cbAutoLabel.Checked = false;
                txtRelationshipName.Text = relationshipName;
                return;
            }

            Relationship.Name = relationshipName;
            Relationship.Type = relationshipType;

            Relationship.Source.Nullable = SourceNullable;
            Relationship.Target.Nullable = TargetNullable;

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

            lblCardinality.Text = GetRelationshipCardinality();

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

        private void TxtRelationshipName_TextChanged(object sender, EventArgs e)
        {
            txtRelationshipName.ValidateText("Relationship Name");
        }

        private void TxtNeo4jName_TextChanged(object sender, EventArgs e)
        {
            txtNeo4jName.ValidateText("Neo4j Name");
        }
    }
}
