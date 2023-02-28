

namespace TourPlanner.EntityFramework.DataAccess
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using TourPlanner.DataTransferObjects.Models;

    public class TourPlannerDbContext : DbContext
    {
        public DbSet<Tour> Tours { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder
                .UseNpgsql("Host=localhost;Database=TourPlanner;Username=postgres;Password=test")
                .UseSnakeCaseNamingConvention();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
