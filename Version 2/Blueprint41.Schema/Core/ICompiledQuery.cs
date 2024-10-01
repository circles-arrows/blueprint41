using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Blueprint41.Persistence;

namespace Blueprint41.Core
{
    public partial interface ICompiledQuery
    {
        IQueryExecutionContext GetExecutionContext();
        ICompiledQueryInfo? CompiledQuery { get; }
        string ToString();
    }
    public partial interface IQueryExecutionContext
    {
        void SetParameter(string parameterName, object? value);
        Dictionary<string, object?> GetParameters();
        List<dynamic> Execute(NodeMapping nodeMapping = NodeMapping.AsReadOnlyEntity);

        ICompiledQueryInfo CompiledQuery { get; }
        IReadOnlyList<string> Errors { get; }
        string ToString();
    }
    public partial interface ICompiledQueryInfo
    {
        string QueryText { get; }
        IReadOnlyList<IParameter> Parameters { get; }
        IReadOnlyList<IParameter> DefaultValues { get; }
        IReadOnlyList<IParameter> ConstantValues { get; }
        IReadOnlyList<string> Errors { get; }
        string ToString();
    }
}
