
namespace TourPlannerFrontEnd.Modules.CreateTour
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows;
    using TourPlanner.DataTransferObjects.Models;
    using TourPlannerBackEnd.Infrastructure;
    using TourPlannerBackEnd.Infrastructure.Services;
    using TourPlannerBackEnd.Repositories;
    using TourPlannerFrontEnd.Infrastructure;
    using TourPlannerFrontEnd.Infrastructure.Helper;
    using TourPlannerFrontEnd.Infrastructure.Validation;
    using TourPlannerFrontEnd.Infrastructure.ViewContainers;

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

        public CreateTourViewModel(ITourRepository tourRepository, TourPlannerMapQuestService mapQuestService, IBusyIndicatorContainer busyIndicatorContainer, TourAutoPropertyService autoPropertyService)
        {
            travellingTypes = Enum.GetValues<RouteType>().Select(v => v.ToString()).ToArray();
            this.tourRepository = tourRepository;
            this.mapQuestService = mapQuestService;
            this.busyIndicatorContainer = busyIndicatorContainer;
            this.autoPropertyService = autoPropertyService;
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
                await ShowAsyncOperation.RunAndShowMessage(() =>
                    {
                        this.Model.TravellingType = SelectedTravellingType;
                        this.Model.Start = mapQuestService.GetLocationFromSingleLineAddress(Start).Result;
                        this.Model.Destination = mapQuestService.GetLocationFromSingleLineAddress(Destination).Result;
                        this.Model.Route = mapQuestService.GetRouteFromLocations(
                            Start,
                            Destination,
                            SelectedTravellingType
                        ).Result;

                        this.Model.Route.MapImage = mapQuestService.GetRouteImage(Start, Destination, 600, 400).Result;

                        autoPropertyService.RecalculateTourProperties(this.Model);

                        tourRepository.InsertTour(this.Model);
                    },
                    successMsg:"Successfully saved!",
                    errorMsg: "Save operation failed.",
                    Log,
                    busyIndicatorContainer
                );
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
        
        private readonly ITourRepository tourRepository;
        private readonly IBusyIndicatorContainer busyIndicatorContainer;
        private readonly TourAutoPropertyService autoPropertyService;
        private readonly TourPlannerMapQuestService mapQuestService;
        private static readonly NLog.ILogger Log = NLog.LogManager.GetCurrentClassLogger();
    }
}
