using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.DataTransferObjects.Models;
using TourPlannerBackEnd.Infrastructure.Services;
using TourPlannerBackEnd.Repositories;
using TourPlannerTests.Data;
using TourPlannerTests.Mockups;

namespace TourPlannerTests
{
    internal class AutoPropertyCalculatorTests
    {
        [SetUp]
        public void Setup()
        {
            CalorieCalculationService caloriesCalculator = new();
            ITourRepository tourRepo = new MockTourRepository();
            propertyCalculator = new TourAutoPropertyService(tourRepo, caloriesCalculator);
        }

        [Test]
        public void TourIdIsInvalid()
        {
            Tour tour = TourData.Tours[0];
            tour.Id = 0;


            Assert.Throws<Exception>(() =>
            {
                propertyCalculator.RecalculateTourProperties(tour.Id);
            });
        }

        //[Test]
        //public void TourPopularityPass()
        //{
        //    Tour tour = TourData.Tours[0];
        //    tour.Id = 0;


        //    propertyCalculator.RecalculateTourProperties()
        //}

        //[Test]
        //public void TourIdIsInvalid()
        //{
        //    Tour tour = TourData.Tours[0];
        //    tour.Id = 0;


        //    Assert.Throws<Exception>(() =>
        //    {
        //        propertyCalculator.RecalculateTourProperties(tour.Id);
        //    });
        //}
        private TourAutoPropertyService propertyCalculator;
    }
}
