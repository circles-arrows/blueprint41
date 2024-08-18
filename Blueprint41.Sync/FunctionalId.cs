using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.Sync
{
    /// <summary>
    /// A functional id
    /// </summary>
    public class FunctionalId
    {
        internal FunctionalId(DatastoreModel parent, string label, string prefix, IdFormat format, int startFrom)
        {
            Label = label;
            Prefix = prefix;
            Format = format;
            StartFrom = startFrom < 0 ? 0 : startFrom;
            Guid = parent?.GenerateGuid(label) ?? Guid.Empty;
        }

        #region Properties

        /// <summary>
        /// The label of the functional id
        /// </summary>
        public string Label { get; private set; }

        /// <summary>
        /// The prefix of the functional id
        /// </summary>
        public string Prefix { get; private set; }

        /// <summary>
        /// The format of the functional id
        /// </summary>
        public IdFormat Format { get; private set; }

        /// <summary>
        /// The start value of the functional id
        /// </summary>
        public int StartFrom { get; private set; }
        
        /// <summary>
        /// A unique identifier for the functional id
        /// </summary>
        public Guid Guid { get; private set; }

        #endregion

        /// <summary>
        /// Sets the prefix of the functional id
        /// </summary>
        /// <param name="DataMigration">The data-migration scope</param>
        /// <param name="prefix">The prefix to set</param>
        public void SetPrefix(DatastoreModel.DataMigrationScope DataMigration, string prefix)
        {
            Prefix = prefix;
            DataMigration.Run(delegate ()
            {
                DataMigration.ExecuteCypher(
                       $@"MATCH (fi:FunctionalId)
                        WHERE fi.Label='{Label}'
                        SET fi.Prefix='{Prefix}'");
            });
        }
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

        /// <summary>
        /// Get the next functional id
        /// </summary>
        /// <returns>The next functional id</returns>
        public string NextFunctionID()
        {
            return Transaction.RunningTransaction.NodePersistenceProvider.NextFunctionID(this);
        }

        #endregion
    }
}
