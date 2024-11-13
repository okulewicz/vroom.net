using Newtonsoft.Json;
using System.Collections.Generic;

namespace VROOM
{
    public class VroomInput
    {
        /// <summary>
        /// List of job objects describing the places to visit.
        /// </summary>
        [JsonProperty("jobs")]
        public List<Job> Jobs { get; set; }
        
        /// <summary>
        /// List of shipment objects describing pickup and delivery tasks.
        /// </summary>
        [JsonProperty("shipments")]
        public List<Shipment> Shipments { get; set; }
        
        /// <summary>
        /// List of vehicle objects describing the available vehicles.
        /// </summary>
        [JsonProperty("vehicles")]
        public List<Vehicle> Vehicles { get; set; }
        
        /// <summary>
        /// Optional description of per-profile custom matrices. Key is vehicle profile type.
        /// </summary>
        [JsonProperty("matrices")]
        public Dictionary<string, Matrix> Matrices { get; set; }
        /// <summary>
        /// Parameters for controlling the vroom
        /// </summary>
        [JsonProperty("options")]
        public VROOM.Models.Options Options { get; set; }
    }
}