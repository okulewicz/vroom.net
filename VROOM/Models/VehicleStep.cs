using Newtonsoft.Json;
using System;
using System.ComponentModel;
using VROOM.Converters;

namespace VROOM
{
    public class VehicleStep
    {
        /// <summary>
        /// The vehicle step type.
        /// </summary>
        [JsonProperty("type")]
        public StepType Type { get; set; }
        
        /// <summary>
        /// The id of the task to be performed at this step.
        /// </summary>
        [JsonProperty("id")]
        public uint? TaskId { get; set; }
        
        /// <summary>
        /// Hard constraint on service time.
        /// </summary>
        [JsonProperty("service_at")]
        public int? ServiceAt { get; set; }
        
        /// <summary>
        /// Hard constraint on service time lower bound.
        /// </summary>
        [JsonProperty("service_after")]
        public int? ServiceAfter { get; set; }
        
        /// <summary>
        /// Hard constraint on service time upper bound.
        /// </summary>
        [JsonProperty("service_before")]
        public int? ServiceBefore { get; set; }
    }
}