using System;
using System.Collections.Generic;

namespace Blueprint41.Refactoring
{
    public interface IRefactorGlobal
    {
        /// <summary>
        /// (Re-)Create FunctionalId definitions in the database and set the initial seed to the max value found in the database.
        /// </summary>
        [Obsolete("Under normal conditions, you should not need to execute this method manually. It is executed automatically.", false)]
        void ApplyFunctionalIds();

        /// <summary>
        /// Apply any missing constraint or index to the database
        /// </summary>
        void ApplyConstraints();

        /// <summary>
        /// Apply any missing full-text-index to the database
        /// </summary>
        void ApplyFullTextSearchIndexes();

        /// <summary>
        /// Apply any missing full-text-index to the database
        /// (force apply when shouldRun = true)
        /// </summary>
        void ApplyFullTextSearchIndexes(bool shouldRun);

        /// <summary>
        /// Scans the data model if any full-text-indexes exist
        /// </summary>
        bool HasFullTextSearchIndexes();
    }
}
