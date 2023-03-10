
namespace TourPlanner.MapQuestApi
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web;

    internal class HttpRestClient : IFluentUriBuilder
    {
        private const string RootURL = "http://www.mapquestapi.com/";

        public HttpRestClient()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(RootURL);

            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public UriBuilder CreateUriBuilder(string relativeUrl)
        {
            return new UriBuilder(Path.Combine(RootURL, relativeUrl));
        }

        public IFluentUriBuilder CreateUriParameterCollection(UriBuilder builder, out NameValueCollection collection)
        {
            collection = HttpUtility.ParseQueryString(builder.Query, UTF8Encoding.UTF8);
            return this;
        }

        public IFluentUriBuilder AddParameter(NameValueCollection collection, string name, string value)
        {
            collection[name] = value;
            return this;
        }
        public string BuildUrl(NameValueCollection collection)
        {
            return collection.ToString();
        }

        public async Task<HttpResponseMessage> GetAsync(string url)
        {
            return await client.GetAsync(url);
        }

        public async Task<HttpResponseMessage> PostAsync(string url, HttpContent content)
        {
            return await client.PostAsync(url, content);
        }

        HttpClient client;
    }
}
