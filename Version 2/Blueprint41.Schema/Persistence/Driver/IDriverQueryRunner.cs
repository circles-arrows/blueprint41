using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blueprint41.Persistence
{
    public interface IDriverQueryRunner
    {
        Task<DriverResultSet> RunAsync(string query);
        Task<DriverResultSet> RunAsync(string query, Dictionary<string, object?> parameters);
    }
}
