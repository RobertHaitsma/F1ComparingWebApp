using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace F1ComparingWebApp.Models.Partials.F1
{
    public partial class Circuit
    {
        [JsonProperty("circuitId")]
        public string CircuitId { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("circuitName")]
        public string CircuitName { get; set; }

        [JsonProperty("Location")]
        public Location Location { get; set; }
    }

}
