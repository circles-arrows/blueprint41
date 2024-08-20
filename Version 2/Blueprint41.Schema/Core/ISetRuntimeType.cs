using System;
using System.Collections.Generic;

namespace Blueprint41.Core
{
    public interface ISetRuntimeType
    {
        [Obsolete("This method is reserved for internal use by the generated code", true)]
        void SetRuntimeTypes(Type returnType, Type classType);
    }
}
