using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.Query
{
    internal class Litheral : Result
    {
        internal Litheral(string text)
        {
            Text = text;
        }
        internal Litheral(decimal value)
        {
            Text = value.ToString();
        }
        internal Litheral(long value)
        {
            Text = value.ToString();
        }

        public string Text { get; private set; }

        protected internal override void Compile(CompileState state)
        {
            state.Text.Append(Text);
        }
        public override string GetFieldName()
        {
            throw new NotSupportedException();
        }
        public override Type GetResultType()
        {
            return null;
        }
    }
}
