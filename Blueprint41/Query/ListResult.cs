using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.Query
{
    public class ListResult : FieldResult
    {
        internal ListResult(FieldResult field) : base(field) { }
        public ListResult(string function, object[] arguments, Type type) : base(function, arguments, type) { }
        public ListResult(AliasResult alias, string fieldName, Entity entity, Property property) : base(alias, fieldName, entity, property) { }
        public ListResult(FieldResult field, string function, object[] arguments = null, Type type = null) : base(field, function, arguments, type) { }
    }
}
