using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace VROOM.Converters
{
    public class StringEnumConverter<T> : JsonConverter<T> where T : struct, Enum
    {
        public override T ReadJson(JsonReader reader, Type objectType, T existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            string s = reader.ReadAsString();
            if (Enum.TryParse(s, out T parsedVal))
            {
                return parsedVal;
            }

            var values = Enum.GetValues(typeof(T));
            foreach (var value in values)
            {
                var enumMemberAttribute = ((T)value).GetAttributeFromEnumValue<EnumMemberAttribute>();
                if (enumMemberAttribute != null)
                {
                    if (s == enumMemberAttribute.Value)
                    {
                        return (T)value;
                    }
                }
            }

            throw new JsonException($"Could not find enum value {s} in {typeof(T)}");
        }

        public override void WriteJson(JsonWriter writer, T value, JsonSerializer serializer)
        {
            var enumMemberAttribute = value.GetAttributeFromEnumValue<EnumMemberAttribute>();
            writer.WriteValue(enumMemberAttribute != null ? enumMemberAttribute.Value : value.ToString());
        }
    }
}