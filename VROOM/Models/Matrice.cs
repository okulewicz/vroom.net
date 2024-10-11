using Newtonsoft.Json;

namespace VROOM
{
    public class Matrice
    {
        [JsonProperty("durations")]
        public int[][] Durations { get; set; } 
    }
}