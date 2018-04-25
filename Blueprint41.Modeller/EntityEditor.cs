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
        private bool isFromCmbInherits = false;

        private bool showAllRelationships = false;

        public Entity Entity { get; private set; }

        public Model StorageModel { get; private set; }

        public DataGridViewComboBoxColumn sourceEntitiesColumn { get; private set; }
        public DataGridViewComboBoxColumn targetEntitiesColumn { get; private set; }

        private ObservableCollection<Relationship> relationshipsObservable;

        public DataTable FunctionalIdDataTable { get; set; }

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

        public EntityEditor()
        {
            InitializeComponent();
            CreateGridColumnsForPrimitiveProperties(dataGridViewPrimitiveProperties);
            CreateGridColumnsForPrimitiveProperties(dataGridViewInheritedPrimitiveProperties, true);
            CreateGridColumnsForRelationships();
            CreateGridColumnsForInheritedRelationships();
            CreateToolTipForShowAllRelationshipsCheckbox();
        }

        private void CreateToolTipForShowAllRelationshipsCheckbox()
        {
            // Create the ToolTip and associate with the Form container.
            ToolTip toolTip1 = new ToolTip();

            // Set up the delays for the ToolTip.
            toolTip1.AutoPopDelay = 5000;
            toolTip1.InitialDelay = 100;
            toolTip1.ReshowDelay = 500;

            // Set up the ToolTip text
            toolTip1.SetToolTip(checkBoxShowAllRelationships, "Also shows relationships to entities outside the current submodel.");
        }

        private void CreateGridColumnsForPrimitiveProperties(DataGridView dataGridView, bool readOnly = false)
        {
            // Initialize the DataGridView.
            dataGridView.AutoGenerateColumns = false;
            dataGridView.AutoSize = true;

            DataGridViewColumn nameColumn = new DataGridViewTextBoxColumn();
            nameColumn.DataPropertyName = "Name";
            nameColumn.Name = "Name";
            nameColumn.ReadOnly = readOnly;
            nameColumn.DefaultCellStyle.BackColor = readOnly ? Color.LightGray : Color.White;
            dataGridView.Columns.Add(nameColumn);

            DataGridViewCheckBoxColumn keyColumn = new DataGridViewCheckBoxColumn();
            keyColumn.DataPropertyName = "IsKey";
            keyColumn.Name = "Is Key";
            keyColumn.ReadOnly = readOnly;
            keyColumn.DefaultCellStyle.BackColor = readOnly ? Color.LightGray : Color.White;
            dataGridView.Columns.Add(keyColumn);

            DataGridViewCheckBoxColumn nullableColumn = new DataGridViewCheckBoxColumn();
            nullableColumn.DataPropertyName = "Nullable";
            nullableColumn.Name = "Optional";
            nullableColumn.ReadOnly = readOnly;
            nullableColumn.DefaultCellStyle.BackColor = readOnly ? Color.LightGray : Color.White;
            dataGridView.Columns.Add(nullableColumn);

            DataGridViewComboBoxColumn typeColumn = new DataGridViewComboBoxColumn();
            typeColumn.Items.Add("string");
            typeColumn.Items.Add("DateTime");
            typeColumn.Items.Add("bool");
            typeColumn.Items.Add("long");
            typeColumn.Items.Add("int");
            typeColumn.Items.Add("decimal");
            typeColumn.Items.Add("double");
            typeColumn.Items.Add("List<string>");
            typeColumn.Items.Add("List<DateTime>");
            typeColumn.Items.Add("List<bool>");
            typeColumn.Items.Add("List<long>");
            typeColumn.Items.Add("List<decimal>");
            typeColumn.Items.Add("List<double>");
            typeColumn.Items.Add("List<int>");
            typeColumn.DataPropertyName = "Type";
            typeColumn.Name = "Type";
            typeColumn.ReadOnly = readOnly;
            dataGridView.Columns.Add(typeColumn);

            DataGridViewComboBoxColumn indexTypeColumn = new DataGridViewComboBoxColumn();
            indexTypeColumn.Items.Add(PropertyIndex.None.ToString());
            indexTypeColumn.Items.Add(PropertyIndex.Indexed.ToString());
            indexTypeColumn.Items.Add(PropertyIndex.Unique.ToString());
            indexTypeColumn.DataPropertyName = "Index";
            indexTypeColumn.Name = "Index";
            indexTypeColumn.ReadOnly = readOnly;
            indexTypeColumn.DefaultCellStyle.BackColor = readOnly ? Color.LightGray : Color.White;
            dataGridView.Columns.Add(indexTypeColumn);
        }

        private void CreateGridColumnsForRelationships()
        {
            // Initialize the DataGridView.
            dataGridViewRelationships.AutoGenerateColumns = false;
            dataGridViewRelationships.AutoSize = true;


            sourceEntitiesColumn = new DataGridViewComboBoxColumn();
            sourceEntitiesColumn.DataPropertyName = "InEntity";
            sourceEntitiesColumn.Name = "IN Entity";
            dataGridViewRelationships.Columns.Add(sourceEntitiesColumn);


            DataGridViewColumn sourceNameColumn = new DataGridViewTextBoxColumn();
            sourceNameColumn.DataPropertyName = "InProperty";
            sourceNameColumn.Name = "IN Property";
            dataGridViewRelationships.Columns.Add(sourceNameColumn);

            DataGridViewComboBoxColumn sourceTypeColumn = new DataGridViewComboBoxColumn();
            sourceTypeColumn.Items.Add("");
            sourceTypeColumn.Items.Add(PropertyType.Lookup.ToString());
            sourceTypeColumn.Items.Add(PropertyType.Collection.ToString());
            sourceTypeColumn.DataPropertyName = "InPropertyType";
            sourceTypeColumn.Name = "IN Prop. Type";
            dataGridViewRelationships.Columns.Add(sourceTypeColumn);

            DataGridViewCheckBoxColumn sourceNullableColumn = new DataGridViewCheckBoxColumn();
            sourceNullableColumn.DataPropertyName = "InNullable";
            sourceNullableColumn.Name = "IN Prop. Optional";
            dataGridViewRelationships.Columns.Add(sourceNullableColumn);

            DataGridViewUpperCaseTextBoxColumn relationshipNameColumn = new DataGridViewUpperCaseTextBoxColumn();
            relationshipNameColumn.DataPropertyName = "Name";
            relationshipNameColumn.Name = "Relationship Name";
            dataGridViewRelationships.Columns.Add(relationshipNameColumn);

            DataGridViewUpperCaseTextBoxColumn neo4jNameColumn = new DataGridViewUpperCaseTextBoxColumn();
            neo4jNameColumn.DataPropertyName = "Type";
            neo4jNameColumn.Name = "Neo4j Name";
            dataGridViewRelationships.Columns.Add(neo4jNameColumn);
            
            targetEntitiesColumn = new DataGridViewComboBoxColumn();
            targetEntitiesColumn.DataPropertyName = "OutEntity";
            targetEntitiesColumn.Name = "OUT Entity";
            dataGridViewRelationships.Columns.Add(targetEntitiesColumn);

            DataGridViewColumn targetNameColumn = new DataGridViewTextBoxColumn();
            targetNameColumn.DataPropertyName = "OutProperty";
            targetNameColumn.Name = "OUT Property";
            dataGridViewRelationships.Columns.Add(targetNameColumn);

            DataGridViewComboBoxColumn targetTypeColumn = new DataGridViewComboBoxColumn();
            targetTypeColumn.Items.Add("");
            targetTypeColumn.Items.Add(PropertyType.Lookup.ToString());
            targetTypeColumn.Items.Add(PropertyType.Collection.ToString());
            targetTypeColumn.DataPropertyName = "OutPropertyType";
            targetTypeColumn.Name = "OUT Prop. Type";
            dataGridViewRelationships.Columns.Add(targetTypeColumn);

            DataGridViewCheckBoxColumn targetNullableColumn = new DataGridViewCheckBoxColumn();
            targetNullableColumn.DataPropertyName = "OutNullable";
            targetNullableColumn.Name = "OUT Prop. Optional";
            dataGridViewRelationships.Columns.Add(targetNullableColumn);
        }

        private void CreateGridColumnsForInheritedRelationships()
        {
            // Initialize the DataGridView.
            dataGridViewInheritedRelationships.AutoGenerateColumns = false;
            dataGridViewInheritedRelationships.AutoSize = true;


            DataGridViewTextBoxColumn sourceEntity = new DataGridViewTextBoxColumn();
            sourceEntity.DataPropertyName = "InEntity";
            sourceEntity.Name = "IN Entity";
            sourceEntity.ReadOnly = true;
            dataGridViewInheritedRelationships.Columns.Add(sourceEntity);


            DataGridViewTextBoxColumn sourceNameColumn = new DataGridViewTextBoxColumn();
            sourceNameColumn.DataPropertyName = "InProperty";
            sourceNameColumn.Name = "IN Property";
            sourceNameColumn.ReadOnly = true;
            dataGridViewInheritedRelationships.Columns.Add(sourceNameColumn);

            DataGridViewTextBoxColumn sourceTypeColumn = new DataGridViewTextBoxColumn();
            sourceTypeColumn.DataPropertyName = "InPropertyType";
            sourceTypeColumn.Name = "IN Prop. Type";
            sourceTypeColumn.ReadOnly = true;
            dataGridViewInheritedRelationships.Columns.Add(sourceTypeColumn);

            DataGridViewTextBoxColumn sourceNullableColumn = new DataGridViewTextBoxColumn();
            sourceNullableColumn.DataPropertyName = "InNullable";
            sourceNullableColumn.Name = "IN Prop. Optional";
            sourceNullableColumn.ReadOnly = true;
            dataGridViewInheritedRelationships.Columns.Add(sourceNullableColumn);

            DataGridViewUpperCaseTextBoxColumn relationshipNameColumn = new DataGridViewUpperCaseTextBoxColumn();
            relationshipNameColumn.DataPropertyName = "Name";
            relationshipNameColumn.Name = "Relationship Name";
            relationshipNameColumn.ReadOnly = true;
            dataGridViewInheritedRelationships.Columns.Add(relationshipNameColumn);

            DataGridViewUpperCaseTextBoxColumn neo4jNameColumn = new DataGridViewUpperCaseTextBoxColumn();
            neo4jNameColumn.DataPropertyName = "Type";
            neo4jNameColumn.Name = "Neo4j Name";
            neo4jNameColumn.ReadOnly = true;
            dataGridViewInheritedRelationships.Columns.Add(neo4jNameColumn);

            DataGridViewTextBoxColumn targetEntity = new DataGridViewTextBoxColumn();
            targetEntity.DataPropertyName = "OutEntity";
            targetEntity.Name = "OUT Entity";
            targetEntity.ReadOnly = true;
            dataGridViewInheritedRelationships.Columns.Add(targetEntity);

            DataGridViewTextBoxColumn targetNameColumn = new DataGridViewTextBoxColumn();
            targetNameColumn.DataPropertyName = "OutProperty";
            targetNameColumn.Name = "OUT Property";
            targetNameColumn.ReadOnly = true;
            dataGridViewInheritedRelationships.Columns.Add(targetNameColumn);

            DataGridViewTextBoxColumn targetTypeColumn = new DataGridViewTextBoxColumn();
            targetTypeColumn.DataPropertyName = "OutPropertyType";
            targetTypeColumn.Name = "OUT Prop. Type";
            targetTypeColumn.ReadOnly = true;
            dataGridViewInheritedRelationships.Columns.Add(targetTypeColumn);

            DataGridViewTextBoxColumn targetNullableColumn = new DataGridViewTextBoxColumn();
            targetNullableColumn.DataPropertyName = "OutNullable";
            targetNullableColumn.Name = "OUT Prop. Optional";
            targetNullableColumn.ReadOnly = true;
            dataGridViewInheritedRelationships.Columns.Add(targetNullableColumn);
        }

        private Collection<Primitive> GetPrimitivesOfBaseTypes(Entity Entity)
        {
            Collection<Primitive> inheritedPrimitives = new Collection<Primitive>();
            Entity current = Entity.ParentEntity;
            if (current == null)
                return null;
            do
            {
                foreach (var primitive in current.Primitive)
                    inheritedPrimitives.Add(primitive);

                current = current.ParentEntity;

            } while (current != null);

            return inheritedPrimitives;
        }

        private Collection<Relationship> GetInheritedRelationShipsOfBaseWithinSubmodel()
        {
            Collection<Relationship> inheritedRelationships = new Collection<Relationship>();
            Entity current = Entity.ParentEntity;
            if (current == null)
                return null;
            do
            {
                foreach (Relationship rel in current.GetRelationships(StorageModel.DisplayedSubmodel, true))
                    inheritedRelationships.Add(rel);

                current = current.ParentEntity;

            } while (current != null);

            return inheritedRelationships;
        }

        private void Assign()
        {
            cmbInherits.DataBindings.Clear();
            bindingSourcePrimitiveProperties.DataSource = null;
            dataGridViewPrimitiveProperties.DataSource = null;
            bindingSourcePrimitiveProperties.DataSource = Entity.Primitive;
            dataGridViewPrimitiveProperties.DataSource = bindingSourcePrimitiveProperties;

            bindingSourceInheritedPrimitiveProperties.DataSource = null;
            dataGridViewInheritedPrimitiveProperties.DataSource = null;
            bindingSourceInheritedPrimitiveProperties.DataSource = GetPrimitivesOfBaseTypes(Entity);
            dataGridViewInheritedPrimitiveProperties.DataSource = bindingSourceInheritedPrimitiveProperties;


            bindingSourceCollectionProperties.DataSource = null;
            dataGridViewRelationships.DataSource = null;

            if (showAllRelationships)
            {
                List<Relationship> allRelationships = new List<Relationship>();
                allRelationships.AddRange(Entity.GetRelationships(RelationshipDirection.In, false));
                allRelationships.AddRange(Entity.GetRelationships(RelationshipDirection.Out, false));

                relationshipsObservable = new ObservableCollection<Schemas.Relationship>(allRelationships);
            }
            else
                relationshipsObservable = new ObservableCollection<Schemas.Relationship>(Entity.GetRelationships(StorageModel.DisplayedSubmodel));

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
                                if (StorageModel.Relationships.Relationship.Contains(item))
                                    StorageModel.Relationships.Relationship.Remove(item);
                            }
                        }
                        break;
                    case NotifyCollectionChangedAction.Reset:
                        {
                            foreach (Relationship item in e.OldItems)
                                StorageModel.Relationships.Relationship.Remove(item);
                        }
                        break;
                    case NotifyCollectionChangedAction.Replace:
                    case NotifyCollectionChangedAction.Move:
                        throw new NotSupportedException();
                }
            };

            bindingSourceCollectionProperties.DataSource = relationshipsObservable;
            dataGridViewRelationships.DataSource = bindingSourceCollectionProperties;
            dataGridViewRelationships.CellValueChanged += DataGridViewRelationships_CellValueChanged;
            dataGridViewRelationships.CurrentCellDirtyStateChanged += DataGridViewRelationships_CurrentCellDirtyStateChanged;

            bindingSourceInheritedRelationships.DataSource = GetInheritedRelationShipsOfBaseWithinSubmodel();
            dataGridViewInheritedRelationships.DataSource = bindingSourceInheritedRelationships;

            bindingSourceEntities.DataSource = null;
            bindingSourceEntities.DataSource = StorageModel.Entities.Entity.OrderBy(x => x.Label);

            sourceEntitiesColumn.DataSource = null;
            sourceEntitiesColumn.DataSource = bindingSourceEntities;
            sourceEntitiesColumn.DisplayMember = "Label";
            sourceEntitiesColumn.ValueMember = "Label";

            targetEntitiesColumn.DataSource = null;
            targetEntitiesColumn.DataSource = bindingSourceEntities;
            targetEntitiesColumn.DisplayMember = "Label";
            targetEntitiesColumn.ValueMember = "Label";

            bindingSource.DataSource = Entity;
            cmbInherits.DataSource = StorageModel.Entities.Entity.Where(e => e.Abstract && e.Label != Entity.Label).ToList();

            Binding baseEntityBinding = new Binding("SelectedValue", this.bindingSource, "inherits", true);//, DataSourceUpdateMode.OnPropertyChanged);
            baseEntityBinding.BindingComplete += BaseEntityBinding_BindingComplete;
            this.cmbInherits.DataBindings.Add(baseEntityBinding);
            
            Entity inherited = StorageModel.Entities.Entity.Where(item => item.Guid == Entity.Inherits).FirstOrDefault();

            if (inherited != null)
                cmbInherits.SelectedItem = inherited;
            
            cmbInherits.SelectedIndexChanged += CmbInherits_SelectedIndexChanged;
            
            // FunctionalId Combobox
            cmbFunctionalId.Enabled = !Entity.Virtual;
            cmbFunctionalId.Items.Clear();
            
            foreach (var functionalId in StorageModel.FunctionalIds.FunctionalId.Where(x => x.Guid == Entity.Guid || !string.IsNullOrEmpty(x.Name)).OrderBy(x => x.Name))
                cmbFunctionalId.InsertNonDataBoundItems(string.Concat(functionalId.Name ?? Entity.Label, " - ", functionalId.Value), functionalId.Guid);
            
            FunctionalId entityFunctionalId = StorageModel.FunctionalIds.FunctionalId.Where(item => item.Guid == Entity.FunctionalId).SingleOrDefault();

            if (entityFunctionalId != null)
            {
                string displayName = string.Concat(entityFunctionalId.Name ?? Entity.Label, " - ", entityFunctionalId.Value);
                cmbFunctionalId.SelectedIndex = cmbFunctionalId.FindStringExact(displayName);
            }

            cmbFunctionalId.SelectedIndexChanged += cmbFunctionalId_SelectedIndexChanged;
            Entity.OnLabelChangeCancelled += Entity_OnLabelChangeCancelled;
            Entity.OnNameChangeCancelled += Entity_OnNameChangeCancelled;
        }

        private void DataGridViewRelationships_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dataGridViewRelationships.IsCurrentCellDirty)
            {
                //dataGridViewRelationships.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void DataGridViewRelationships_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == sourceEntitiesColumn.Index && e.RowIndex >= 0) //check if combobox column
            {
                object selectedValue = dataGridViewRelationships.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
            }

            if (e.ColumnIndex == targetEntitiesColumn.Index && e.RowIndex >= 0) //check if combobox column
            {
                object selectedValue = dataGridViewRelationships.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
            }
        }


        private void CmbInherits_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbInherits.SelectedItem == null)
                return;

            if ((cmbInherits.SelectedItem as Entity).Guid == Entity.Inherits)
                return;

            StorageModel.RemoveAllEdges(Entity);
            this.isFromCmbInherits = true;
            cmbInherits.DataBindings[0].WriteValue();
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
            ClearDataSourceAndHandlers();
            Enabled = true;
            Entity = model;
            StorageModel = modeller;
            Assign();
        }

        public void CloseEditor()
        {
            Enabled = false;
            ClearDataSourceAndHandlers();
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
            ClearDataSourceAndHandlers();
            Assign();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {

            //Entity.Complex.Clear();
            //foreach (Complex item in (IEnumerable<Complex>)bindingSourceCollectionProperties.DataSource)
            //{
            //    Entity relatedEntity = StorageModel.Entities.Entity.First(search => search.Name == item.Entity);
            //    //if (!relatedEntity.complex.Any(search => search.entity == relatedEntity.name))
            //    //    relatedEntity.complex.Add(new complex() {
            //    //        name = Entity.name,
            //    //        entity = Entity.name,
            //    //        type = "Lookup",
            //    //        role = "None"
            //    //    });

            //    Entity.Complex.Add(item);

            //    StorageModel.Relationships.Relationship.Add(new Relationship()
            //    {
            //        Name = Entity.Name.ToUpper() + "_" + relatedEntity.Name.ToUpper(),
            //        Type = Entity.Name.ToUpper() + "_" + relatedEntity.Name.ToUpper(),
            //        Source = new NodeReference()
            //        {
            //            Name = Entity.Name,
            //            Label = Entity.Label,
            //            Complex = item.Name
            //        },
            //        Target = new NodeReference()
            //        {
            //            Name = relatedEntity.Name,
            //            Label = relatedEntity.Label
            //        }
            //    });
            //}

            //if (ApplyChangesButtonClicked != null)
            //    ApplyChangesButtonClicked(this, new EventArgs());

            //CloseEditor();
        }

        private void dataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void dataGridViewCollectionProperties_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void dataGridViewPrimitiveProperties_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            e.Row.Cells["Type"].Value = "string";
            e.Row.Cells["Index"].Value = PropertyIndex.None.ToString();
        }

        private void dataGridViewPrimitiveProperties_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                DataGridViewTextBoxCell textBox = (DataGridViewTextBoxCell)dataGridViewPrimitiveProperties.Rows[e.RowIndex].Cells[0];

                if (textBox.Value == null)
                {
                    ShowMessageAndResetTextBoxValue("Property name cannot be empty.", textBox);
                    return;
                }

                string newName = ((string)textBox.Value).Replace(" ", string.Empty);

                if (string.IsNullOrEmpty(newName))
                {
                    ShowMessageAndResetTextBoxValue("Property name cannot be empty.", textBox);
                    return;
                }
                else if (CheckInheritedPropertyExists(Entity, newName))
                {
                    ShowMessageAndResetTextBoxValue(string.Format("Property \"{0}\" already exists in base entity.", newName), textBox);
                    return;
                }

                if (CheckIfReservedKeyword(newName))
                {
                    ShowMessageAndResetTextBoxValue(string.Format("Property \"{0}\" is a reserved keyword.", newName), textBox);
                    return;
                }

                textBox.Value = newName;
            }
        }
        
        private void ShowMessageAndResetTextBoxValue(string message, DataGridViewTextBoxCell textBox)
        {
            MessageBox.Show(message);
            textBox.Value = "PropertyName";
        }
        
        private void dataGridViewRelationships_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                DataGridViewComboBoxCell cb = (DataGridViewComboBoxCell)dataGridViewRelationships.Rows[e.RowIndex].Cells[0];
                relationshipsObservable[e.RowIndex].Source.ReferenceGuid = StorageModel.Entities.Entity.Where(x => x.Label == (string)cb.Value).SingleOrDefault()?.Guid;
            }

            if (e.ColumnIndex == 6)
            {
                DataGridViewComboBoxCell cb = (DataGridViewComboBoxCell)dataGridViewRelationships.Rows[e.RowIndex].Cells[6];
                relationshipsObservable[e.RowIndex].Target.ReferenceGuid = StorageModel.Entities.Entity.Where(x => x.Label == (string)cb.Value).SingleOrDefault()?.Guid;
            }

            if (e.ColumnIndex == 1 || e.ColumnIndex == 4 || e.ColumnIndex == 5 || e.ColumnIndex == 7)
            {
                DataGridViewTextBoxCell textBox = (DataGridViewTextBoxCell)dataGridViewRelationships.Rows[e.RowIndex].Cells[e.ColumnIndex];
                string newName = ((string)textBox.Value)?.Replace(" ", string.Empty);
                textBox.Value = newName;

                if (!string.IsNullOrEmpty(newName) && e.ColumnIndex == 5)
                    relationshipsObservable[e.RowIndex].RecreateEdge();
            }
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
            
            StorageModel.Entities.Entity.Where(x => x.Guid == Entity.Guid).SingleOrDefault().FunctionalId = (cmbFunctionalId.SelectedItem as ComboxBoxItem).Value;
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
            Entity.FunctionalId = null;
            cmbFunctionalId.SelectedItem = null;
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
                prim.Name = name;
                prim.IsKey = false;
                prim.Nullable = true;
                prim.Type = "string";
                prim.Index = "None";

                Entity.Primitive.Add(prim);
            }
            Entity.CleanPrimitive();
            bindingSourcePrimitiveProperties.DataSource = null;
            bindingSourcePrimitiveProperties.DataSource = Entity.Primitive;
        }

        private void InsertNonDataBoundItemsToComboBox()
        {

        }

        private bool CheckInheritedPropertyExists(Entity entity, string propertyName)
        {
            Entity parentEntity = entity.ParentEntity;

            if (parentEntity == null)
                return false;
            
            if (parentEntity.Primitive.Where(x => x.Name == propertyName).Count() > 0)
                return true;

            return CheckInheritedPropertyExists(parentEntity, propertyName);
        }

        private bool CheckIfReservedKeyword(string propertyName)
        {
            return Keywords.Instance.Contains(propertyName);
        }

        public void UpdateRelationshipGridView()
        {
            relationshipsObservable = new ObservableCollection<Schemas.Relationship>(Entity.GetRelationships(StorageModel.DisplayedSubmodel));
            bindingSourceCollectionProperties.DataSource = relationshipsObservable;
            dataGridViewRelationships.DataSource = bindingSourceCollectionProperties;


        }

        private void btnAddFunctionalId_Click(object sender, EventArgs e)
        {
            UpdateFunctionalIdForm form = new UpdateFunctionalIdForm(StorageModel, this);
            form.ShowDialog(this);
        }

        private void splitContainer2_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void chkIsStaticData_CheckedChanged(object sender, EventArgs e)
        {
            if(!chkIsStaticData.Checked && Entity.IsStaticData)
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

        private void checkBoxShowAllRelationships_CheckedChanged(object sender, EventArgs e)
        {
            showAllRelationships = checkBoxShowAllRelationships.Checked;
            Reload();
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
