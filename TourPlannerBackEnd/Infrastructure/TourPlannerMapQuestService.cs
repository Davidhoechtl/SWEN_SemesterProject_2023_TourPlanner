
namespace TourPlannerBackEnd.Infrastructure
{
    using TourPlanner.DataTransferObjects.Models;
    using TourPlanner.MapQuestApi;
    using TourPlanner.MapQuestApi.Domain;

    /// <summary>
    /// Bridge between the MapQuest REST API and TourPlanner
    /// it sends requests to the api and convert the response in our domain model
    /// </summary>
    public class TourPlannerMapQuestService
    {
        public TourPlannerMapQuestService(ApiKeyLoader apiKeyLoader)
        {
            string apiKey = apiKeyLoader.Load();
            this.mapQuestApiService = new MapQuestApiService(apiKey);
        }

        public async Task<Location> GetLocationFromSingleLineAddress(string singleLineAddress)
        {
            MapQuestLocation location = await mapQuestApiService.GetLocationFromAddressLine(singleLineAddress);
            // convert logic here

            return new Location();
        }

        private readonly MapQuestApiService mapQuestApiService;
    }
}
