using Newtonsoft.Json;
using System.Collections.Generic;

namespace VROOM
{
    public class Shipment
    {
        /// <summary>
        /// A ShipmentStep object describing pickup.
        /// </summary>
        [JsonProperty("pickup")]
        public ShipmentStep Pickup { get; set; }
        
        /// <summary>
        /// A ShipmentStep object describing delivery.
        /// </summary>
        [JsonProperty("delivery")]
        public ShipmentStep Delivery { get; set; }
        
        /// <summary>
        /// List of ints describing multidimensional quantities for delivery.
        /// </summary>
        [JsonProperty("amount")]
        public List<int> Amount { get; set; }
        
        /// <summary>
        /// A List of ints defining mandatory skills.
        /// </summary>
        [JsonProperty("skills")]
        public List<int> Skills { get; set; }
        
        /// <summary>
        /// An integer in the [0, 100] range describing priority level.
        /// </summary>
        [JsonProperty("priority")]
        public Priority? Priority { get; set; }
    }
}