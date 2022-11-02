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
            if (apocDataTypes is not null)
                return;

            lock (typeof(ApocDataType))
            {
                if (apocDataTypes is not null)
                    return;

                apocDataTypes = new List<ApocDataType>()
                {
                    new ApocDataType("ANY?",                      Neo4jType.Any,          ListType.None),
                    new ApocDataType("BOOLEAN?",                  Neo4jType.Boolean,      ListType.None),
                    new ApocDataType("BYTEARRAY?",                Neo4jType.ByteArray,    ListType.None),
                    new ApocDataType("DATETIME?",                 Neo4jType.DateTime,     ListType.None),
                    new ApocDataType("DURATION?",                 Neo4jType.Duration,     ListType.None),
                    new ApocDataType("FLOAT?",                    Neo4jType.Float,        ListType.None),
                    new ApocDataType("INTEGER?",                  Neo4jType.Integer,      ListType.None),
                    new ApocDataType("MAP?",                      Neo4jType.Map,          ListType.None),
                    new ApocDataType("NODE?",                     Neo4jType.Node,         ListType.None),
                    new ApocDataType("NUMBER?",                   Neo4jType.Number,       ListType.None),
                    new ApocDataType("PATH?",                     Neo4jType.Path,         ListType.None),
                    new ApocDataType("RELATIONSHIP?",             Neo4jType.Relationship, ListType.None),
                    new ApocDataType("STRING?",                   Neo4jType.String,       ListType.None),
                    new ApocDataType("LIST? OF ANY?",             Neo4jType.Any,          ListType.List),
                    new ApocDataType("LIST? OF DURATION?",        Neo4jType.Duration,     ListType.List),
                    new ApocDataType("LIST? OF FLOAT?",           Neo4jType.Float,        ListType.List),
                    new ApocDataType("LIST? OF INTEGER?",         Neo4jType.Integer,      ListType.List),
                    new ApocDataType("LIST? OF MAP?",             Neo4jType.Map,          ListType.List),
                    new ApocDataType("LIST? OF NODE?",            Neo4jType.Node,         ListType.List),
                    new ApocDataType("LIST? OF NUMBER?",          Neo4jType.Number,       ListType.List),
                    new ApocDataType("LIST? OF PATH?",            Neo4jType.Path,         ListType.List),
                    new ApocDataType("LIST? OF RELATIONSHIP?",    Neo4jType.Relationship, ListType.List),
                    new ApocDataType("LIST? OF STRING?",          Neo4jType.String,       ListType.List),
                    new ApocDataType("LIST? OF LIST? OF ANY?",    Neo4jType.Any,          ListType.JaggedList),
                    new ApocDataType("LIST? OF LIST? OF STRING?", Neo4jType.String,       ListType.JaggedList),
                }.ToDictionary(item => item.Name, item => item);
            }
        }
        private static Dictionary<string, ApocDataType>? apocDataTypes = null;

        private ApocDataType(string name, Neo4jType neo4jType, ListType listType)
        {
            Name = name;
            Neo4jType = neo4jType;
            ListType = listType;
        }

        public string Name { get; private set; }
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
