using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.DataTransferObjects.Models;

namespace TourPlannerBackEnd.Infrastructure.Services
{
    public class CalorieCalculationService
    {
        public double? CalculateCaloriesForTour(Tour tour)
        {
            if (tour == null)
            {
                throw new ArgumentNullException(nameof(tour));
            }

            double distanceInKm = tour.Route.DistanceInKm;
            if (tour.TravellingType.Equals(RouteType.Pedestriant.ToString()))
            {
                // Calculate Calories for walking
                // source: https://www.aipt.edu.au/articles/how-many-calories-are-burned-walking
                // 1km = 62cal (average estimate)
                return distanceInKm * 62;
            }
            else if (tour.TravellingType.Equals(RouteType.Bicycle.ToString()))
            {
                // Calculate Calories for bicycling
                // source: https://burned-calories.com/cycling#:~:text=With%20a%20normal%20pace%20of,and%2032%20calories%20every%20kilometer.
                // 1km = 32cal (average estimate)
                return distanceInKm * 32;
            }

            return null;
        }
    }
}
