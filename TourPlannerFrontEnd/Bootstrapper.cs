using Autofac;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlannerBackEnd;
using TourPlannerFrontEnd.Infrastructure;

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
            InitializeIoCContainer();
        }

        private void InitializeIoCContainer()
        {
            ContainerBuilder containerBuilder = new ContainerBuilder();

            BackendIoCModule backendIoCModule = new(containerBuilder);
            backendIoCModule.Load();

            containerBuilder.Build();
        }
    }
}
