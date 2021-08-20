using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using F1ComparingWebApp.Models.Partials;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using static F1ComparingWebApp.Models.Partials.QualifyingData;

namespace F1ComparingWebApp.Service
{
    public interface IEregastAPI
    {
        public Task<QualifyingData> GetQualifyingOfCurrentSeasonAsync();
        public Task<DriverQualifyingData> GetQualifiyingOfCurrentSeasonByDriver(string driver);
        public Task<DriverRaceResultsData> GetRaceResultsOfCurrentSeasonByDriver(string driver);
        public Task<DriversInSeasonData> GetDriverOfCurrentSeason();

    }

    public class EregastAPI : IEregastAPI
    {
        static readonly HttpClient Client = new HttpClient();
        private string RootUrl = "https://ergast.com/api/f1";
        private string Type = ".json";

        private readonly ILogger<EregastAPI> _logger;

        public EregastAPI(ILogger<EregastAPI> logger)
        {
            _logger = logger;
        }

        public async Task<DriverRaceResultsData> GetRaceResultsOfCurrentSeasonByDriver(string driver)
        {
            var response = await GetJsonResponse(RootUrl, $"/current/drivers/{driver}/results", Type);

            DriverRaceResultsData driverRaceResultData = JsonConvert.DeserializeObject<DriverRaceResultsData>(response);

            return driverRaceResultData;
        }

        public async Task<QualifyingData> GetQualifyingOfCurrentSeasonAsync()
        {
            var response = await GetJsonResponse(RootUrl, "/current/qualifying", Type);

            QualifyingData myDeserializedClass = JsonConvert.DeserializeObject<QualifyingData>(response);
            
            return myDeserializedClass;
        }

        public async Task<DriverQualifyingData> GetQualifiyingOfCurrentSeasonByDriver(string driver)
        {
            var response = await GetJsonResponse(RootUrl, $"/current/drivers/{driver}/qualifying", Type);

            DriverQualifyingData driverQualifyingData = JsonConvert.DeserializeObject<DriverQualifyingData>(response);

            return driverQualifyingData;
        } 

        public async Task<DriversInSeasonData> GetDriverOfCurrentSeason()
        {
            var response = await GetJsonResponse(RootUrl, $"/current/drivers/", Type);

            DriversInSeasonData driversInSeason = JsonConvert.DeserializeObject<DriversInSeasonData>(response);

            return driversInSeason;
        }

        private static async Task<string> GetJsonResponse(string rootUrl, string url, string type)
        {
            url = rootUrl + url + type;

            var json = "";
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url).ConfigureAwait(false);

                json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            }

            return json;
        }

        private string GetResponseString(string url)
        {
            System.Net.WebClient client = new System.Net.WebClient();
            string data = Encoding.Default.GetString(client.DownloadData(url));

            return data;
        }

        private async Task<string> ResponseAsync(string url)
        {
            try
            {
                var response = await Client.GetAsync(url);

                response.EnsureSuccessStatusCode();

                var responseStream = response.Content.ReadAsStringAsync();

                if (responseStream == null) throw new ArgumentNullException(nameof(responseStream));

                return await responseStream;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }

            return string.Empty;
        }

    }

}
