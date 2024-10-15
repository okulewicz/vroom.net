using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using VROOM.Converters;

namespace VROOM
{
    public class Job
    {
        /// <summary>
        /// The job ID.
        /// </summary>
        [JsonProperty("id")]
        public uint Id { get; set; }
        
        /// <summary>
        /// A description of this job.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }
        
        /// <summary>
        /// The location of this job.
        /// </summary>
        [JsonProperty("location")]
        public Coordinate? Location { get; set; }
        
        /// <summary>
        /// Index of relevant row and column in custom matrices. Only needed if supplying custom matrix.
        /// </summary>
        [JsonProperty("location_index")]
        public int? LocationIndex { get; set; }
        
        /// <summary>
        /// Job service duration. In VROOM this is the "service" property.
        /// </summary>
        [JsonProperty("service")]
        public int Service { get; set; }
        /// <summary>
        /// Job setup duration
        /// </summary>
        [JsonProperty("setup")]
        public int Setup { get; set; }

        /// <summary>
        /// List of ints describing multidimensional quantities for delivery.
        /// </summary>
        [JsonProperty("delivery")]
        public List<int> Delivery { get; set; }
        
        /// <summary>
        /// List of ints describing multidimensional quantities for pickup.
        /// </summary>
        [JsonProperty("pickup")]
        public List<int> Pickup { get; set; }
        
        /// <summary>
        /// List of ints defining mandatory skills.
        /// </summary>
        [JsonProperty("skills")]
        public List<int> Skills { get; set; }
        
        /// <summary>
        /// An integer in the [0, 100] range describing priority level.
        /// </summary>
        [JsonProperty("priority")]
        public Priority? Priority { get; set; }
        
        /// <summary>
        /// List of timewindows describing valid slots for job service start.
        /// </summary>
        [JsonProperty("time_windows")]
        public List<int[]> TimeWindows { get; set; }
    }
}