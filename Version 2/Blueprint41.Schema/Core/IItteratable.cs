using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.Core
{
    public interface IItteratable<T>
    {
        void ForEach(Action<int, T> action);
    }
}
