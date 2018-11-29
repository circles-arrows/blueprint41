using Gremlin.Net.Driver;
using Gremlin.Net.Driver.Exceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Blueprint41.Response
{
    public class ResponseResult
    {
        public static JsonSerializerSettings DefaultSerializerSettings = new JsonSerializerSettings()
        {
            Converters = new JsonConverter[]
            {
                new GremlinConverter(),
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AdjustToUniversal }
            },
        };

        /// <summary>
        /// The original gremlin result.
        /// </summary>
        public ResultSet<JToken> ResultSet { get; }

        private List<object> data;
        public List<object> Data
        {
            get
            {
                if (data == null)
                {
                    JsonSerializer serializer = JsonSerializer.Create(DefaultSerializerSettings);
                    data = new List<object>();

                    foreach (JToken token in ResultSet)
                        data.Add(token.ToObject<object>(serializer));
                }
                return data;
            }
        }
        public long StatusCode { get; }
        public string ErrorMessage { get; }

        public ResponseResult(ResultSet<JToken> resultSet)
        {
            ResultSet = resultSet ?? throw new System.ArgumentNullException(nameof(resultSet));
            StatusCode = (long)ResultSet.StatusAttributes["x-ms-status-code"];
        }

        public ResponseResult(ResponseException ex)
        {
            ErrorMessage = ex.Message;
            long.TryParse(ex.StatusCode.ToString(), out long statusCode);
            StatusCode = statusCode;
        }
    }

    public class GremlinConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(object);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.Null:
                    return null;
                case JsonToken.StartObject:
                    JToken o = JToken.ReadFrom(reader);
                    return ConvertToType(o);
                default:
                    throw new JsonSerializationException("Unexpected reader.TokenType: " + reader.TokenType);
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        private static Dictionary<JTokenType, Type> ScalerTypeCache = new Dictionary<JTokenType, Type>()
        {
            { JTokenType.Boolean, typeof(bool) },
            { JTokenType.String, typeof(string) },
            { JTokenType.Date, typeof(DateTime) },
            { JTokenType.Float, typeof(float) },
            { JTokenType.Integer, typeof(int) },
            { JTokenType.TimeSpan, typeof(TimeSpan) },
        };

        private object ConvertToType(JToken token)
        {
            if (ScalerTypeCache.ContainsKey(token.Type))
                return token.ToObject(ScalerTypeCache[token.Type]);

            switch (token.Type)
            {
                case JTokenType.Property:
                    JProperty prop = (JProperty)token;
                    return ConvertToType(prop.Value);

                case JTokenType.Object:
                    Dictionary<string, object> values = new Dictionary<string, object>();
                    JObject obj = (JObject)token;
                    foreach (JProperty property in obj.Properties())
                        values.Add(property.Name, ConvertToType(property));

                    return values;
                case JTokenType.Array:
                    JArray arr = (JArray)token;

                    //Gremlin seems to add "id" on the property thus making the result an array of values
                    // So when an array is equal to 1, just return the first value and not making it an array.

                    if (arr.Count == 1)
                        return ConvertToType(arr[0]);

                    List<object> array = new List<object>();
                    for (var i = 0; i < arr.Count; i++)
                    {
                        array.Add(ConvertToType(arr[i]));
                    }

                    return array.ToArray();
            }
            return null;
        }
    }
}
