using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using F1ComparingWebApp.Models.Partials.F1;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace F1ComparingWebApp.Models.Partials
{
    public partial class QualifyingData
    {
        [JsonProperty("MRData")]
        public MrData MrData { get; set; }
    }
        
}





