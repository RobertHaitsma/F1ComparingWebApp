using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace F1ComparingWebApp.Models.Partials.F1
{
    public partial class QualifyingResult
    {
        [JsonProperty("number")]
        public long Number { get; set; }

        [JsonProperty("position")]
        public long Position { get; set; }

        [JsonProperty("Driver")]
        public Driver Driver { get; set; }

        [JsonProperty("Constructor")]
        public Constructor Constructor { get; set; }

        [JsonProperty("Q1")]
        public string Q1 { get; set; }

        [JsonProperty("Q2", NullValueHandling = NullValueHandling.Ignore)]
        public string Q2 { get; set; }

        [JsonProperty("Q3", NullValueHandling = NullValueHandling.Ignore)]
        public string Q3 { get; set; }
    }
}
