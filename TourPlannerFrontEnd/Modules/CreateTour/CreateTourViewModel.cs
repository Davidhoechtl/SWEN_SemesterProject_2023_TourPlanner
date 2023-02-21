using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TourPlannerFrontEnd.Infrastructure;
using TourPlannerFrontEnd.Models;

namespace TourPlannerFrontEnd.Modules.CreateTour
{
    internal class CreateTourViewModel : ViewModel<Tour>
    {

        public string Name
        {
            get => Model?.Name;
            set
            {
                if(Model != null)
                {
                    Model.Name = value;
                }
            }
        }

        public DateTime? StartDate
        {
            get => Model?.StartDate;
            set
            {
                if (Model != null && value.HasValue)
                {
                    Model.StartDate = value.Value;
                }
            }
        }

        public CreateTourViewModel(ShellViewModel conductor)
        {
            this.conductor = conductor;
        }

        /// <summary>
        /// Gets called when the button save is pressed
        /// </summary>
        public void Save()
        {
            MessageBox.Show($"Name of the new tour: {Name} \n" +
                            $"StartDate of the new tour: {StartDate}");
        }

        public async Task Back()
        {
            await conductor.NavigateBackOneStep(new System.Threading.CancellationToken());
        }

        private readonly ShellViewModel conductor;
    }
}
