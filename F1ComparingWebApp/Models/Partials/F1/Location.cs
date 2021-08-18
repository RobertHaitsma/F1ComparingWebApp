using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace F1ComparingWebApp.Models.Partials.F1
{
    public partial class Location
    {
        [JsonProperty("lat")]
        public string Lat { get; set; }

        [JsonProperty("long")]
        public string Long { get; set; }

        [JsonProperty("locality")]
        public string Locality { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }
    }
}
