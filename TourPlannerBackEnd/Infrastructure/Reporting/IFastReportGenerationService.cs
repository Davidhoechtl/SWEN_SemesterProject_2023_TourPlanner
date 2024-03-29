﻿
namespace TourPlannerBackEnd.Infrastructure.Reporting
{
    using TourPlanner.DataTransferObjects.Models;
    public interface IFastReportGenerationService
    {
        public void GenerateTourReport(Tour tour);
        public void GenerateSummarizeReport(IEnumerable<Tour> tour);
    }
}
