using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlannerFrontEnd.Infrastructure.ViewContainers
{
    internal interface IBusyIndicatorContainer
    {
        void SetBusy(string msg);
        void SetNotBusy();
    }
}
