
namespace TourPlanner.MapQuestApi
{
    using Newtonsoft.Json;
    using System.Collections.Specialized;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Text.Json;
    using System.Web;
    using System.Xml.Linq;
    using TourPlanner.MapQuestApi.Domain;

    public class MapQuestGeocodingApiService
    {
        public MapQuestGeocodingApiService(string apiKey)
        {
            client = new HttpRestClient();
            this.apiKey = apiKey;
        }

        public async Task<MapQuestLocation> GetLocationFromAddressLine(string addressLine)
        {
            UriBuilder uriBuilder = client.CreateUriBuilder("geocoding/v1/address");

            uriBuilder.Query = client
                .CreateUriParameterCollection(uriBuilder, out NameValueCollection query)
                .AddParameter(query, "key", apiKey)
                .AddParameter(query, "location", addressLine)
                .BuildUrl(query);

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

        HttpRestClient client;
        private readonly string apiKey;
    }
}
