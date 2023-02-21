using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlannerFrontEnd.Models;

namespace TourPlannerFrontEnd.Infrastructure
{
    internal class ExampleViewModel : PropertyChangedBase
    {
        public string Message
        {
            get => Model.Message;
            set
            {
                if(Model != null)
                {
                    Model.Message = value;
                    NotifyOfPropertyChange(nameof(Message));
                }
            }
        }

        public ExampleViewModel()
        {
            Model = new ExampleModel();
            Model.Message = "Test";
        }

        private ExampleModel Model;
    }
}
