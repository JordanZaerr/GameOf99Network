using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Windows;
using Caliburn.Micro;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Client.ServiceObjects;

namespace Client
{
    public class ClientBootstrapper : Bootstrapper
    {
        private IWindsorContainer _container;
        private EventAggregator _eventAggregator;

        protected override void Configure()
        {
            _container = new WindsorContainer();
            _eventAggregator = new EventAggregator();

            _container.Register(Component.For<IWindowManager>()
                                         .ImplementedBy<WindowManager>()
                                         .LifestyleSingleton(),
                                Component.For<IEventAggregator>()
                                         .Instance(_eventAggregator)
                                         .LifestyleSingleton(),
                                Component.For<IHostingService>()
                                         .ImplementedBy<HostingService>()
                                         .LifestyleSingleton(),
                                Component.For<IClient>()
                                         .ImplementedBy<Client.ServiceObjects.Client>()
                                         .LifestyleSingleton(), 
                                Component.For<IShell>()
                                         .ImplementedBy<ShellViewModel>()
                                         .LifestyleSingleton(),
                                Classes.FromThisAssembly()
                                       .InNamespace("Client", true)
                                       .WithService.DefaultInterfaces()
                                       .LifestyleTransient());
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.ResolveAll(service).Cast<object>();
        }

        protected override object GetInstance(Type service, string key)
        {
            return string.IsNullOrWhiteSpace(key)
                       ? _container.Resolve(service)
                       : _container.Resolve(key, service);
        }

        protected override void OnStartup(object sender, System.Windows.StartupEventArgs e)
        {
            //Do custom stuff with the window....
            dynamic settings = new ExpandoObject();
            settings.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            //settings.SizeToContent = SizeToContent.Manual;
            //settings.Height = 500;
            //settings.Width = 800;

            IoC.Get<IWindowManager>().ShowWindow(
                IoC.GetInstance(typeof(IShell), null),
                null,
                settings);
        }

        protected override void OnExit(object sender, EventArgs e)
        {
            base.OnExit(sender, e);
            var hostService = _container.Resolve<IHostingService>();
            hostService.StopHost();
        }
    }
}