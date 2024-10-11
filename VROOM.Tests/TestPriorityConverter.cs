using System.Globalization;
using System.IO;
using System.Text;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using VROOM.Converters;

namespace VROOM.Tests
{
    [TestClass]
    public class TestPriorityConverter
    {

        [TestMethod]
        [DataRow(10)]
        [DataRow(100)]
        [DataRow(0)]
        public void CanSerialize(int val)
        {
            PriorityConverter converter = new PriorityConverter();

            using MemoryStream stream = new MemoryStream();
            using var writer = new JsonTextWriter(new StreamWriter(stream));
            
            converter.WriteJson(writer, new Priority(val), new JsonSerializer());
            
            writer.Flush();
            stream.Position = 0;
            
            using TextReader reader = new StreamReader(stream);
            string result = reader.ReadToEnd();
            
            result.Should().Be(val.ToString(CultureInfo.InvariantCulture));
        }
        
        [TestMethod]
        [DataRow(10)]
        [DataRow(100)]
        [DataRow(0)]
        public void CanDeserialize(int val)
        {
            PriorityConverter converter = new PriorityConverter();

            var reader = new JsonTextReader(new StringReader(val.ToString()));
            reader.Read();
            var result = converter.ReadJson(reader, typeof(Priority), null, new JsonSerializer());

            result.Should().Be(val);
        }
    }
}