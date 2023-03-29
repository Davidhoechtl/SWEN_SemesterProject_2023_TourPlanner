using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.DataTransferObjects.Models.Reporting
{
    public class TourReportDataContext
    {
        public string Name { get; set; }
        public string TravellingType { get; set; }
        public DateTime StartDate { get; set; }

        public string StartStreet { get; set; }
        public string StartCity { get; set; }
        public int StartPostCode { get; set; }
        public string StartCountry { get; set; }

        public string DestinationStreet { get; set; }
        public string DestinationCity { get; set; }
        public int DestinationPostCode { get; set; }
        public string DestinationCountry { get; set; }

        public double EstimatedTimeInMinutes { get; set; }
        public double DistanceInKm { get; set; }

        public List<TourLog> TourLogs { get; set; }

        public TourReportDataContext(Tour tour)
        {
            this.Name = tour.Name;
            this.TravellingType = tour.TravellingType;
            this.StartDate = tour.StartDate;
            this.StartStreet = tour.Start.Street;
            this.StartPostCode = tour.Start.PostCode;
            this.StartCity = tour.Start.City;
            this.StartCountry = tour.Start.Country;
            this.DestinationStreet = tour.Destination.Street;
            this.DestinationPostCode = tour.Destination.PostCode;
            this.DestinationCity = tour.Destination.City;
            this.DestinationCountry = tour.Destination.Country;
            this.EstimatedTimeInMinutes = tour.Route.EstimatedTimeInSeconds;
            this.DistanceInKm = tour.Route.DistanceInKm;

            this.TourLogs = tour.TourLogs;
        }
    }
}
