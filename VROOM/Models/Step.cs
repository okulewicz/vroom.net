using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using VROOM.Converters;

namespace VROOM
{
    public class Step
    {
        /// <summary>
        /// The step type.
        /// </summary>
        [JsonProperty("type")]
        public StepType StepType { get; set; }
        
        /// <summary>
        /// Estimated time of arrival at this step.
        /// </summary>
        [JsonProperty("arrival")]
        public int Arrival { get; set; }
        
        /// <summary>
        /// Cumulated travel time upon arrival at this step.
        /// </summary>
        [JsonProperty("duration")]
        public int Duration { get; set; }
        
        /// <summary>
        /// Service time at this step.
        /// </summary>
        [JsonProperty("service")]
        public int Service { get; set; }
        
        /// <summary>
        /// Waiting time upon arrival at this step.
        /// </summary>
        [JsonProperty("waiting_time")]
        public int WaitingTime { get; set; }
        
        /// <summary>
        /// List of violation objects for this step.
        /// </summary>
        [JsonProperty("violations")]
        public List<Violation> Violations { get; set; }
        
        /// <summary>
        /// Step description, if provided in input.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }
        
        /// <summary>
        /// Coordinates for this step, if provided in input.
        /// </summary>
        [JsonProperty("location")]
        public Coordinate? Location { get; set; }
        
        /// <summary>
        /// Id of the task performed at this step.
        /// Only provided if type value is job, pickup, delivery or break.
        /// </summary>
        [JsonProperty("id")]
        public uint? Id { get; set; }
        
        /// <summary>
        /// Vehicle load after step completion (with capacity constraints).
        /// </summary>
        [JsonProperty("load")]
        public List<int> Load { get; set; }
        
        /// <summary>
        /// Traveled distance upon arrival at this step. Provided when using the -g flag.
        /// </summary>
        [JsonProperty("distance")]
        public int? Distance { get; set; }
    }
}