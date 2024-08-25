using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Laboratory.AsyncLocalStorage
{
    public record class StackItem
    {
        public required string Context { get; init; }
        public required int Depth { get; init; }
        public required int Index { get; init; }
    }
}
