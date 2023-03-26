
namespace Infrastructure.Reporting
{
    using FastReport;
    using FastReport.Export.Image;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using TourPlanner.DataTransferObjects.Models;

    public class FastReportGenerationService : IFastReportGenerationService
    {
        private string InFolder = Path.Combine(Environment.CurrentDirectory, "Reporting");

        public void GenerateSummarizeReport(Tour tour)
        {
            throw new NotImplementedException();
        }

        public void GenerateTourReport(Tour tour)
        {
            Report report = new Report();
            report.Load(Path.Combine(InFolder, "TourReport.frx"));
            report.RegisterData(new List<Tour>() { tour }, "Tours");
            report.Prepare();

            report.SavePrepared("C:\\Testreport.fpx");

            ImageExport image = new ImageExport();
            image.ImageFormat = ImageExportFormat.Jpeg;
            report.Export(image, "C:\\Testreport.jgp");
        }
    }
}
