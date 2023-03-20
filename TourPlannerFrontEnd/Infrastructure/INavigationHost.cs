

namespace TourPlannerFrontEnd.Infrastructure
{
    using Caliburn.Micro;
    using System.Threading;
    using System.Threading.Tasks;

    internal interface INavigationHost
    {
        Task NavigateToScreen<T>(CancellationToken cancellationToken) where T : Screen;
        Task NavigateToScreen<T>(CancellationToken cancellationToken, object dataContext) where T : Screen;
    }
}
