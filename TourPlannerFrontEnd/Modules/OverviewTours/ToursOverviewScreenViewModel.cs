
namespace TourPlannerFrontEnd.Modules.OverviewTours
{
    using Caliburn.Micro;
    using Microsoft.WindowsAPICodePack.Dialogs;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows;
    using TourPlanner.DataTransferObjects.Models;
    using TourPlannerBackEnd.Infrastructure;
    using TourPlannerBackEnd.Infrastructure.Reporting;
    using TourPlannerBackEnd.Infrastructure.Services;
    using TourPlannerBackEnd.Infrastructure.TourExport;
    using TourPlannerBackEnd.Infrastructure.TourImport;
    using TourPlannerBackEnd.Repositories;
    using TourPlannerFrontEnd.Infrastructure;
    using TourPlannerFrontEnd.Infrastructure.Extensions;
    using TourPlannerFrontEnd.Infrastructure.Helper;
    using TourPlannerFrontEnd.Infrastructure.Messages;
    using TourPlannerFrontEnd.Modules.CreateTour;
    using TourPlannerFrontEnd.Modules.CreateTourLog;
    using TourPlannerFrontEnd.Modules.Search;

    internal class ToursOverviewScreenViewModel : NavigationScreen, IHandle<RefreshToursMessage>
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
            TourLogRepository tourLogRepository,
            TourPlannerMapQuestService mapQuestService,
            TourAutoPropertyService tourAutoPropertyService,
            IExportService exportService,
            IImportService importService,
            IFastReportGenerationService reportGenerationService,
            IEventAggregator eventAggregator)
        {
            this.tourRepository = tourRepository;
            this.tourLogRepository = tourLogRepository;
            this.mapQuestService = mapQuestService;
            this.tourAutoPropertyService = tourAutoPropertyService;
            this.exportService = exportService;
            this.importService = importService;
            this.reportGenerationService = reportGenerationService;
            this.eventAggregator = eventAggregator;

            this.SearchBar = new SearchBarViewModel(tourRepository);
            this.SearchBar.Model = new TourSearchResult();
            this.SearchBar.OnSearch = OnSearch;

            eventAggregator.SubscribeOnUIThread(this);

            DisplayName = "Tour Overview";
        }

        public override async Task OnPageNavigatedTo(CancellationToken cancellationToken)
        {
            this.SearchBar = new SearchBarViewModel(tourRepository);
            this.SearchBar.Model = new TourSearchResult();
            this.SearchBar.OnSearch = OnSearch;
            NotifyOfPropertyChange(nameof(SearchBar));

            await ReloadTours(cancellationToken);
        }
        public async Task HandleAsync(RefreshToursMessage message, CancellationToken cancellationToken)
        {
            await ReloadTours(cancellationToken);
        }

        public async Task CreateTour()
        {
            await NavigationHost.NavigateToScreen<CreateTourScreenViewModel>(new System.Threading.CancellationToken());
        }

        public async Task UpdateTour()
        {
            if (SelectedTour.Model != null)
            {
                await ShowAsyncOperation.Run(() =>
                    {
                        tourRepository.UpdateTour(SelectedTour.Model);
                    },
                    Log
                );

                await ReloadTours(new CancellationToken());
            }
        }

        public async Task DeleteTour()
        {
            if (SelectedTour.Model != null)
            {
                await ShowAsyncOperation.Run(() =>
                    {
                        tourRepository.DeleteTour(SelectedTour.Model.Id);
                    },
                    Log
                );
                await ReloadTours(new CancellationToken());
            }
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
                await ShowAsyncOperation.RunAndShowMessage( () =>
                    {
                        foreach (Tour tour in tours)
                        {
                            byte[] routeImage = mapQuestService.GetRouteImage(
                                tour.Start.GetSingleLineString(),
                                tour.Destination.GetSingleLineString(),
                                width: 600,
                                height: 400
                            ).Result;

                            tour.Route.MapImage = routeImage;

                            tourRepository.InsertTour(tour);
                        }
                    },
                    successMsg: "Import was successful.",
                    errorMsg: "Import failed.",
                    Log
                );

                Refresh();
            }
        }

        public async Task GenerateTourReport()
        {
            await ShowAsyncOperation.Run(() =>
                {
                    if (SelectedTour != null)
                    {
                        reportGenerationService.GenerateTourReport(SelectedTour.Model);
                    }
                },
                Log
            );
        }

        public async Task GenerateTourSummarizeReport()
        {
            await ShowAsyncOperation.Run(() =>
                {
                    if (SelectedTour != null)
                    {
                        reportGenerationService.GenerateSummarizeReport(Tours.Select(vw => vw.Model));
                    }
                },
                Log
            );
        }

        private async Task<IEnumerable<Tour>> GetToursAsync(CancellationToken cancellationToken)
        {
            List<Tour> tours = new();
            bool success =  await ShowAsyncOperation.Run(() =>
                {
                    tours = tourRepository.GetAllTours();
                },
                Log
            );

            if (success)
            {
                return tours;
            }
            else
            {
                MessageBox.Show("Failed to load tours");
                return new List<Tour>();
            }
        }

        private void OnSearch(TourSearchResult searchResult)
        {
            ConvertToursToViewModelsAndSelectFirst(searchResult.FoundTours);
        }


        private async Task ReloadTours(CancellationToken cancellationToken)
        {
            IEnumerable<Tour> allTours = await GetToursAsync(cancellationToken);
            ConvertToursToViewModelsAndSelectFirst(allTours);

        }

        private void ConvertToursToViewModelsAndSelectFirst(IEnumerable<Tour> tours)
        {
            Tours = tours.SelectViewModels<Tour, TourDetailViewModel>(viewModel =>
            {
                viewModel.Setup(tourLogRepository, NavigationHost, eventAggregator, tourAutoPropertyService);
            }).ToList();

            SelectedTour = Tours.FirstOrDefault();

            NotifyOfPropertyChange(nameof(Tours));
            NotifyOfPropertyChange(nameof(SelectedTour));
        }

        private TourDetailViewModel selectedTour;
        private readonly TourRepository tourRepository;
        private readonly TourLogRepository tourLogRepository;
        private readonly TourPlannerMapQuestService mapQuestService;
        private readonly TourAutoPropertyService tourAutoPropertyService;
        private readonly IExportService exportService;
        private readonly IImportService importService;
        private readonly IFastReportGenerationService reportGenerationService;
        private readonly IEventAggregator eventAggregator;
        private static readonly NLog.ILogger Log = NLog.LogManager.GetCurrentClassLogger();
    }
}
