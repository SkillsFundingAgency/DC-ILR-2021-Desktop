using Autofac;
using ESFA.DC.ILR.Desktop.Internal.Interface.Services;
using ESFA.DC.ILR.Desktop.Models;
using ESFA.DC.ILR.Desktop.Service;
using ESFA.DC.ILR.Desktop.Service.APIClient;
using ESFA.DC.ILR.Desktop.Service.Factories;
using ESFA.DC.ILR.Desktop.Service.ReferenceData;
using System.IO;

namespace ESFA.DC.ILR.Desktop.Modules
{
    public class APIModule : Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<DesktopReferenceDataDownloadService>().As<IDesktopReferenceDataDownloadService>().SingleInstance();
            containerBuilder.RegisterType<ApplicationVersionResultFactory>().As<IAPIResultFactory<ApplicationVersionResult>>();
            containerBuilder.RegisterType<ApplicationVersionClientFactory>().As<IAPIClientFactory<ApplicationVersion>>();
            containerBuilder.RegisterType<ApplicationVersionClient>().As<IApplicationVersionResultClient>();
            containerBuilder.RegisterType<ReferenceDataClient>().As<IReferenceDataResultClient>();
            containerBuilder.RegisterType<ReferenceDataClientFactory>().As<IAPIClientFactory<Stream>>();

            containerBuilder.RegisterType<VersionMediatorService>().As<IVersionMediatorService>();
            containerBuilder.RegisterType<VersionFactory>().As<IVersionFactory>();
            containerBuilder.RegisterType<VersionMessageFactory>().As<IVersionMessageFactory>();
            containerBuilder.RegisterType<VersionService>().As<IVersionService>();
        }
    }
}
