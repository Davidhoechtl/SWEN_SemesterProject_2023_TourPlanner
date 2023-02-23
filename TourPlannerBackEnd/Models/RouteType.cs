using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlannerBackEnd.Models
{
    /// <summary>
    /// This enum contains all possible route options
    /// </summary>
    public enum RouteType
    {
        DrivingFastest,
        DrivingShortest,
        Pedestriant,
        Bicycle
    }
}
