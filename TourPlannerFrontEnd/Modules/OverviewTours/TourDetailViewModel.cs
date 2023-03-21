
namespace TourPlannerFrontEnd.Modules.OverviewTours
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using TourPlanner.DataTransferObjects.Models;
    using TourPlannerFrontEnd.Infrastructure;

    internal class TourDetailViewModel : ViewModel<Tour>
    {
        public string TourName => this.Model?.Name;

        public DateTime StartDate => this.Model?.StartDate ?? DateTime.MinValue;

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

        public List<TourLog> TourLogs => this.Model?.TourLogs;

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

            base.OnModelChanged();
        }


        public static ImageSource ByteToImage(byte[] imageData)
        {
            BitmapImage biImg = new BitmapImage();
            MemoryStream ms = new MemoryStream(imageData);
            biImg.BeginInit();
            biImg.StreamSource = ms;
            biImg.EndInit();

            ImageSource imgSrc = biImg as ImageSource;

            return imgSrc;
        }
    }
}
