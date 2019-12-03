﻿using System.Threading;
using Autofac;
using ESFA.DC.ILR.Desktop.Internal.Interface.Configuration;
using ESFA.DC.ILR.Desktop.Internal.Interface.Services;
using ESFA.DC.ILR.Desktop.Service;
using ESFA.DC.ILR.Desktop.Service.APIClient;
using ESFA.DC.ILR.Desktop.Service.Factories;
using ESFA.DC.ILR.Desktop.Service.Interface;
using ESFA.DC.ILR.Desktop.WPF.Config;
using ESFA.DC.ILR.Desktop.WPF.Service;
using ESFA.DC.ILR.Desktop.WPF.Service.Interface;
using ESFA.DC.ILR.Desktop.WPF.ViewModel;
using ESFA.DC.Telemetry;
using ESFA.DC.Telemetry.Interfaces;

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
            containerBuilder.RegisterType<VersionMessageFactory>().As<IVersionMessageFactory>();

            containerBuilder.RegisterType<ApplicationVersionClientStub>().As<IApplicationVersionClient>();
            containerBuilder.RegisterType<VersionService>().As<IVersionService>();
            containerBuilder.RegisterType<VersionMediatorService>().As<IVersionMediatorService>();

            containerBuilder.RegisterType<DesktopServiceConfiguration>().As<IServiceConfiguration>();
            containerBuilder.RegisterType<APIConfiguration>().As<IAPIConfiguration>();

            containerBuilder.RegisterType<DesktopContextFactory>().As<IDesktopContextFactory>();

            containerBuilder.Register(c => new ViewModelLocator()).SingleInstance();

            containerBuilder.Register(c =>
            {
                var settings = new DesktopServiceSettings();
                settings.LoadAsync(CancellationToken.None).Wait();
                return settings;
            }).As<IDesktopServiceSettings>().SingleInstance();

            containerBuilder.RegisterType<FeatureSwitchService>().As<IFeatureSwitchService>();

            containerBuilder.RegisterType<NullTelemetry>()
                .As<ITelemetry>()
                .InstancePerLifetimeScope();
        }
    }
}
