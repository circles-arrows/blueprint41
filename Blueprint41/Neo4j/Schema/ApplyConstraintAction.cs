using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Neo4j.Driver.V1;

namespace Blueprint41.Neo4j.Schema
{
    public enum ApplyConstraintAction
    {
        CreateIndex,
        CreateUniqueConstraint,
        CreateExistsConstraint,
        DeleteIndex,
        DeleteUniqueConstraint,
        DeleteExistsConstraint,
    }
}
