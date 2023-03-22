using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.EntityFramework.DataAccess;
using TourPlannerBackEnd.Infrastructure;
using TourPlannerBackEnd.Infrastructure.TourExport;
using TourPlannerBackEnd.Repositories;

namespace TourPlannerBackEnd
{
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
                typeof(TourCsvExportService)
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
