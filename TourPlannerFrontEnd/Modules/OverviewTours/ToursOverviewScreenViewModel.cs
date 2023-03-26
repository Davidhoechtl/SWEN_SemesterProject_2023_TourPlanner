
namespace TourPlannerFrontEnd.Modules.OverviewTours
{
    using Microsoft.WindowsAPICodePack.Dialogs;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows;
    using TourPlanner.DataTransferObjects.Models;
    using TourPlannerBackEnd.Infrastructure;
    using TourPlannerBackEnd.Infrastructure.Reporting;
    using TourPlannerBackEnd.Infrastructure.TourExport;
    using TourPlannerBackEnd.Infrastructure.TourImport;
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
                NotifyOfPropertyChange(nameof(TourDetailSpecificActionsVisible));
            }
        }

        public SearchBarViewModel SearchBar { get; set; }

        public bool TourDetailSpecificActionsVisible => SelectedTour != null;

        public ToursOverviewScreenViewModel(
            TourRepository tourRepository,
            TourPlannerMapQuestService mapQuestService,
            IExportService exportService,
            IImportService importService,
            IFastReportGenerationService reportGenerationService)
        {
            this.tourRepository = tourRepository;
            this.mapQuestService = mapQuestService;
            this.exportService = exportService;
            this.importService = importService;
            this.reportGenerationService = reportGenerationService;

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
                MessageBox.Show("Export was successful");
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
                List<Tour> tours = importService.Import(dialog.FileName);
                await Task.Run(async () =>
                {
                    foreach (Tour tour in tours)
                    {
                        byte[] routeImage = await mapQuestService.GetRouteImage(
                            tour.Start.GetSingleLineString(),
                            tour.Destination.GetSingleLineString(),
                            width: 600,
                            height: 400
                        );

                        tour.Route.MapImage = routeImage;

                        tourRepository.InsertTour(tour);
                    }
                });

                MessageBox.Show("Import was successful");
                Refresh();
            }
        }

        public async Task GenerateTourReport()
        {
            await Task.Run(() =>
            {
                if (SelectedTour != null)
                {
                    reportGenerationService.GenerateTourReport(SelectedTour.Model);
                }
            });
        }

        public async Task GenerateTourSummarizeReport()
        {
            await Task.Run(() =>
            {
                if (SelectedTour != null)
                {
                    MessageBox.Show("Not implemented yet.");
                    //reportGenerationService.GenerateSummarizeReport(SelectedTour.Model);
                }
            });
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
        private readonly TourPlannerMapQuestService mapQuestService;
        private readonly IExportService exportService;
        private readonly IImportService importService;
        private readonly IFastReportGenerationService reportGenerationService;
    }
}
