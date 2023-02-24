using Autofac;
using MTCG.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlannerBackEnd.Infrastructure;
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
            RegisterIoCModules(builder);
            RegisterRepositories(builder);
        }

        private void RegisterInfrastructure(ContainerBuilder builder)
        {
            builder.RegisterTypes(
                typeof(ApiKeyLoader),
                typeof(MapQuestApiService)
            )
            .SingleInstance();
        }

        private void RegisterRepositories(ContainerBuilder builder)
        {
            builder.RegisterTypes(
                typeof(TourRepository)
            )
            .SingleInstance();
        }

        private void RegisterIoCModules(ContainerBuilder builder)
        {
            DatabaseIoC databaeIoc = new(builder);
            databaeIoc.Load();
        }

        private readonly ContainerBuilder builder;
    }
}
