
namespace TourPlannerTests.Data
{
    using TourPlanner.DataTransferObjects.Models;

    internal static class TourData
    {
        public static List<Tour> Tours
        {
            get
            {
                return new List<Tour>()
                {
                    new Tour()
                    {
                        Name = "Foo",
                        StartDate = DateTime.Now,
                        TravellingType = RouteType.DrivingFastest.ToString(),
                        CaloriesCount = 100,
                        ChildFriendliness = 1,
                        Popularity = 1,
                        Start = new Location()
                        {
                            City="Wien",
                            Country="Österreich",
                            PostCode=1200,
                            State = "Wien",
                            Street = "Wehlistraße"
                        },
                        Destination = new Location()
                        {
                            City="Strasshof",
                            Country="Österreich",
                            PostCode=2231,
                            State = "Niederösterreich",
                            Street = "Bahnhofstraße"
                        },
                        Route = new Route()
                        {
                            EstimatedTimeInSeconds = 13000,
                            DistanceInKm = 24,
                            TravellingType = RouteType.DrivingFastest.ToString(),
                        },
                        TourLogs = new List<TourLog>()
                        {
                            new TourLog()
                            {
                                Date = DateTime.Now,
                                Comment = "Test",
                                TakenTimeInSeconds = 20,
                                Rating = 10,
                                Difficulty = 2
                            }
                        }
                    },
                    new Tour()
                    {
                        Name = "Bar",
                        StartDate = DateTime.Now,
                        TravellingType = RouteType.DrivingFastest.ToString(),
                        CaloriesCount = 200,
                        ChildFriendliness = 5,
                        Popularity = 2,
                        Start = new Location()
                        {
                            City="Wien",
                            Country="Österreich",
                            PostCode=1200,
                            State = "Wien",
                            Street = "Wehlistraße"
                        },
                        Destination = new Location()
                        {
                            City="Strasshof",
                            Country="Österreich",
                            PostCode=2231,
                            State = "Niederösterreich",
                            Street = "Bahnhofstraße"
                        },
                        Route = new Route()
                        {
                            EstimatedTimeInSeconds = 13000,
                            DistanceInKm = 24,
                            TravellingType = RouteType.DrivingFastest.ToString(),
                        },
                        TourLogs = new List<TourLog>()
                        {
                            new TourLog()
                            {
                                Date = DateTime.Now,
                                Comment = "Test1",
                                TakenTimeInSeconds = 20,
                                Rating = 10,
                                Difficulty = 2
                            },
                            new TourLog()
                            {
                                Date = DateTime.Now,
                                Comment = "Test2",
                                TakenTimeInSeconds = 20,
                                Rating = 10,
                                Difficulty = 2
                            }
                        }
                    }
                };
            }
        }
    }
}
