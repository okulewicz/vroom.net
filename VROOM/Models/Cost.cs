using Newtonsoft.Json;

namespace VROOM
{
    public class Cost
    {
        [JsonProperty("fixed")]
        /// <summary>
        /// Integer defining the cost of using this vehicle in the solution (defaults to 0).
        /// </summary>
        public int? Fixed { get; set; }

        [JsonProperty("per_hour")]
        /// <summary>
        /// Integer defining the cost for one hour of travel time with this vehicle (defaults to 3600).
        /// </summary>
        public int? PerHour { get; set; }

        [JsonProperty("per_km")]
        /// <summary>
        /// Integer defining the cost for one km of travel time with this vehicle (defaults to 0).
        /// </summary>
        public int? PerKm { get; set; }
    }
}