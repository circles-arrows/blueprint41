using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Blueprint41.Core;

namespace Blueprint41.Neo4j.Schema
{
    public class FunctionalIdInfo
    {
        internal FunctionalIdInfo(RawRecord record)
        {
            Initialize(record);
        }
        protected virtual void Initialize(RawRecord record)
        {
            Label = record.Values["Label"].As<string>();
            Prefix = record.Values["Prefix"].As<string>();
            SequenceNumber = (int)record.Values["Sequence"].As<long>();
            SequenceHash = record.Values["Uid"].As<string>();
        }

        public string Label { get; protected set; } = null!;
        public string Prefix { get; protected set; } = null!;
        public int SequenceNumber { get; protected set; }
        public string SequenceHash { get; protected set; } = null!;

    }
}