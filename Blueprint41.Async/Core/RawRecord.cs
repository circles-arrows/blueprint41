using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Blueprint41.Async.Core
{
    public abstract class RawRecord
    {
        public abstract object this[int index] { get; }
        public abstract object this[string key] { get; }
        public abstract IReadOnlyDictionary<string, object> Values { get; }
        public abstract IReadOnlyList<string> Keys { get; }

    }
}
