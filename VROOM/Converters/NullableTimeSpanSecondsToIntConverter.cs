using Newtonsoft.Json;
using System;

namespace VROOM.Converters
{
    public class NullableTimeSpanSecondsToIntConverter : JsonConverter<TimeSpan?>
    {
        public override TimeSpan? ReadJson(JsonReader reader, Type objectType, TimeSpan? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                return null;
            }
            else if (reader.TokenType == JsonToken.Integer)
            {
                int parsed = Convert.ToInt32(reader.Value);
                return new TimeSpan(0, 0, parsed);
            }
            else
            {
                throw new JsonException("Unsupported JSON type.");
            }
        }

        public override void WriteJson(JsonWriter writer, TimeSpan? value, JsonSerializer serializer)
        {
            if (value != null)
            {
                writer.WriteValue((int)Math.Round(value.Value.TotalSeconds));
            }
            else
            {
                writer.WriteNull();
            }
        }
    }
}