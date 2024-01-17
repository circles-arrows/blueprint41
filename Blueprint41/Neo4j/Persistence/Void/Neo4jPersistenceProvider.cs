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
        // Precision (roughly) 10.8
        internal const decimal DECIMAL_FACTOR = 100000000m;

        public TransactionLogger? TransactionLogger { get; private set; }
        public string? Uri { get; private set; }
        public string? Username { get; private set; }
        public string? Password { get; private set; }
        public string? Database { get; private set; }
        public AdvancedConfig? AdvancedConfig { get; private set; }

        private Neo4jPersistenceProvider() : this(null, null, null, null) { }
        public Neo4jPersistenceProvider(string? uri, string? username, string? password, AdvancedConfig? advancedConfig = null) : base()
        {
            Uri = uri;
            Username = username;
            Password = password;
            Database = null;
            AdvancedConfig = advancedConfig;
            TransactionLogger = advancedConfig?.GetLogger();

            _nodePropertyFeatures = new Lazy<FeatureSupport>(() => new FeatureSupport()
            {
                Index = true,
                Exists = IsEnterpriseEdition,
                Unique = true,
                Key = IsEnterpriseEdition && VersionGreaterOrEqual(5, 7),
                Type = IsEnterpriseEdition && VersionGreaterOrEqual(5, 9),
            });
            _relationshipPropertyFeatures = new Lazy<FeatureSupport>(() => new FeatureSupport()
            {
                Index = VersionGreaterOrEqual(4, 3),
                Exists = IsEnterpriseEdition,
                Unique = VersionGreaterOrEqual(5, 7),
                Key = IsEnterpriseEdition && VersionGreaterOrEqual(5, 7),
                Type = IsEnterpriseEdition && VersionGreaterOrEqual(5, 9),
            });
    }
    public Neo4jPersistenceProvider(string? uri, string? username, string? password, string database, AdvancedConfig? advancedConfig = null) : base()
        {
            Uri = uri;
            Username = username;
            Password = password;
            Database = database;
            AdvancedConfig = advancedConfig;
            TransactionLogger = advancedConfig?.GetLogger();

            _nodePropertyFeatures = new Lazy<FeatureSupport>(() => new FeatureSupport()
            {
                Index = true,
                Exists = IsEnterpriseEdition,
                Unique = true,
                Key = IsEnterpriseEdition && VersionGreaterOrEqual(5, 7),
                Type = IsEnterpriseEdition && VersionGreaterOrEqual(5, 9),
            });
            _relationshipPropertyFeatures = new Lazy<FeatureSupport>(() => new FeatureSupport()
            {
                Index = VersionGreaterOrEqual(4, 3),
                Exists = IsEnterpriseEdition,
                Unique = VersionGreaterOrEqual(5, 7),
                Key = IsEnterpriseEdition && VersionGreaterOrEqual(5, 7),
                Type = IsEnterpriseEdition && VersionGreaterOrEqual(5, 9),
            });
        }

        public string Version { get; private set; } = "0.0.0";
        public int Major { get; private set; } = 0;
        public int Minor { get; private set; } = 0;
        public int? Revision { get; private set; } = null;
        public bool IsAura { get; set; } = false;
        public bool IsEnterpriseEdition { get; private set; } = false;

        public FeatureSupport NodePropertyFeatures => _nodePropertyFeatures.Value;
        private readonly Lazy<FeatureSupport> _nodePropertyFeatures;

        public FeatureSupport RelationshipPropertyFeatures => _relationshipPropertyFeatures.Value;
        private readonly Lazy<FeatureSupport> _relationshipPropertyFeatures;

        public bool VersionGreaterOrEqual(int major, int minor = 0, int revision = 0)
        {
            if (Major > major) return true;
            if (Major == major && Minor > minor) return true;
            if (Major == major && Minor == minor && Revision >= revision) return true;

            return false;
        }

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

                                string[] parts = Version.Split(new[] { '.' });
                                Major = int.Parse(parts[0]);

                                if (parts[1].ToLower().Contains("-aura"))
                                {
                                    parts[1] = parts[1].Replace("-aura", "");
                                    IsAura = true;
                                }
                                
                                Minor = int.Parse(parts[1]);                                

                                if (parts.Length > 2)
                                    Revision = int.Parse(parts[2]);

                                functions = new HashSet<string>(Transaction.RunningTransaction.Run(GetFunctions(Major)).Select(item => item.Values["name"].As<string>()));
                                procedures = new HashSet<string>(Transaction.RunningTransaction.Run(GetProcedures(Major)).Select(item => item.Values["name"].As<string>()));
                            }

                            translator = Major switch
                            {
                                3 => new v3.Neo4jQueryTranslator(this),
                                4 => new v4.Neo4jQueryTranslator(this),
                                5 => new v5.Neo4jQueryTranslator(this),
                                _ => throw new NotSupportedException($"Neo4j v{Version} is not supported by this version of Blueprint41, please upgrade to a later version.")
                            };
                        }
                    }
                }
                return translator;

                static string GetFunctions(int version) => version switch
                {
                    < 5 => "call dbms.functions() yield name return name",
                    >= 5 => "show functions yield name return name",
                };
                static string GetProcedures(int version) => version switch
                {
                    < 5 => "call dbms.procedures() yield name as name",
                    >= 5 => "show procedures yield name return name",
                };
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

    public record FeatureSupport
    {
        public bool Index;
        public bool Exists;
        public bool Unique;
        public bool Key;
        public bool Type;
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
