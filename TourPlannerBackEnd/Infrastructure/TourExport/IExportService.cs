
namespace TourPlannerBackEnd.Infrastructure.TourExport
{
    using TourPlanner.DataTransferObjects.Models;
    public interface IExportService
    {
        public void Export(IEnumerable<Tour> tours, string path);
    }
}
