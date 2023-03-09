
namespace TourPlanner.MapQuestApi
{
    using Newtonsoft.Json;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Text.Json;
    using System.Web;
    using System.Xml.Linq;
    using TourPlanner.MapQuestApi.Domain;

    public class MapQuestGeocodingApiService
    {
        private const string RootURL = "http://www.mapquestapi.com/";
        private readonly string apiKey;

        public MapQuestGeocodingApiService(string apiKey)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(RootURL);

            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            this.apiKey = apiKey;
        }

        public async Task<MapQuestLocation> GetLocationFromAddressLine(string addressLine)
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

                JsonDocument jsonDocument = JsonDocument.Parse(json);
                foreach( JsonElement resultItem in GetResultIfSuccessfull(jsonDocument.RootElement))
                {
                    foreach (JsonElement locationJson in resultItem.GetProperty("locations").EnumerateArray())
                    {
                        MapQuestLocation location = locationJson.Deserialize<MapQuestLocation>();
                        return location;
                    }
                }
              
                return null;
            }
            else
            {
                // handle api error
                return null;
            }
        }

        private IEnumerable<JsonElement> GetResultIfSuccessfull(JsonElement root)
        {
            if(root.TryGetProperty("info", out JsonElement info))
            {
                if (IsResponseSuccessfull(info))
                {
                    return GetResultsArrayEnumerator(root);
                }
            }
         
            return new List<JsonElement>();
        }

        private bool IsResponseSuccessfull(JsonElement info)
        {
            if(info.TryGetProperty("statuscode", out JsonElement status))
            {
                short statusCode = status.GetInt16();
                if(statusCode == 0)
                {
                    return true;
                }
            }

            return false;
        }

        private IEnumerable<JsonElement> GetResultsArrayEnumerator(JsonElement root)
        {
            if(root.TryGetProperty("results", out JsonElement resultArray))
            {
                return resultArray.EnumerateArray();
            }

            return new List<JsonElement>();
        }

        HttpClient client;
    }
}
