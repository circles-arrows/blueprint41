using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

using Blueprint41.Core;

namespace Blueprint41
{
    public interface IParameter
    {
        string Name { get; }
        object? Value { get; }
        bool HasValue { get; }

        Type? Type { get; }
        bool IsConstant { get; }
    }
}
