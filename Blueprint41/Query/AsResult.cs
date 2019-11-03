using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.Query
{
    public class AsResult : Result
    {
        internal AsResult(Result result, string alias)
        {
            Result = result;
            AliasName = alias;
        }

        public Result Result { get; private set; }
        public string AliasName { get; private set; }

        protected internal override void Compile(CompileState state)
        {
            Result.Compile(state);
            state.Text.Append(" AS ");
            state.Text.Append(AliasName);
        }

        public override Type? GetResultType()
        {
            return Result.GetResultType();
        }
        public override string? GetFieldName()
        {
            return AliasName;
        }
    }
}
