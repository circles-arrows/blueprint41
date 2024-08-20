using System;
using System.Collections.Generic;

namespace Blueprint41.Refactoring
{
    public enum MergeAlgorithm
    {
        NotApplicable,
        ThrowOnConflict,
        PreferSource,
        PreferTarget,
    }

    public enum ConvertAlgorithm
    {
        ThrowOnConflict,
        TakeLast,
        TakeFirst,
    }

    public enum ApplyAlgorithm
    {
        /// <summary>
        /// None of the previously assigned primary key values will be changed.
        /// </summary>
        DoNotApply,

        /// <summary>
        /// Previously assigned primary key values that do not match the newly assigned prefix will be overwritten with new values.
        /// </summary>
        ReapplyIncompatible,

        /// <summary>
        /// All of the previously assigned primary key values will be overwritten with new values.
        /// </summary>
        ReapplyAll
    }
}
