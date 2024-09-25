using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.Query
{
    public abstract class Result
    {
        internal protected abstract void Compile(CompileState state);

        public abstract string? GetFieldName();
        public abstract Type? GetResultType();
    }
}
