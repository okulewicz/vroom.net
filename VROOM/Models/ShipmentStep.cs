using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using VROOM.Converters;

namespace VROOM
{
    public class ShipmentStep
    {
        /// <summary>
        /// The shipment step ID.
        /// </summary>
        [JsonProperty("id")]
        public uint Id { get; set; }
        
        /// <summary>
        /// A description of this shipment step.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }
        
        /// <summary>
        /// The location of this shipment step.
        /// </summary>
        [JsonProperty("location")]
        public Coordinate? Location { get; set; }
        
        /// <summary>
        /// Index of relevant row and column in custom matrices. Only needed if supplying custom matrix.
        /// </summary>
        [JsonProperty("location_index")]
        public MatrixIndex? LocationIndex { get; set; }
        
        /// <summary>
        /// Shipment step service duration.
        /// </summary>
        [JsonProperty("service")]
        [JsonConverter(typeof(NullableTimeSpanSecondsToIntConverter))]
        public TimeSpan? Service { get; set; }
        
        /// <summary>
        /// List of timewindows describing valid slots for job service start.
        /// </summary>
        [JsonProperty("time_windows")]
        public List<TimeWindow> TimeWindows { get; set; }
    }
}