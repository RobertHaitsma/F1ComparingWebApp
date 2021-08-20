using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace F1ComparingWebApp.Models.Partials.F1
{
    public partial class DriverTable
    {
        [JsonProperty("season")]
        public long Season { get; set; }

        [JsonProperty("Drivers")]
        public Driver[] Drivers { get; set; }
    }
}
