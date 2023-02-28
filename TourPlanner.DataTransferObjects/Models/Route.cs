using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.DataTransferObjects.Models
{
    public class Route
    {
        public Location Start { get; set; }
        public Location Destination { get; set; }
        public RouteType TravellingType { get; set; }
    }
}
