using System;
using System.Collections.Generic;
using System.Reflection;

using Blueprint41.Refactoring.Schema;

namespace Blueprint41
{
    public interface IDatastoreUnitTesting
    {
        /// <summary>
        /// Execute the data-store and subsequently execute a unit-test script if provided
        /// </summary>
        /// <param name="upgradeDatastore">Whether or not the data-store should be upgraded</param>
        /// <param name="unitTestScript">The unit-test script</param>
        void Execute(bool upgradeDatastore, MethodInfo? unitTestScript);

        /// <summary>
        /// Get the schema info for the data-store
        /// </summary>
        /// <returns>The schema info</returns>
        SchemaInfo GetSchemaInfo();
    }
}
