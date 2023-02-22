using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlannerFrontEnd.Infrastructure;
using TourPlannerFrontEnd.Modules.CreateTour;

namespace TourPlannerFrontEnd.Modules.OverviewTours
{
    internal class ToursOverviewScreenViewModel : Screen
    {
        public ToursOverviewScreenViewModel( ShellViewModel conductor )
        {
            this.conductor = conductor;

            DisplayName = "Tour Overview";
        }
        
        public async Task CreateTour()
        {
            await conductor.NavigateToScreen<CreateTourScreenViewModel>(new System.Threading.CancellationToken());
        }

        private readonly ShellViewModel conductor;
    }
}
