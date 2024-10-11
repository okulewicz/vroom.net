using Newtonsoft.Json;
using System;

namespace VROOM.Converters
{
    public class TimeWindowConverter : JsonConverter<TimeWindow>
    {
        public override TimeWindow ReadJson(JsonReader reader, Type objectType, TimeWindow existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType != JsonToken.StartArray)
            {
                throw new JsonException("Failed converting TimeWindow.");
            }

            reader.Read();
            long start = Convert.ToInt64(reader.Value);
            reader.Read();
            long end = Convert.ToInt64(reader.Value);

            reader.Read();
            if (reader.TokenType != JsonToken.EndArray)
            {
                throw new JsonException("Failed converting TimeWindow.");
            }

            return new TimeWindow(DateTimeOffset.FromUnixTimeSeconds(start), DateTimeOffset.FromUnixTimeSeconds(end));
        }

        public override void WriteJson(JsonWriter writer, TimeWindow value, JsonSerializer serializer)
        {
            writer.WriteStartArray();
            writer.WriteValue(value.Start.ToUnixTimeSeconds());
            writer.WriteValue(value.End.ToUnixTimeSeconds());
            writer.WriteEndArray();
        }
    }
}