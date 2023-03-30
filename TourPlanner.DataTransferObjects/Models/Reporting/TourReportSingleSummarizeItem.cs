using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.DataTransferObjects.Models.Reporting
{
    public class TourReportSingleSummarizeItem
    {
        public string TourName { get; set; }
        public int TourLogsCount { get; set; }
        public double AverageTimeInMinutes { get; set; }
        public double AverageDifficutly { get; set; }
        public double AverageRating { get; set; }

        public TourReportSingleSummarizeItem(Tour tour)
        {
            this.TourName = tour.Name;
            this.TourLogsCount = tour.TourLogs.Count;
            this.AverageTimeInMinutes = Math.Round(tour.TourLogs.Sum(tl => tl.TakenTimeInSeconds) / tour.TourLogs.Count, 2);
            this.AverageDifficutly = Math.Round((double)tour.TourLogs.Sum(tl => tl.Difficulty) / tour.TourLogs.Count, 2);
            this.AverageRating = Math.Round((double)tour.TourLogs.Sum(tl => tl.Rating) / tour.TourLogs.Count, 2);
        }
    }
}
