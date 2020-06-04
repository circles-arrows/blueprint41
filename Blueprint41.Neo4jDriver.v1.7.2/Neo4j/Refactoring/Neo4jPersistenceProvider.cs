using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Blueprint41.Core;
using t = Blueprint41.Neo4j.Refactoring.Templates;

namespace Blueprint41.Core
{
    public class RefactorTemplates_v3 : RefactorTemplates
    {
    }
}

namespace Blueprint41.Neo4j.Persistence.Driver.v3
{
    public partial class Neo4jPersistenceProvider : Void.Neo4jPersistenceProvider
    {
        protected override RefactorTemplates GetTemplates() => new RefactorTemplates_v3();
    }
}
