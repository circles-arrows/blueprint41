using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
