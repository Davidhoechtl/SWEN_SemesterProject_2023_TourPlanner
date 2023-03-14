
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
            this.mapQuestDirectionApiService = new MapQuestDirectionsApiService(apiKey);
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
                        Country = mapQuestLocation.adminArea1,
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

        public async Task<Route> GetRouteFromLocations(string startSingleAddressLine, string destinationAddressLine, string travellingType)
        {
            MapQuestRoute apiRout = await mapQuestDirectionApiService.GetRoute(
                startSingleAddressLine,
                destinationAddressLine,
                travellingType
            );

            if(apiRout != null)
            {
                Route route = new Route()
                {
                    TravellingType = travellingType,
                    DistanceInKm = apiRout.distance,
                    EstimatedTimeInSeconds = apiRout.time
                };

                return route;
            }

            return null;
        }

        private readonly MapQuestGeocodingApiService mapQuestApiService;
        private readonly MapQuestDirectionsApiService mapQuestDirectionApiService;
    }
}
