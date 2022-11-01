using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.ApocDocumentationParser
{
    public class ApocDataType
    {
        public static ApocDataType Get(string docName)
        {
            InitDataTypes();
            return apocDataTypes![docName];
        }
        private static void InitDataTypes()
        {
            if (apocDataTypes is null)
                return;

            lock (apocDataTypes)
            {
                if (apocDataTypes is null)
                    return;

                apocDataTypes = new Dictionary<string, ApocDataType>()
                {
                    { "ANY?",                      new ApocDataType(Neo4jType.Any,          ListType.None) },
                    { "BOOLEAN?",                  new ApocDataType(Neo4jType.Boolean,      ListType.None) },
                    { "BYTEARRAY?",                new ApocDataType(Neo4jType.ByteArray,    ListType.None) },
                    { "DATETIME?",                 new ApocDataType(Neo4jType.DateTime,     ListType.None) },
                    { "DURATION?",                 new ApocDataType(Neo4jType.Duration,     ListType.None) },
                    { "FLOAT?",                    new ApocDataType(Neo4jType.Float,        ListType.None) },
                    { "INTEGER?",                  new ApocDataType(Neo4jType.Integer,      ListType.None) },
                    { "MAP?",                      new ApocDataType(Neo4jType.Map,          ListType.None) },
                    { "NODE?",                     new ApocDataType(Neo4jType.Node,         ListType.None) },
                    { "NUMBER?",                   new ApocDataType(Neo4jType.Number,       ListType.None) },
                    { "PATH?",                     new ApocDataType(Neo4jType.Path,         ListType.None) },
                    { "RELATIONSHIP?",             new ApocDataType(Neo4jType.Relationship, ListType.None) },
                    { "STRING?",                   new ApocDataType(Neo4jType.String,       ListType.None) },
                    { "LIST? OF ANY?",             new ApocDataType(Neo4jType.Any,          ListType.List) },
                    { "LIST? OF DURATION?",        new ApocDataType(Neo4jType.Duration,     ListType.List) },
                    { "LIST? OF FLOAT?",           new ApocDataType(Neo4jType.Float,        ListType.List) },
                    { "LIST? OF INTEGER?",         new ApocDataType(Neo4jType.Integer,      ListType.List) },
                    { "LIST? OF MAP?",             new ApocDataType(Neo4jType.Map,          ListType.List) },
                    { "LIST? OF NODE?",            new ApocDataType(Neo4jType.Node,         ListType.List) },
                    { "LIST? OF NUMBER?",          new ApocDataType(Neo4jType.Number,       ListType.List) },
                    { "LIST? OF PATH?",            new ApocDataType(Neo4jType.Path,         ListType.List) },
                    { "LIST? OF RELATIONSHIP?",    new ApocDataType(Neo4jType.Relationship, ListType.List) },
                    { "LIST? OF STRING?",          new ApocDataType(Neo4jType.String,       ListType.List) },
                    { "LIST? OF LIST? OF ANY?",    new ApocDataType(Neo4jType.Any,          ListType.JaggedList) },
                    { "LIST? OF LIST? OF STRING?", new ApocDataType(Neo4jType.String,       ListType.JaggedList) },
                };
            }
        }
        private static Dictionary<string, ApocDataType>? apocDataTypes = null;

        private ApocDataType(Neo4jType neo4jType, ListType listType)
        {
            Neo4jType = neo4jType;
            ListType = listType;
        }

        public Neo4jType Neo4jType { get; private set; }
        public ListType ListType { get; private set; }
        public bool IsList => (ListType != ListType.None);
        public bool IsListOfList => (ListType == ListType.JaggedList);
    }
    public enum Neo4jType
    {
        Any,
        Boolean,
        ByteArray,
        DateTime,
        Duration,
        Float,
        Integer,
        Number,
        String,
        Map,
        Node,
        Path,
        Relationship,
    }
    public enum ListType
    {
        None,
        List,
        JaggedList,
    }
}
