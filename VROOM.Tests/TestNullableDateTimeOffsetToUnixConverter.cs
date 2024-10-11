using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VROOM.Converters;

namespace VROOM.Tests
{
    [TestClass]
    public class TestNullableDateTimeOffsetToUnixConverter
    {
        private static readonly DateTimeOffset?[] TestValues = new DateTimeOffset?[]
        {
            new DateTimeOffset(2020, 9, 11, 10, 1, 45, 0, new TimeSpan()),
            new DateTimeOffset(2020, 1, 1, 0, 0, 0, 0, new TimeSpan()),
            new DateTimeOffset(1980, 1, 11, 20, 59, 0, 0, new TimeSpan()),
            null
        };

        [TestMethod]
        public void CanSerialize()
        {
            NullableDateTimeOffsetToUnixConverter converter = new NullableDateTimeOffsetToUnixConverter();

            foreach (var value in TestValues)
            {
                StringBuilder sb = new StringBuilder();
                using StringWriter sw = new StringWriter(sb);
                using JsonTextWriter writer = new JsonTextWriter(sw);

                converter.WriteJson(writer, value, new JsonSerializer());

                writer.Flush();
                string result = sb.ToString();

                result.Should().Be(value?.ToUnixTimeSeconds().ToString() ?? "null");
            }
        }

        [TestMethod]
        public void CanDeserialize()
        {
            NullableDateTimeOffsetToUnixConverter converter = new NullableDateTimeOffsetToUnixConverter();
            foreach (var value in TestValues)
            {
                string json = value?.ToUnixTimeSeconds().ToString() ?? "null";
                using JsonTextReader reader = new JsonTextReader(new StringReader(json));
                reader.Read();
                var result = converter.ReadJson(reader, typeof(DateTimeOffset?), null, new JsonSerializer());

                result.Should().Be(value);
            }
        }
    }
}
