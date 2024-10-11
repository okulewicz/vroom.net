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
    public class TestMatrixIndexConverter
    {
        private static readonly MatrixIndex[] TestValues = new[]
        {
            new MatrixIndex(0, 0),
            new MatrixIndex(1, 100),
            new MatrixIndex(1000, 1000000),
        };

        [TestMethod]
        public void CanSerialize()
        {
            MatrixIndexConverter converter = new MatrixIndexConverter();

            foreach (var value in TestValues)
            {
                StringBuilder sb = new StringBuilder();
                using StringWriter sw = new StringWriter(sb);
                using JsonTextWriter writer = new JsonTextWriter(sw);

                converter.WriteJson(writer, value, new JsonSerializer());

                writer.Flush();
                string result = sb.ToString();

                result.Should().Be($"[{value.Row},{value.Column}]");
            }
        }

        [TestMethod]
        public void CanDeserialize()
        {
            MatrixIndexConverter converter = new MatrixIndexConverter();
            foreach (var value in TestValues)
            {
                string json = $"[{value.Row},{value.Column}]";
                using JsonTextReader reader = new JsonTextReader(new StringReader(json));
                reader.Read();
                var result = converter.ReadJson(reader, typeof(MatrixIndex), null, new JsonSerializer());

                result.Should().BeEquivalentTo(value);
            }
        }
    }
}
