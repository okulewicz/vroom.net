using System;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using VROOM.Converters;

namespace VROOM.Tests
{
    [TestClass]
    public class TestStringEnumConverter
    {
        private static readonly Enum[] TestValues = {
            ViolationCause.Load,
            ViolationCause.LeadTime,
            ViolationCause.MissingBreak,
        };

        [TestMethod]
        public void CanSerialize()
        {
            StringEnumConverter<ViolationCause> converter = new StringEnumConverter<ViolationCause>();

            foreach (var value in TestValues)
            {
                using MemoryStream stream = new MemoryStream();
                //initialize json writer with stream

                using JsonWriter writer = new JsonTextWriter(new StreamWriter(stream));

                converter.WriteJson(writer, (ViolationCause) value, new JsonSerializer());

                writer.Flush();
                stream.Position = 0;

                using TextReader reader = new StreamReader(stream);
                string result = reader.ReadToEnd();

                result.Should().Be('"' + value.GetAttributeFromEnumValue<EnumMemberAttribute>()?.Value + '"');
            }
        }

        [TestMethod]
        public void CanDeserialize()
        {
            StringEnumConverter<ViolationCause> converter = new StringEnumConverter<ViolationCause>();
            foreach (var value in TestValues)
            {
                JsonTextReader reader =
                    new JsonTextReader(new StringReader(
                            '"' + value.GetAttributeFromEnumValue<EnumMemberAttribute>()?.Value + '"'));
                reader.Read();
                var result = converter.ReadJson( reader, typeof(ViolationCause), null, new JsonSerializer());

                result.Should().BeEquivalentTo(value);
            }
        }
    }
}