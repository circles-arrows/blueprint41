using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.Query
{
    public class AliasResult : Result
    {
        public static QueryCondition operator ==(AliasResult a, AliasResult b)
        {
            return new QueryCondition(a, Operator.Equals, b);
        }
        public static QueryCondition operator ==(AliasResult a, Parameter b)
        {
            return new QueryCondition(a, Operator.Equals, b);
        }
        public static QueryCondition operator !=(AliasResult a, AliasResult b)
        {
            return new QueryCondition(a, Operator.NotEquals, b);
        }
        public static QueryCondition operator !=(AliasResult a, Parameter b)
        {
            return new QueryCondition(a, Operator.NotEquals, b);
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public string AliasName { get; internal set; }
        public Node Node { get; protected set; }

        protected internal override void Compile(CompileState state)
        {
            state.Text.Append(AliasName);
        }

        public QueryCondition HasLabel(string label)
        {
            return new QueryCondition(this, Operator.HasLabel, new Litheral(label));
        }

        public AsResult As(string alias)
        {
            return new AsResult(this, alias);
        }

        public override string GetFieldName()
        {
            return AliasName;
        }

        public override Type GetResultType()
        {
            return null;
        }

        new public StringResult ToString()
        {
            return new StringResult(AliasName, null, typeof(string));
        }

        public virtual IReadOnlyDictionary<string, FieldResult> AliasFields { get { return null; }  }
    }
}
