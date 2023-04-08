using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.DataTransferObjects.Models;
using TourPlanner.EntityFramework.DataAccess;

namespace TourPlannerBackEnd.Repositories
{
    public class TourLogRepository
    {
        public TourLogRepository(TourPlannerDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void SaveTourLog(TourLog tourLog)
        {
            dbContext.TourLogs.Add(tourLog);
            dbContext.SaveChanges();
        }

        public void UpdateTourLog(TourLog tourLog)
        {
            dbContext.Update(tourLog);
            dbContext.SaveChanges();
        }

        public void DeleteTourLog(TourLog tourLog)
        {
            //tourLog.Tour.TourLogs.Remove(tourLog);
            //dbContext.Update(tourLog.Tour);
            dbContext.TourLogs.Remove(tourLog);

            //int deltaRows = dbContext.TourLogs.Where(tl => tl.Id == tourLog.Id).ExecuteDelete(); // does not keep tracked instances in sync
            dbContext.SaveChanges();
        }

        private readonly TourPlannerDbContext dbContext;
    }
}
