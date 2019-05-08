using System.Windows;
using Autofac;
using Autofac.Extras.CommonServiceLocator;
using CommonServiceLocator;
using ESFA.DC.ILR.Desktop.WPF.Modules;

namespace ESFA.DC.ILR.Desktop.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var containerBuilder = BuildContainerBuilder();

            var container = containerBuilder.Build();

            ServiceLocator.SetLocatorProvider(() => new AutofacServiceLocator(container));

            base.OnStartup(e);
        }

        private ContainerBuilder BuildContainerBuilder()
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterModule<DesktopServicesModule>();
            containerBuilder.RegisterModule<ViewModelsModule>();
            containerBuilder.RegisterModule<IlrServicesModule>();
            
            // Common Service Registration
            containerBuilder.RegisterModule<LoggingModule>();
            containerBuilder.RegisterModule<SerializationModule>();
            containerBuilder.RegisterModule<IOModule>();

            return containerBuilder;
        }
    }
}
