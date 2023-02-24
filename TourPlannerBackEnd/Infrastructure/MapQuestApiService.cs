using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using TourPlannerBackEnd.Models;

namespace TourPlannerBackEnd.Infrastructure
{
    public class MapQuestApiService
    {
        private const string RootURL = "http://www.mapquestapi.com/";
        private readonly string apiKey;

        public MapQuestApiService(ApiKeyLoader apiKeyLoader)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(RootURL);

            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            apiKey = apiKeyLoader.Load();
        }

        public async Task<Location> GetLocationFromAddressLine(string addressLine)
        {
            UriBuilder uriBuilder = new UriBuilder(Path.Combine(RootURL, "geocoding/v1/address"));
            var query = HttpUtility.ParseQueryString(uriBuilder.Query, UTF8Encoding.UTF8);
            query["key"] = apiKey;
            query["location"] = addressLine;
            uriBuilder.Query = query.ToString();

            HttpResponseMessage response = await client.GetAsync(uriBuilder.ToString());
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();

                // convert to location object
                return new Location();
            }
            else
            {
                // handle api error
                return null;
            }
        }

        HttpClient client;
    }
}
