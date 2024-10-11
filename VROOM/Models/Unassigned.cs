using Newtonsoft.Json;

namespace VROOM
{
    public class Unassigned
    {
        [JsonProperty("id")]
        public uint Id { get; set; }
        
        [JsonProperty("type")]
        public string Type { get; set; }
        
        [JsonProperty("location")]
        public Coordinate? Location { get; set; }
    }
}