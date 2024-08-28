using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blueprint41.Driver
{
    public interface IQueryRunner
    {
        Task<ResultCursor> RunAsync(string query);
        Task<ResultCursor> RunAsync(string query, Dictionary<string, object?> parameters);
    }
}
