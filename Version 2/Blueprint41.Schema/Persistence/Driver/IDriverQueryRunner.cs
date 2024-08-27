using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blueprint41.Persistence
{
    public interface IDriverQueryRunner
    {
        Task<DriverRecordSet> RunAsync(string query);
        Task<DriverRecordSet> RunAsync(string query, Dictionary<string, object?> parameters);
    }
}
