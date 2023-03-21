using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TourPlannerFrontEnd.Modules.Search
{
    using System.Threading.Tasks;
    using TourPlanner.DataTransferObjects.Models;
    using TourPlannerBackEnd.Repositories;
    using TourPlannerFrontEnd.Infrastructure;

    internal class SearchBarViewModel : ViewModel<TourSearchResult>
    {
        public string SearchText { get; set; }

        public Action<TourSearchResult> OnSearch { get; set; }

        public SearchBarViewModel(TourRepository tourRepository)
        {
            this.tourRepository = tourRepository;
        }

        public async Task Search()
        {
            if(!string.IsNullOrEmpty(SearchText))
            {
                await Task.Run(() =>
                {
                    this.Model.FoundTours = tourRepository.GetToursBySearchText(SearchText);
                });
            }

            OnSearch?.Invoke(this.Model);
        }

        private readonly TourRepository tourRepository;
    }
}
