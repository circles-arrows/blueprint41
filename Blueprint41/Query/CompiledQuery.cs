using Blueprint41.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.Query
{
    [DebuggerDisplay("{DebuggerDisplay}")]
    public class CompiledQuery
    {
        internal CompiledQuery(CompileState state, AsResult[] resultColumns)
        {
            QueryText = state.Text.ToString();
            Parameters = new List<Parameter>(state.Parameters);
            ConstantValues = new List<Parameter>(state.Values);
            ResultColumns = resultColumns;
            ResultColumnTypeByName = resultColumns.ToDictionary(key => key.GetFieldName(), item => item.GetResultType());
            Errors = new List<string>(state.Errors);
        }

        public string QueryText { get; private set; }
        public IReadOnlyList<Parameter> Parameters { get; private set; }
        public IReadOnlyList<Parameter> ConstantValues { get; private set; }
        public IReadOnlyList<AsResult> ResultColumns { get; private set; }
        public IReadOnlyDictionary<string, Type> ResultColumnTypeByName { get; private set; }
        public IReadOnlyList<string> Errors { get; private set; }
        public override string ToString()
        {
            if (this == null)
                return null;

            Transaction transaction = Transaction.RunningTransaction;
            string cypherQuery = this.QueryText;
            Dictionary<string, object> parameterValues = new Dictionary<string, object>();

            foreach (var queryParameter in this.ConstantValues)
            {
                if (queryParameter.Value == null)
                    parameterValues.Add(string.Format("{{{0}}}", queryParameter.Name), null);
                else
                    parameterValues.Add(string.Format("{{{0}}}", queryParameter.Name), transaction.PersistenceProviderFactory.ConvertToStoredType(queryParameter.Value.GetType(), queryParameter.Value));
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
