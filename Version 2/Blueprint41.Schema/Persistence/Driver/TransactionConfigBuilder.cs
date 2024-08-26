using System;
using System.Collections.Generic;
using System.Text;

namespace Blueprint41.Persistence
{
    public sealed class TransactionConfigBuilder
    {
        internal TransactionConfigBuilder(object value)
        {
            Value = value;
        }
        internal object Value { get; private set; }

        public TransactionConfigBuilder WithTimeout(TimeSpan? timeout)
        {
            Driver.TRANSACTION_CONFIG_BUILDER.WithTimeout(Value, timeout);
            return this;
        }

        public TransactionConfigBuilder WithMetadata(IDictionary<string, object> metadata)
        {
            Driver.TRANSACTION_CONFIG_BUILDER.WithMetadata(Value, metadata);
            return this;
        }
    }
}
