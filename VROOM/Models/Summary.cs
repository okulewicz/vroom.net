using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using VROOM.Converters;

namespace VROOM
{
    public class Summary
    {
        /// <summary>
        /// Total cost for all routes.
        /// </summary>
        [JsonProperty("cost")]
        public int Cost { get; set; }
        
        /// <summary>
        /// Number of tasks that could not be served.
        /// </summary>
        [JsonProperty("unassigned")]
        public int Unassigned { get; set; }
        
        /// <summary>
        /// Total service time for all routes.
        /// </summary>
        [JsonProperty("service")]
        public int Service { get; set; }
        
        /// <summary>
        /// Total travel time for all routes.
        /// </summary>
        [JsonProperty("duration")]
        public int Duration { get; set; }
        
        /// <summary>
        /// Total waiting time for all routes.
        /// </summary>
        [JsonProperty("waiting_time")]
        public int WaitingTime { get; set; }
        
        /// <summary>
        /// Total priority sum for all assigned tasks.
        /// </summary>
        [JsonProperty("priority")]
        public int Priority { get; set; }
        
        /// <summary>
        /// List of violation objects for all routes.
        /// </summary>
        [JsonProperty("violations")]
        public List<Violation> Violations { get; set; } = null;
        
        /// <summary>
        /// Total delivery for all routes.
        /// </summary>
        [JsonProperty("delivery")]
        public List<int> Delivery { get; set; }
        
        /// <summary>
        /// Total pickup for all routes.
        /// </summary>
        [JsonProperty("pickup")]
        public int[] Pickup { get; set; }
        
        /// <summary>
        /// Total distance for all routes.
        /// </summary>
        [JsonProperty("distance")]
        public int? Distance { get; set; }
    }
}