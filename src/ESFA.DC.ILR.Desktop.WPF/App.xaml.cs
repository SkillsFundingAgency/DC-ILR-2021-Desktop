using System.Windows;
using Autofac;
using Autofac.Extras.CommonServiceLocator;
using CommonServiceLocator;
using ESFA.DC.ILR.Desktop.Modules;
using ESFA.DC.ILR.Desktop.Service;
using ESFA.DC.ILR.Desktop.Service.Interface;
using ESFA.DC.ILR.Desktop.WPF.Modules;
using ESFA.DC.ILR.Desktop.WPF.ViewModel;
using ESFA.DC.Logging.Interfaces;

namespace ESFA.DC.ILR.Desktop.WPF
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var containerBuilder = BuildContainerBuilder();

            var container = containerBuilder.Build();

            ServiceLocator.SetLocatorProvider(() => new AutofacServiceLocator(container));

            var logger = container.Resolve<ILogger>();

            var featureSwitch = container.Resolve<IFeatureSwitchService>();

            if (featureSwitch.VersionUpdate)
            {
                container.Resolve<ViewModelLocator>();
                var versionMediatorService = container.Resolve<IVersionMediatorService>();
                versionMediatorService.CheckForUpdates();
            }

            logger.LogInfo("ILR Desktop Application Start");

            base.OnStartup(e);
        }

        private ContainerBuilder BuildContainerBuilder()
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterModule<DesktopServicesModule>();
            containerBuilder.RegisterModule<ViewModelsModule>();

            // Common Service Registration
            containerBuilder.RegisterModule<IlrServicesModule>();
            containerBuilder.RegisterModule<LoggingModule>();
            containerBuilder.RegisterModule<SerializationModule>();
            containerBuilder.RegisterModule<IOModule>();

            return containerBuilder;
        }
    }
}
