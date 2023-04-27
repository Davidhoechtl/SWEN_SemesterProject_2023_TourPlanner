

namespace TourPlannerTests.Mockups
{
    using TourPlannerBackEnd.Repositories;
    using TourPlanner.DataTransferObjects.Models;
    using TourPlannerTests.Data;

    internal class MockTourRepository : ITourRepository
    {
        public void DeleteTour(int tourId)
        {
            //noop
        }

        public List<Tour> GetAllTours()
        {
            return TourData.Tours;
        }

        public Tour GetTourById(int id)
        {
            return TourData.Tours.FirstOrDefault(t => t.Id == id);
        }

        public List<Tour> GetToursBySearchText(Func<Tour, string, Dictionary<string, string>, bool> matchFunction, string searchText, Dictionary<string, string> searchParameters)
        {
            return TourData.Tours.Where(t => matchFunction(t, searchText, searchParameters)).ToList();
        }

        public void InsertTour(Tour tour)
        {
            //noop
        }

        public void UpdateTour(Tour tour)
        {
            //noop
        }
    }
}
