using System;
using System.Collections.Generic;

namespace Blueprint41.Persistence
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
