
namespace TourPlannerFrontEnd.Modules.RateTour
{
    using NLog;
    using System;
    using System.Threading.Tasks;
    using System.Windows;
    using TourPlanner.DataTransferObjects.Models;
    using TourPlannerBackEnd.Infrastructure.Services;
    using TourPlannerBackEnd.Repositories;
    using TourPlannerFrontEnd.Infrastructure;
    using TourPlannerFrontEnd.Infrastructure.Helper;
    using TourPlannerFrontEnd.Infrastructure.Validation;
    using TourPlannerFrontEnd.Infrastructure.ViewContainers;

    internal class CreateTourLogViewModel : ValidatingViewModel<TourLog>
    {
        public DateTime Date
        {
            get => this.Model.Date;
            set
            {
                this.Model.Date = value;
                NotifyOfPropertyChange(nameof(Date));
            }
        }

        public string Comment
        {
            get => this.Model.Comment;
            set
            {
                this.Model.Comment = value;
                NotifyOfPropertyChange(nameof(Comment));
            }
        }

        public double TakenTimeInMinutes
        {
            get => this.Model.TakenTimeInSeconds;
            set
            {
                this.Model.TakenTimeInSeconds = value;
                NotifyOfPropertyChange(nameof(TakenTimeInMinutes));
            }
        }

        public int Difficulty
        {
            get => this.Model.Difficulty;
            set
            {
                this.Model.Difficulty = value;
                NotifyOfPropertyChange(nameof(Difficulty));
            }
        }

        public int Rating
        {
            get => this.Model.Rating;
            set
            {
                this.Model.Rating = value;
                NotifyOfPropertyChange(nameof(Rating));
            }
        }

        public CreateTourLogViewModel(ITourRepository tourRepository, TourLogRepository tourLogRepository, TourAutoPropertyService tourAutoPropertyService, IBusyIndicatorContainer busyIndicator)
        {
            this.tourRepository = tourRepository;
            this.tourLogRepository = tourLogRepository;
            this.tourAutoPropertyService = tourAutoPropertyService;
            this.busyIndicator = busyIndicator;
        }

        public async Task Save()
        {
            string error = Validators.HasError();
            if (!string.IsNullOrEmpty(error))
            {
                MessageBox.Show(error);
            }
            else
            {
                await ShowAsyncOperation.RunAndShowMessage(() =>
                    {
                        // saves tour log into the database
                        tourLogRepository.SaveTourLog(this.Model);

                        // updates auto calculated properties of tour
                        Tour tour = tourRepository.GetTourById(this.Model.TourId);
                        tourAutoPropertyService.RecalculateTourProperties(tour);
                        tourRepository.UpdateTour(tour);
                    }, 
                    successMsg: "Saved succesful.",
                    errorMsg: "Operation failed",
                    Log,
                    busyIndicator
                );
             }
        }

        public override ValidatorCollection SetupValidation()
        {
            ValidatorCollection validators = new ValidatorCollection();

            validators.Add(nameof(Date), () =>
            {
                if (Date < DateTime.Today)
                {
                    return "Date must be in the future";
                }

                return string.Empty;
            });
            validators.Add(nameof(Comment), () =>
            {
                if (string.IsNullOrEmpty(this.Comment))
                {
                    return "Comment must not be null or empty";
                }

                return string.Empty;
            });
            validators.Add(nameof(TakenTimeInMinutes), () =>
            {
                if (TakenTimeInMinutes <= 0)
                {
                    return "Taken Time in Minutes must be greater than 0";
                }

                return string.Empty;
            });

            return validators;
        }

        private readonly TourLogRepository tourLogRepository;
        private readonly TourAutoPropertyService tourAutoPropertyService;
        private readonly IBusyIndicatorContainer busyIndicator;
        private readonly ITourRepository tourRepository;
        private static readonly ILogger Log = LogManager.GetCurrentClassLogger();
    }
}
