using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.MapQuestApi
{
    public class MapQuestStaticMapApiService
    {
        private const string relativeUrl = "staticmap/v5/map";
        private readonly string apiKey;
        public MapQuestStaticMapApiService(string apiKey)
        {
            client = new HttpRestClient();
            this.apiKey = apiKey;
        }

        public async Task<byte[]> GetTourImage(string start, string end, int width, int height)
        {
            UriBuilder uriBuilder = client.CreateUriBuilder(relativeUrl);

            uriBuilder.Query = client
                .CreateUriParameterCollection(uriBuilder, out NameValueCollection collection)
                .AddParameter(collection, "key", apiKey)
                .AddParameter(collection, "start", start)
                .AddParameter(collection, "end", end)
                .AddParameter(collection, "size", $"{width},{height}")
                .BuildUrl(collection);

            HttpResponseMessage response = await client.GetAsync(uriBuilder.ToString());
            if (response.IsSuccessStatusCode)
            {
                var test2 = await response.Content.ReadAsStringAsync();
                //using (Stream stream = await response.Content.ReadAsStreamAsync())
                //{
                //    using(FileStream fileStream = new FileStream("C:/Testimage.jpg", FileMode.Create))
                //    {
                //        stream.CopyTo(fileStream);
                //    }
                //}
                return await response.Content.ReadAsByteArrayAsync();
            }
            else
            {
                return null;
            }
        }

        HttpRestClient client;
    }
}
