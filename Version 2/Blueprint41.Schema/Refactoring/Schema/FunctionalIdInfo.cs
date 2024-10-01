using System;
using System.Collections.Generic;

using Blueprint41.Core;

namespace Blueprint41.Refactoring.Schema
{
    public class FunctionalIdInfo
    {
        internal FunctionalIdInfo(IReadOnlyDictionary<string, object> record)
        {
            Initialize(record);
        }
        protected virtual void Initialize(IReadOnlyDictionary<string, object> record)
        {
            Label = record["Label"].As<string>();
            Prefix = record["Prefix"].As<string>();
            SequenceNumber = record["Sequence"].As<long>();
            SequenceHash = record["Uid"].As<string>();
        }

        public string Label { get; protected set; } = null!;
        public string Prefix { get; protected set; } = null!;
        public long SequenceNumber { get; protected set; }
        public string SequenceHash { get; protected set; } = null!;

    }
}