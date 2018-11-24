using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41
{
    public class FunctionalId
    {
        internal FunctionalId(DatastoreModel parent, string label, string prefix, IdFormat format, int startFrom)
        {
            Label = label;
            Prefix = prefix;
            Format = format;
            StartFrom = startFrom < 0 ? 0 : startFrom;
            Guid = parent.GenerateGuid(label);
        }

        #region Properties

        public string Label { get; private set; }
        public string Prefix { get; private set; }
        public IdFormat Format { get; private set; }
        public int StartFrom { get; private set; }
        public Guid Guid { get; private set; }

        #endregion

        #region FunctionalId max value cache & apply

        internal bool wasApplied = true;
        internal long highestSeenId = -1;

        internal void SeenUid(string value)
        {
            long decoded = -1;

            if (Format == IdFormat.Hash)
            {
                if (!value.StartsWith(Prefix))
                    return;
                else
                    decoded = Hashing.DecodeIdentifier(value.Substring(Prefix.Length), false);
            }

            if (Format == IdFormat.Numeric && !long.TryParse(value, out decoded))
                return;

            SeenUid(decoded);
        }
        internal void SeenUid(long value)
        {
            if (highestSeenId < value)
            {
                lock (this)
                {
                    highestSeenId = value;
                    wasApplied = false;
                }
            }
        }

        public string NextFunctionID()
        {
            return Transaction.Current.NodePersistenceProvider.NextFunctionID(this);
        }
        #endregion
    }
}
