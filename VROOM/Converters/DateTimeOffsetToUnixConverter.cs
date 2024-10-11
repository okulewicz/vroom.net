using Newtonsoft.Json;
using System;

namespace VROOM.Converters
{
    public class DateTimeOffsetToUnixConverter : JsonConverter<DateTimeOffset>
    {
        public override DateTimeOffset ReadJson(JsonReader reader, Type objectType, DateTimeOffset existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType != JsonToken.Integer)
            {
                throw new JsonException("Expected integer for DateTimeOffset conversion.");
            }

            long unixTime = Convert.ToInt64(reader.Value);
            return DateTimeOffset.FromUnixTimeSeconds(unixTime);
        }

        public override void WriteJson(JsonWriter writer, DateTimeOffset value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToUnixTimeSeconds());
        }
    }
}