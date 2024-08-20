using System;
using System.Collections.Generic;

namespace Blueprint41.Core
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    public class ConversionAttribute : Attribute
    {
        public ConversionAttribute(Type converter)
        {
            Converter = converter;
        }

        public Type Converter { get; private set; }
    }
}
