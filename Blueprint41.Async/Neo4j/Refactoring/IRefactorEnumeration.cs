using System;
using System.Dynamic;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Blueprint41.Async.Dynamic;

namespace Blueprint41.Async.Neo4j.Refactoring
{
    public interface IRefactorEnumeration
    {
        void RemoveValue(string name);
        void RemoveValues(params string[] names);
        void Rename(string newName);
        void Deprecate();
    }
}
