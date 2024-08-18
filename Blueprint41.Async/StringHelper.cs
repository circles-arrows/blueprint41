using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

using Newtonsoft.Json;

namespace Blueprint41.Async
{
    internal static class StringHelper
    {
        public static string ToNeo4jParams(this Dictionary<string, object?> self)
        {
            var serializer = new Newtonsoft.Json.JsonSerializer();
            var stringWriter = new StringWriter();
            using (var writer = new JsonTextWriter(stringWriter))
            {
                writer.QuoteName = false;
                writer.Formatting = Newtonsoft.Json.Formatting.None;

                foreach (var item in self)
                {
                    if (item.Value is null)
                        continue;

                    stringWriter.Write($":param {item.Key} => ");
                    serializer.Serialize(writer, item.Value);
                    stringWriter.WriteLine(";");
                }
                return stringWriter.ToString();
            }
        }
    }
}
