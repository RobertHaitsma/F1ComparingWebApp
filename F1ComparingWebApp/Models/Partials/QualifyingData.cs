using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace F1ComparingWebApp.Models.Partials
{
    public partial class QualifyingData
    {
        [JsonProperty("MRData")]
        public MrData MrData { get; set; }
    }

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
    }

    public partial class RaceTable
    {
        [JsonProperty("season")]
        public long Season { get; set; }

        [JsonProperty("Races")]
        public Race[] Races { get; set; }
    }

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

        [JsonProperty("QualifyingResults")]
        public QualifyingResult[] QualifyingResults { get; set; }

        public QualifyingResult QualifyingResult { get => this.QualifyingResults.FirstOrDefault(); set => QualifyingResult = value; }
}

    public partial class Circuit
    {
        [JsonProperty("circuitId")]
        public string CircuitId { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("circuitName")]
        public string CircuitName { get; set; }

        [JsonProperty("Location")]
        public Location Location { get; set; }
    }

    public partial class Location
    {
        [JsonProperty("lat")]
        public string Lat { get; set; }

        [JsonProperty("long")]
        public string Long { get; set; }

        [JsonProperty("locality")]
        public string Locality { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }
    }

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





