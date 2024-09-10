using System;
using System.Collections.Generic;
using System.Text;

using Blueprint41.Persistence;

namespace Blueprint41.Config
{
    public sealed class TransactionConfigBuilder
    {
        internal TransactionConfigBuilder(object instance)
        {
            _instance = instance;
        }
        internal object _instance { get; private set; }

        public TransactionConfigBuilder WithTimeout(TimeSpan? timeout)
        {
            Driver.TRANSACTION_CONFIG_BUILDER.WithTimeout(_instance, timeout);
            return this;
        }

        public TransactionConfigBuilder WithMetadata(IDictionary<string, object> metadata)
        {
            Driver.TRANSACTION_CONFIG_BUILDER.WithMetadata(_instance, metadata);
            return this;
        }
    }
}
