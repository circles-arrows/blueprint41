using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

using Blueprint41.Core;
using Blueprint41.Log;
using Blueprint41.Neo4j.Model;

namespace Blueprint41.Neo4j.Persistence.Void
{
    public partial class Neo4jPersistenceProvider : PersistenceProvider
    {
        internal const decimal DECIMAL_FACTOR = 100000000m;

        public TransactionLogger? TransactionLogger { get; private set; }
        public string? Uri { get; private set; }
        public string? Username { get; private set; }
        public string? Password { get; private set; }
        public string? Database { get; private set; }

        private Neo4jPersistenceProvider() : this(null, null, null, false) { }
        public Neo4jPersistenceProvider(string? uri, string? username, string? password, bool withLogging = false) : this(uri, username, password, null, withLogging) { }
        public Neo4jPersistenceProvider(string? uri, string? username, string? password, string? database, bool withLogging = false) : base()
        {
            Uri = uri;
            Username = username;
            Password = password;
            Database = database;
            TransactionLogger = (withLogging) ? new TransactionLogger() : null;
        }
        public Neo4jPersistenceProvider(string? uri, string? username, string? password, Action<string> logger) : this(uri, username, password, null, logger) { }
        public Neo4jPersistenceProvider(string? uri, string? username, string? password, string? database, Action<string> logger) : base()
        {
            Uri = uri;
            Username = username;
            Password = password;
            Database = database;
            TransactionLogger = (logger is not null) ? new TransactionLogger(logger) : null;
        }

        public string Version { get; private set; } = "0.0.0";
        public int Major { get; private set; } = 0;
        public int Minor { get; private set; } = 0;
        public int Revision { get; private set; } = 0;
        public bool IsEnterpriseEdition { get; private set; } = false;

        public bool HasFunction(string function)
        {
            return functions.Contains(function);
        }
        private HashSet<string> functions = new HashSet<string>();

        public bool HasProcedure(string procedure)
        {
            return procedures.Contains(procedure);
        }
        private HashSet<string> procedures = new HashSet<string>();

        internal override QueryTranslator Translator
        {
            get
            {
                if (translator is null)
                {
                    lock (this)
                    {
                        if (translator is null)
                        {
                            if (this.GetType() == typeof(Void.Neo4jPersistenceProvider))
                                return GetVoidTranslator();

                            if (Uri is null && Username is null && Password is null)
                                return GetVoidTranslator();

                            using (Transaction.Begin())
                            {
                                RawResult? components = Transaction.RunningTransaction.Run("call dbms.components() yield name, versions, edition unwind versions as version return name, version, edition");
                                var record = components.First();

                                Version = record["version"].As<string>();
                                IsEnterpriseEdition = (record["edition"].As<string>().ToLowerInvariant() == "enterprise");

                                string[] parts = Version.Split('.');
                                Major = int.Parse(parts[0]);
                                Minor = int.Parse(parts[1]);
                                Revision = int.Parse(parts[2]);

                                functions = new HashSet<string>(Transaction.RunningTransaction.Run("call dbms.functions() yield name return name").Select(item => item.Values["name"].As<string>()));
                                procedures = new HashSet<string>(Transaction.RunningTransaction.Run("call dbms.procedures() yield name as name").Select(item => item.Values["name"].As<string>()));
                            }

                            if (Major == 3)
                            {
                                translator = new v3.Neo4jQueryTranslator(this);
                            }
                            else if (Major == 4)
                            {
                                translator = new v4.Neo4jQueryTranslator(this);
                            }
                            else
                            {
                                throw new NotSupportedException($"Neo4j v{Version} is not supported by this version of Blueprint41, please upgrade to a later version.");
                            }
                        }
                    }
                }
                return translator;
            }
        }
        private QueryTranslator? translator = null;
        private QueryTranslator GetVoidTranslator()
        {
            if (voidTranslator is null)
                lock (typeof(PersistenceProvider))
                    voidTranslator ??= new Void.Neo4jQueryTranslator(this);

            return voidTranslator;
        }
        private static QueryTranslator? voidTranslator = null;

        public override Transaction NewTransaction(bool readWriteMode)
        {
            return new Neo4jTransaction(readWriteMode, TransactionLogger);
        }

        public override List<TypeMapping> SupportedTypeMappings
        {
            get
            {
                return supportedTypeMappings;
            }
        }
    }
}
/*

Neo4j type  Dotnet type Description                                                 Value range
----------- ----------- ----------------------------------------------------------- ------------------------------------------------------
boolean     bool        bit                                                         true/false
byte        sbyte       8-bit integer                                               -128 to 127, inclusive
short       short       16-bit integer                                              -32768 to 32767, inclusive
int         int         32-bit integer                                              -2147483648 to 2147483647, inclusive
long        long        64-bit integer                                              -9223372036854775808 to 9223372036854775807, inclusive
float       float       32-bit IEEE 754 floating-point number
double      double      64-bit IEEE 754 floating-point number
char        char        16-bit unsigned integers representing Unicode characters    u0000 to uffff(0 to 65535)
String      string      sequence of Unicode characters

*/
