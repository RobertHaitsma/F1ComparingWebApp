using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace F1ComparingWebApp.Models.Partials.F1
{
    public partial class FastestLap
    {
        [JsonProperty("rank")]
        public long Rank { get; set; }

        [JsonProperty("lap")]
        public long Lap { get; set; }

        [JsonProperty("Time")]
        public FastestLapTime Time { get; set; }

        [JsonProperty("AverageSpeed")]
        public AverageSpeed AverageSpeed { get; set; }
    }
}
