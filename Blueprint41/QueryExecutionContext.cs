#nullable disable

using Blueprint41.Core;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Blueprint41.Query;
using System.Diagnostics;

namespace Blueprint41
{
    [DebuggerDisplay("{DebuggerDisplay}")]
    public class QueryExecutionContext
    {
        internal QueryExecutionContext(CompiledQuery query)
        {
            Transaction transaction = Transaction.RunningTransaction;

            CompiledQuery = query;
            QueryParameters = new Dictionary<string, object>();
            foreach (var item in query.ConstantValues)
            {
                QueryParameters.Add(item.Name, transaction.PersistenceProviderFactory.ConvertToStoredType(item.Type, item.Value));
            }
        }

        public void SetParameter(string parameterName, object value)
        {
            Transaction transaction = Transaction.RunningTransaction;
            if (value == null)
                QueryParameters.Add(parameterName, null);
            else
                QueryParameters.Add(parameterName, transaction.PersistenceProviderFactory.ConvertToStoredType(value.GetType(), value));
        }
        public List<dynamic> Execute()
        {
            List<dynamic> items = new List<dynamic>();

            Transaction transaction = Transaction.RunningTransaction;
            Dictionary<string, object> parameters = new Dictionary<string, object>(QueryParameters.Count);
            foreach (var queryParameter in QueryParameters)
            {
                if (queryParameter.Value == null)
                    parameters.Add(queryParameter.Key, null);
                else
                    parameters.Add(queryParameter.Key, transaction.PersistenceProviderFactory.ConvertToStoredType(queryParameter.Value.GetType(), queryParameter.Value));
            }

            var result = Neo4j.Persistence.Neo4jTransaction.Run(CompiledQuery.QueryText, parameters);
            foreach (var row in result)
            {
                IDictionary<string, object> record = new ExpandoObject();
                foreach (AsResult field in CompiledQuery.ResultColumns)
                {
                    string fieldname = field.GetFieldName();
                    object value;
                    if (row.Values.TryGetValue(fieldname, out value) && value != null)
                        record.Add(fieldname, transaction.PersistenceProviderFactory.ConvertFromStoredType(field.GetResultType(), value));
                    else
                        record.Add(fieldname, null);
                }
                items.Add(record);
            }
            return items;
        }

        public CompiledQuery CompiledQuery { get; set; }
        public IReadOnlyList<string> Errors { get; private set; }
        internal Dictionary<string, object> QueryParameters { get; private set; }
        public override string ToString()
        {
            if (this == null)
                return null;

            Transaction transaction = Transaction.RunningTransaction;
            string cypherQuery = CompiledQuery.QueryText;
            Dictionary<string, object> parameterValues = new Dictionary<string, object>();

            foreach (var queryParameter in QueryParameters)
            {
                if (queryParameter.Value == null)
                    parameterValues.Add(string.Format("{{{0}}}", queryParameter.Key), null);
                else
                    parameterValues.Add(string.Format("{{{0}}}", queryParameter.Key), transaction.PersistenceProviderFactory.ConvertToStoredType(queryParameter.Value.GetType(), queryParameter.Value));
            }

            foreach (var queryParam in parameterValues)
            {
                object paramValue = queryParam.Value.GetType() == typeof(string) ? string.Format("'{0}'", queryParam.Value.ToString()) : queryParam.Value.ToString();
                cypherQuery = cypherQuery.Replace(queryParam.Key, paramValue.ToString());
            }
            return cypherQuery;
        }
        private string DebuggerDisplay { get => ToString(); }
    }
}
