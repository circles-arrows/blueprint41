using Neo4j.Driver.V1;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blueprint41.Response
{
    public class RecordWrapper : IRecord
    {
        Dictionary<string, object> recordDictionary;
        List<object> recordList;

        JToken token;

        public object this[int index]
        {
            get
            {
                SetRecords();
                return recordList[index];
            }
        }

        public object this[string key]
        {
            get
            {
                SetRecords();
                return recordDictionary[key];
            }
        }

        public IReadOnlyDictionary<string, object> Values
        {
            get
            {
                SetRecords();
                return recordDictionary;
            }
        }
        public IReadOnlyList<string> Keys
        {
            get
            {
                SetRecords();
                return recordDictionary.Select(x => x.Key).ToList();
            }
        }

        internal RecordWrapper() { }

        internal RecordWrapper(JToken token)
        {
            this.token = token;
        }

        void SetRecords()
        {
            if (recordDictionary != null && recordList != null)
                return;

            recordDictionary = new Dictionary<string, object>();
            recordList = new List<object>();

            if (token is JObject obj)
            {
                foreach (JProperty prop in obj.Properties())
                {
                    if (recordDictionary.ContainsKey(prop.Name.Trim()))
                        continue;

                    object value = ConvertToType(prop);

                    recordDictionary.Add(prop.Name.Trim(), ConvertToType(prop));
                    recordList.Add(value);
                }
            }
            else if (token is JArray array)
            {
                for (var i = 0; i < array.Count; i++)
                {
                    object value = ConvertToType(array[i]);

                    recordDictionary.Add(i.ToString(), value);
                    recordList.Add(value);
                }
            }
        }

        internal static RecordWrapper CreateFromToken(JToken token)
        {
            return new RecordWrapper(token);
        }

        private static Dictionary<JTokenType, Type> ScalerTypeCache = new Dictionary<JTokenType, Type>()
        {
            { JTokenType.Boolean, typeof(bool) },
            { JTokenType.String, typeof(string) },
            { JTokenType.Date, typeof(DateTime) },
            { JTokenType.Float, typeof(long) },
            { JTokenType.Integer, typeof(long) },
            { JTokenType.TimeSpan, typeof(TimeSpan) },
        };

        private object ConvertToType(JToken token, bool isRelationship = false)
        {
            if (ScalerTypeCache.ContainsKey(token.Type))
                return token.ToObject(ScalerTypeCache[token.Type]);

            switch (token.Type)
            {
                case JTokenType.Property:
                    JProperty prop = (JProperty)token;
                    return ConvertToType(prop.Value, prop.Name.Trim() == "rel");

                case JTokenType.Object:
                    JObject obj = (JObject)token;

                    if (isRelationship)
                    {
                        RelationshipWrapper rel = new RelationshipWrapper();

                        foreach (JProperty property in obj.Properties())
                            rel.AddProperty(property.Name.Trim(), ConvertToType(property));

                        return rel;
                    }
                    else
                    {
                        NodeWrapper node = new NodeWrapper();
                        foreach (JProperty property in obj.Properties())
                            node.AddProperty(property.Name.Trim(), ConvertToType(property));

                        return node;
                    }
                case JTokenType.Array:
                    JArray arr = (JArray)token;

                    //Cosmos db seems to add "id" on the property thus making the result an array of values
                    // So when an array is equal to 1, just return the first value.

                    if (arr.Count == 1)
                        return ConvertToType(arr[0]);

                    List<object> array = new List<object>();
                    for (var i = 0; i < arr.Count; i++)
                        array.Add(ConvertToType(arr[i]));

                    return array.ToArray();
            }
            return null;
        }
    }


}
