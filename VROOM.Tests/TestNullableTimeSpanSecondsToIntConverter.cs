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
    public class TestNullableTimeSpanSecondsToIntConverter
    {
        private static readonly TimeSpan?[] TestValues = new TimeSpan?[]
        {
            new TimeSpan(10, 10, 10),
            new TimeSpan(0, 0, 10),
            new TimeSpan(100, 100, 100),
            null
        };

        [TestMethod]
        public void CanSerialize()
        {
            NullableTimeSpanSecondsToIntConverter converter = new NullableTimeSpanSecondsToIntConverter();

            foreach (var value in TestValues)
            {
                StringBuilder sb = new StringBuilder();
                using StringWriter sw = new StringWriter(sb);
                using JsonTextWriter writer = new JsonTextWriter(sw);

                converter.WriteJson(writer, value, new JsonSerializer());

                writer.Flush();
                string result = sb.ToString();

                result.Should().Be(value == null ? "null" : ((int)Math.Round(value.Value.TotalSeconds)).ToString());
            }
        }

        [TestMethod]
        public void CanDeserialize()
        {
            NullableTimeSpanSecondsToIntConverter converter = new NullableTimeSpanSecondsToIntConverter();
            foreach (var value in TestValues)
            {
                string json = value == null ? "null" : ((int)Math.Round(value.Value.TotalSeconds)).ToString();
                using JsonTextReader reader = new JsonTextReader(new StringReader(json));
                reader.Read();
                var result = converter.ReadJson(reader, typeof(TimeSpan?), null, new JsonSerializer());

                result.Should().Be(value);
            }
        }
    }
}
