using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace VROOM.Models
{
    /// <summary>
    /// Overriding CLI options for VROOM.
    /// </summary>
    public class Options
    {
        [JsonProperty("t")]
        public int Threads { get; set; }
        [JsonProperty("x")]
        public int Explore { get; set; }
        [JsonProperty("c")]
        public bool? PlanMode { get; set; }
    }
}
