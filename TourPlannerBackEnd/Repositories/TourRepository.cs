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
            string insertStatement = "INSERT INTO public.\"Tour\" (\"Name\", \"Start\", \"Destination\", \"TravellingType\") VALUES (@name, @start, @destination, @travellingType)";
            //unitOfWork.

            unitOfWork.ExecuteNonQuery(insertStatement,
                new NpgsqlParameter("name", tour.Name),
                new NpgsqlParameter("start", tour.route.Start.Street),
                new NpgsqlParameter("destination", tour.route.Destination.Street),
                new NpgsqlParameter("travellingType", tour.route.TravellingType.ToString())
            );
        }
    }
}
