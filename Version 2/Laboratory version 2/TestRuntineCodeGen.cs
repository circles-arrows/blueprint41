﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;

using neo4j = Neo4j.Driver;
using bp41 = Blueprint41.Driver;

namespace Blueprint41.Driver.RuntimeGeneration
{
    public sealed class ServerAddressResolverProxy : neo4j.IServerAddressResolver
    {
        private ServerAddressResolverProxy(bp41.ServerAddressResolver resolver)
        {
            _resolver = resolver;
        }
        public static ServerAddressResolverProxy Get(bp41.ServerAddressResolver resolver)
        {
            return new ServerAddressResolverProxy(resolver);
        }
        private bp41.ServerAddressResolver _resolver;

        public ISet<neo4j.ServerAddress>? Resolve(neo4j.ServerAddress address)
        {
            ISet<bp41.ServerAddress>? result = _resolver.Resolve(new ServerAddress(address));
            if (result == null)
                return null;

            return new HashSet<neo4j.ServerAddress>(result.Select(item => (neo4j.ServerAddress)item._instance));
        }
    }

    public sealed class LoggerProxy : neo4j.ILogger
    {
        private LoggerProxy(bp41.ILogger logger)
        {
            _logger = logger;
        }
        public static LoggerProxy Get(bp41.ILogger logger)
        {
            return new LoggerProxy(logger);
        }
        private bp41.ILogger _logger;


        public void Debug(string message, params object[] args)
        {
            _logger.Debug(message, args);
        }
        public void Error(Exception cause, string message, params object[] args)
        {
            _logger.Error(cause, message, args);
        }
        public void Info(string message, params object[] args)
        {
            _logger.Info(message, args);
        }
        public bool IsDebugEnabled()
        {
            return _logger.IsDebugEnabled();
        }
        public bool IsTraceEnabled()
        {
            return _logger.IsTraceEnabled();
        }
        public void Trace(string message, params object[] args)
        {
            _logger.Trace(message, args);
        }
        public void Warn(Exception cause, string message, params object[] args)
        {
            _logger.Warn(cause, message, args);
        }
    }
}