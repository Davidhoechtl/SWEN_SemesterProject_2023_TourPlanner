using MTCG.DAL;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.DataAccess.Extensions;
using TourPlanner.DataTransferObjects.Models;

namespace TourPlannerBackEnd.Repositories
{
    public class TourRepository
    {
        public void InsertTour(Tour tour, IUnitOfWork unitOfWork)
        {
            string insertStatement = "INSERT INTO tour (name, start, destination, travellingType) VALUES (@name, @start, @destination, @travellingType)";
            //unitOfWork.

            unitOfWork.ExecuteNonQuery(insertStatement,
                new NpgsqlParameter("name", tour.Name),
                new NpgsqlParameter("start", tour.route.Start.Street),
                new NpgsqlParameter("destination", tour.route.Destination.Street),
                new NpgsqlParameter("travellingType", tour.route.TravellingType.ToString())
            );
        }

        public List<Tour> GetAllTours(IQueryDatabase queryDatabase)
        {
            string selectStatement = "SELECT name, start, destination, travellingType FROM tour";
            return queryDatabase.GetItems(
                selectStatement,
                reader =>
                {
                    List<Tour> tours = new();
                    while (reader.Read())
                    {
                        Tour next = ConvertTourFromReader(reader);
                        if(next != null)
                        {
                            tours.Add(next);
                        }
                    }

                    reader.Close();
                    return tours;
                }
            );
        }

        private Tour ConvertTourFromReader(NpgsqlDataReader reader)
        {
            Tour tour = new();
            if (reader.IsOnRow)
            {
                tour.Name = reader.GetValue<string>("name");

                string travellingType = reader.GetValue<string>("travellingtype");
                RouteType routeType = Enum.GetValues<RouteType>().First(v => v.ToString().Equals(travellingType));

                tour.route = new Route()
                {
                    Start = new Location()
                    {
                        Street = reader.GetValue<string>("start")
                    },
                    Destination = new Location()
                    {
                        Street = reader.GetValue<string>("destination")
                    },
                    TravellingType = routeType
                };
            }

            return tour;
        }
    }
}
