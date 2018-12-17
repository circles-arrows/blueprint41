using Neo4j.Driver.V1;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blueprint41.Response
{
    public class RelationshipWrapper : IRelationship
    {
        Dictionary<string, object> properties;

        public object this[string key] => properties[key];

        public string Type => properties["cypher.element"].As<INode>()["label"].ToString();

        public long Id => properties.ContainsKey("id") == false ? -1 : long.TryParse(properties["id"].ToString(), out long id) ? id : -1;
        public long StartNodeId => properties.ContainsKey("cypher.inv") == false ? -1 : long.TryParse(properties["cypher.inv"]?.ToString(), out long id) ? id : -1;
        public long EndNodeId => properties.ContainsKey("cypher.outv") == false ? -1 : long.TryParse(properties["cypher.outv"]?.ToString(), out long id) ? id : -1;

        public IReadOnlyDictionary<string, object> Properties => properties;


        #region Cosmos DB Id's
        public string GremlinId => properties["id"].ToString();
        public string GremlinStartNodeId => properties["cypher.inv"]?.ToString();
        public string GremlinEndNodeId => properties["cypher.outv"]?.ToString();
        #endregion

        public bool Equals(IRelationship other)
        {
            if (other is RelationshipWrapper otherRel)
            {
                return GremlinId == otherRel.GremlinId && GremlinStartNodeId == otherRel.GremlinStartNodeId &&
                    GremlinEndNodeId == otherRel.GremlinEndNodeId && Type == otherRel.Type;
            }

            return false;
        }

        internal RelationshipWrapper() { }

        internal void AddProperty(string key, object propertyValue)
        {
            if (properties == null)
                properties = new Dictionary<string, object>();

            properties.Add(key, propertyValue);
        }
    }
}
