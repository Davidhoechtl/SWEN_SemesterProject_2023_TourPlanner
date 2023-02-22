using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TourPlannerFrontEnd.Infrastructure;
using TourPlannerFrontEnd.Models;

namespace TourPlannerFrontEnd.Modules.CreateTour
{
    class CreateTourScreenViewModel : Screen
    {
        private readonly ShellViewModel conductor;

        public CreateTourViewModel CreateTourViewModel { get; init; }

        public CreateTourScreenViewModel(ShellViewModel conductor)
        {
            this.conductor = conductor;

            DisplayName = "Create Tour";
            CreateTourViewModel = new CreateTourViewModel();
            CreateTourViewModel.Model = new Tour();
        }

        public async Task Back()
        {
            await conductor.NavigateBackOneStep(new System.Threading.CancellationToken());
        }
    }
}
