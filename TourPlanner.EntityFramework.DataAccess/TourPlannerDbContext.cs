﻿

namespace TourPlanner.EntityFramework.DataAccess
{
    using Microsoft.EntityFrameworkCore;
    using TourPlanner.DataTransferObjects.Models;

    public class TourPlannerDbContext : DbContext
    {
        public DbSet<Location> Locations { get; set; }
        public DbSet<Route> Routes { get; set; }
        public DbSet<Tour> Tours { get; set; }
        public DbSet<TourLog> TourLogs { get; set; }

        public TourPlannerDbContext(ApplicationConfiguration config)
        {
            this.config = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder
                .UseNpgsql(config.DataBaseConnectionString)
                .UseSnakeCaseNamingConvention();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Location>()
                .Property(l => l.Id)
                .UseIdentityAlwaysColumn();

            modelBuilder.Entity<Route>()
                .Property(l => l.Id)
                .UseIdentityAlwaysColumn();

            modelBuilder.Entity<TourLog>()
                .Property(tl => tl.Id)
                .UseIdentityAlwaysColumn();

            modelBuilder.Entity<TourLog>()
                .HasOne<Tour>(tl => tl.Tour)
                .WithMany(t => t.TourLogs)
                .HasForeignKey(tl => tl.TourId);

            modelBuilder.Entity<Tour>()
                .Property(t => t.Id)
                .UseIdentityAlwaysColumn();

            modelBuilder.Entity<Tour>()
                .HasOne(t => t.Start)
                .WithOne();

            modelBuilder.Entity<Tour>()
                .HasOne(t => t.Destination)
                .WithOne();

            modelBuilder.Entity<Tour>()
                .HasOne(t => t.Route)
                .WithOne();
        }

        private readonly ApplicationConfiguration config;
    }
}
