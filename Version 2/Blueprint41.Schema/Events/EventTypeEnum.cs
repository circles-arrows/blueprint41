using System;
using System.Collections.Generic;
using System.Text;

namespace Blueprint41.Events
{
    public enum EventTypeEnum
    {
        OnNew,
        OnPropertyChange,
        OnSave,
        OnAfterSave,
        OnDelete,
        OnBegin,
        OnCommit,
        OnNodeLoading,
        OnNodeLoaded,
        OnBatchFinished,
        OnNodeCreate,
        OnNodeCreated,
        OnNodeUpdate,
        OnNodeUpdated,
        OnNodeDelete,
        OnNodeDeleted,
        OnRelationCreate,
        OnRelationCreated,
        OnRelationDelete,
        OnRelationDeleted,
        LoadedViaCompiledQuery
    }
}
