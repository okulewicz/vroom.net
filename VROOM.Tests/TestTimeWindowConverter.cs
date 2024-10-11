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
    public class TestTimeWindowConverter
    {
        private static readonly TimeWindow[] TestValues = new[]
        {
            new TimeWindow(DateTimeOffset.UtcNow.ToSecondsAccuracy(), DateTimeOffset.UtcNow.ToSecondsAccuracy() + TimeSpan.FromSeconds(100)),
            new TimeWindow(DateTimeOffset.UtcNow.ToSecondsAccuracy(), DateTimeOffset.UtcNow.ToSecondsAccuracy() + TimeSpan.FromSeconds(1)),
            new TimeWindow(DateTimeOffset.UtcNow.ToSecondsAccuracy(), DateTimeOffset.UtcNow.ToSecondsAccuracy() + TimeSpan.FromDays(100)),
        };

        [TestMethod]
        public void CanSerialize()
        {
            TimeWindowConverter converter = new TimeWindowConverter();

            foreach (var value in TestValues)
            {
                StringBuilder sb = new StringBuilder();
                using StringWriter sw = new StringWriter(sb);
                using JsonTextWriter writer = new JsonTextWriter(sw);

                converter.WriteJson(writer, value, new JsonSerializer());

                writer.Flush();
                string result = sb.ToString();

                result.Should().Be($"[{value.Start.ToUnixTimeSeconds()},{value.End.ToUnixTimeSeconds()}]");
            }
        }

        [TestMethod]
        public void CanDeserialize()
        {
            TimeWindowConverter converter = new TimeWindowConverter();
            foreach (var value in TestValues)
            {
                string json = $"[{value.Start.ToUnixTimeSeconds()},{value.End.ToUnixTimeSeconds()}]";
                using JsonTextReader reader = new JsonTextReader(new StringReader(json));
                reader.Read();
                var result = converter.ReadJson(reader, typeof(TimeWindow), null, new JsonSerializer());

                result.Should().BeEquivalentTo(value);
            }
        }
    }
}
