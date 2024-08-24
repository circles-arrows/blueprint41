using System;
using System.Collections.Generic;

using Blueprint41.Core;
using t = Blueprint41.Refactoring.Templates;

namespace Blueprint41.Refactoring
{
    public class RefactorTemplates_MemgraphV1 : RefactorTemplates_Neo4jV5
    {
        internal RefactorTemplates_MemgraphV1(DatastoreModel datastoreModel) : base(datastoreModel) { }
    }
}
