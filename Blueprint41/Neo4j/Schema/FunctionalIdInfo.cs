#nullable disable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Neo4j.Driver.V1;

namespace Blueprint41.Neo4j.Schema
{
    public class FunctionalIdInfo
    {
        internal FunctionalIdInfo(IRecord record)
        {
            Label = record.Values["Label"].As<string>();
            Prefix = record.Values["Prefix"].As<string>();
            SequenceNumber = (int)record.Values["Sequence"].As<long>();
            SequenceHash = record.Values["Uid"].As<string>();
        }

        public string Label { get; private set; }
        public string Prefix { get; private set; }
        public int SequenceNumber { get; private set; }
        public string SequenceHash { get; private set; }

    }
}