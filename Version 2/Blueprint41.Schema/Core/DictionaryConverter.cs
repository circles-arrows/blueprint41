using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Blueprint41.Core
{
    public class DictionaryConverter : JsonConverterFactory
    {
        public override bool CanConvert(Type typeToConvert)
        {
            if (!typeToConvert.IsGenericType)
            {
                return false;
            }

            if (typeToConvert.GetGenericTypeDefinition() != typeof(Dictionary<,>))
            {
                return false;
            }

            return true;
        }

        public override JsonConverter CreateConverter(
            Type type,
            JsonSerializerOptions options)
        {
            Type keyType = type.GetGenericArguments()[0];
            Type valueType = type.GetGenericArguments()[1];

            JsonConverter? converter = (JsonConverter?)Activator.CreateInstance(
                typeof(DictionaryConverterInner<,>).MakeGenericType(
                    new Type[] { keyType, valueType }),
                BindingFlags.Instance | BindingFlags.Public,
                binder: null,
                args: new object[] { options },
                culture: null);

            return converter ?? throw new TypeLoadException();
        }

#nullable disable
        private sealed class DictionaryConverterInner<TKey, TValue> : JsonConverter<Dictionary<TKey, TValue>>
        {
            private readonly JsonConverter<TKey> _keyConverter;
            private readonly JsonConverter<TValue> _valueConverter;
            private readonly Type _keyType;
            private readonly Type _valueType;

#pragma warning disable S1144 // Unused private types or members should be removed
            public DictionaryConverterInner(JsonSerializerOptions options)
            {
                // For performance, use the existing converter if available.
                _keyConverter = (JsonConverter<TKey>)options
                    .GetConverter(typeof(TKey));

                // For performance, use the existing converter if available.
                _valueConverter = (JsonConverter<TValue>)options
                    .GetConverter(typeof(TValue));

                // Cache the key and value types.
                _keyType = typeof(TKey);
                _valueType = typeof(TValue);
            }
#pragma warning restore S1144 // Unused private types or members should be removed

            public override Dictionary<TKey, TValue> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {

                if (reader.TokenType != JsonTokenType.StartObject && reader.TokenType != JsonTokenType.StartArray)
                {
                    throw new JsonException();
                }

                bool simpleDictionary = reader.TokenType != JsonTokenType.StartArray;
                var dictionary = new Dictionary<TKey, TValue>();

                while (reader.Read())
                {
                    if (reader.TokenType == JsonTokenType.EndObject || reader.TokenType == JsonTokenType.EndArray)
                    {
                        return dictionary;
                    }

                    TKey key;
                    TValue value;
                    if (simpleDictionary)
                        reader = ParseSimple(reader, options, out key, out value);
                    else
                        reader = ParseComplex(reader, options, out key, out value);

                    if (key is null)
                        throw new JsonException("Dictionary entry should have a key.");
                    else
#pragma warning disable CS8604 // Possible null reference argument.
                        dictionary.Add(key, value ?? default);
#pragma warning restore CS8604 // Possible null reference argument.
                }

                throw new JsonException();
            }

            private Utf8JsonReader ParseSimple(Utf8JsonReader reader, JsonSerializerOptions options, out TKey key, out TValue value)
            {
                // Get the key.
                if (reader.TokenType != JsonTokenType.PropertyName)
                    throw new JsonException();

                key = default;
                value = default;

                if (_keyConverter != null)
                {
                    key = _keyConverter.Read(ref reader, _keyType, options);
                }
                else
                {
                    key = JsonSerializer.Deserialize<TKey>(ref reader, options);
                }
                if (_valueConverter != null)
                {
                    reader.Read();
                    if (reader.TokenType != JsonTokenType.Null)
                        value = _valueConverter.Read(ref reader, _valueType, options);
                }
                else
                {
                    if (reader.TokenType != JsonTokenType.Null)
                        value = JsonSerializer.Deserialize<TValue>(ref reader, options);
                }

                return reader;
            }
            private Utf8JsonReader ParseComplex(Utf8JsonReader reader, JsonSerializerOptions options, out TKey key, out TValue value)
            {
                // Get the key.
                if (reader.TokenType != JsonTokenType.StartObject)
                    throw new JsonException("Expects start of object");

                bool keyIsAssigned = false;
                bool valueIsAssigned = false;
                key = default;
                value = default;
                while (!keyIsAssigned || !valueIsAssigned)
                {
                    reader.Read();

                    // Get the key.
                    if (reader.TokenType != JsonTokenType.PropertyName && (reader.GetString()?.ToLower() != "key" || reader.GetString()?.ToLower() != "value"))
                        throw new JsonException();

                    if (reader.GetString()!.ToLower() == "key")
                    {
                        if (_keyConverter != null)
                        {
                            reader.Read();
                            key = _keyConverter.Read(ref reader, _keyType, options);
                        }
                        else
                        {
                            key = JsonSerializer.Deserialize<TKey>(ref reader, options);
                        }
                        keyIsAssigned = true;
                    }
                    else if (reader.GetString()!.ToLower() == "value")
                    {
                        if (_valueConverter != null)
                        {
                            reader.Read();
                            if (reader.TokenType != JsonTokenType.Null)
                                value = _valueConverter.Read(ref reader, _valueType, options);
                        }
                        else
                        {
                            if (reader.TokenType != JsonTokenType.Null)
                                value = JsonSerializer.Deserialize<TValue>(ref reader, options);
                        }
                        valueIsAssigned = true;
                    }
                    else
                        throw new JsonException();
                }

                if (!keyIsAssigned || !valueIsAssigned)
                    throw new JsonException("Dictionary entry should have a key and a value.");

                reader.Read();
                // Get the key.
                if (reader.TokenType != JsonTokenType.EndObject)
                    throw new JsonException("Expects end of object");

                return reader;
            }

            public override void Write(Utf8JsonWriter writer, Dictionary<TKey, TValue> dictionary, JsonSerializerOptions options)
            {
                writer.WriteStartObject();

                foreach (KeyValuePair<TKey, TValue> kvp in dictionary)
                {
                    string propertyName = kvp.Key!.ToString()!;
                    writer.WritePropertyName(options.PropertyNamingPolicy?.ConvertName(propertyName) ?? propertyName);

                    if (_valueConverter != null)
                    {
                        if (kvp.Value is not null)
                            _valueConverter.Write(writer, kvp.Value, options);
                        else
                            writer.WriteNullValue();
                    }
                    else
                    {
                        if (kvp.Value is not null)
                            JsonSerializer.Serialize(writer, kvp.Value, options);
                        else
                            writer.WriteNullValue();
                    }
                }

                writer.WriteEndObject();
            }
        }
#nullable  enable
    }
}
