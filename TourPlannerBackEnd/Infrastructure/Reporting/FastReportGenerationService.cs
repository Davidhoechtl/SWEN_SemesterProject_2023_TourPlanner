
namespace TourPlannerBackEnd.Infrastructure.Reporting
{
    using FastReport;
    using FastReport.Export.Image;
    using FastReport.Utils;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Text;
    using TourPlanner.DataTransferObjects.Models;
    using TourPlanner.DataTransferObjects.Models.Reporting;

    public class FastReportGenerationService : IFastReportGenerationService
    {
        //private string InFolder = @"D:\Studium\Sommersemester 2023\SWEN2\SWEN_SemesterProject_2023_TourPlanner\Reporting";
        private string InFolder = @"C:\Studium\SWENSemesterProject_TourPlanner\Reporting";

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

            //ReportPage page = new ReportPage();
            //report.Pages.Add(page);
            //page.CreateUniqueName();

            //DataBand data = new DataBand();
            //page.Bands.Add(data);
            //data.CreateUniqueName();
            //data.Height = Units.Centimeters * 1; //Set band height

            //PictureObject tourImage = new PictureObject();
            report.RegisterData(businessObject, "Tours");

            PictureObject pic = report.FindObject("Picture1") as PictureObject;
            using (MemoryStream ms = new MemoryStream(tour.Route.MapImage))
            {

                Bitmap bitmap = new Bitmap(ms);
                pic.Image = bitmap;
                pic.Bounds = new RectangleF(380, 80, bitmap.Width / 2, bitmap.Height/ 2);
                pic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
                report.Prepare();
            }

            report.SavePrepared("C:\\Testreport.fpx");

            ImageExport image = new ImageExport();
            image.ImageFormat = ImageExportFormat.Jpeg;
            report.Export(image, "C:\\Testreport.jpg");

            report.Dispose();
        }
    }
}
