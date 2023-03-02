
namespace TourPlannerFrontEnd.Modules.CreateTour
{
    using Caliburn.Micro;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Navigation;
    using TourPlanner.DataTransferObjects.Models;
    using TourPlannerBackEnd.Repositories;
    using TourPlannerFrontEnd.Infrastructure;

    class CreateTourScreenViewModel : NavigationScreen
    {
        public CreateTourViewModel CreateTourViewModel { get; init; }

        public INavigationHost NavigationHost { get; set; }

        public CreateTourScreenViewModel(
            TourRepository tourRepository)
        {
            CreateTourViewModel = new CreateTourViewModel(tourRepository);
            CreateTourViewModel.Model = new Tour();

            DisplayName = "Create Tour";
        }


        public async Task Back()
        {
            await NavigationHost.NavigateBackOneStep(new System.Threading.CancellationToken());
        }

        public override Task OnPageNavigatedTo(CancellationToken cancellationToken)
        {
            // noop
            return Task.CompletedTask;
        }
    }
}
