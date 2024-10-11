using Newtonsoft.Json;
using System;
using System.Threading;

namespace VROOM.Converters
{
    public class CoordinateConverter : JsonConverter<Coordinate>
    {
        public override Coordinate ReadJson(JsonReader reader, Type objectType, Coordinate existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType != JsonToken.StartArray)
            {
                throw new JsonException("Failed converting coordinate.");
            }

            reader.Read();
            double lon = (double)reader.Value;
            reader.Read();
            double lat = (double)reader.Value;

            reader.Read();
            if (reader.TokenType != JsonToken.EndArray)
            {
                throw new JsonException("Failed converting coordinate.");
            }

            return new Coordinate(lon, lat);
        }

        public override void WriteJson(JsonWriter writer, Coordinate value, JsonSerializer serializer)
        {
            writer.WriteStartArray();
            writer.WriteValue(value.Longitude);
            writer.WriteValue(value.Latitude);
            writer.WriteEndArray();
        }
    }
}