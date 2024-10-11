using Newtonsoft.Json;
using System;

namespace VROOM.Converters
{
    public class PriorityConverter : JsonConverter<Priority>
    {

        public override Priority ReadJson(JsonReader reader, Type objectType, Priority existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            return (Priority)reader.ReadAsInt32();
        }

        public override void WriteJson(JsonWriter writer, Priority value, JsonSerializer serializer)
        {
            writer.WriteValue(value.Value);
        }
    }
}