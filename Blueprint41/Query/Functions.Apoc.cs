using System;
using System.Collections.Generic;
using System.Linq;

namespace Blueprint41.Query
{
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
