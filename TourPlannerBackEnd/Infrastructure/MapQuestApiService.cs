using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TourPlannerBackEnd.Models;

namespace TourPlannerBackEnd.Infrastructure
{
    internal class MapQuestApiService
    {
        private const string RootURL = "http://www.mapquestapi.com/";

        public MapQuestApiService()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(RootURL);

            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
        }

        //public async Task<Location> GetLocationFromAddressLine(string addressLine)
        //{
        //    UriBuilder uriBuilder = new()
        //    HttpResponseMessage response = await client.post("geocoding/v1/address");
        //}

        HttpClient client;
    }
}
