
namespace TourPlannerBackEnd
{
    using Autofac;
    using TourPlanner.DataTransferObjects.Models;
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
            ApplicationConfigLoader configLoader = new ApplicationConfigLoader();
            builder.Register<ApplicationConfiguration>((context, param) =>
            {
                return configLoader.Load();
            });

            RegisterInfrastructure(builder);
            RegisterDataAccess(builder);
            RegisterRepositories(builder);
        }

        private void RegisterInfrastructure(ContainerBuilder builder)
        {
            builder.RegisterTypes(
                typeof(ApplicationConfigLoader),
                typeof(TourPlannerMapQuestService),
                typeof(TourCsvExportService),
                typeof(TourCsvImportService),
                typeof(FastReportGenerationService),
                typeof(TourAutoPropertyService),
                typeof(CalorieCalculationService)
            )
            .SingleInstance()
            .AsImplementedInterfaces()
            .AsSelf();
        }

        private void RegisterRepositories(ContainerBuilder builder)
        {
            builder.RegisterTypes(
                typeof(TourRepository)
            )
            .SingleInstance()
            .AsImplementedInterfaces();

            builder.RegisterTypes(
                typeof(TourLogRepository)
            )
            .SingleInstance();
        }

        private void RegisterDataAccess(ContainerBuilder builder)
        {
            builder.RegisterType<TourPlannerDbContext>()
                .SingleInstance();
        }

        private readonly ContainerBuilder builder;
    }
}
