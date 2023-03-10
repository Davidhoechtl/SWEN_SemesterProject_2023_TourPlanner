using Microsoft.EntityFrameworkCore;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.DataTransferObjects.Models;
using TourPlanner.EntityFramework.DataAccess;

namespace TourPlannerBackEnd.Repositories
{
    public class TourRepository
    {
        private readonly TourPlannerDbContext dbContext;

        public TourRepository(TourPlannerDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void InsertTour(Tour tour)
        {
            //string insertStatement = "INSERT INTO tour (name, start, destination, travellingType) VALUES (@name, @start, @destination, @travellingType)";
            ////unitOfWork.

            //unitOfWork.ExecuteNonQuery(insertStatement,
            //    new NpgsqlParameter("name", tour.Name),
            //    new NpgsqlParameter("start", tour.Start.Street),
            //    new NpgsqlParameter("destination", tour.Destination.Street),
            //    new NpgsqlParameter("travellingType", tour.TravellingType.ToString())
            //);

            dbContext.Tours.Add(tour);
            dbContext.SaveChanges();
        }

        public List<Tour> GetAllTours()
        {
            return dbContext.Tours
                .Include(t => t.Start)
                .Include(t => t.Destination)
                .ToList();
        }
    }
}
