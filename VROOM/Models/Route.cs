using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using VROOM.Converters;

namespace VROOM
{
    public class Route
    {
        /// <summary>
        /// Id of the vehicle assigned to this route.
        /// </summary>
        [JsonProperty("vehicle")]
        public uint VehicleId { get; set; }

        /// <summary>
        /// List of step objects.
        /// </summary>
        [JsonProperty("steps")]
        public List<Step> Steps { get; set; }

        /// <summary>
        /// Cost for this route.
        /// </summary>
        [JsonProperty("cost")]
        public int Cost { get; set; }

        /// <summary>
        /// Total service time for this route.
        /// </summary>
        [JsonProperty("service")]
        [JsonConverter(typeof(TimeSpanSecondsToIntConverter))]
        public TimeSpan Service { get; set; }

        /// <summary>
        /// Total travel time for this route.
        /// </summary>
        [JsonProperty("duration")]
        [JsonConverter(typeof(TimeSpanSecondsToIntConverter))]
        public TimeSpan Duration { get; set; }

        /// <summary>
        /// Total waiting time for this route.
        /// </summary>
        [JsonProperty("waiting_time")]
        [JsonConverter(typeof(TimeSpanSecondsToIntConverter))]
        public TimeSpan WaitingTime { get; set; }

        /// <summary>
        /// Total priority sum for tasks in this route.
        /// </summary>
        [JsonProperty("priority")]
        public int Priority { get; set; }

        /// <summary>
        /// List of violation objects for this route.
        /// </summary>
        [JsonProperty("violations")]
        public List<Violation> Violations { get; set; }

        /// <summary>
        /// Total delivery for tasks in this route.
        /// </summary>
        [JsonProperty("delivery")]
        public List<int> Delivery { get; set; }

        /// <summary>
        /// Total pickup for tasks in this route.
        /// </summary>
        [JsonProperty("pickup")]
        public List<int> Pickup { get; set; }

        /// <summary>
        /// Vehicle description, if provided in input.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Polyline encoded route geometry. Provided when using the -g flag.
        /// </summary>
        [JsonProperty("geometry")]
        public string Geometry { get; set; }

        /// <summary>
        /// Total route distance. Provided when using the -g flag.
        /// </summary>
        [JsonProperty("distance")]
        public int? Distance { get; set; }
    }
}