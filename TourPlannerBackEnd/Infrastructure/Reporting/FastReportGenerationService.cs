
namespace TourPlannerBackEnd.Infrastructure.Reporting
{
    using FastReport;
    using FastReport.Export.Image;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using TourPlanner.DataTransferObjects.Models;
    using TourPlanner.DataTransferObjects.Models.Reporting;

    public class FastReportGenerationService : IFastReportGenerationService
    {
        private string InFolder = @"D:\Studium\Sommersemester 2023\SWEN2\SWEN_SemesterProject_2023_TourPlanner\Reporting";
        //private string InFolder = @"C:\Studium\SWENSemesterProject_TourPlanner\Reporting";

        public void GenerateSummarizeReport(IEnumerable<Tour> tours)
        {
            List<TourReportSummarizeContext> bussinesObject = new List<TourReportSummarizeContext>() { new TourReportSummarizeContext(tours) };

            Report report = new Report();
            report.Load(Path.Combine(InFolder, "TourSummaries.frx"));
            report.RegisterData(bussinesObject, "Tours");
            report.Prepare();

            ImageExport image = new ImageExport();
            image.ImageFormat = ImageExportFormat.Jpeg;
            report.Export(image, "C:\\TestSummary.jpg");

            report.Dispose();
        }

        public void GenerateTourReport(Tour tour)
        {
            List<TourReportDataContext> businessObject = new List<TourReportDataContext> { new TourReportDataContext(tour) };
            //string assemblyTest = businessObject.GetType().Assembly.FullName;

            Report report = new Report();
            report.Load(Path.Combine(InFolder, "TourReport.frx"));
            report.RegisterData(businessObject, "Tours");
            report.Prepare();

            report.SavePrepared("C:\\Testreport.fpx");

            ImageExport image = new ImageExport();
            image.ImageFormat = ImageExportFormat.Jpeg;
            report.Export(image, "C:\\Testreport.jpg");

            report.Dispose();
        }
    }
}
