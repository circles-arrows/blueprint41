using System;
using System.Collections.Generic;

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
