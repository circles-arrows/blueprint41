using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blueprint41.Persistence
{
    public interface IQueryRunner
    {
        ResultCursor Run(string query);
        ResultCursor Run(string query, Dictionary<string, object?> parameters);

        Task<ResultCursor> RunAsync(string query);
        Task<ResultCursor> RunAsync(string query, Dictionary<string, object?> parameters);
    }
}
