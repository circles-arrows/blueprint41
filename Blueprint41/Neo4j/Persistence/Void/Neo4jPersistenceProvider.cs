using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

using Blueprint41.Core;
using Blueprint41.Log;

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

        private protected override NodePersistenceProvider GetNodePersistenceProvider()
        {
            return new Neo4jNodePersistenceProvider(this);
        }

        private protected override RelationshipPersistenceProvider GetRelationshipPersistenceProvider()
        {
            return new Neo4jRelationshipPersistenceProvider(this);
        }

        public override Transaction NewTransaction(bool withTransaction)
        {
            return new Neo4jTransaction(withTransaction, TransactionLogger);
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
