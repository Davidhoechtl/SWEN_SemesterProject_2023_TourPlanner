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

        private readonly TourPlannerDbContext dbContext;
    }
}
