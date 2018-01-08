using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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
