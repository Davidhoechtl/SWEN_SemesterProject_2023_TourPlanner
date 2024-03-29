﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.DataTransferObjects.Models;
using TourPlannerBackEnd.Infrastructure.Services;
using TourPlannerBackEnd.Repositories;
using TourPlannerFrontEnd.Modules.Search;
using TourPlannerTests.Mockups;

namespace TourPlannerTests
{
    internal class SearchbarTests
    {
        [SetUp]
        public void Setup()
        {
            ITourRepository tourRepo = new MockTourRepository();
            searchBarVw = new SearchBarViewModel(tourRepo);    
        }

        [Test]
        public void SearchWithNoResult_TourName()
        {
            string searchText = "None";

            searchBarVw.HasCustomText = true;
            List<Tour> found = searchBarVw.SearchTours(searchText);

            Assert.That(found.Count == 0);
        }

        [Test]
        public void SearchWithOneResult()
        {
            string searchText = "Foo";

            searchBarVw.HasCustomText = true;
            List<Tour> found = searchBarVw.SearchTours(searchText);

            Assert.That(found.Count == 1);
        }

        [Test]
        public void SearchWithOneResult_LowerCasing()
        {
            string searchText = "foo";

            searchBarVw.HasCustomText = true;
            List<Tour> found = searchBarVw.SearchTours(searchText);

            Assert.That(found.Count == 1);
        }

        [Test]
        public void SearchWithOneResult_TourLog()
        {
            string searchText = "Test";

            searchBarVw.HasCustomText = true;
            List<Tour> found = searchBarVw.SearchTours(searchText);

            Assert.That(found.Count == 1);
        }

        [Test]
        public void SearchWithOneResult_TourLog_LowerCasing()
        {
            string searchText = "test";


            searchBarVw.HasCustomText = true;
            List<Tour> found = searchBarVw.SearchTours(searchText);

            Assert.That(found.Count == 1);
        }

        [Test]
        public void SearchWithOneResult_PopularityAttribute()
        {
            string searchText = "<popularity:1>";

            searchBarVw.HasCustomText = true;
            List<Tour> found = searchBarVw.SearchTours(searchText);

            Assert.That(found.Count == 1);
        }

        [Test]
        public void SearchWithOneResult_ChildfriendlinessAttribute()
        {
            string searchText = "<childfriendliness:1>";

            searchBarVw.HasCustomText = true;
            List<Tour> found = searchBarVw.SearchTours(searchText);

            Assert.That(found.Count == 1);
        }


        [Test]
        public void SearchWithOneResult_CaloriesAttribute()
        {
            string searchText = "<calories:100>";

            searchBarVw.HasCustomText = true;
            List<Tour> found = searchBarVw.SearchTours(searchText);

            Assert.That(found.Count == 1);
        }

        [Test]
        public void Search_WithEmptyText()
        {
            string searchText = "";

            searchBarVw.HasCustomText = false;
            List<Tour> found = searchBarVw.SearchTours(searchText);

            Assert.That(found.Count == 2);
        }

        private SearchBarViewModel searchBarVw;
    }
}
