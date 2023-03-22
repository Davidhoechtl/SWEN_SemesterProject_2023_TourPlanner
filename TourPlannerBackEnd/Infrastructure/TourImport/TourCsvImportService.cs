using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.DataTransferObjects.Models;

namespace TourPlannerBackEnd.Infrastructure.TourImport
{
    public class TourCsvImportService : IImportService
    {
        /// <summary>
        /// Tourname, TravellingType,Startdate,StartStreet,Startcity,StartPostalCode,StartState,StartCountry,
        /// EndStreet,Endcity,EndPostalCode,EndState,EndCountry,EstimatedTimeInSeconds,DistanceInKm
        /// </summary>
        public Task<List<Tour>> Import(string filename)
        {
            ValidateFileName(filename);

            using (StreamReader reader = new StreamReader(filename))
            {
                //while(reader.ReadLine() != null)
                //{
                //    string line = reader.ReadLine();
                //}
            }

            return null;
        }

        private void ValidateFileName(string filename)
        {
            if (string.IsNullOrEmpty(filename))
            {
                throw new ArgumentException("The filename ist empty or null");
            }
            else if (Path.GetExtension(filename) != "csv")
            {
                throw new ArgumentException($"The filename has an invalid extension for the {nameof(TourCsvImportService)} it should be csv");
            }
        }

        private object ParseObject(string str)
        {
            if (str is null)
            {
                return "-";
            }
            else if (double.TryParse(str, out double parsedDouble))
            {
                return parsedDouble;
            }
            else if (int.TryParse(str, out int parsedInt))
            {
                return parsedInt;
            }
            else if (DateTime.TryParse(str, out DateTime parsedDateTime))
            {
                return parsedDateTime;
            }
            else
            {
                throw new ArgumentException($"Dont know what to do wiht {str}");
            }
        }
    }
}
