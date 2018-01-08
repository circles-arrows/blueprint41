using Blueprint41.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.Query
{
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
    }
}
