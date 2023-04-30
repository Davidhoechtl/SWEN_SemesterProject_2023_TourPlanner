using TourPlanner.DataTransferObjects.Models;
using TourPlannerBackEnd.Infrastructure.Services;
using TourPlannerBackEnd.Repositories;
using TourPlannerTests.Data;
using TourPlannerTests.Mockups;

namespace TourPlannerTests
{
    internal class AutoPropertyCalculatorTests
    {
        [SetUp]
        public void Setup()
        {
            CalorieCalculationService caloriesCalculator = new();
            ITourRepository tourRepo = new MockTourRepository();
            propertyCalculator = new TourAutoPropertyService(tourRepo, caloriesCalculator);
        }

        [Test]
        public void TourIdIsInvalid()
        {
            Tour tour = TourData.Tours[0];

            Assert.Throws<Exception>(() =>
            {
                propertyCalculator.RecalculateTourProperties(-1);
            });
        }

        [Test]
        public void TourPopularityPass()
        {
            Tour tour = TourData.Tours[0];
            tour.Id = 0;

            propertyCalculator.RecalculateTourProperties(tour.Id);

            Assert.That(tour.Popularity == 1);
        }

        [Test]
        public void TourPedestriantChildfriendlinessPass()
        {
            Tour tour = TourData.Tours[0];
            tour.Id = 0;
            tour.Route.DistanceInKm = 3;
            tour.TourLogs[0].Difficulty = 2;
            tour.TourLogs[0].TakenTimeInSeconds = 30;
            tour.TravellingType = RouteType.Pedestriant.ToString();
            tour.Route.TravellingType = RouteType.Pedestriant.ToString();

            propertyCalculator.RecalculateTourProperties(tour.Id);

            Assert.That(tour.ChildFriendliness == 4);
        }

        [Test]
        public void TourBicycleChildfriendlinessPass()
        {
            Tour tour = TourData.Tours[0];
            tour.Id = 0;
            tour.Route.DistanceInKm = 15;
            tour.TourLogs[0].Difficulty = 4;
            tour.TourLogs[0].TakenTimeInSeconds = 60;
            tour.TravellingType = RouteType.Bicycle.ToString();
            tour.Route.TravellingType = RouteType.Bicycle.ToString();

            propertyCalculator.RecalculateTourProperties(tour.Id);

            Assert.That(tour.ChildFriendliness == 0);
        }

        private TourAutoPropertyService propertyCalculator;
    }
}
