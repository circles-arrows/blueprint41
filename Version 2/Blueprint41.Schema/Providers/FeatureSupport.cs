using System;
using System.Collections.Generic;
using System.Text;

namespace Blueprint41.Providers
{
    public record FeatureSupport
    {
        public bool Index;
        public bool Exists;
        public bool Unique;
        public bool Key;
        public bool Type;
    }
}
