using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Blueprint41.Persistence;

namespace Blueprint41.Config
{
    public sealed class SessionConfigBuilder
    {
        internal SessionConfigBuilder(object instance)
        {
            _instance = instance;
        }
        internal object _instance { get; private set; }

        public SessionConfigBuilder WithDatabase(string database)
        {
            Driver.SESSION_CONFIG_BUILDER.WithDatabase(_instance, database);
            return this;
        }

        public SessionConfigBuilder WithDefaultAccessMode(AccessMode defaultAccessMode)
        {
            object readwrite = defaultAccessMode switch
            {
                AccessMode.Read => Driver.ACCESS_MODE.Read,
                AccessMode.Write => Driver.ACCESS_MODE.Write,
                _ => throw new NotSupportedException()
            };

            Driver.SESSION_CONFIG_BUILDER.WithDefaultAccessMode(_instance, readwrite);
            return this;
        }

        public SessionConfigBuilder WithBookmarks(params Bookmarks[] bookmarks)
        {
            Driver.SESSION_CONFIG_BUILDER.WithBookmarks(_instance, bookmarks);
            return this;
        }

        public SessionConfigBuilder WithFetchSize(long size)
        {
            Driver.SESSION_CONFIG_BUILDER.WithFetchSize(_instance, size);
            return this;
        }

        public SessionConfigBuilder WithImpersonatedUser(string impersonatedUser)
        {
            Driver.SESSION_CONFIG_BUILDER.WithImpersonatedUser(_instance, impersonatedUser);
            return this;
        }
    }
}
