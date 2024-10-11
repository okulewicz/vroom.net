using Newtonsoft.Json;
using System;

namespace VROOM.Converters
{
    public class TimeSpanSecondsToIntConverter : JsonConverter<TimeSpan>
    {
        public override TimeSpan ReadJson(JsonReader reader, Type objectType, TimeSpan existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType != JsonToken.Integer)
            {
                throw new JsonException("Expected integer for TimeSpan conversion.");
            }

            int seconds = Convert.ToInt32(reader.Value);
            return new TimeSpan(0, 0, seconds);
        }

        public override void WriteJson(JsonWriter writer, TimeSpan value, JsonSerializer serializer)
        {
            writer.WriteValue((int)Math.Round(value.TotalSeconds));
        }
    }
}