﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TourPlanner.EntityFramework.DataAccess;

#nullable disable

namespace TourPlanner.DataAccess.EntityFramework.Migrations
{
    [DbContext(typeof(TourPlannerDbContext))]
    [Migration("20230420082509_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("TourPlanner.DataTransferObjects.Models.Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<int>("Id"));

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("city");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("country");

                    b.Property<int>("PostCode")
                        .HasColumnType("integer")
                        .HasColumnName("post_code");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("state");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("street");

                    b.HasKey("Id")
                        .HasName("pk_locations");

                    b.ToTable("locations", (string)null);
                });

            modelBuilder.Entity("TourPlanner.DataTransferObjects.Models.Route", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<int>("Id"));

                    b.Property<double>("DistanceInKm")
                        .HasColumnType("double precision")
                        .HasColumnName("distance_in_km");

                    b.Property<double>("EstimatedTimeInSeconds")
                        .HasColumnType("double precision")
                        .HasColumnName("estimated_time_in_seconds");

                    b.Property<byte[]>("MapImage")
                        .HasColumnType("bytea")
                        .HasColumnName("map_image");

                    b.Property<string>("TravellingType")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("travelling_type");

                    b.HasKey("Id")
                        .HasName("pk_routes");

                    b.ToTable("routes", (string)null);
                });

            modelBuilder.Entity("TourPlanner.DataTransferObjects.Models.Tour", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<int>("Id"));

                    b.Property<double?>("CaloriesCount")
                        .HasColumnType("double precision")
                        .HasColumnName("calories_count");

                    b.Property<int>("ChildFriendliness")
                        .HasColumnType("integer")
                        .HasColumnName("child_friendliness");

                    b.Property<int>("LocationDestinationId")
                        .HasColumnType("integer")
                        .HasColumnName("location_destination_id");

                    b.Property<int>("LocationStartId")
                        .HasColumnType("integer")
                        .HasColumnName("location_start_id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<int>("Popularity")
                        .HasColumnType("integer")
                        .HasColumnName("popularity");

                    b.Property<int>("RouteId")
                        .HasColumnType("integer")
                        .HasColumnName("route_id");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("start_date");

                    b.Property<string>("TravellingType")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("travelling_type");

                    b.HasKey("Id")
                        .HasName("pk_tours");

                    b.HasIndex("LocationDestinationId")
                        .IsUnique()
                        .HasDatabaseName("ix_tours_location_destination_id");

                    b.HasIndex("LocationStartId")
                        .IsUnique()
                        .HasDatabaseName("ix_tours_location_start_id");

                    b.HasIndex("RouteId")
                        .IsUnique()
                        .HasDatabaseName("ix_tours_route_id");

                    b.ToTable("tours", (string)null);
                });

            modelBuilder.Entity("TourPlanner.DataTransferObjects.Models.TourLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<int>("Id"));

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("comment");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("date");

                    b.Property<int>("Difficulty")
                        .HasColumnType("integer")
                        .HasColumnName("difficulty");

                    b.Property<int>("Rating")
                        .HasColumnType("integer")
                        .HasColumnName("rating");

                    b.Property<double>("TakenTimeInSeconds")
                        .HasColumnType("double precision")
                        .HasColumnName("taken_time_in_seconds");

                    b.Property<int>("TourId")
                        .HasColumnType("integer")
                        .HasColumnName("tour_id");

                    b.HasKey("Id")
                        .HasName("pk_tour_logs");

                    b.HasIndex("TourId")
                        .HasDatabaseName("ix_tour_logs_tour_id");

                    b.ToTable("tourLogs");
                });

            modelBuilder.Entity("TourPlanner.DataTransferObjects.Models.Tour", b =>
                {
                    b.HasOne("TourPlanner.DataTransferObjects.Models.Location", "Destination")
                        .WithOne()
                        .HasForeignKey("TourPlanner.DataTransferObjects.Models.Tour", "LocationDestinationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_tours_locations_location_destination_id");

                    b.HasOne("TourPlanner.DataTransferObjects.Models.Location", "Start")
                        .WithOne()
                        .HasForeignKey("TourPlanner.DataTransferObjects.Models.Tour", "LocationStartId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_tours_locations_location_start_id");

                    b.HasOne("TourPlanner.DataTransferObjects.Models.Route", "Route")
                        .WithOne()
                        .HasForeignKey("TourPlanner.DataTransferObjects.Models.Tour", "RouteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_tours_routes_route_id");

                    b.Navigation("Destination");

                    b.Navigation("Route");

                    b.Navigation("Start");
                });

            modelBuilder.Entity("TourPlanner.DataTransferObjects.Models.TourLog", b =>
                {
                    b.HasOne("TourPlanner.DataTransferObjects.Models.Tour", "Tour")
                        .WithMany("TourLogs")
                        .HasForeignKey("TourId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_tour_logs_tours_tour_id");

                    b.Navigation("Tour");
                });

            modelBuilder.Entity("TourPlanner.DataTransferObjects.Models.Tour", b =>
                {
                    b.Navigation("TourLogs");
                });
#pragma warning restore 612, 618
        }
    }
}
