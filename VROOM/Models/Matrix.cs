using Newtonsoft.Json;

namespace VROOM
{
    public class Matrix
    {
        [JsonProperty("durations")]
        public int[][] Durations { get; set; }
        [JsonProperty("distances ")]
        public int[][] Distances { get; set; }
        [JsonProperty("costs ")]
        public int[][] Costs { get; set; }
    }
}