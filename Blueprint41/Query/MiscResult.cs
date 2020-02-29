using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.Query
{
    public partial class MiscResult : FieldResult
    {
        internal MiscResult(FieldResult field) : base(field) { }
        public MiscResult(string function, object[]? arguments, Type? type) : base(function, arguments, type) { }
        public MiscResult(AliasResult alias, string fieldName, Entity entity, Property? property) : base(alias, fieldName, entity, property) { }
        public MiscResult(FieldResult field, string function, object[]? arguments = null, Type? type = null) : base(field, function, arguments, type) { }

        public MiscResult Coalesce(object value)
        {
            if (value == null)
                throw new NullReferenceException("value cannot be null");

            return new MiscResult(this, "coalesce({base}, {0})", new object[] { Parameter.Constant(value, value.GetType()) });
        }
        public MiscResult Coalesce(MiscResult value)
        {
            return new MiscResult(this, "coalesce({base}, {0})", new object[] { value });
        }
        public MiscResult Coalesce(Parameter value)
        {
            return new MiscResult(this, "coalesce({base}, {0})", new object[] { value });
        }
    }
}
