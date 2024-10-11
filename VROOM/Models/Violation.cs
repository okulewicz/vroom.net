using Newtonsoft.Json;
using System;

namespace VROOM
{
    public class Violation
    {
        /// <summary>
        /// The cause of the violation.
        /// </summary>
        [JsonProperty("cause")]
        public ViolationCause Cause { get; set; }
        
        [JsonProperty("duration")]
        public double Duration { get; set; }
    }
}