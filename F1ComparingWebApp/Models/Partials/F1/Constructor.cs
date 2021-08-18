using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace F1ComparingWebApp.Models.Partials.F1
{
    public partial class Constructor
    {
        [JsonProperty("constructorId")]
        public string ConstructorId { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("nationality")]
        public string Nationality { get; set; }
    }
}
