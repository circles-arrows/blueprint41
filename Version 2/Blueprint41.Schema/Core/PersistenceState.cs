using System;
using System.Collections.Generic;

namespace Blueprint41.Core
{
    public enum PersistenceState
    {
        New,
        NewAndChanged,
        HasUid,
        Loaded,
        LoadedAndChanged,
        Persisted,
        Delete,
        ForceDelete,
        Deleted,
        DoesntExist,
        OutOfScope,
        Error,
    }
}
