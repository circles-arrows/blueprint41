#nullable disable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.Neo4j.Refactoring
{
    public interface IRefactorGlobal
    {
        /// <summary>
        /// (Re-)Create FunctionalId definitions in the database and set the initial seed to the max value found in the database.
        /// </summary>
        [Obsolete("Under normal conditions, you should not need to execute this method manually. It is executed automatically.", false)]
        void ApplyFunctionalIds();

        void ApplyConstraints();
        void SetCreationDate();
        void ApplyFullTextSearchIndexes();
        void ApplyFullTextSearchIndexes(bool shouldRun);

        bool HasFullTextSearchIndexes();
    }
}
