using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.MapQuestApi
{
    internal class MapQuestStaticMapApiService
    {
        private const string relativeUrl = "directions/v2/route";
        private readonly string apiKey;
        public MapQuestStaticMapApiService(string apiKey)
        {
            client = new HttpRestClient();
            this.apiKey = apiKey;
        }

        public byte[] GetTourImage(string start, string end)
        {
            return null;
        }

        HttpRestClient client;
    }
}
