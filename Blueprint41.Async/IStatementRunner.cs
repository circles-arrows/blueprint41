using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

using Blueprint41.Async.Core;

namespace Blueprint41.Async
{
    internal interface IStatementRunner
    {
        RawResult Run(string cypher, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0);
        RawResult Run(string cypher, Dictionary<string, object?>? parameters, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0);
    }
}
