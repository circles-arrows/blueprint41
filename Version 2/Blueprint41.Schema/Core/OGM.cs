using System;
using System.Collections.Generic;

namespace Blueprint41.Core
{
#pragma warning disable S101 // Types should be named in PascalCase
#pragma warning disable VSSpell001 // Spell Check
    public interface OGM
#pragma warning restore VSSpell001 // Spell Check
#pragma warning restore S101 // Types should be named in PascalCase
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