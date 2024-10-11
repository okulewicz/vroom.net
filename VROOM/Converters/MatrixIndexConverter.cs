using Newtonsoft.Json;
using System;
using System.Threading;

namespace VROOM.Converters
{
    public class MatrixIndexConverter : JsonConverter<MatrixIndex>
    {
        public override MatrixIndex ReadJson(JsonReader reader, Type objectType, MatrixIndex existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType != JsonToken.StartArray)
            {
                throw new JsonException("Failed converting MatrixIndex.");
            }

            reader.Read();
            int row = (int)(long)reader.Value; // Cast to long first because reader.Value is of type object
            reader.Read();
            int col = (int)(long)reader.Value;

            reader.Read();
            if (reader.TokenType != JsonToken.EndArray)
            {
                throw new JsonException("Failed converting MatrixIndex.");
            }

            return new MatrixIndex(row, col);
        }

        public override void WriteJson(JsonWriter writer, MatrixIndex value, JsonSerializer serializer)
        {
            writer.WriteStartArray();
            writer.WriteValue(value.Row);
            writer.WriteValue(value.Column);
            writer.WriteEndArray();
        }
    }
}