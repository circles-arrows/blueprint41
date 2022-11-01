using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.ApocDocumentationParser
{
    public class ApocParameter
    {
        internal ApocParameter(ApocMethod parent, string name, string type, string? defaultValue)
        {
            Parent = parent;
            Name = name;
            ParameterType = type;
            DefaultValue = (defaultValue?.ToLowerInvariant() == "null") ? null : DefaultValue;
            HasDefaultValue = DefaultValue is not null;
        }

        public ApocMethod Parent { get; private set; }
        public string Name { get; private set; }
        public string ParameterType { get; private set; }
        public string? DefaultValue { get; private set; }
        public bool HasDefaultValue { get; private set; }
    }
}
