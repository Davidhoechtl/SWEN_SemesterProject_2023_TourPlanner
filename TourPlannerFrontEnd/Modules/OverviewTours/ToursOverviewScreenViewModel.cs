
namespace TourPlannerFrontEnd.Modules.OverviewTours
{
    using Caliburn.Micro;
    using Microsoft.Win32;
    using Microsoft.WindowsAPICodePack.Dialogs;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows;
    using TourPlanner.DataTransferObjects.Models;
    using TourPlannerBackEnd.Repositories;
    using TourPlannerFrontEnd.Infrastructure;
    using TourPlannerFrontEnd.Infrastructure.Extensions;
    using TourPlannerFrontEnd.Modules.CreateTour;
    using TourPlannerFrontEnd.Modules.CreateTourLog;
    using TourPlannerFrontEnd.Modules.Search;

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

        public SearchBarViewModel SearchBar { get; set; }

        public bool IsCreateTourLogVisible => SelectedTour != null;

        public ToursOverviewScreenViewModel(TourRepository tourRepository)
        {
            this.tourRepository = tourRepository;
            this.SearchBar = new SearchBarViewModel(tourRepository);
            this.SearchBar.Model = new TourSearchResult();
            this.SearchBar.OnSearch = OnSearch;

            DisplayName = "Tour Overview";
        }

        public async Task CreateTour()
        {
            await NavigationHost.NavigateToScreen<CreateTourScreenViewModel>(new System.Threading.CancellationToken());
        }

        public async Task ExportTours()
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.InitialDirectory = "C:\\Users";
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                MessageBox.Show("You selected: " + dialog.FileName);
            }
        }

        public async Task ImportTours()
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.InitialDirectory = "C:\\Users";
            dialog.IsFolderPicker = false;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                MessageBox.Show("You selected: " + dialog.FileName);
            }
        }

        public async Task CreateTourLog()
        {
            await NavigationHost.NavigateToScreen<CreateTourLogScreenViewModel>(new System.Threading.CancellationToken(), SelectedTour.Model);
        }

        private async Task<IEnumerable<Tour>> GetToursAsync(CancellationToken cancellationToken)
        {
            return await Task.Run(() =>
            {
                return tourRepository.GetAllTours();
            }, cancellationToken);
        }

        private void OnSearch(TourSearchResult searchResult)
        {
            Tours = searchResult.FoundTours.SelectViewModels<Tour, TourDetailViewModel>().ToList();
            SelectedTour = Tours.FirstOrDefault();
            NotifyOfPropertyChange(nameof(Tours));
            NotifyOfPropertyChange(nameof(SelectedTour));
        }

        public override async Task OnPageNavigatedTo(CancellationToken cancellationToken)
        {
            this.SearchBar = new SearchBarViewModel(tourRepository);
            this.SearchBar.Model = new TourSearchResult();
            this.SearchBar.OnSearch = OnSearch;
            NotifyOfPropertyChange(nameof(SearchBar));

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
