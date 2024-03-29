﻿
namespace TourPlannerBackEnd.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using TourPlanner.DataTransferObjects.Models;
    using TourPlanner.EntityFramework.DataAccess;
    
    public class TourRepository : ITourRepository
    {
        private readonly TourPlannerDbContext dbContext;

        public TourRepository(TourPlannerDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Tour GetTourById(int id)
        {
            return dbContext.Tours
                .Where(t => t.Id == id)
                .Include(t => t.Start)
                .Include(t => t.Destination)
                .Include(t => t.Route)
                .Include(t => t.TourLogs)
                .FirstOrDefault();
        }

        public List<Tour> GetToursBySearchText(
            Func<Tour, string, Dictionary<string, string>, bool> matchFunction,
            string searchText,
            Dictionary<string, string> searchParameters)
        {
            IEnumerable<Tour> allTours = GetAllTours();
            return allTours
                .Where(tour => matchFunction(tour, searchText, searchParameters))
                .ToList();
        }

        public void InsertTour(Tour tour)
        {
            dbContext.Tours.Add(tour);
            dbContext.SaveChanges();
        }

        public void UpdateTour(Tour tour)
        {
            dbContext.Tours.Update(tour);
            dbContext.SaveChanges();
        }

        public void DeleteTour(int tourId)
        {
            int deltaRows = dbContext.Tours
                .Where(t => t.Id == tourId)
                .ExecuteDelete();

            if (deltaRows == 0)
            {
                throw new Exception("Delete command didnt delete any rows");
            }
        }

        public List<Tour> GetAllTours()
        {
            return dbContext.Tours
                .Include(t => t.Start)
                .Include(t => t.Destination)
                .Include(t => t.Route)
                .Include(t => t.TourLogs)
                .ToList();
        }
    }
}
