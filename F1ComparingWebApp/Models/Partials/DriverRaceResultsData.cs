using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using F1ComparingWebApp.Models.Partials.F1;
using Newtonsoft.Json;

namespace F1ComparingWebApp.Models.Partials
{
    public class DriverRaceResultsData
    {
        [JsonProperty("MRData")]
        public MrData MrData { get; set; }
    }

}
