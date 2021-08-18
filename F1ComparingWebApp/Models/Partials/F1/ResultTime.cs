using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace F1ComparingWebApp.Models.Partials.F1
{
    public partial class ResultTime
    {
        [JsonProperty("millis")]
        public long Millis { get; set; }

        [JsonProperty("time")]
        public string Time { get; set; }
    }
}
