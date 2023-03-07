﻿
namespace TourPlannerFrontEnd.Modules.CreateTour
{
    using Caliburn.Micro;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Navigation;
    using TourPlanner.DataTransferObjects.Models;
    using TourPlannerBackEnd.Infrastructure;
    using TourPlannerBackEnd.Repositories;
    using TourPlannerFrontEnd.Infrastructure;

    class CreateTourScreenViewModel : NavigationScreen
    {
        public CreateTourViewModel CreateTourViewModel { get; init; }

        public INavigationHost NavigationHost { get; set; }

        public CreateTourScreenViewModel(
            TourRepository tourRepository,
            TourPlannerMapQuestService mapQuestService)
        {
            CreateTourViewModel = new CreateTourViewModel(tourRepository, mapQuestService);
            CreateTourViewModel.Model = new Tour();

            DisplayName = "Create Tour";
        }


        public async Task Back()
        {
            await NavigationHost.NavigateBackOneStep(new System.Threading.CancellationToken());
        }

        public override Task OnPageNavigatedTo(CancellationToken cancellationToken)
        {
            // noop
            return Task.CompletedTask;
        }
    }
}
