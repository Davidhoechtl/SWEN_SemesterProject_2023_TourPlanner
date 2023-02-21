
namespace TourPlannerFrontEnd.Infrastructure
{
    using Caliburn.Micro;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using TourPlannerFrontEnd.Models;
    using TourPlannerFrontEnd.Modules.CreateTour;

    /// <summary>
    /// Root view that will be set in the <see cref="Bootstrapper"/>
    /// 
    /// Note:
    /// Conducter inherits <see cref="Screen"/> and enables an easy way to swap between views from its children
    /// </summary>
    internal class ShellViewModel : Conductor<object>
    {
        public ExampleViewModel Example { get; set; }

        /// <summary>
        /// This method will be called when the Conductor is ready to do stuff
        /// </summary>
        protected override async Task OnActivateAsync(CancellationToken cancellationToken)
        {
            createTourViewModel = new CreateTourViewModel(this);

            //create an empty tour object
            Tour tour = new Tour();
            createTourViewModel.Model = tour;

            Example = new ExampleViewModel();
            ExampleModel model = new ExampleModel();
            Example.Model = model;
            await ActivateItemAsync(Example, cancellationToken);

            // This method enables switching between views. Every time you want to go from one page to another call this method
            await ActivateItemAsync(Example, cancellationToken);
            await ActivateItemAsync(createTourViewModel, cancellationToken);
        }

        protected override Task ChangeActiveItemAsync(object newItem, bool closePrevious, CancellationToken cancellationToken)
        {
            screenStack.Push(newItem);
            return base.ChangeActiveItemAsync(newItem, closePrevious, cancellationToken);
        }

        public async Task NavigateBackOneStep(CancellationToken cancellationToken)
        {
            // current view needs to be skipped
            screenStack.Pop();

            object screen = screenStack.Pop();
            if (screen != null)
            {
                await ActivateItemAsync(screen, cancellationToken);
            }
        }

        private Stack<object> screenStack = new();
        private CreateTourViewModel createTourViewModel;
    }
}
