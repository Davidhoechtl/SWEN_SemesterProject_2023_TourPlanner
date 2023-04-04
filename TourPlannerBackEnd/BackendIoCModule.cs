
namespace TourPlannerBackEnd
{
    using Autofac;
    using TourPlanner.EntityFramework.DataAccess;
    using TourPlannerBackEnd.Infrastructure;
    using TourPlannerBackEnd.Infrastructure.Reporting;
    using TourPlannerBackEnd.Infrastructure.Services;
    using TourPlannerBackEnd.Infrastructure.TourExport;
    using TourPlannerBackEnd.Infrastructure.TourImport;
    using TourPlannerBackEnd.Repositories;

    public class BackendIoCModule : Autofac.Module
    {
        public BackendIoCModule(ContainerBuilder builder)
        {
            this.builder = builder;
        }

        public void Load()
        {
            RegisterInfrastructure(builder);
            RegisterDataAccess(builder);
            RegisterRepositories(builder);
        }

        private void RegisterInfrastructure(ContainerBuilder builder)
        {
            builder.RegisterTypes(
                typeof(ApiKeyLoader),
                typeof(TourPlannerMapQuestService),
                typeof(TourCsvExportService),
                typeof(TourCsvImportService),
                typeof(FastReportGenerationService),
                typeof(TourAutoPropertyService)
            )
            .SingleInstance()
            .AsImplementedInterfaces()
            .AsSelf();
        }

        private void RegisterRepositories(ContainerBuilder builder)
        {
            builder.RegisterTypes(
                typeof(TourRepository),
                typeof(TourLogRepository)
            )
            .SingleInstance();
        }

        private void RegisterDataAccess(ContainerBuilder builder)
        {
            builder.RegisterType<TourPlannerDbContext>();
        }

        private readonly ContainerBuilder builder;
    }
}
