
namespace TourPlannerFrontEnd.Modules.OverviewTours
{
    using Caliburn.Micro;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Web;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using TourPlanner.DataTransferObjects.Models;
    using TourPlannerBackEnd.Infrastructure.Services;
    using TourPlannerBackEnd.Repositories;
    using TourPlannerFrontEnd.Infrastructure;
    using TourPlannerFrontEnd.Infrastructure.Validation;
    using TourPlannerFrontEnd.Modules.RateTour;
    using TourPlannerFrontEnd.Modules.TourLogs;

    internal class TourDetailViewModel : ValidatingViewModel<Tour>
    {
        public string TourName
        {
            get => this.Model?.Name;
            set
            {
                if (this.Model != null)
                {
                    this.Model.Name = value;
                    NotifyOfPropertyChange(nameof(TourName));
                }
            }
        }

        public DateTime StartDate
        {
            get => this.Model?.StartDate ?? DateTime.MinValue;
            set
            {
                if (this.Model != null)
                {
                    this.Model.StartDate = value;
                    NotifyOfPropertyChange(nameof(StartDate));
                }
            }
        }

        public string TravellingType => this.Model?.TravellingType;

        public string StartStreet => this.Model?.Start?.Street;
        public string StartCity => this.Model?.Start?.City;
        public int? StartPostCode => this.Model?.Start?.PostCode;
        public string StartState => this.Model?.Start?.State;
        public string StartCountry => this.Model?.Start?.Country;

        public string EndStreet => this.Model?.Destination?.Street;
        public string EndCity => this.Model?.Destination?.City;
        public int? EndPostCode => this.Model?.Destination?.PostCode;
        public string EndState => this.Model?.Destination?.State;
        public string EndCountry => this.Model?.Destination?.Country;

        public string RouteTime => GetRouteTimeInMinutes();
        public string RouteDistance => this.Model?.Route?.DistanceInKm.ToString() ?? "error";

        public ImageSource MapImageSource { get; set; }

        public TourLogsViewModel TourLogs { get; private set; }
        //public List<TourLog> TourLogs => this.Model?.TourLogs;

        public void Setup(
            TourLogRepository tourLogRepository, 
            INavigationHost navigationHost, 
            IEventAggregator eventAggregator, 
            TourAutoPropertyService tourAutoPropertyService)
        {
            TourLogs = new TourLogsViewModel(tourLogRepository, tourAutoPropertyService, navigationHost, eventAggregator);
        }

        private string GetRouteTimeInMinutes()
        {
            if (this.Model?.Route?.EstimatedTimeInSeconds != null)
            {
                double minutes = this.Model.Route.EstimatedTimeInSeconds / 60;
                double roundedMinutes = Math.Round(minutes, 1);
                return roundedMinutes.ToString();
            }
            return "error";
        }

        protected override void OnModelChanged()
        {
            if (this.Model.Route?.MapImage != null)
            {
                MapImageSource = ByteToImage(this.Model.Route.MapImage);
            }

            TourLogs.Model = this.Model;

            base.OnModelChanged();
        }

        private static ImageSource ByteToImage(byte[] imageData)
        {
            BitmapImage biImg = new BitmapImage();
            MemoryStream ms = new MemoryStream(imageData);
            biImg.BeginInit();
            biImg.StreamSource = ms;
            biImg.EndInit();

            ImageSource imgSrc = biImg as ImageSource;

            return imgSrc;
        }

        public override ValidatorCollection SetupValidation()
        {
            ValidatorCollection validators = new ValidatorCollection();
            validators.Add(nameof(TourName), () =>
            {
                if (string.IsNullOrEmpty(TourName))
                {
                    return "Tour name must be set.";
                }
                return string.Empty;
            });

            return validators;
        }
    }
}
