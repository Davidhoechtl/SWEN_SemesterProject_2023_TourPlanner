using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.DataTransferObjects.Models
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
