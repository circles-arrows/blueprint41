using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.Query
{
    public class StringListResult : ListResult
    {
        internal StringListResult(FieldResult field) : base(field) { }
        public StringListResult(string function, object[] arguments, Type type) : base(function, arguments, type) { }
        public StringListResult(AliasResult alias, string fieldName, Entity entity, Property property) : base(alias, fieldName, entity, property) { }
        public StringListResult(FieldResult field, string function, object[] arguments = null, Type type = null) : base(field, function, arguments, type) { }

        public StringResult this[int index]
        {
            get
            {
                return new StringResult(this, "{base}[{0}]", new object[] { Parameter.Constant<int>(index) }, typeof(string));
            }
        }

        public StringResult Reduce(string init, Func<StringResult, StringResult, StringResult> logic)
        {
            StringResult valueField = new StringResult("value", new object[0], typeof(string));
            StringResult itemField = new StringResult("item", new object[0], typeof(string));
            StringResult result = logic.Invoke(valueField, itemField);

            return new StringResult(this, "reduce(value = {0}, item in {base} | {1})", new object[] { Parameter.Constant<string>(init), result }, typeof(string));
        }

        public StringResult Union(StringListResult stringListResult)
        {
            return new StringResult(this, "{base} + {0}", new object[] { stringListResult });
        }
    }
}
