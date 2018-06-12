using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Model = Blueprint41.Modeller.Schemas.Modeller;
using Blueprint41.Modeller.Generation;
using Blueprint41.Modeller.Schemas;
using System.Dynamic;

namespace Blueprint41.Modeller
{
    public partial class ManageEntityStaticDataForm : Form
    {
        public Model Model { get; private set; }

        public DataTable RecordPropertiesDataTable { get; private set; }

        public Entity Entity { get; private set; }

        public ManageEntityStaticDataForm(Model model, Entity entity)
        {
            InitializeComponent();

            Model = model;
            Entity = entity;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Entity.StaticData.Records.Record.Clear();
            foreach (DataRow dr in RecordPropertiesDataTable.Rows)
            {
                Record record = new Record(Model);
                foreach (DataColumn clm in RecordPropertiesDataTable.Columns)
                {
                    string value = (dr.ItemArray[clm.Ordinal]) == DBNull.Value ? null : (string)dr.ItemArray[clm.Ordinal];

                    if (clm.ColumnName == "RecordGuid")
                    {
                        if (string.IsNullOrEmpty(value))
                            record.Guid = Guid.NewGuid().ToString();
                        else
                            record.Guid = value;

                        continue;
                    }

                    if (string.IsNullOrEmpty(value))
                        continue;

                    record.Property.Add(new Property()
                    {
                        Value = value,
                        PropertyGuid = clm.ColumnName,
                    });
                }
                Entity.StaticData.Records.Record.Add(record);
            }
            this.Close();
        }

        private Dictionary<string, string> GetPropertiesOfBaseTypeAndSelf(Entity entity)
        {
            Dictionary<string, string> primitivePropertiesOfBaseTypeAndSelf = new Dictionary<string, string>();
            Entity current = Entity;
            do
            {
                foreach (var primitive in current.Primitive)
                {
                    primitivePropertiesOfBaseTypeAndSelf.Add(primitive.Guid, primitive.Name);
                }

                current = current.ParentEntity;
            } while (current != null);

            return primitivePropertiesOfBaseTypeAndSelf;
        }
        private void GridViewLab_Load(object sender, EventArgs e)
        {
            dataGridView.AutoGenerateColumns = false;
            dataGridView.AutoSize = true;
            dataGridView.Columns.Clear();

            RecordPropertiesDataTable = new DataTable();
            RecordPropertiesDataTable.Columns.Add("RecordGuid");

            Dictionary<string, string> recordProperties = new Dictionary<string, string>();
            foreach (var record in Entity.StaticData.Records.Record)
            {
                foreach (var property in record.Property)
                {
                    if (!recordProperties.ContainsKey(property.PropertyGuid))
                        recordProperties.Add(property.PropertyGuid, property.Value);
                }
            }
           
            var primitiveGuidNameMapping = GetPropertiesOfBaseTypeAndSelf(Entity);
            foreach (var recordProperty in recordProperties)
            {
                DataGridViewColumn column = new DataGridViewTextBoxColumn();
                column.DataPropertyName = recordProperty.Key;
                column.Name = primitiveGuidNameMapping[recordProperty.Key];
                dataGridView.Columns.Add(column);
                RecordPropertiesDataTable.Columns.Add(recordProperty.Key);
            }

            // if we don't have anything to map, add the primitive mapping instead
            if(recordProperties.Count == 0)
            {
                foreach(KeyValuePair<string, string> primiviteGuidName in primitiveGuidNameMapping)
                {
                    DataGridViewColumn column = new DataGridViewTextBoxColumn();
                    column.DataPropertyName = primiviteGuidName.Key;
                    column.Name = primiviteGuidName.Value;
                    dataGridView.Columns.Add(column);
                    RecordPropertiesDataTable.Columns.Add(primiviteGuidName.Key);
                }
            }

            //TODO: Review this part with Marko
            //Entity and Parent Relationship and Properties
            foreach (var relationship in Entity.GetRelationships(RelationshipDirection.In, true))
            {
                DataGridViewColumn column = new DataGridViewTextBoxColumn();
                column.DataPropertyName = relationship.InProperty;
                column.Name = relationship.InProperty;
                dataGridView.Columns.Add(column);
                RecordPropertiesDataTable.Columns.Add(relationship.InProperty);
            }

            foreach (var record in Entity.StaticData.Records.Record)
            {
                IDictionary<string, object> fields = new Dictionary<string, object>();
                fields.Add("RecordGuid", record.Guid);

                foreach (var primitiveProperty in Entity.Primitive)
                    fields.Add(primitiveProperty.Guid, "");

                foreach (var property in record.Property)
                {
                    if (property.PropertyGuid == null)
                        throw new ArgumentNullException($"<record propertyGuid=\"{record.Guid}\"> has a <property> node without a 'propertyGuid' attribute.");

                    fields[property.PropertyGuid] = property.Value;
                }

                DataRow dataRow = RecordPropertiesDataTable.Rows.Add(fields.Values.ToArray());
            }

            bindingSource.DataSource = null;
            dataGridView.DataSource = null;

            bindingSource.DataSource = RecordPropertiesDataTable;
            dataGridView.DataSource = bindingSource;
        }
    }
}
