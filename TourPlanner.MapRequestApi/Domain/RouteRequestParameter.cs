using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.MapQuestApi.Domain
{
    internal class RouteRequestParameter
    {
        public string[] locations { get; set; }
        public RouteOptions options { get; set; }
    }
}
