using TourPlanner.DataTransferObjects.Models;
using TourPlannerBackEnd.Infrastructure.Services;
using TourPlannerTests.Data;

namespace TourPlannerTests
{
    public class CalorieCalculatorTests
    {
        [SetUp]
        public void Setup()
        {
            calculator = new CalorieCalculationService();
        }

        [Test]
        public void TourIsNull()
        {
            Tour tour = null;

            Assert.Throws<ArgumentNullException>(() => { calculator.CalculateCaloriesForTour(tour); });
        }

        [Test]
        public void PedestriantPass()
        {
            Tour tour = TourData.Tours[0];
            tour.Route.DistanceInKm = 5;
            tour.Route.TravellingType = RouteType.Pedestriant.ToString();

            double? cal = calculator.CalculateCaloriesForTour(tour);

            // distanceInKm * 62
            Assert.NotNull(cal);
            Assert.That(cal == 5 * 62);
        }

        [Test]
        public void BicyclePass()
        {
            Tour tour = TourData.Tours[0];
            tour.Route.DistanceInKm = 15;
            tour.Route.TravellingType = RouteType.Bicycle.ToString();

            double? cal = calculator.CalculateCaloriesForTour(tour);

            // distanceInKm * 32
            Assert.NotNull(cal);
            Assert.That(cal == 15 * 32);
        }

        [Test]
        public void TourHasInvalidTravellingType()
        {
            Tour tour = TourData.Tours[0];
            tour.Route.DistanceInKm = 100;
            tour.Route.TravellingType = RouteType.DrivingFastest.ToString();

            double? cal = calculator.CalculateCaloriesForTour(tour);

            Assert.IsNull(cal);
        }

        private CalorieCalculationService calculator;
    }
}