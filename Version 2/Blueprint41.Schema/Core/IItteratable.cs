using System;
using System.Collections.Generic;

namespace Blueprint41.Core
{
    public interface IItteratable<T>
    {
        void ForEach(Action<int, T> action);
    }
}
