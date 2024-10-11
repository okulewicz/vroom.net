using Newtonsoft.Json;
using System;
using System.Globalization;
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

            double lon = (double)reader.ReadAsDouble();
            double lat = (double)reader.ReadAsDouble();

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
            writer.WriteRawValue(value.Longitude.ToString(CultureInfo.InvariantCulture));
            writer.WriteRawValue(value.Latitude.ToString(CultureInfo.InvariantCulture));
            writer.WriteEndArray();
        }
    }
}