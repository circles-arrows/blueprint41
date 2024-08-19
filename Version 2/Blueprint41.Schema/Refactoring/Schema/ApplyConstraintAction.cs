using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Blueprint41.Refactoring.Schema
{
    public enum ApplyConstraintAction
    {
        CreateIndex,
        CreateUniqueConstraint,
        CreateExistsConstraint,
        CreateKeyConstraint,
        DeleteIndex,
        DeleteUniqueConstraint,
        DeleteExistsConstraint,
        DeleteKeyConstraint
    }
}
