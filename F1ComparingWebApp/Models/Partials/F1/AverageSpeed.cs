using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace F1ComparingWebApp.Models.Partials.F1
{
    public partial class AverageSpeed
    {
        [JsonProperty("units")]
        public string Units { get; set; }

        [JsonProperty("speed")]
        public string Speed { get; set; }
    }
}
