using Blueprint41.Core;
using Blueprint41.Neo4j.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.TypeConversion
{
    internal class ListOfStringToListOfObject : Conversion<List<string>, List<object>>
    {
        protected override List<object> Converter(List<string> value)
        {
            if (value == null)
                return null;

            return value.Cast<object>().ToList();
        }
    }
}
