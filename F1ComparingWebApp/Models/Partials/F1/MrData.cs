using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace F1ComparingWebApp.Models.Partials.F1
{
    public partial class MrData
    {
        [JsonProperty("xmlns")]
        public Uri Xmlns { get; set; }

        [JsonProperty("series")]
        public string Series { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("limit")]
        public long Limit { get; set; }

        [JsonProperty("offset")]
        public long Offset { get; set; }

        [JsonProperty("total")]
        public long Total { get; set; }

        [JsonProperty("RaceTable")]
        public RaceTable RaceTable { get; set; }

        [JsonProperty("DriverTable")]
        public DriverTable DriverTable { get; set; }
    }
}
