#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;

namespace Blueprint41.Query
{
    public static partial class Functions
    {
        public static partial class Apoc
        {
            public static partial class Coll
            {
                public static MiscJaggedListResult PairsMin(MiscListResult list)
                {
                    return new MiscJaggedListResult(list, t => t.FnApocCollPairsMin);
                }
            }
            public static partial class Json
            {
                public static MiscResult Path(StringResult json, StringResult path, PathOptions? pathOptions = null)
                {
                    if (pathOptions.HasValue)
                        return new MiscResult(json, t => t.FnApocJsonPath(2), new object[] { path, GetPathOptions(pathOptions.Value) });

                    return new MiscResult(json, t => t.FnApocJsonPath(1), new object[] { path });
                }
                public static MiscResult Path(StringResult json, string? path = null, PathOptions? pathOptions = null)
                {
                    if (pathOptions.HasValue)
                        return new MiscResult(json, t => t.FnApocJsonPath(2), new object[] { Parameter.Constant(path ?? "$"), GetPathOptions(pathOptions.Value) });

                    if (path is not null)
                        return new MiscResult(json, t => t.FnApocJsonPath(1), new object[] { Parameter.Constant(path) });

                    return new MiscResult(json, t => t.FnApocJsonPath(0));
                }
            }
            public static partial class Map
            {
                public static StringJaggedListResult SortedProperties(MiscResult map)
                {
                    return new StringJaggedListResult(map, t => t.FnApocMapSortedProperties);
                }
            }
        }
    }
}
