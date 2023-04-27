using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TourPlannerFrontEnd.Modules.Search
{
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using TourPlanner.DataTransferObjects.Models;
    using TourPlannerBackEnd.Repositories;
    using TourPlannerFrontEnd.Infrastructure;

    internal class SearchBarViewModel : ViewModel<TourSearchResult>
    {
        public string SearchText { get; set; }

        public Action<TourSearchResult> OnSearch { get; set; }

        public SearchBarViewModel(ITourRepository tourRepository)
        {
            this.tourRepository = tourRepository;
        }

        public async Task Search()
        {
            if (!string.IsNullOrEmpty(SearchText))
            {
                await Task.Run(() =>
                {
                    this.Model.FoundTours = tourRepository.GetToursBySearchText( Match, SearchText, GetSearchParameters(SearchText));
                });
            }

            OnSearch?.Invoke(this.Model);
        }

        private bool Match(Tour tour, string searchText, Dictionary<string, string> searchParameter)
        {
            if (tour.Name.Equals(searchText, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            if(searchParameter.TryGetValue("popularity", out string popularityValue))
            {
                if(int.TryParse(popularityValue, out int convertedPopularityValue) &&
                   tour.Popularity == convertedPopularityValue )
                {
                    return true;
                }
            }
            if (searchParameter.TryGetValue("childfriendliness", out string childfriendlinessValue))
            {
                if (int.TryParse(popularityValue, out int convertedChildfriendlinessValue) &&
                   tour.ChildFriendliness == convertedChildfriendlinessValue)
                {
                    return true;
                }
            }
            if (searchParameter.TryGetValue("calories", out string caloriesValue))
            {
                if (int.TryParse(popularityValue, out int convertedCaloriesValue) &&
                   tour.CaloriesCount == convertedCaloriesValue)
                {
                    return true;
                }
            }

            foreach (TourLog tourlog in tour.TourLogs)
            {
                if (tourlog.Comment.Equals(searchText, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        private Dictionary<string, string> GetSearchParameters(string searchText)
        {
            Regex expression = new Regex("(?<=\\<)(.*?)(?=\\>)");
            MatchCollection matches = expression.Matches(searchText);

            Dictionary<string, string> found = new();
            foreach (Match match in matches)
            {
                string searchParameter = match.Value;
                if (searchParameter.Contains(':'))
                {
                    string[] data = searchParameter.Split(':');
                    found.TryAdd(data[0].ToLower(), data[1].ToLower());
                }
            }

            return found;
        }

        private readonly ITourRepository tourRepository;
    }
}
