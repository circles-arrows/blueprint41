﻿using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Blueprint41.Core
{
    internal interface IStatementRunner
    {
        DateTime TransactionDate { get; }
        OptimizeFor OptimizeFor { get; }
        ReadWriteMode ReadWriteMode { get; }

        RawResult Run(string cypher, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0);
        RawResult Run(string cypher, Dictionary<string, object?>? parameters, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0);
    }
}