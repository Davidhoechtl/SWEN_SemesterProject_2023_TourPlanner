using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TourPlannerFrontEnd.Infrastructure
{
    internal abstract class NavigationScreen : Screen
    {
        public abstract Task OnPageNavigatedTo(CancellationToken cancellationToken);
        public virtual Task OnPageNavigatedTo(CancellationToken cancellationToken, object dataContext)
        {
            //noop
            return Task.CompletedTask;
        }
    }
}
