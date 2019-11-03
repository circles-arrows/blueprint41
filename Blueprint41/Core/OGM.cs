using Blueprint41;
using Blueprint41.Core;
using System;
using System.Collections.Generic;

public interface OGM
{
    object? GetKey();
    void SetKey(object key);
    DateTime GetRowVersion();
    void SetRowVersion(DateTime? value);
    IDictionary<string,object?> GetData();
    void SetData(IReadOnlyDictionary<string, object?> data);
    void Delete(bool force);
    void Save();
    void ValidateSave();
    void ValidateDelete();

    PersistenceState PersistenceState { get; set; }
    Transaction? Transaction { get; set; }

    Entity GetEntity();

    void SetChanged();
}