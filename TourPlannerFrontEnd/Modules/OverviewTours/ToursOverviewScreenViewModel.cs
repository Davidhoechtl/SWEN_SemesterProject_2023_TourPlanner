
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
    using TourPlannerBackEnd.Infrastructure.TourExport;
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

        public ToursOverviewScreenViewModel(TourRepository tourRepository, IExportService exportService)
        {
            this.tourRepository = tourRepository;
            this.exportService = exportService;

            this.SearchBar = new SearchBarViewModel(tourRepository);
            this.SearchBar.Model = new TourSearchResult();
            this.SearchBar.OnSearch = OnSearch;

            DisplayName = "Tour Overview";
        }

        public async Task CreateTour()
        {
            await NavigationHost.NavigateToScreen<CreateTourScreenViewModel>(new System.Threading.CancellationToken());
        }

        public void ExportTours()
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.InitialDirectory = "C:\\";
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                exportService.Export(Tours.Select(t => t.Model), dialog.FileName);
            }
            else
            {
                MessageBox.Show("Export was cancelled");
            }
        }

        public async Task ImportTours()
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.InitialDirectory = "C:\\";
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
        private readonly IExportService exportService;
    }
}
