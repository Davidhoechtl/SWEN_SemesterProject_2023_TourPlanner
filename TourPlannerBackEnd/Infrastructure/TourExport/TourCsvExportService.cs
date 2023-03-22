using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.DataTransferObjects.Models;

namespace TourPlannerBackEnd.Infrastructure.TourExport
{
    public class TourCsvExportService : IExportService
    {
        /// <summary>
        /// Tourname, TravellingType,Startdate,StartStreet,Startcity,StartPostalCode,StartState,StartCountry,
        /// EndStreet,Endcity,EndPostalCode,EndState,EndCountry,EstimatedTimeInSeconds,DistanceInKm
        /// </summary>
        /// <param name="tours"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void Export(IEnumerable<Tour> tours, string path)
        {
            StringBuilder csvBuilder= new StringBuilder();
            foreach(Tour tour in tours)
            {
                csvBuilder.Append( ParseObject(tour.Name) + ";");
                csvBuilder.Append( ParseObject(tour.TravellingType) + ";");
                csvBuilder.Append( ParseObject(tour.StartDate) + ";");
                csvBuilder.Append( ParseObject(tour.Start.Street) + ";");
                csvBuilder.Append( ParseObject(tour.Start.City) + ";");
                csvBuilder.Append( ParseObject(tour.Start.PostCode) + ";");
                csvBuilder.Append( ParseObject(tour.Start.State) + ";");
                csvBuilder.Append( ParseObject(tour.Start.Country) + ";");
                csvBuilder.Append( ParseObject(tour.Destination.Street) + ";");
                csvBuilder.Append( ParseObject(tour.Destination.City) + ";");
                csvBuilder.Append( ParseObject(tour.Destination.PostCode) + ";");
                csvBuilder.Append( ParseObject(tour.Destination.State) + ";");
                csvBuilder.Append( ParseObject(tour.Destination.Country) + ";");
                csvBuilder.Append( ParseObject(tour.Route.EstimatedTimeInSeconds) + ";");
                csvBuilder.AppendLine( ParseObject(tour.Route.DistanceInKm));
            }

            string fileName = DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss") + ".csv";
            using(StreamWriter writer = new StreamWriter(Path.Combine(path, fileName)))
            {
                writer.Write(csvBuilder.ToString());
                writer.Close();
            }
        }

        private string ParseObject(object obj)
        {
            if(obj is null)
            {
                return "-";
            }
            else if(obj is string)
            {
                return (string)obj;
            }
            else if(obj is DateTime)
            {
                return ((DateTime)obj).ToShortDateString();
            }
            else
            {
                return obj.ToString();
            }
        }
    }
}
