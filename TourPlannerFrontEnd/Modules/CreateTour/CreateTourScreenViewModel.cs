
namespace TourPlannerFrontEnd.Modules.CreateTour
{
    using Caliburn.Micro;
    using MTCG.DAL;
    using System.Threading.Tasks;
    using TourPlanner.DataTransferObjects.Models;
    using TourPlannerBackEnd.Repositories;
    using TourPlannerFrontEnd.Infrastructure;

    class CreateTourScreenViewModel : Screen
    {
        public CreateTourViewModel CreateTourViewModel { get; init; }

        public INavigationHost NavigationHost { get; set; }

        public CreateTourScreenViewModel(
            TourRepository tourRepository,
            UnitOfWorkFactory unitOfWorkFactory)
        {
            CreateTourViewModel = new CreateTourViewModel(tourRepository, unitOfWorkFactory);
            CreateTourViewModel.Model = new Tour();

            DisplayName = "Create Tour";
        }


        public async Task Back()
        {
            await NavigationHost.NavigateBackOneStep(new System.Threading.CancellationToken());
        }
    }
}
