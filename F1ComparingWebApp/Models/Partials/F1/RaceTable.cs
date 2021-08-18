using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace F1ComparingWebApp.Models.Partials.F1
{
    public partial class RaceTable
    {
        [JsonProperty("season")]
        public long Season { get; set; }

        [JsonProperty("driverId")]
        public string DriverId { get; set; }

        [JsonProperty("Races")]
        public Race[] Races { get; set; }
    }
}
