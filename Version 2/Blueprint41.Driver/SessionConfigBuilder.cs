using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blueprint41.Driver
{
    public sealed class SessionConfigBuilder
    {
        internal SessionConfigBuilder(object value)
        {
            Value = value;
        }
        internal object Value { get; private set; }

        public SessionConfigBuilder WithDatabase(string database)
        {
            Driver.SESSION_CONFIG_BUILDER.WithDatabase(Value, database);
            return this;
        }

        public SessionConfigBuilder WithDefaultAccessMode(AccessMode defaultAccessMode)
        {
            object readwrite = defaultAccessMode switch
            {
                AccessMode.Read  => Driver.ACCESS_MODE.Read,
                AccessMode.Write => Driver.ACCESS_MODE.Write,
                _ => throw new NotSupportedException()
            };

            Driver.SESSION_CONFIG_BUILDER.WithDefaultAccessMode(Value, readwrite);
            return this;
        }

        public SessionConfigBuilder WithBookmarks(params Bookmarks[] bookmarks)
        {
            Driver.SESSION_CONFIG_BUILDER.WithBookmarks(Value, bookmarks);
            return this;
        }

        public SessionConfigBuilder WithFetchSize(long size)
        {
            Driver.SESSION_CONFIG_BUILDER.WithFetchSize(Value, size);
            return this;
        }

        public SessionConfigBuilder WithImpersonatedUser(string impersonatedUser)
        {
            Driver.SESSION_CONFIG_BUILDER.WithImpersonatedUser(Value, impersonatedUser);
            return this;
        }
    }
}
