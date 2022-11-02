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

    public static partial class Functions
    {
        public static partial class Apoc
        {
            public static partial class Json 
            {
                private static Parameter GetPathOptions(PathOptions pathOptions) => Parameter.Constant(pathOptions.ToString().Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries));
            }
         }
    }

    [Flags]
    public enum PathOptions
    {
        NONE = 0,

        /// <summary>
        /// Makes this implementation more compliant to the Goessner spec.
        /// </summary>
        ALWAYS_RETURN_LIST = 1,

        /// <summary>
        /// Returns a list of path strings representing the path of the evaluation hits
        /// </summary>
        AS_PATH_LIST = 2,

        /// <summary>
        /// returns null for missing leaf.
        /// </summary>
        DEFAULT_PATH_LEAF_TO_NULL = 4,

        /// <summary>
        /// Configures JsonPath to require properties defined in path when an indefinite path is evaluated.
        /// </summary>
        REQUIRE_PROPERTIES = 8,

        /// <summary>
        /// Suppress all exceptions when evaluating path.
        /// </summary>
        SUPPRESS_EXCEPTIONS = 16,
    }
}
