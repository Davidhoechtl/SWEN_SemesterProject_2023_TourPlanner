using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TourPlanner.DataTransferObjects.Models;
using TourPlannerBackEnd.Infrastructure.Services;
using TourPlannerBackEnd.Repositories;
using TourPlannerFrontEnd.Infrastructure;
using TourPlannerFrontEnd.Infrastructure.Extensions;
using TourPlannerFrontEnd.Infrastructure.Helper;
using TourPlannerFrontEnd.Infrastructure.Messages;
using TourPlannerFrontEnd.Modules.CreateTourLog;

namespace TourPlannerFrontEnd.Modules.TourLogs
{
    internal class TourLogsViewModel : ViewModel<Tour>
    {
        public TourLogValidationViewModel SelectedTourLog { get; set; }
        public List<TourLogValidationViewModel> TourLogs
        {
            get => tourLogs;
            set
            {
                tourLogs = value;
                NotifyOfPropertyChange(nameof(TourLogs));
            }
        }

        public TourLogsViewModel(
            ITourRepository tourRepository,
            TourLogRepository tourLogRepository,
            TourAutoPropertyService tourAutoPropertyService,
            INavigationHost navigationHost,
            IEventAggregator eventAggregator)
        {
            this.tourLogRepository = tourLogRepository;
            this.tourAutoPropertyService = tourAutoPropertyService;
            this.tourRepository = tourRepository;
            this.navigationHost = navigationHost;
            this.eventAggregator = eventAggregator;
        }

        public async Task Add()
        {
            await navigationHost.NavigateToScreen<CreateTourLogScreenViewModel>(new System.Threading.CancellationToken(), this.Model);
        }

        public async Task Update()
        {
            if (SelectedTourLog != null)
            {
                string error = SelectedTourLog.Validators.HasError();
                if (!string.IsNullOrEmpty(error))
                {
                    MessageBox.Show(error);
                }
                else
                {
                    await ShowAsyncOperation.Run(() =>
                        {
                            tourLogRepository.UpdateTourLog(SelectedTourLog.Model);
                            UpdateTourAutoProperties();
                        },
                        Log
                    );

                    await eventAggregator.PublishOnUIThreadAsync(new RefreshToursMessage());
                }
            }
        }

        public async Task Delete()
        {
            if (SelectedTourLog != null)
            {
                string error = SelectedTourLog.Validators.HasError();
                if (!string.IsNullOrEmpty(error))
                {
                    MessageBox.Show(error);
                }
                else
                {
                    await ShowAsyncOperation.Run(() =>
                        {
                            tourLogRepository.DeleteTourLog(this.SelectedTourLog.Model);
                            UpdateTourAutoProperties();
                        },
                        Log
                    );

                    await eventAggregator.PublishOnUIThreadAsync(new RefreshToursMessage());
                }
            }
        }

        protected override void OnModelChanged()
        {
            if (this.Model != null)
            {
                tourLogs = this.Model.TourLogs
                    .SelectViewModels<TourLog, TourLogValidationViewModel>()
                    .ToList();
            }

            base.OnModelChanged();
        }

        private void UpdateTourAutoProperties()
        {
            Tour tour = tourRepository.GetTourById(this.Model.Id);
            tourAutoPropertyService.RecalculateTourProperties(this.Model);
            tourRepository.UpdateTour(this.Model);
        }

        private List<TourLogValidationViewModel> tourLogs;
        private readonly TourLogRepository tourLogRepository;
        private readonly TourAutoPropertyService tourAutoPropertyService;
        private readonly INavigationHost navigationHost;
        private readonly IEventAggregator eventAggregator;
        private readonly ITourRepository tourRepository;
        private static readonly NLog.ILogger Log = NLog.LogManager.GetCurrentClassLogger();
    }
}
