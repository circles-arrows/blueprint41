using Blueprint41;
using Blueprint41.Core;
using System.Collections.Generic;

public interface OGM
{
    object GetKey();
    void SetKey(object key);
    IDictionary<string,object> GetData();
    void SetData(IReadOnlyDictionary<string, object> data);
    void Delete(bool force);
    void Save();
    void ValidateSave();
    void ValidateDelete();

    PersistenceState PersistenceState { get; set; }
    Transaction Transaction { get; set; }

    Entity GetEntity();
}