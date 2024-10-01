using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.Query
{
    [Serializable]

    public class QueryException : ApplicationException
    {
        public QueryException() { }
        public QueryException(CompiledQueryInfo query) : base($"There were errors during compilation of the query. Please check the exception details.")
        {
            QueryText = query.QueryText;
            QueryParameters = query.Parameters.Select(item => $"Parameter: Name = {item.Name}, Type = {item.Type?.FullName ?? "NULL"}").ToArray();
            ErrorMessages = query.Errors.Select(item => $"Error: {item}").ToArray();
        }

        public string? QueryText { get; set; }

        public object[]? QueryParameters { get; set; }

        public object[]? ErrorMessages { get; set; }
    }
}
