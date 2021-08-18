using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace F1ComparingWebApp.Models.Partials.F1
{
    public partial class Driver
    {
        [JsonProperty("driverId")]
        public string DriverId { get; set; }

        [JsonProperty("permanentNumber")]
        public long PermanentNumber { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("givenName")]
        public string GivenName { get; set; }

        [JsonProperty("familyName")]
        public string FamilyName { get; set; }

        [JsonProperty("dateOfBirth")]
        public DateTimeOffset DateOfBirth { get; set; }

        [JsonProperty("nationality")]
        public string Nationality { get; set; }
    }
}
