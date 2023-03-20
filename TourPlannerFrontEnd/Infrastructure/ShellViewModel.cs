
namespace TourPlannerFrontEnd.Infrastructure
{
    using Caliburn.Micro;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using TourPlannerBackEnd.Infrastructure;
    using TourPlannerBackEnd.Repositories;
    using TourPlannerFrontEnd.Modules.CreateTour;
    using TourPlannerFrontEnd.Modules.OverviewTours;

    /// <summary>
    /// Root view that will be set in the <see cref="Bootstrapper"/>
    /// 
    /// Note:
    /// Conducter inherits <see cref="Screen"/> and enables an easy way to swap between views from its children
    /// </summary>
    internal class ShellViewModel : Conductor<NavigationScreen>, INavigationHost
    {
        public ShellViewModel(
            IEnumerable<NavigationScreen> screens)
        {
            this.screens = screens.ToList();
        }

        /// <summary>
        /// This method will be called when the Conductor is ready to do stuff
        /// </summary>
        protected override async Task OnActivateAsync(CancellationToken cancellationToken)
        {
            //Example = new ExampleViewModel();
            //ExampleModel model = new ExampleModel();
            //Example.Model = model;
            //await ActivateItemAsync(Example, cancellationToken);

            // This method enables switching between views. Every time you want to go from one page to another call this method
            await NavigateToScreen<ToursOverviewScreenViewModel>(cancellationToken);
        }
        public async Task NavigateToScreen<T>(CancellationToken cancellationToken) where T : Screen
        {
            NavigationScreen screen = screens.FirstOrDefault(s => s is T);
            if (screen != null)
            {
                await screen.OnPageNavigatedTo(cancellationToken);
                await ActivateItemAsync(screen, cancellationToken);
            }
            else
            {
                throw new Exception("The page with type " + typeof(T).Name + " is not recognized in the children of the conductor");
            }
        }

        public async Task NavigateToScreen<T>(CancellationToken cancellationToken, object dataContext) where T : Screen
        {
            NavigationScreen screen = screens.FirstOrDefault(s => s is T);
            if (screen != null)
            {
                await screen.OnPageNavigatedTo(cancellationToken, dataContext);
                await ActivateItemAsync(screen, cancellationToken);
            }
            else
            {
                throw new Exception("The page with type " + typeof(T).Name + " is not recognized in the children of the conductor");
            }
        }

        public override IEnumerable<NavigationScreen> GetChildren()
        {
            return screens;
        }

        private List<NavigationScreen> screens = new();
    }
}
