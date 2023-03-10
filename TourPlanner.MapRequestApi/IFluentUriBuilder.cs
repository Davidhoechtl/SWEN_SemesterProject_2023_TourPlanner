using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.MapQuestApi
{
    internal interface IFluentUriBuilder
    {
        IFluentUriBuilder CreateUriParameterCollection(UriBuilder builder, out NameValueCollection collection);
        IFluentUriBuilder AddParameter(NameValueCollection collection, string name, string value);
        string BuildUrl(NameValueCollection collection);
    }
}
