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
    public class TestTimeSpanSecondsToIntConverter
    {
        private static readonly TimeSpan[] TestValues = new[]
        {
            new TimeSpan(10, 10, 10),
            new TimeSpan(0, 0, 10),
            new TimeSpan(100, 100, 100),
        };

        [TestMethod]
        public void CanSerialize()
        {
            TimeSpanSecondsToIntConverter converter = new TimeSpanSecondsToIntConverter();

            foreach (var value in TestValues)
            {
                StringBuilder sb = new StringBuilder();
                using StringWriter sw = new StringWriter(sb);
                using JsonTextWriter writer = new JsonTextWriter(sw);

                converter.WriteJson(writer, value, new JsonSerializer());

                writer.Flush();
                string result = sb.ToString();

                result.Should().Be(((int)Math.Round(value.TotalSeconds)).ToString());
            }
        }

        [TestMethod]
        public void CanDeserialize()
        {
            TimeSpanSecondsToIntConverter converter = new TimeSpanSecondsToIntConverter();
            foreach (var value in TestValues)
            {
                string json = ((int)Math.Round(value.TotalSeconds)).ToString();
                using JsonTextReader reader = new JsonTextReader(new StringReader(json));
                reader.Read();
                var result = converter.ReadJson(reader, typeof(TimeSpan), null, new JsonSerializer());

                result.Should().Be(value);
            }
        }
    }
}
