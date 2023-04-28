using System.Linq.Expressions;
using TourPlanner.DataTransferObjects.Models;
using TourPlannerBackEnd.Repositories;

namespace TourPlannerBackEnd.Infrastructure.Services
{
    public class TourAutoPropertyService
    {
        public TourAutoPropertyService(ITourRepository tourRepository, CalorieCalculationService calorieCalculationService)
        {
            this.tourRepository = tourRepository;
            this.calorieCalculationService = calorieCalculationService;
        }

        public void RecalculateTourProperties(int tourId)
        {
            Tour tour = tourRepository.GetTourById(tourId);

            if(tour != null)
            {
                tour.Popularity = CalculatePopularity(tour);
                tour.ChildFriendliness = CalculateChildFriendliness(tour);
                tour.CaloriesCount = calorieCalculationService.CalculateCaloriesForTour(tour);

                tourRepository.UpdateTour(tour);
            }
            else
            {
                throw new Exception($"Tour with id {tourId} was not found");
            }
        }

        private int CalculatePopularity(Tour tour)
        {
            return tour.TourLogs.Count;
        }

        /// <summary>
        /// Calculate the child friendliness
        /// 
        /// tours that take under 15 min -> +2
        /// tours that take under 30 min -> +1
        /// tours that take up to 45 min -> 0
        /// tours that take up to 1 hour -> -1
        /// tours that take more than 1 hour -> -2
        /// 
        /// tours that have difficulty equals to 1 -> +3
        /// tours that have difficulty equals to 2 -> +2
        /// tours that have difficulty equals to 3 -> +1
        /// tours that have difficulty equals to 4 -> +1
        /// tours that have difficulty equals to 5 -> 0
        /// tours that have difficulty equals to 6 -> -1
        /// tours that have difficulty equals to 7 -> -2
        /// tours that have difficulty equals to 8 -> -2
        /// tours that have difficulty equals to 9 -> -3
        /// tours that have difficulty equals to 10 -> -4
        /// 
        /// Pedestrian
        /// tours that have a distance equals to or shorter 1km -> +2
        /// tours that have a distance equals to or shorter 3km -> +1
        /// tours that have a distance equals to or shorter 7km -> 0
        /// tours that have a distance equals to or shorter 10km -> -1
        /// tours that have a distance longer than 10km -> -2
        /// 
        /// Bycicle
        /// tours that have a distance equals to or shorter 5km -> +2
        /// tours that have a distance equals to or shorter 10km -> +1
        /// tours that have a distance equals to or shorter 15km -> 0
        /// tours that have a distance equals to or shorter 20km -> -1
        /// tours that have a distance longer than 20km -> -2
        /// 
        /// Car (will not be taken into account)
        /// 
        /// </summary>
        /// <param name="tour"></param>
        /// <returns></returns>
        private int CalculateChildFriendliness(Tour tour)
        {


            double FriendlinessSum = 0;
            foreach (TourLog log in tour.TourLogs)
            {
                FriendlinessSum += log.TakenTimeInSeconds switch
                {
                    <= 15 => 2,
                    > 15 and <= 30 => 1,
                    > 30 and <= 45 => 0,
                    > 45 and <= 60 => -1,
                    > 60 => -2,
                    _ => 0
                };

                FriendlinessSum += log.Difficulty switch
                {
                    1 => 3,
                    2 => 2,
                    3 => 1,
                    4 => 1,
                    5 => 0,
                    6 => -1,
                    7 => -2,
                    8 => -2,
                    9 => -3,
                    10 => -4,
                    _ => 0
                };
            }

            // average the difficulty and takenTimeInSeconds score from the Tourlogs
            FriendlinessSum /= tour.TourLogs.Count;

            if (tour.TravellingType.Equals(RouteType.Pedestriant.ToString()))
            {
                FriendlinessSum += tour.Route.DistanceInKm switch
                {
                    <= 1 => 2,
                    > 1 and <= 3 => 1,
                    > 3 and <= 7 => 0,
                    > 7 and <= 10 => -1,
                    > 10 => -2,
                    _ => 0
                };
            }
            else if (tour.TravellingType.Equals(RouteType.Bicycle.ToString()))
            {
                FriendlinessSum += tour.Route.DistanceInKm switch
                {
                    <= 5 => 2,
                    > 5 and <= 10 => 1,
                    > 10 and <= 15 => 0,
                    > 15 and <= 20 => -1,
                    > 20 => -2,
                    _ => 0
                };
            }

            // FriendlinessSum below 0 are equals to 0
            FriendlinessSum = FriendlinessSum < 0 ? 0 : FriendlinessSum;

            return (int)Math.Ceiling(FriendlinessSum);
        }

        private readonly ITourRepository tourRepository;
        private readonly CalorieCalculationService calorieCalculationService;
    }
}
