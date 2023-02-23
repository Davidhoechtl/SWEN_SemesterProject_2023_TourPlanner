using Caliburn.Micro;
using MTCG.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TourPlannerBackEnd.Models;
using TourPlannerBackEnd.Repositories;
using TourPlannerFrontEnd.Infrastructure;

namespace TourPlannerFrontEnd.Modules.CreateTour
{
    class CreateTourScreenViewModel : Screen
    {
        private readonly ShellViewModel conductor;

        public CreateTourViewModel CreateTourViewModel { get; init; }

        public CreateTourScreenViewModel(ShellViewModel conductor, TourRepository tourRepository, UnitOfWorkFactory unitOfWorkFactory)
        {
            this.conductor = conductor;

            DisplayName = "Create Tour";
            CreateTourViewModel = new CreateTourViewModel(tourRepository, unitOfWorkFactory);
            CreateTourViewModel.Model = new Tour();
        }

        public async Task Back()
        {
            await conductor.NavigateBackOneStep(new System.Threading.CancellationToken());
        }
    }
}
