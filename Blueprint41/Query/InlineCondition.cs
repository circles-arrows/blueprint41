using System;
using System.Collections.Generic;
using System.Text;

namespace Blueprint41.Query
{
    public struct InlineCondition<T>
    {
        public Parameter Parameter;
        public T Value;
        public bool HasValue;

        public static implicit operator InlineCondition<T>(T value)
        {
            return new InlineCondition<T>() { Value = value, HasValue = true };
        }
        public static implicit operator InlineCondition<T>(Parameter parameter)
        {
            return new InlineCondition<T>() { Parameter = parameter, HasValue = true };
        }
    }
}
