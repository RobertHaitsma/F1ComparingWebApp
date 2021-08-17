using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using F1ComparingWebApp.Models.Partials;
using Microsoft.Extensions.Logging;

namespace F1ComparingWebApp.Service
{
    public interface IEregastAPI
    {

    }

    public class EregastAPI : IEregastAPI
    {
        static readonly HttpClient Client = new HttpClient();

        private readonly ILogger<EregastAPI> _logger;

        public EregastAPI(ILogger<EregastAPI> logger)
        {
            _logger = logger;
        }

        public QualifyingDto GetQualifying()
        {

            return new QualifyingDto();
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
