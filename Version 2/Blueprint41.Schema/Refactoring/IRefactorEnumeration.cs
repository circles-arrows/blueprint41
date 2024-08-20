using System;
using System.Collections.Generic;

namespace Blueprint41.Refactoring
{
    public interface IRefactorEnumeration
    {
        void RemoveValue(string name);
        void RemoveValues(params string[] names);
        void Rename(string newName);
        void Deprecate();
    }
}
