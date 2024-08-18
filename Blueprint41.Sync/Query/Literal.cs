using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.Sync.Query
{
    internal class Literal : Result
    {
        internal Literal(string text)
        {
            Text = text;
        }
        internal Literal(decimal value)
        {
            Text = value.ToString();
        }
        internal Literal(long value)
        {
            Text = value.ToString();
        }

        public string Text { get; private set; }

        protected internal override void Compile(CompileState state)
        {
            state.Translator.Compile(this, state);
        }
        public override string GetFieldName()
        {
            throw new NotSupportedException();
        }
        public override Type? GetResultType()
        {
            return null;
        }
    }
}
