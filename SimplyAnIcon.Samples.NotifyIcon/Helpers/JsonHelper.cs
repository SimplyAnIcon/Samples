using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SimplyAnIcon.Samples.NotifyIcon.Helpers.Interfaces;

namespace SimplyAnIcon.Samples.NotifyIcon.Helpers
{
    public class JsonHelper : IJsonHelper
    {
        private readonly JsonSerializerSettings _settings = new JsonSerializerSettings()
        {
            Formatting = Formatting.Indented,
            Converters = new List<JsonConverter>()
            {
                new StringEnumConverter()
            }
        };
        public T DeserializeFile<T>(string filepath)
        {
            return Deserialize<T>(File.ReadAllText(filepath));
        }

        public T Deserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json, _settings);
        }

        public void SerializeToFile<T>(T obj, string filepath)
        {
            File.WriteAllText(filepath, Serialize(obj));
        }

        public string Serialize<T>(T obj)
        {
            return JsonConvert.SerializeObject(obj, _settings);
        }
    }
}
