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

        public long Id => properties.ContainsKey("Uid") == false ? -1 : long.TryParse(properties["Uid"].ToString(), out long id) ? id : -1;

        public bool Equals(INode other)
        {
            return Id == other.Id && Id > 0;
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
