
namespace TourPlannerBackEnd.Infrastructure.TourImport
{
    using TourPlanner.DataTransferObjects.Models;
    public interface IImportService
    {
        Task<List<Tour>> Import(string filename);
    }
}
