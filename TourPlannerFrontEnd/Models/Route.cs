using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlannerFrontEnd.Models
{
    class Route
    {
        public Location Start { get; set; }
        public Location Destination { get; set; }
        public RouteType TravellingType { get; set; }
    }
}
