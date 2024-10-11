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
    public class TestCoordinateConverter
    {
        [TestMethod]
        //[DataRow(-10, 50.5)]
        //[DataRow(100.345, -0.5)]
        [DataRow(-1345.8, 0)]
        //[DataRow(10.456456456678, 0.5)]
        public void CanSerialize(double lon, double lat)
        {
            CoordinateConverter converter = new CoordinateConverter();

            using MemoryStream stream = new MemoryStream();
            using JsonTextWriter writer = new JsonTextWriter(new StreamWriter(stream));

            converter.WriteJson(writer, new Coordinate(lon, lat), new JsonSerializer());


            writer.Flush();
            stream.Position = 0;
            
            using TextReader reader = new StreamReader(stream);
            string result = reader.ReadToEnd();

            string formattedLon = lon.ToString("0.############", CultureInfo.InvariantCulture);
            formattedLon = formattedLon.Contains(".") ? formattedLon.TrimEnd('0').TrimEnd('.') : formattedLon;
            string formattedLat = lat.ToString("0.############", CultureInfo.InvariantCulture);
            formattedLat = formattedLat.Contains(".") ? formattedLat.TrimEnd('0').TrimEnd('.') : formattedLat;


            result.Should().Be($"[{formattedLon},{formattedLat}]");
        }

        [TestMethod]
        [DataRow("[-10,50.5]", -10, 50.5)]
        [DataRow("[100.345,-0.5]", 100.345, -0.5)]
        [DataRow("[-1345.8,0]", -1345.8, 0)]
        [DataRow("[10.456456456678,0.5]", 10.456456456678, 0.5)]
        public void CanDeserialize(string input, double lon, double lat)
        {
            CoordinateConverter converter = new CoordinateConverter();

            using JsonTextReader reader = new JsonTextReader(new StringReader(input));
            reader.Read();
            var result = (Coordinate)converter.ReadJson(reader, typeof(Coordinate), null, new JsonSerializer());

            result.Longitude.Should().Be(lon);
            result.Latitude.Should().Be(lat);
        }
    }
}