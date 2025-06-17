using Newtonsoft.Json;
using VROOM.Converters;

namespace VROOM
{
    public class Unassigned
    {
        [JsonProperty("id")]
        public uint Id { get; set; }
        
        [JsonProperty("type")]
        public StepType Type { get; set; }
        
        [JsonProperty("location")]
        public Coordinate? Location { get; set; }
    }
}