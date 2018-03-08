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

        public DataTable DataTable { get; private set; }

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
            foreach (DataRow dr in DataTable.Rows)
            {
                Record record = new Record(Model);
                foreach (DataColumn clm in DataTable.Columns)
                {
                    string value = (dr.ItemArray[clm.Ordinal]) == DBNull.Value ? null : (string)dr.ItemArray[clm.Ordinal];

                    if (clm.ColumnName == "Guid")
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

        private void GridViewLab_Load(object sender, EventArgs e)
        {
            dataGridView.AutoGenerateColumns = false;
            dataGridView.AutoSize = true;
            dataGridView.Columns.Clear();

            DataTable = new DataTable();
            DataTable.Columns.Add("Guid");

            //Entity Properties
            Entity current = Entity;
            do
            {
                foreach (var primitiveProperty in current.Primitive)
                {
                    if (current.Label == "Neo4jBase" && primitiveProperty.Name != "Uid")
                        continue;

                    DataGridViewColumn column = new DataGridViewTextBoxColumn();
                    column.DataPropertyName = primitiveProperty.Guid;
                    column.Name = primitiveProperty.Name;
                    dataGridView.Columns.Add(column);
                    DataTable.Columns.Add(primitiveProperty.Guid);
                }

                current = current.ParentEntity;
            } while (current != null);

            //TODO: Review this part with Marko
            //Entity and Parent Relationship and Properties
            foreach (var relationship in Entity.GetRelationships(RelationshipDirection.In, true))
            {
                DataGridViewColumn column = new DataGridViewTextBoxColumn();
                column.DataPropertyName = relationship.InProperty;
                column.Name = relationship.InProperty;
                dataGridView.Columns.Add(column);
                DataTable.Columns.Add(relationship.InProperty);
            }
            
            foreach (var record in Entity.StaticData.Records.Record)
            {
                IDictionary<string, object> fields = new Dictionary<string, object>();
                fields.Add("Guid", record.Guid);

                foreach (var primitiveProperty in Entity.Primitive)
                {
                    fields.Add(primitiveProperty.Guid, "");
                }

                foreach (var property in record.Property)
                {
                    fields[property.PropertyGuid] = property.Value;
                }

                DataRow dr = DataTable.Rows.Add(fields.Values.ToArray());
            }

            bindingSource.DataSource = null;
            dataGridView.DataSource = null;

            bindingSource.DataSource = DataTable;
            dataGridView.DataSource = bindingSource;
        }
    }
}
