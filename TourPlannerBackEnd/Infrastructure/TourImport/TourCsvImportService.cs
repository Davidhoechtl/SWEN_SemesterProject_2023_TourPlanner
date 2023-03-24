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
        public List<Tour> Import(string filename)
        {
            ValidateFileName(filename);

            using (StreamReader reader = new StreamReader(filename))
            {
                List<Tour> tours = new List<Tour>();
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();

                    tours.Add(ConvertTourFromLine(line));
                }

                return tours;
            }
        }

        private Tour ConvertTourFromLine(string line)
        {
            string[] lineData = line.Split(';');

            Tour tour = new Tour()
            {
                Name = ParseObject<string>(lineData[0]),
                TravellingType = ParseObject<string>(lineData[1]),
                StartDate = ParseObject<DateTime>(lineData[2]),

                Start = new Location()
                {
                    Street = ParseObject<string>(lineData[3]),
                    City = ParseObject<string>(lineData[4]),
                    PostCode = ParseObject<int>(lineData[5]),
                    State = ParseObject<string>(lineData[6]),
                    Country = ParseObject<string>(lineData[7])
                },

                Destination = new Location()
                {
                    Street = ParseObject<string>(lineData[8]),
                    City = ParseObject<string>(lineData[9]),
                    PostCode = ParseObject<int>(lineData[10]),
                    State = ParseObject<string>(lineData[11]),
                    Country = ParseObject<string>(lineData[12])
                },

                Route = new Route()
                {
                    TravellingType = ParseObject<string>(lineData[1]),
                    EstimatedTimeInSeconds = ParseObject<double>(lineData[13]),
                    DistanceInKm = ParseObject<double>(lineData[14])
                }
            };

            return tour;
        }

        private void ValidateFileName(string filename)
        {
            if (string.IsNullOrEmpty(filename))
            {
                throw new ArgumentException("The filename ist empty or null");
            }
            else if (Path.GetExtension(filename) != ".csv")
            {
                throw new ArgumentException($"The filename has an invalid extension for the {nameof(TourCsvImportService)} it should be csv");
            }
        }

        private T ParseObject<T>(string str)
        {
            Type convertType = typeof(T);

            if (str == "-")
            {
                return default(T);
            }
            else if (convertType == typeof(DateTime))
            {
                return (T)(object)DateTime.Parse(str);
            }
            else if (convertType == typeof(int))
            {
                return (T)(object)int.Parse(str);
            }
            else if (convertType == typeof(double))
            {
                return (T)(object)double.Parse(str);
            }
            else
            {
                return (T)(object)str;
            }
        }
    }
}
