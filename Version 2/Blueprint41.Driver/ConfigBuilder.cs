using System;
using System.Collections.Generic;
using System.Text;

namespace Blueprint41.Driver
{
    //
    // Summary:
    //     Provides a way to generate a Neo4j.Driver.Config instance fluently.
    public sealed class ConfigBuilder
    {
        internal ConfigBuilder(object value)
        {
            Value = value;
        }
        internal object Value { get; private set; }

        public const int Infinite = -1;
        public static readonly TimeSpan InfiniteInterval = TimeSpan.FromMilliseconds(-1.0);

        //public ConfigBuilder WithEncryptionLevel(EncryptionLevel level)
        //{
        //    _config.NullableEncryptionLevel = level;
        //    return this;
        //}
        //public ConfigBuilder WithTrustManager(TrustManager manager)
        //{
        //    _config.TrustManager = manager;
        //    return this;
        //}
        //public ConfigBuilder WithLogger(ILogger logger)
        //{
        //    _config.Logger = logger;
        //    return this;
        //}
        public ConfigBuilder WithMaxIdleConnectionPoolSize(int size)
        {
            Driver.CONFIG_BUILDER.WithMaxIdleConnectionPoolSize(Value, size);
            return this;
        }
        public ConfigBuilder WithMaxConnectionPoolSize(int size)
        {
            Driver.CONFIG_BUILDER.WithMaxConnectionPoolSize(Value, size);
            return this;
        }
        public ConfigBuilder WithConnectionAcquisitionTimeout(TimeSpan timeSpan)
        {
            Driver.CONFIG_BUILDER.WithConnectionAcquisitionTimeout(Value, timeSpan);
            return this;
        }
        public ConfigBuilder WithConnectionTimeout(TimeSpan timeSpan)
        {
            Driver.CONFIG_BUILDER.WithConnectionTimeout(Value, timeSpan);
            return this;
        }
        public ConfigBuilder WithSocketKeepAliveEnabled(bool enable)
        {
            Driver.CONFIG_BUILDER.WithSocketKeepAliveEnabled(Value, enable);
            return this;
        }
        public ConfigBuilder WithMaxTransactionRetryTime(TimeSpan time)
        {
            Driver.CONFIG_BUILDER.WithMaxTransactionRetryTime(Value, time);
            return this;
        }
        public ConfigBuilder WithConnectionIdleTimeout(TimeSpan timeSpan)
        {
            Driver.CONFIG_BUILDER.WithConnectionIdleTimeout(Value, timeSpan);
            return this;
        }
        public ConfigBuilder WithMaxConnectionLifetime(TimeSpan timeSpan)
        {
            Driver.CONFIG_BUILDER.WithMaxConnectionLifetime(Value, timeSpan);
            return this;
        }
        public ConfigBuilder WithIpv6Enabled(bool enable)
        {
            Driver.CONFIG_BUILDER.WithIpv6Enabled(Value, enable);
            return this;
        }
        //public ConfigBuilder WithResolver(IServerAddressResolver resolver)
        //{
        //    Driver.ConfigBuilder.Resolver = resolver;
        //    return this;
        //}
        public ConfigBuilder WithDefaultReadBufferSize(int defaultReadBufferSize)
        {
            Driver.CONFIG_BUILDER.WithDefaultReadBufferSize(Value, defaultReadBufferSize);
            return this;
        }
        public ConfigBuilder WithMaxReadBufferSize(int maxReadBufferSize)
        {
            Driver.CONFIG_BUILDER.WithMaxReadBufferSize(Value, maxReadBufferSize);
            return this;
        }
        public ConfigBuilder WithDefaultWriteBufferSize(int defaultWriteBufferSize)
        {
            Driver.CONFIG_BUILDER.WithDefaultWriteBufferSize(Value, defaultWriteBufferSize);
            return this;
        }
        public ConfigBuilder WithMaxWriteBufferSize(int maxWriteBufferSize)
        {
            Driver.CONFIG_BUILDER.WithMaxWriteBufferSize(Value, maxWriteBufferSize);
            return this;
        }
        public ConfigBuilder WithFetchSize(long size)
        {
            Driver.CONFIG_BUILDER.WithFetchSize(Value, size);
            return this;
        }
    }
}
