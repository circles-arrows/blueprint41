using System;
using System.Collections.Generic;
using System.Text;

using Blueprint41.Log;

namespace Blueprint41.Neo4j.Persistence
{
    public class AdvancedConfig
    {
        public bool SimpleLogging { get; set; } = false;

        /// <summary>
        /// When null %ApplicationFolder%\TransactionLogs will be used as the default.
        /// </summary>
        public string? LogDirectory { get; set; } = null;

        /// <summary>
        /// This method requires that you specify the unit like GB, MB, KB, B. (eg: "2mb")
        /// </summary>
        public string? MaxFileSize { get; set; } = null;

        /// <summary>
        /// Only queries that take longer than the threshold will be logged
        /// </summary>
        public int ThresholdInSeconds { get; set; } = 0;

        /// <summary>
        /// Your custom logging logic
        /// </summary>
        public Action<string>? CustomLogging { get; set; } = null;

        /// <summary>
        /// Your advanced custom logging logic
        /// </summary>
        public CypherLoggingHandler? CustomCypherLogging { get; set; } = null;


        /// <summary>
        /// Neo4j 4.x only: Hook to catch DNS resolver requests. You can use this instead of hard-coding the IP lookup in the hosts file or on the DNS server.
        /// </summary>
        /// <remarks>
        /// Returns a list of physical hosts of the Neo4j cluster that can be used to bootstrap the initial connection.
        /// </remarks>
        public Func<Neo4jHost, Neo4jHost[]>? DNSResolverHook { get; set; } = null;

        internal TransactionLogger? GetLogger()
        {
            if (SimpleLogging || CustomLogging is not null)
                return new TransactionLogger(this);
            
            return null;
        }
    }

    public delegate void CypherLoggingHandler(string cypher, Dictionary<string, object?>? parameters, string? memberName, string? sourceFilePath, int sourceLineNumber);

    public struct Neo4jHost
    {
        public Neo4jHost(string host)
        {
            string[] parts = host.Split(new[] { ':' });

            if (parts.Length != 2)
                throw new ArgumentException("Provide a host in the format 'hostname:port'.", nameof(host));

            if (string.IsNullOrWhiteSpace(parts[0]) || parts[0].Contains(" "))
                throw new ArgumentException("Not a valid hostname, provide in the format 'hostname:port'.", nameof(host));

            int port;
            if (!int.TryParse(parts[1], out port))
                throw new ArgumentException("Not a valid port, provide in the format 'hostname:port'.", nameof(host));

            Host = parts[0];
            Port = port;
        }
        public Neo4jHost(string host, int port)
        {
            Host = host.Trim();
            Port = port;
        }

        /// <summary>
        /// Hostname or IP address
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// Network Port
        /// </summary>
        public int Port { get; set; }
    }
}
