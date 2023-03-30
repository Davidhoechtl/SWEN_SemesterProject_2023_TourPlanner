using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.DataTransferObjects.Models.Reporting
{
    public class TourReportSummarizeContext
    {
        public List<TourReportSingleSummarizeItem> TourSummaries { get; set; }
        public int TourCount { get; set; }
        public DateTime CreationDate { get; set; }

        public TourReportSummarizeContext(IEnumerable<Tour> tours)
        {
            this.TourCount = tours.Count();
            this.CreationDate = DateTime.Now;

            TourSummaries = new List<TourReportSingleSummarizeItem>();
            foreach(Tour tour in tours)
            {
                TourSummaries.Add(new TourReportSingleSummarizeItem(tour));
            }
        }
    }
}
