using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.DataTransferObjects.Models;

namespace TourPlannerBackEnd.Infrastructure.Services
{
    internal class CalorieCalculationService
    {
        public double? CalculateCaloriesForTour(Tour tour)
        {
            if (tour.TravellingType.Equals(RouteType.Pedestriant))
            {
                // Calculate Calories for walking
            }
            else if (tour.TravellingType.Equals(RouteType.Bicycle))
            {
                // Calculate Calories for bicycling
            }

            return null;
        }
    }
}
