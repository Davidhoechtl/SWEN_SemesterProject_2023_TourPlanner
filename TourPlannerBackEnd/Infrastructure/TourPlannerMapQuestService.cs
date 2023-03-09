
namespace TourPlannerBackEnd.Infrastructure
{
    using Microsoft.EntityFrameworkCore.Metadata.Conventions;
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
            this.mapQuestApiService = new MapQuestGeocodingApiService(apiKey);
        }

        public async Task<Location> GetLocationFromSingleLineAddress(string singleLineAddress)
        {
            MapQuestLocation mapQuestLocation = await mapQuestApiService.GetLocationFromAddressLine(singleLineAddress);
            // convert logic here

            if (mapQuestLocation != null)
            {
                try
                {
                    Location location = new()
                    {
                        State = mapQuestLocation.adminArea4,
                        City = mapQuestLocation.adminArea5,
                        PostCode = int.Parse(mapQuestLocation.postalCode),
                        Street = mapQuestLocation.street
                    };

                    return location;
                }
                catch (NullReferenceException ex)
                {
                    throw new Exception("The map quest location object is provided but invalid", ex);
                }
            }

            return null;
        }

        private readonly MapQuestGeocodingApiService mapQuestApiService;
    }
}
