
namespace TourPlannerFrontEnd.Modules.OverviewTours
{
    using Caliburn.Micro;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using TourPlanner.DataTransferObjects.Models;
    using TourPlannerBackEnd.Repositories;
    using TourPlannerFrontEnd.Infrastructure;
    using TourPlannerFrontEnd.Infrastructure.Extensions;
    using TourPlannerFrontEnd.Modules.CreateTour;
    using TourPlannerFrontEnd.Modules.CreateTourLog;

    internal class ToursOverviewScreenViewModel : NavigationScreen
    {
        public INavigationHost NavigationHost { get; set; }

        public List<TourDetailViewModel> Tours { get; private set; }
        public TourDetailViewModel SelectedTour
        {
            get => selectedTour;
            set
            {
                selectedTour = value;
                NotifyOfPropertyChange(nameof(SelectedTour));
                NotifyOfPropertyChange(nameof(IsCreateTourLogVisible));
            }
        }

        public bool IsCreateTourLogVisible => SelectedTour != null;

        public ToursOverviewScreenViewModel(TourRepository tourRepository)
        {
            this.tourRepository = tourRepository;
            DisplayName = "Tour Overview";
        }

        public async Task CreateTour()
        {
            await NavigationHost.NavigateToScreen<CreateTourScreenViewModel>(new System.Threading.CancellationToken());
        }
        public async Task CreateTourLog()
        {
            await NavigationHost.NavigateToScreen<CreateTourLogScreenViewModel>(new System.Threading.CancellationToken());
        }

        private async Task<IEnumerable<Tour>> GetToursAsync(CancellationToken cancellationToken)
        {
            return await Task.Run(() =>
            {
                return tourRepository.GetAllTours();
            }, cancellationToken);
        }

        public override async Task OnPageNavigatedTo(CancellationToken cancellationToken)
        {
            IEnumerable<Tour> allTours = await GetToursAsync(cancellationToken);
            Tours = allTours.SelectViewModels<Tour, TourDetailViewModel>().ToList();
            SelectedTour = Tours.FirstOrDefault();

            NotifyOfPropertyChange(nameof(Tours));
            NotifyOfPropertyChange(nameof(SelectedTour));
        }

        private TourDetailViewModel selectedTour;
        private readonly TourRepository tourRepository;
    }
}
