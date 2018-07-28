using Blueprint41.Core;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Blueprint41.Query;

namespace Blueprint41
{
    public class QueryExecutionContext
    {
        internal QueryExecutionContext(CompiledQuery query)
        {
            Transaction transaction = Transaction.RunningTransaction;

            CompiledQuery = query;
            QueryParameters = new Dictionary<string, object>();
            foreach (var item in query.ConstantValues)
            {
                QueryParameters.Add(item.Name, transaction.ConvertToStoredType(item.Type, item.Value));
            }
        }

        public void SetParameter(string parameterName, object value)
        {
            Transaction transaction = Transaction.RunningTransaction;
            if (value == null)
                QueryParameters.Add(parameterName, null);
            else
                QueryParameters.Add(parameterName, transaction.ConvertToStoredType(value.GetType(), value));
        }
        public List<dynamic> Execute()
        {
            List<dynamic> items = new List<dynamic>();

            Transaction transaction = Transaction.RunningTransaction;
            Dictionary<string, object> parameters = new Dictionary<string, object>(QueryParameters.Count);
            foreach (var queryParameter in QueryParameters)
                parameters.Add(queryParameter.Key, queryParameter.Value);

            var result = Neo4j.Persistence.Neo4jTransaction.Run(CompiledQuery.QueryText, parameters);
            foreach (var row in result)
            {
                IDictionary<string, object> record = new ExpandoObject();
                foreach (AsResult field in CompiledQuery.ResultColumns)
                {
                    string fieldname = field.GetFieldName();
                    object value;
                    if (row.Values.TryGetValue(fieldname, out value) && value != null)
                        record.Add(fieldname, transaction.ConvertFromStoredType(field.GetResultType(), value));
                    else
                        record.Add(fieldname, null);
                }
                items.Add(record);
            }
            return items;
        }
        
        public Dictionary<string, object> QueryParameters { get; private set; } 
        public CompiledQuery CompiledQuery { get; set; }
        public IReadOnlyList<string> Errors { get; private set; }
    }
}
