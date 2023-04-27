
namespace TourPlannerBackEnd.Repositories
{
    using TourPlanner.DataTransferObjects.Models;
    
    public interface ITourRepository
    {
        Tour GetTourById(int id);

        List<Tour> GetToursBySearchText(
            Func<Tour, string, Dictionary<string, string>, bool> matchFunction,
            string searchText,
            Dictionary<string, string> searchParameters);

        void InsertTour(Tour tour);
        void UpdateTour(Tour tour);
        void DeleteTour(int tourId);

        List<Tour> GetAllTours();
    }
}
