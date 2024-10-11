using Newtonsoft.Json;
using System.Collections.Generic;

namespace VROOM
{
    public class VroomOutput
    {
        [JsonIgnore]
        public bool WasSuccessful => Code == OutputCode.NoError;
        
        /// <summary>
        /// Status code.
        /// </summary>
        [JsonProperty("code")]
        public OutputCode Code { get; set; }
        
        /// <summary>
        /// Error message. Present if code is different from 0.
        /// </summary>
        [JsonProperty("error")]
        public string Error { get; set; }
        
        /// <summary>
        /// Object summarising solution indicators.
        /// </summary>
        [JsonProperty("summary")]
        public Summary Summary { get; set; }
        
        /// <summary>
        /// List of objects describing unassigned tasks with their id, type and location (if provided).
        /// </summary>
        [JsonProperty("unassigned")]
        public List<Unassigned> Unassigned { get; set; }
        
        /// <summary>
        /// List of route objects.
        /// </summary>
        [JsonProperty("routes")]
        public List<Route> Routes { get; set; }
    }
}