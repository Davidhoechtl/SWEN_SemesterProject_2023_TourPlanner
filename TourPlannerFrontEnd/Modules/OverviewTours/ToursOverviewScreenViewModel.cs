using Caliburn.Micro;
using MTCG.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using TourPlannerBackEnd.Models;
using TourPlannerBackEnd.Repositories;
using TourPlannerFrontEnd.Infrastructure;
using TourPlannerFrontEnd.Modules.CreateTour;

namespace TourPlannerFrontEnd.Modules.OverviewTours
{
    internal class ToursOverviewScreenViewModel : Screen
    {
        public List<Tour> Tours { get; private set; }

        public ToursOverviewScreenViewModel(ShellViewModel conductor, IQueryDatabase queryDatabase, TourRepository tourRepository)
        {
            this.conductor = conductor;
            this.queryDatabase = queryDatabase;
            this.tourRepository = tourRepository;
            DisplayName = "Tour Overview";
        }

        public async Task CreateTour()
        {
            await conductor.NavigateToScreen<CreateTourScreenViewModel>(new System.Threading.CancellationToken());
        }

        protected override async Task OnActivateAsync(CancellationToken cancellationToken)
        {
            Tours = await GetToursAsync(cancellationToken);
            NotifyOfPropertyChange(nameof(Tours));
            await base.OnActivateAsync(cancellationToken);
        }

        private async Task<List<Tour>> GetToursAsync(CancellationToken cancellationToken)
        {
            return await Task.Run(() =>
            {
                return tourRepository.GetAllTours(queryDatabase); ;
            }, cancellationToken);
        }

        private readonly ShellViewModel conductor;
        private readonly IQueryDatabase queryDatabase;
        private readonly TourRepository tourRepository;
    }
}
