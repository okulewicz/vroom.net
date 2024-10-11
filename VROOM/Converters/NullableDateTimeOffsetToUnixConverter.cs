using Newtonsoft.Json;
using System;
using System.Threading;

namespace VROOM.Converters
{
    public class NullableDateTimeOffsetToUnixConverter : JsonConverter<DateTimeOffset?>
    {
        public override DateTimeOffset? ReadJson(JsonReader reader, Type objectType, DateTimeOffset? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                return null;
            }
            else if (reader.TokenType == JsonToken.Integer)
            {
                long parsed = Convert.ToInt64(reader.Value);
                return DateTimeOffset.FromUnixTimeSeconds(parsed);
            }
            else
            {
                throw new JsonException("Unsupported JSON type.");
            }
        }

        public override void WriteJson(JsonWriter writer, DateTimeOffset? value, JsonSerializer serializer)
        {
            if (value != null)
            {
                writer.WriteValue(value.Value.ToUnixTimeSeconds());
            }
            else
            {
                writer.WriteNull();
            }
        }
    }
}