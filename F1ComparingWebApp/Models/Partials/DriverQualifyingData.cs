using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using F1ComparingWebApp.Models.Partials.F1;

namespace F1ComparingWebApp.Models.Partials
{
    public partial class DriverQualifyingData
    {
        [JsonProperty("MRData")]
        public MrData MrData { get; set; }
    }

}
