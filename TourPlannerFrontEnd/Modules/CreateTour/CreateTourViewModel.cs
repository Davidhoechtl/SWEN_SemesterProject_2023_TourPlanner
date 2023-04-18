
namespace TourPlannerFrontEnd.Modules.CreateTour
{
    using Caliburn.Micro;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows;
    using TourPlanner.DataTransferObjects.Models;
    using TourPlannerBackEnd.Infrastructure;
    using TourPlannerBackEnd.Repositories;
    using TourPlannerFrontEnd.Infrastructure;
    using TourPlannerFrontEnd.Infrastructure.Validation;

    internal class CreateTourViewModel : ValidatingViewModel<Tour>
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

        public CreateTourViewModel(TourRepository tourRepository, TourPlannerMapQuestService mapQuestService)
        {
            travellingTypes = Enum.GetValues<RouteType>().Select(v => v.ToString()).ToArray();
            this.tourRepository = tourRepository;
            this.mapQuestService = mapQuestService;
        }

        /// <summary>
        /// Gets called when the button save is pressed
        /// </summary>
        public async Task Save()
        {
            string error = Validators.HasError();
            if (!string.IsNullOrEmpty(error))
            {
                MessageBox.Show(error);
            }
            else if (this.Model != null)
            {
                this.Model.TravellingType = SelectedTravellingType;

                try
                {
                    this.Model.Start = await mapQuestService.GetLocationFromSingleLineAddress(Start);
                    if (this.Model.Start == null)
                    {
                        MessageBox.Show("Error: Startlocation is invalid");
                        return;
                    }

                    this.Model.Destination = await mapQuestService.GetLocationFromSingleLineAddress(Destination);
                    if (this.Model.Destination == null)
                    {
                        MessageBox.Show("Error: Destination location is invalid");
                        return;
                    }

                    this.Model.Route = await mapQuestService.GetRouteFromLocations(
                        Start,
                        Destination,
                        SelectedTravellingType
                    );

                    this.Model.Route.MapImage = await mapQuestService.GetRouteImage(Start, Destination, 600, 400);
                }
                catch (Exception ex)
                {
                    string errorMsg = "Error with the mapquest api request (maybe invalid input).";
                    Log.Error(ex, errorMsg);
                    MessageBox.Show(errorMsg);
                    return;
                }

                if (this.Model.Route == null)
                {
                    MessageBox.Show("Error: Route could not be found");
                }

                await Task.Run(() =>
                {
                    tourRepository.InsertTour(this.Model);
                });

                MessageBox.Show($"Successfully saved!");
            }
        }

        protected override void OnModelChanged()
        {
            startLocationData = null;
            destinationLocationData = null;
            Refresh();
        }

        public override ValidatorCollection SetupValidation()
        {
            ValidatorCollection validators = new ValidatorCollection();

            validators.Add(nameof(Name), () =>
            {
                if (string.IsNullOrEmpty(Name))
                {
                    return "Name of Tour must be set.";
                }
                return string.Empty;
            });

            validators.Add(nameof(Start), () =>
            {
                if (string.IsNullOrEmpty(Start))
                {
                    return "Start of Tour must be set.";
                }
                return string.Empty;
            });

            validators.Add(nameof(Destination), () =>
            {
                if (string.IsNullOrEmpty(Destination))
                {
                    return "Destination of Tour must be set.";
                }
                return string.Empty;
            });

            validators.Add(nameof(SelectedTravellingType), () =>
            {
                if (string.IsNullOrEmpty(SelectedTravellingType))
                {
                    return "TravellingType of Tour must be set.";
                }
                return string.Empty;
            });

            validators.Add(nameof(StartDate), () =>
            {
                if (StartDate.HasValue)
                {
                    if (StartDate < DateTime.Today)
                    {
                        return "Start date must be in the future.";
                    }
                }
                else
                {
                    return "Start date must be set.";
                }
                return string.Empty;
            });

            return validators;
        }

        private string startLocationData;
        private string destinationLocationData;

        private string selectedTravellingType;
        private readonly string[] travellingTypes;
        private readonly TourRepository tourRepository;
        private readonly TourPlannerMapQuestService mapQuestService;
        private static readonly NLog.ILogger Log = NLog.LogManager.GetCurrentClassLogger();
    }
}
