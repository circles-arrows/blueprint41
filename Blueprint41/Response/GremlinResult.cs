using Gremlin.Net.Driver;
using Gremlin.Net.Driver.Exceptions;
using Neo4j.Driver.V1;
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
    public class GremlinResult : IEnumerable<IRecord>, IEnumerable
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

        /// <summary>
        /// The gremlin result
        /// </summary>
        private List<IRecord> result;
        public List<IRecord> Result
        {
            get
            {
                if (result == null)
                {
                    JsonSerializer serializer = JsonSerializer.Create(DefaultSerializerSettings);
                    result = new List<IRecord>();

                    foreach (JToken token in ResultSet)
                        result.Add(token.ToObject<IRecord>(serializer));
                }
                return result;
            }
        }
        public long StatusCode { get; }
        public string ErrorMessage { get; }
        public bool HasError => StatusCode != 200;

        public GremlinResult(ResultSet<JToken> resultSet)
        {
            ResultSet = resultSet ?? throw new System.ArgumentNullException(nameof(resultSet));

            StatusCode = 200;

            // For cosmos db status code
            if (ResultSet.StatusAttributes.ContainsKey("x-ms-status-code"))
                StatusCode = (long)ResultSet.StatusAttributes["x-ms-status-code"];
        }

        public GremlinResult(ResponseException ex)
        {
            ErrorMessage = ex.Message;
            long.TryParse(ex.StatusCode.ToString(), out long statusCode);

            StatusCode = statusCode;

            // For cosmos db status code
            if (ex.StatusAttributes.ContainsKey("x-ms-status-code"))
                StatusCode = (long)ex.StatusAttributes["x-ms-status-code"];
        }

        public IEnumerator<IRecord> GetEnumerator()
        {
            return Result.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }



    public class GremlinConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(IRecord);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.StartArray:
                case JsonToken.StartObject:
                    JToken obj = JToken.ReadFrom(reader);
                    return RecordWrapper.CreateFromToken(obj);
                default:
                    throw new JsonSerializationException("Unexpected reader.TokenType: " + reader.TokenType);
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
