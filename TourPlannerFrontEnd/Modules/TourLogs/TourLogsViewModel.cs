using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.DataTransferObjects.Models;
using TourPlannerBackEnd.Infrastructure.Services;
using TourPlannerBackEnd.Repositories;
using TourPlannerFrontEnd.Infrastructure;
using TourPlannerFrontEnd.Infrastructure.Messages;
using TourPlannerFrontEnd.Modules.CreateTourLog;

namespace TourPlannerFrontEnd.Modules.TourLogs
{
    internal class TourLogsViewModel : ViewModel<Tour>
    {
        public TourLog SelectedTourLog { get; set; }
        public List<TourLog> TourLogs
        {
            get => this.Model.TourLogs;
            set
            {
                if (this.Model != null)
                {
                    this.Model.TourLogs = value;
                }
            }
        }

        public TourLogsViewModel(
            TourLogRepository tourLogRepository,
            TourAutoPropertyService tourAutoPropertyService,
            INavigationHost navigationHost, 
            IEventAggregator eventAggregator)
        {
            this.tourLogRepository = tourLogRepository;
            this.tourAutoPropertyService = tourAutoPropertyService;
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
                await Task.Run(() =>
                {
                    tourLogRepository.UpdateTourLog(SelectedTourLog);
                    tourAutoPropertyService.RecalculateTourProperties(this.Model.Id);
                });
                await eventAggregator.PublishOnUIThreadAsync(new RefreshToursMessage());
            }
        }

        public async Task Delete()
        {
            if (SelectedTourLog != null)
            {
                await Task.Run(() =>
                {
                    tourLogRepository.DeleteTourLog(this.SelectedTourLog);
                    tourAutoPropertyService.RecalculateTourProperties(this.Model.Id);
                });
                await eventAggregator.PublishOnUIThreadAsync(new RefreshToursMessage());
            }
        }

        private readonly TourLogRepository tourLogRepository;
        private readonly TourAutoPropertyService tourAutoPropertyService;
        private readonly INavigationHost navigationHost;
        private readonly IEventAggregator eventAggregator;
    }
}
