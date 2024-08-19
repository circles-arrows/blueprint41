﻿using System;
using System.Collections.Generic;

using Blueprint41;

namespace Blueprint41.Core
{
    public interface OGM
    {
        object? GetKey();
        void SetKey(object key);
        DateTime GetRowVersion();
        void SetRowVersion(DateTime? value);
        IDictionary<string, object?> GetData();
        void SetData(IReadOnlyDictionary<string, object?> data);
        void Delete(bool force);
        void Save();
        void ValidateSave();
        void ValidateDelete();

        PersistenceState OriginalPersistenceState { get; set; }
        PersistenceState PersistenceState { get; set; }
        Transaction? Transaction { get; set; }
        Transaction RunningTransaction { get; }

        Entity GetEntity();

        void SetChanged();
    }
}