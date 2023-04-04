

namespace TourPlannerFrontEnd.Modules.CreateTourLog
{
    using System.Threading;
    using System.Threading.Tasks;
    using TourPlanner.DataTransferObjects.Models;
    using TourPlannerBackEnd.Infrastructure.Services;
    using TourPlannerBackEnd.Repositories;
    using TourPlannerFrontEnd.Infrastructure;
    using TourPlannerFrontEnd.Modules.OverviewTours;
    using TourPlannerFrontEnd.Modules.RateTour;

    internal class CreateTourLogScreenViewModel : NavigationScreen
    {
        public CreateTourLogViewModel TourLogViewModel { get; set; }

        public INavigationHost NavigationHost { get; set; }

        public CreateTourLogScreenViewModel(TourLogRepository tourLogRepository, TourAutoPropertyService tourAutoPropertyService)
        {
            TourLogViewModel = new CreateTourLogViewModel(tourLogRepository, tourAutoPropertyService);
            DisplayName = "Create Tour Log";
        }

        public async Task Back()
        {
            await NavigationHost.NavigateToScreen<ToursOverviewScreenViewModel>(new CancellationToken());
        }

        public override Task OnPageNavigatedTo(CancellationToken cancellationToken, object dataContext)
        {
            if(dataContext is Tour tour)
            {
                TourLogViewModel.Model = new TourLog()
                {
                    TourId = tour.Id
                };
                return Task.CompletedTask;
            }
            else
            {
                throw new System.Exception($"The datacontext for {nameof(CreateTourLogScreenViewModel)} is not a Tour");
            }
        }

        // ToDo base method should be virtual
        public override Task OnPageNavigatedTo(CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
