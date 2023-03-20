

namespace TourPlannerFrontEnd.Modules.CreateTourLog
{
    using System.Threading;
    using System.Threading.Tasks;
    using TourPlanner.DataTransferObjects.Models;
    using TourPlannerFrontEnd.Infrastructure;
    using TourPlannerFrontEnd.Modules.OverviewTours;
    using TourPlannerFrontEnd.Modules.RateTour;

    internal class CreateTourLogScreenViewModel : NavigationScreen
    {
        public CreateTourLogViewModel TourLogViewModel { get; set; }

        public INavigationHost NavigationHost { get; set; }

        public CreateTourLogScreenViewModel()
        {
            TourLogViewModel = new CreateTourLogViewModel();
            DisplayName = "Create Tour Log";
        }

        public async Task Back()
        {
            await NavigationHost.NavigateToScreen<ToursOverviewScreenViewModel>(new CancellationToken());
        }

        public override Task OnPageNavigatedTo(CancellationToken cancellationToken)
        {
            TourLogViewModel.Model = new TourLog();
            return Task.CompletedTask;
        }
    }
}
