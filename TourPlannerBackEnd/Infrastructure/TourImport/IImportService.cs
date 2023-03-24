
namespace TourPlannerBackEnd.Infrastructure.TourImport
{
    using TourPlanner.DataTransferObjects.Models;
    public interface IImportService
    {
        List<Tour> Import(string filename);
    }
}
