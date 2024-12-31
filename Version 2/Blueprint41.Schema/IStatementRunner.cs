using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Blueprint41
{
    public interface IStatementRunner : IDisposable
    {
        DateTime TransactionDate { get; }
        OptimizeFor OptimizeFor { get; }
        ReadWriteMode ReadWriteMode { get; }

        Persistence.ResultCursor Run(string cypher, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0);
        Persistence.ResultCursor Run(string cypher, Dictionary<string, object?>? parameters, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0);

        Persistence.PersistenceProvider PersistenceProvider { get; }
    }
    public interface IStatementRunnerAsync : IAsyncDisposable
    {
        DateTime TransactionDate { get; }
        OptimizeFor OptimizeFor { get; }
        ReadWriteMode ReadWriteMode { get; }

        Task<Persistence.ResultCursor> RunAsync(string cypher, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0);
        Task<Persistence.ResultCursor> RunAsync(string cypher, Dictionary<string, object?>? parameters, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0);

        Persistence.PersistenceProvider PersistenceProvider { get; }
    }
}
