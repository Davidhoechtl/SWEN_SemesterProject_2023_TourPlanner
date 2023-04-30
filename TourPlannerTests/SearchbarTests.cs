using System;
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

            List<Tour> found = searchBarVw.SearchTours(searchText);

            Assert.That(found.Count == 0);
        }

        [Test]
        public void SearchWithOneResult()
        {
            string searchText = "Foo";

            List<Tour> found = searchBarVw.SearchTours(searchText);

            Assert.That(found.Count == 1);
        }

        [Test]
        public void SearchWithOneResult_LowerCasing()
        {
            string searchText = "foo";

            List<Tour> found = searchBarVw.SearchTours(searchText);

            Assert.That(found.Count == 1);
        }

        [Test]
        public void SearchWithOneResult_TourLog()
        {
            string searchText = "Test";

            List<Tour> found = searchBarVw.SearchTours(searchText);

            Assert.That(found.Count == 1);
        }

        [Test]
        public void SearchWithOneResult_TourLog_LowerCasing()
        {
            string searchText = "test";

            List<Tour> found = searchBarVw.SearchTours(searchText);

            Assert.That(found.Count == 1);
        }

        [Test]
        public void SearchWithOneResult_PopularityAttribute()
        {
            string searchText = "<popularity:1>";

            List<Tour> found = searchBarVw.SearchTours(searchText);

            Assert.That(found.Count == 1);
        }

        [Test]
        public void SearchWithOneResult_ChildfriendlinessAttribute()
        {
            string searchText = "<childfriendliness:1>";

            List<Tour> found = searchBarVw.SearchTours(searchText);

            Assert.That(found.Count == 1);
        }


        [Test]
        public void SearchWithOneResult_CaloriesAttribute()
        {
            string searchText = "<calories:100>";

            List<Tour> found = searchBarVw.SearchTours(searchText);

            Assert.That(found.Count == 1);
        }


        private SearchBarViewModel searchBarVw;
    }
}
