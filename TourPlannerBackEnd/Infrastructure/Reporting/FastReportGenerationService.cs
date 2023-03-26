
namespace TourPlannerBackEnd.Infrastructure.Reporting
{
    using FastReport;
    using FastReport.Export.Image;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using TourPlanner.DataTransferObjects.Models;

    public class FastReportGenerationService : IFastReportGenerationService
    {
        private string InFolder = @"D:\Studium\Sommersemester 2023\SWEN2\SWEN_SemesterProject_2023_TourPlanner\Reporting";

        public void GenerateSummarizeReport(Tour tour)
        {
            throw new NotImplementedException();
        }

        public void GenerateTourReport(Tour tour)
        {
            List<Tour> businessObject = new List<Tour> { tour };
            string assemblyTest = businessObject.GetType().Assembly.FullName;

            Report report = new Report();
            report.Load(Path.Combine(InFolder, "TourReport.frx"));
            report.RegisterData(new List<Tour>() { tour }, "Tours");
            report.Prepare();

            report.SavePrepared("C:\\Testreport.fpx");

            ImageExport image = new ImageExport();
            image.ImageFormat = ImageExportFormat.Jpeg;
            report.Export(image, "C:\\Testreport.jgp");

            report.Dispose();
        }
    }
}
