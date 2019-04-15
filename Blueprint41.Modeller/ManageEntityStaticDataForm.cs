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

        private void DataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (e.Control && e.KeyCode == Keys.V)
            {
                PerformPaste();
                e.Handled = true;
            }
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

                    record.Property.Add(new Property(Model)
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
            dataGridView.KeyDown += DataGridView_KeyDown;

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

            IDictionary<string, object> fields = new Dictionary<string, object>();
            fields.Add("RecordGuid", "");

            //foreach (var recordProperty in recordProperties)
            //{
            //    DataGridViewColumn column = new DataGridViewTextBoxColumn();
            //    column.DataPropertyName = recordProperty.Key;
            //    column.Name = primitiveGuidNameMapping[recordProperty.Key];
            //    dataGridView.Columns.Add(column);
            //    RecordPropertiesDataTable.Columns.Add(recordProperty.Key);

            //    // Add the field names here so that it won't mixed up
            //    fields.Add(recordProperty.Key, "");
            //}

            // if we don't have anything to map, add the primitive mapping instead
            //if (recordProperties.Count == 0)
            //{
            foreach (KeyValuePair<string, string> primiviteGuidName in primitiveGuidNameMapping)
            {
                DataGridViewColumn column = new DataGridViewTextBoxColumn();
                column.DataPropertyName = primiviteGuidName.Key;
                column.Name = primiviteGuidName.Value;
                dataGridView.Columns.Add(column);
                RecordPropertiesDataTable.Columns.Add(primiviteGuidName.Key);

                fields.Add(primiviteGuidName.Key, "");
            }
            //}

            //TODO: Review this part with Marko
            //Entity and Parent Relationship and Properties
            //foreach (var relationship in Entity.GetRelationships(RelationshipDirection.In, true))
            //{
            //    DataGridViewColumn column = new DataGridViewTextBoxColumn();
            //    column.DataPropertyName = relationship.InProperty;
            //    column.Name = relationship.InProperty;
            //    dataGridView.Columns.Add(column);
            //    RecordPropertiesDataTable.Columns.Add(relationship.InProperty);
            //}

            foreach (var record in Entity.StaticData.Records.Record)
            {
                ResetFields(fields);

                fields["RecordGuid"] = record.Guid;

                foreach (var property in record.Property)
                {
                    if (property.PropertyGuid == null)
                        throw new ArgumentNullException($"<record propertyGuid=\"{record.Guid}\"> has a <property> node without a 'propertyGuid' attribute.");

                    fields[property.PropertyGuid] = property.Value;
                }

                DataRow dataRow = RecordPropertiesDataTable.Rows.Add(fields.Values.ToArray());
            }

            RefreshDatasource();
        }
        private void ResetFields(IDictionary<string, object> fields)
        {
            foreach (string key in fields.Keys.ToList())
                fields[key] = "";
        }

        private void PerformPaste()
        {
            System.Windows.Forms.IDataObject dtObj = System.Windows.Forms.Clipboard.GetDataObject();
            // get unicode text instead of text
            if (dtObj.GetDataPresent(DataFormats.UnicodeText, true))
            {
                string buffer = (string)dtObj.GetData(DataFormats.UnicodeText, true);

                StringDataToArray(buffer, out int totalRows, out int totalColumns, out string[,] values);

                if (totalRows == 0 || totalColumns == 0)
                    return;

                int currentSelectedRow = dataGridView.SelectedCells[0].RowIndex;
                int currentSelectedColumn = dataGridView.SelectedCells[0].ColumnIndex;

                if (dataGridView.Rows.Count < (currentSelectedRow + totalRows))
                {
                    int rowCount = currentSelectedRow + totalRows - dataGridView.Rows.Count;

                    while (rowCount > 0)
                    {
                        AddRow(RecordPropertiesDataTable);
                        rowCount--;
                    }

                    RefreshDatasource();
                }

                for (int i = 0; i < totalRows; i++)
                {
                    int currentRow = currentSelectedRow + i;
                    for (int j = 0; j < totalColumns; j++)
                    {
                        int currentColumn = currentSelectedColumn + j;

                        if (dataGridView.Columns.Count - 1 >= currentColumn)
                        {
                            dataGridView.Rows[currentRow].Cells[currentColumn].Value = values[i, j];
                        }
                    }
                }
            }
        }

        private void AddRow(DataTable table)
        {
            DataRow newRow = table.NewRow();
            table.Rows.Add(newRow);
        }

        private void RefreshDatasource()
        {
            bindingSource.DataSource = null;
            dataGridView.DataSource = null;

            bindingSource.DataSource = RecordPropertiesDataTable;
            dataGridView.DataSource = bindingSource;
        }

        private void StringDataToArray(string buffer, out int rows, out int columns, out string[,] values)
        {
            buffer = buffer.Replace("\x0D\x0A", "\x0A");
            string[] rowsData = buffer.Split('\x0A', '\x0D');

            rows = rowsData.Length;

            //Check if the last row is not null (some application put a last \n character at the end of the cells, for example excel)
            if (rows > 0 && (rowsData[rows - 1] == null || rowsData[rows - 1].Length == 0))
                rows--;

            if (rows == 0)
            {
                columns = 0;
                values = null;
                return;
            }

            //Calculate the columns based on the first rows! Note: probably is better to calculate the maximum columns.
            string[] firstColumnsData = rowsData[0].Split('\t');
            columns = firstColumnsData.Length;

            values = new string[rows, columns];

            for (int r = 0; r < rows; r++)
            {
                string rowData = rowsData[r];
                string[] colsData = rowData.Split('\t');

                for (int c = 0; c < columns; c++)
                {
                    if (c < colsData.Length)
                        values[r, c] = colsData[c];
                    else
                        values[r, c] = "";
                }
            }
        }
    }
}
