using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
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
            requestParameter.locations = new string[] { start, destination };
            requestParameter.options.routeType = travellingType;

            HttpContent content = new StringContent(JsonConvert.SerializeObject(requestParameter));
            content.Headers.Add("key", apiKey);
            HttpResponseMessage response = await client.PostAsync(Path.Combine(HttpRestClient.RootURL, relativeUrl), content);
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                JsonDocument jDoc = JsonDocument.Parse(json);

                if (jDoc.RootElement.TryGetProperty("route", out JsonElement result))
                {
                    MapQuestRoute route = result.Deserialize<MapQuestRoute>();
                    return route;
                }
                else
                {
                    return null;
                }

            }
            else
            {
                // handle api error
                return null;
            }
        }

        HttpRestClient client;
    }
}
