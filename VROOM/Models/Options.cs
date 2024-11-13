using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace VROOM.Models
{
    public class Options
    {
        [JsonProperty("t")]
        public int Threads { get; set; }
        [JsonProperty("x")]
        public int Explore { get; set; }
    }
}
