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
                    NotifyOfPropertyChange(nameof(Name));
                }
            }
        }

        public string From
        {
            get => startLocationData;
            set
            {
                if(startLocationData != value)
                {
                    startLocationData = value;
                    NotifyOfPropertyChange(nameof(From));
                }
            }
        }

        public string To
        {
            get => destinationLocationData;
            set
            {
                if (destinationLocationData != value)
                {
                    destinationLocationData = value;
                    NotifyOfPropertyChange(nameof(To));
                }
            }
        }

        public IEnumerable<string> TravellingTypes => travellingTypes;
        public string SelectedTravellingType
        {
            get => selectedTravellingType;
            set
            {
                if(selectedTravellingType != value)
                {
                    selectedTravellingType = value;
                    NotifyOfPropertyChange(nameof(SelectedTravellingType));
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
                    NotifyOfPropertyChange(nameof(StartDate));
                }
            }
        }

        public CreateTourViewModel(ShellViewModel conductor)
        {
            this.conductor = conductor;

            travellingTypes = Enum.GetValues<RouteType>().Select(v => v.ToString()).ToArray();
        }

        /// <summary>
        /// Gets called when the button save is pressed
        /// </summary>
        public void Save()
        {
            // api get route

            MessageBox.Show($"Name of the new tour: {Name} \n" +
                            $"StartDate of the new tour: {StartDate}");
        }

        public async Task Back()
        {
            await conductor.NavigateBackOneStep(new System.Threading.CancellationToken());
        }

        private string startLocationData;
        private string destinationLocationData;

        private string selectedTravellingType;
        private readonly string[] travellingTypes;

        private readonly ShellViewModel conductor;
    }
}
