using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlannerFrontEnd.Models;

namespace TourPlannerFrontEnd.Infrastructure
{
    internal class ExampleViewModel : ViewModel<ExampleModel>
    {
        public string Message
        {
            get => Model?.Message;
            set
            {
                if(Model != null)
                {
                    Model.Message = value;
                    NotifyOfPropertyChange(nameof(Message));
                }
            }
        }

        protected override void OnModelChanged()
        {
            if(Model != null)
            {
                Message = "Model got set";
            }

            base.OnModelChanged();
        }
    }
}
