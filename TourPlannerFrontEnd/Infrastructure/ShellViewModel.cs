using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlannerFrontEnd.Infrastructure
{
    internal class ShellViewModel : Screen
    {
        public ExampleViewModel Example { get; set; }

        public ShellViewModel()
        {
            Example = new ExampleViewModel();
        }
    }
}
