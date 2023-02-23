using MTCG.DAL;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlannerBackEnd.Models;

namespace TourPlannerBackEnd.Repositories
{
    public class TourRepository
    {
        public void InsertTour(Tour tour, IUnitOfWork unitOfWork)
        {
            string insertStatement = "INSERT INTO Tour(Name, From, To, TravellingType) VALUES (@name, @from, @to, @travellingType)";
            //unitOfWork.

            unitOfWork.ExecuteNonQuery(insertStatement,
                new NpgsqlParameter("name", tour.Name),
                new NpgsqlParameter("From", tour.route.Start.Street),
                new NpgsqlParameter("From", tour.route.Destination.Street),
                new NpgsqlParameter("From", tour.route.TravellingType.ToString())
            );
        }
    }
}
