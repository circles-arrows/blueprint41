using Neo4j.Driver.V1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blueprint41.Response
{
    public class NodeWrapper : INode
    {
        Dictionary<string, object> properties;

        public object this[string key] => properties[key];

        public IReadOnlyList<string> Labels => properties.Select(x => x.Key).ToList();

        public IReadOnlyDictionary<string, object> Properties => properties;

        /// <summary>
        /// Don't confuse Id to the Key of an entity, e.g Uid
        /// </summary>
        public long Id => properties.ContainsKey("id") == false ? -1 : long.TryParse(properties["id"].ToString(), out long id) ? id : -1;

        public string GremlinId => properties["id"].ToString();

        public bool Equals(INode other)
        {
            if (other is NodeWrapper otherNode)
                return GremlinId == otherNode.GremlinId;

            return false;
        }

        internal NodeWrapper() { }

        internal void AddProperty(string key, object propertyValue)
        {
            if (properties == null)
                properties = new Dictionary<string, object>();

            properties.Add(key, propertyValue);
        }
    }
}
