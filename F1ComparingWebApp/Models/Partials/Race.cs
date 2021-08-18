using F1ComparingWebApp.Models.Partials.F1;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace F1ComparingWebApp.Models.Partials
{
    public partial class Race
    {
        [JsonProperty("season")]
        public long Season { get; set; }

        [JsonProperty("round")]
        public long Round { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("raceName")]
        public string RaceName { get; set; }

        [JsonProperty("Circuit")]
        public Circuit Circuit { get; set; }

        [JsonProperty("date")]
        public DateTimeOffset Date { get; set; }

        [JsonProperty("time")]
        public DateTimeOffset Time { get; set; }

        [JsonProperty("Results")]
        public Result[] Results { get; set; }

        public Result Result { get => this.Results.FirstOrDefault(); set => Result = value; }

        [JsonProperty("QualifyingResults")]
        public QualifyingResult[] QualifyingResults { get; set; }

        public QualifyingResult QualifyingResult { get => this.QualifyingResults.FirstOrDefault(); set => QualifyingResult = value; }

    }
}
