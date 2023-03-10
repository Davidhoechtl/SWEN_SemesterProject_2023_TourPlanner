using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.MapQuestApi.Domain;

namespace TourPlanner.MapQuestApi
{
    public class MapQuestDirectionsApiService
    {
        private const string relativeUrl = "directions/v2/route";
        private readonly string apiKey;

        public MapQuestDirectionsApiService(string apiKey)
        {
            client = new HttpRestClient();
            this.apiKey = apiKey;
        }

        public async Task<MapQuestRoute> GetRoute(string start, string destination, string travellingType)
        {
            RouteRequestParameter requestParameter = new();
            HttpContent content = new HttpContent
            HttpResponseMessage respone = await client.PostAsync();
        }

        HttpRestClient client;
    }
}
