using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Blueprint41.Driver
{
    //
    // Summary:
    //     Provides a way to generate a Neo4j.Driver.Config instance fluently.
    public sealed class ConfigBuilder
    {
        internal ConfigBuilder(object instance)
        {
            _instance = instance;
        }
        internal object _instance { get; private set; }

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
            Driver.CONFIG_BUILDER.WithMaxIdleConnectionPoolSize(_instance, size);
            return this;
        }
        public ConfigBuilder WithMaxConnectionPoolSize(int size)
        {
            Driver.CONFIG_BUILDER.WithMaxConnectionPoolSize(_instance, size);
            return this;
        }
        public ConfigBuilder WithConnectionAcquisitionTimeout(TimeSpan timeSpan)
        {
            Driver.CONFIG_BUILDER.WithConnectionAcquisitionTimeout(_instance, timeSpan);
            return this;
        }
        public ConfigBuilder WithConnectionTimeout(TimeSpan timeSpan)
        {
            Driver.CONFIG_BUILDER.WithConnectionTimeout(_instance, timeSpan);
            return this;
        }
        public ConfigBuilder WithSocketKeepAliveEnabled(bool enable)
        {
            Driver.CONFIG_BUILDER.WithSocketKeepAliveEnabled(_instance, enable);
            return this;
        }
        public ConfigBuilder WithMaxTransactionRetryTime(TimeSpan time)
        {
            Driver.CONFIG_BUILDER.WithMaxTransactionRetryTime(_instance, time);
            return this;
        }
        public ConfigBuilder WithConnectionIdleTimeout(TimeSpan timeSpan)
        {
            Driver.CONFIG_BUILDER.WithConnectionIdleTimeout(_instance, timeSpan);
            return this;
        }
        public ConfigBuilder WithMaxConnectionLifetime(TimeSpan timeSpan)
        {
            Driver.CONFIG_BUILDER.WithMaxConnectionLifetime(_instance, timeSpan);
            return this;
        }
        public ConfigBuilder WithIpv6Enabled(bool enable)
        {
            Driver.CONFIG_BUILDER.WithIpv6Enabled(_instance, enable);
            return this;
        }

        // Not Implemented
        public ConfigBuilder WithResolver(ServerAddressResolver resolver)
        {
            object neo4jResolver = Driver.I_SERVER_ADDRESS_RESOLVER.ConvertToIServerAddressResolver(resolver);

            throw new NotImplementedException();
            return this;
        }

        public ConfigBuilder WithDefaultReadBufferSize(int defaultReadBufferSize)
        {
            Driver.CONFIG_BUILDER.WithDefaultReadBufferSize(_instance, defaultReadBufferSize);
            return this;
        }
        public ConfigBuilder WithMaxReadBufferSize(int maxReadBufferSize)
        {
            Driver.CONFIG_BUILDER.WithMaxReadBufferSize(_instance, maxReadBufferSize);
            return this;
        }
        public ConfigBuilder WithDefaultWriteBufferSize(int defaultWriteBufferSize)
        {
            Driver.CONFIG_BUILDER.WithDefaultWriteBufferSize(_instance, defaultWriteBufferSize);
            return this;
        }
        public ConfigBuilder WithMaxWriteBufferSize(int maxWriteBufferSize)
        {
            Driver.CONFIG_BUILDER.WithMaxWriteBufferSize(_instance, maxWriteBufferSize);
            return this;
        }
        public ConfigBuilder WithFetchSize(long size)
        {
            Driver.CONFIG_BUILDER.WithFetchSize(_instance, size);
            return this;
        }

        // Not Implemented
        internal ConfigBuilder WithMetricsEnabled(bool enabled)
        {
            throw new NotImplementedException();
            return this;
        }
        // Not Implemented
        public ConfigBuilder WithUserAgent(string userAgent)
        {
            throw new NotImplementedException();
            return this;
        }





        // NOT SUPPORTED IN DRIVER v4.4


        //// Not Implemented
        //public ConfigBuilder WithCertificateTrustRule(CertificateTrustRule certificateTrustRule, IReadOnlyList<X509Certificate2>? trustedCaCertificates = null)
        //{
        //    throw new NotImplementedException();
        //    return this;
        //}
        //// Not Implemented
        //public ConfigBuilder WithCertificateTrustRule(CertificateTrustRule certificateTrustRule, IReadOnlyList<string>? trustedCaCertificateFileNames = null)
        //{
        //    throw new NotImplementedException();
        //    return this;
        //}
        //// Not Implemented
        //public ConfigBuilder WithNotifications(Severity? minimumSeverity, Category[] disabledCategories)
        //{
        //    throw new NotImplementedException();
        //    return this;
        //}
        //// Not Implemented
        //public ConfigBuilder WithNotificationsDisabled()
        //{
        //    throw new NotImplementedException();
        //    return this;
        //}
    }
}
