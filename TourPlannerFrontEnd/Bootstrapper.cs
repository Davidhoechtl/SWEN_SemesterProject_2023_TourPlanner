using Autofac;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlannerBackEnd;
using TourPlannerFrontEnd.Infrastructure;
using TourPlannerFrontEnd.Modules.CreateTour;
using TourPlannerFrontEnd.Modules.OverviewTours;

namespace TourPlannerFrontEnd
{
    internal class Bootstrapper : BootstrapperBase
    {
        public Bootstrapper()
        {
            Initialize();
        }

        protected override void OnStartup(object sender, System.Windows.StartupEventArgs e)
        {
            DisplayRootViewForAsync<ShellViewModel>();

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        protected override void Configure()
        {
            ContainerBuilder builder = new ContainerBuilder();

            BackendIoCModule backendIoCModule = new(builder);
            backendIoCModule.Load();

            builder.RegisterType<WindowManager>()
                .AsImplementedInterfaces()
                .SingleInstance();

            builder.RegisterType<EventAggregator>()
                .AsImplementedInterfaces()
                .SingleInstance();

            // autowiring of propertie NavigationHost is nessecary (conductor depends on screens screens depend on navigation)
            // other options are navigatonMessages via Eventaggregator to louse couple those two types
            builder.RegisterTypes(
                    typeof(CreateTourScreenViewModel),
                    typeof(ToursOverviewScreenViewModel)
                )
              .As<Screen>()
              .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies) 
              .SingleInstance();

            builder.RegisterType<ShellViewModel>()
                .AsImplementedInterfaces()
                .AsSelf()
                .SingleInstance();

            Container = builder.Build();
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            var type = typeof(IEnumerable<>).MakeGenericType(service);
            return Container.Resolve(type) as IEnumerable<object>;
        }

        protected override object GetInstance(Type service, string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                if (Container.IsRegistered(service))
                    return Container.Resolve(service);
            }
            else
            {
                if (Container.IsRegisteredWithKey(key, service))
                    return Container.ResolveKeyed(key, service);
            }

            var msgFormat = "Could not locate any instances of contract {0}.";
            var msg = string.Format(msgFormat, key ?? service.Name);
            throw new Exception(msg);
        }

        protected override void BuildUp(object instance)
        {
            Container.InjectProperties(instance);
        }

        private static IContainer Container;
    }
}
