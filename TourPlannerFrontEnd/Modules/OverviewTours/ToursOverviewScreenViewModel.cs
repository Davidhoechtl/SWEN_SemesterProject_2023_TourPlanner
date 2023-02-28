
namespace TourPlannerFrontEnd.Modules.OverviewTours
{
    using Caliburn.Micro;
    using MTCG.DAL;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using TourPlanner.DataTransferObjects.Models;
    using TourPlannerBackEnd.Repositories;
    using TourPlannerFrontEnd.Infrastructure;
    using TourPlannerFrontEnd.Modules.CreateTour;

    internal class ToursOverviewScreenViewModel : Screen
    {
        public List<Tour> Tours { get; private set; }

        public INavigationHost NavigationHost { get; set; }

        public ToursOverviewScreenViewModel(IQueryDatabase queryDatabase, TourRepository tourRepository)
        {
            this.queryDatabase = queryDatabase;
            this.tourRepository = tourRepository;
            DisplayName = "Tour Overview";
        }

        public async Task CreateTour()
        {
            await NavigationHost.NavigateToScreen<CreateTourScreenViewModel>(new System.Threading.CancellationToken());
        }

        protected override async Task OnActivateAsync(CancellationToken cancellationToken)
        {
            Tours = await GetToursAsync(cancellationToken);
            NotifyOfPropertyChange(nameof(Tours));
            await base.OnActivateAsync(cancellationToken);
        }

        private async Task<List<Tour>> GetToursAsync(CancellationToken cancellationToken)
        {
            return await Task.Run(() =>
            {
                return tourRepository.GetAllTours(queryDatabase); ;
            }, cancellationToken);
        }

        private readonly IQueryDatabase queryDatabase;
        private readonly TourRepository tourRepository;
    }
}
