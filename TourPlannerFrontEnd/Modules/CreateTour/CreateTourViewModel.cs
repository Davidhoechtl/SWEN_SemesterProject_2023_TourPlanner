
namespace TourPlannerFrontEnd.Modules.CreateTour
{
    using MTCG.DAL;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows;
    using TourPlanner.DataTransferObjects.Models;
    using TourPlannerBackEnd.Infrastructure;
    using TourPlannerBackEnd.Repositories;
    using TourPlannerFrontEnd.Infrastructure;

    internal class CreateTourViewModel : ViewModel<Tour>
    {
        public string Name
        {
            get => Model?.Name;
            set
            {
                if (Model != null)
                {
                    Model.Name = value;
                    NotifyOfPropertyChange(nameof(Name));
                }
            }
        }

        public string Start
        {
            get => startLocationData;
            set
            {
                if (startLocationData != value)
                {
                    startLocationData = value;
                    NotifyOfPropertyChange(nameof(Start));
                }
            }
        }

        public string Destination
        {
            get => destinationLocationData;
            set
            {
                if (destinationLocationData != value)
                {
                    destinationLocationData = value;
                    NotifyOfPropertyChange(nameof(Destination));
                }
            }
        }

        public IEnumerable<string> TravellingTypes => travellingTypes;
        public string SelectedTravellingType
        {
            get => selectedTravellingType;
            set
            {
                if (selectedTravellingType != value)
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

        public CreateTourViewModel(TourRepository tourRepository, UnitOfWorkFactory unitOfWorkFactory)
        {
            travellingTypes = Enum.GetValues<RouteType>().Select(v => v.ToString()).ToArray();
            this.tourRepository = tourRepository;
            this.unitOfWorkFactory = unitOfWorkFactory;
        }

        /// <summary>
        /// Gets called when the button save is pressed
        /// </summary>
        public async Task Save()
        {
            // api get route


            if (this.Model != null)
            {
                //Location start = await mapQuestApiService.GetLocationFromAddressLine(Start);
                //Location destination = await mapQuestApiService.GetLocationFromAddressLine(Destination);
                this.Model.route = new Route()
                {
                    Start = new Location() { Street = Start },
                    Destination = new Location() { Street = Destination },
                    TravellingType = Enum.GetValues<RouteType>().First(type => type.ToString().Equals(SelectedTravellingType, StringComparison.Ordinal))
                };

                using (IUnitOfWork unitOfWork = unitOfWorkFactory.CreateAndBeginTransaction())
                {
                    tourRepository.InsertTour(this.Model, unitOfWork);
                    unitOfWork.Commit();

                }

                MessageBox.Show($"Erfolgreich gespeichert");
            }
        }

        private string startLocationData;
        private string destinationLocationData;

        private string selectedTravellingType;
        private readonly string[] travellingTypes;
        private readonly TourRepository tourRepository;
        private readonly UnitOfWorkFactory unitOfWorkFactory;
    }
}
