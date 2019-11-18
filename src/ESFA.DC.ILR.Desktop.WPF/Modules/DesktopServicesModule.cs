using System.Threading;
using Autofac;
using ESFA.DC.ILR.Desktop.Interface;
using ESFA.DC.ILR.Desktop.Interface.Configuration;
using ESFA.DC.ILR.Desktop.Interface.Services;
using ESFA.DC.ILR.Desktop.Service;
using ESFA.DC.ILR.Desktop.Service.APIClient;
using ESFA.DC.ILR.Desktop.Service.Factories;
using ESFA.DC.ILR.Desktop.Service.Interface;
using ESFA.DC.ILR.Desktop.WPF.Config;
using ESFA.DC.ILR.Desktop.WPF.Service;
using ESFA.DC.ILR.Desktop.WPF.Service.Interface;

namespace ESFA.DC.ILR.Desktop.WPF.Modules
{
    public class DesktopServicesModule : Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<MessengerService>().As<IMessengerService>().SingleInstance();
            containerBuilder.RegisterType<WindowService>().As<IWindowService>();
            containerBuilder.RegisterType<DialogInteractionService>().As<IDialogInteractionService>();
            containerBuilder.RegisterType<WindowsProcessService>().As<IWindowsProcessService>();
            containerBuilder.RegisterType<ReferenceDataVersionInformationService>().As<IReferenceDataVersionInformationService>();
            containerBuilder.RegisterType<ReportFilterService>().As<IReportFilterService>().SingleInstance();

            containerBuilder.RegisterType<ApplicationVersionResultFactory>().As<IApplicationVersionResultFactory>();
            containerBuilder.RegisterType<APIClientFactory>().As<IAPIClientFactory>();
            containerBuilder.RegisterType<VersionFactory>().As<IVersionFactory>();

            containerBuilder.RegisterType<ApplicationVersionClient>().As<IApplicationVersionClient>();
            containerBuilder.RegisterType<VersionService>().As<IVersionService>();

            containerBuilder.RegisterType<DesktopServiceConfiguration>().As<IServiceConfiguration>();
            containerBuilder.RegisterType<APIConfiguration>().As<IAPIConfiguration>();

            containerBuilder.RegisterType<DesktopContextFactory>().As<IDesktopContextFactory>();

            containerBuilder.Register(c =>
            {
                var settings = new DesktopServiceSettings();
                settings.LoadAsync(CancellationToken.None).Wait();
                return settings;
            }).As<IDesktopServiceSettings>().SingleInstance();

            containerBuilder.RegisterType<FeatureSwitchService>().As<IFeatureSwitchService>();
        }
    }
}
