using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace F1ComparingWebApp.Models.Partials.F1
{
    public partial class Result
    {
        [JsonProperty("number")]
        public long Number { get; set; }

        [JsonProperty("position")]
        public long Position { get; set; }

        [JsonProperty("positionText")]
        public string PositionText { get; set; }

        [JsonProperty("points")]
        public long Points { get; set; }

        [JsonProperty("Driver")]
        public Driver Driver { get; set; }

        [JsonProperty("Constructor")]
        public Constructor Constructor { get; set; }

        [JsonProperty("grid")]
        public long Grid { get; set; }

        [JsonProperty("laps")]
        public long Laps { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("Time", NullValueHandling = NullValueHandling.Ignore)]
        public ResultTime Time { get; set; }

        [JsonProperty("FastestLap", NullValueHandling = NullValueHandling.Ignore)]
        public FastestLap FastestLap { get; set; }
    }
}
