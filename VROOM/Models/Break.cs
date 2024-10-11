using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using VROOM.Converters;

namespace VROOM
{
    public class Break
    {
        /// <summary>
        /// The break Id.
        /// </summary>
        [JsonProperty("id")]
        public uint Id { get; set; }

        /// <summary>
        /// A description of the break.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// A list of TimeWindows describing valid slots for break start.
        /// </summary>
        [JsonProperty("time_windows")]
        public List<int[]> TimeWindows { get; set; }
        
        /// <summary>
        /// The break duration (in VROOM this is "service").
        /// </summary>
        [JsonProperty("service")]
        public int Duration { get; set; }
    }
}