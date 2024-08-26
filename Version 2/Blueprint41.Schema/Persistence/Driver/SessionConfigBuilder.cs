using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blueprint41.Persistence
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

        public SessionConfigBuilder WithDefaultAccessMode(ReadWriteMode defaultAccessMode)
        {
            object readwrite = defaultAccessMode switch
            {
                ReadWriteMode.ReadWrite => Driver.ACCESS_MODE.Write,
                ReadWriteMode.ReadOnly  => Driver.ACCESS_MODE.Read,
                _ => throw new NotSupportedException()
            };

            Driver.SESSION_CONFIG_BUILDER.WithDefaultAccessMode(Value, readwrite);
            return this;
        }

        public SessionConfigBuilder WithBookmarks(params Bookmark[] bookmarks)
        {
            object[] tokens = bookmarks.Select(item => Driver.BOOKMARKS.From(item.ToToken())).ToArray();

            Driver.SESSION_CONFIG_BUILDER.WithBookmarks(Value, tokens);
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
