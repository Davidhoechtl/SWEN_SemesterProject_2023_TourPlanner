
namespace TourPlannerFrontEnd.Modules.CreateTour
{
    using Caliburn.Micro;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Navigation;
    using TourPlanner.DataTransferObjects.Models;
    using TourPlannerBackEnd.Infrastructure;
    using TourPlannerBackEnd.Repositories;
    using TourPlannerFrontEnd.Infrastructure;
    using TourPlannerFrontEnd.Infrastructure.ViewContainers;
    using TourPlannerFrontEnd.Modules.OverviewTours;

    class CreateTourScreenViewModel : NavigationScreen, IBusyIndicatorContainer
    {
        public CreateTourViewModel CreateTourViewModel { get; init; }

        public INavigationHost NavigationHost { get; set; }

        public bool IsBusy 
        {
            get => isBusy;
            set
            {
                isBusy = value;
                NotifyOfPropertyChange(nameof(IsBusy));
            }
        }

        public string BusyText 
        {
            get => busyText; 
            set
            {
                busyText = value;
                NotifyOfPropertyChange(nameof(BusyText));
            }
        }

        public CreateTourScreenViewModel(
            ITourRepository tourRepository,
            TourPlannerMapQuestService mapQuestService)
        {
            CreateTourViewModel = new CreateTourViewModel(tourRepository, mapQuestService, this);
            DisplayName = "Create Tour";
        }


        public async Task Back()
        {
            await NavigationHost.NavigateToScreen<ToursOverviewScreenViewModel>(new CancellationToken());
        }

        public override Task OnPageNavigatedTo(CancellationToken cancellationToken)
        {
            // noop
            CreateTourViewModel.Model = new Tour();
            return Task.CompletedTask;
        }

        public void SetBusy(string msg)
        {
            IsBusy = true;
            BusyText = msg;
        }

        public void SetNotBusy()
        {
            IsBusy = false;
            busyText = string.Empty;
        }

        private bool isBusy;
        private string busyText;
    }
}
