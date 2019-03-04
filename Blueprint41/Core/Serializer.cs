using Blueprint41.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Blueprint41
{
    internal abstract class Serializer
    {
        static internal DataContractJsonSerializerSettings settings = new DataContractJsonSerializerSettings()
        {
            EmitTypeInformation = EmitTypeInformation.AsNeeded,
            UseSimpleDictionaryFormat = true,
            DateTimeFormat = new DateTimeFormat(@"yyyy-MM-dd\THH:mm:ss.fff\Z"),
            KnownTypes = new[]
            {
                typeof(List<object>),
                typeof(Dictionary<object, object>),
            },
        };
            
        protected abstract string SerializeInternal(object value);
        protected abstract object DeserializeInternal(string value);

        public static string Serialize(object value)
        {
            return GetSerializer(value?.GetType()).SerializeInternal(value);
        }
        public static T Deserialize<T>(string value)
        {
            return (T)GetSerializer(typeof(T)).DeserializeInternal(value);
        }

        private static Serializer GetSerializer(Type type)
        {
            if (type == null)
                type = typeof(object);

            return cache.TryGetOrAdd(type, key =>
            {
                Type serializerType = typeof(Serializer<>).MakeGenericType(new[] { type });
                return (Serializer)Activator.CreateInstance(serializerType);
            });
        }
        private static AtomicDictionary<Type, Serializer> cache = new AtomicDictionary<Type, Serializer>();
    }
    internal class Serializer<T> : Serializer
    {
        static DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T), settings);

        protected sealed override string SerializeInternal(object value)
        {
            return this.Serialize((T)value);
        }
        protected sealed override object DeserializeInternal(string value)
        {
            return this.Deserialize(value);
        }

        public string Serialize(T value)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                serializer.WriteObject(ms, value);
                ms.Position = 0;
                using (StreamReader sr = new StreamReader(ms, Encoding.UTF8))
                {
                    return sr.ReadToEnd();
                }
            }
        }
        public T Deserialize(string value)
        {
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(value)))
            {
                return (T)serializer.ReadObject(ms);
            }
        }
    }
}
