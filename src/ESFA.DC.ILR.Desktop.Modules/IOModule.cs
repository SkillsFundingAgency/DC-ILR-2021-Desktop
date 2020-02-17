﻿using Autofac;
using ESFA.DC.FileService;
using ESFA.DC.FileService.Interface;
using ESFA.DC.ILR.Desktop.Internal.Interface.Services;
using ESFA.DC.ILR.Desktop.Service;
using ESFA.DC.ILR.Desktop.Service.Interface;
using ESFA.DC.ILR.Desktop.WPF.Config;
using ESFA.DC.IO.FileSystem;
using ESFA.DC.IO.FileSystem.Config.Interfaces;
using ESFA.DC.IO.Interfaces;

namespace ESFA.DC.ILR.Desktop.Modules
{
    public class IOModule : Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<FileSystemFileService>().As<IFileService>();
            containerBuilder.RegisterType<FileSystemKeyValuePersistenceService>()
                .As<IKeyValuePersistenceService>()
                .As<IStreamableKeyValuePersistenceService>();

            containerBuilder.RegisterType<DecompressionService>().As<IDecompressionService>();

            var fileSystemKeyValuePersistenceServiceConfiguration = new FileSystemKeyValuePersistenceServiceConfig()
            {
                Directory = "Sandbox"
            };

            containerBuilder.RegisterInstance(fileSystemKeyValuePersistenceServiceConfiguration).As<IFileSystemKeyValuePersistenceServiceConfig>();

            containerBuilder.RegisterType<AssemblyService>().As<IAssemblyService>();
            containerBuilder.RegisterType<ReleaseVersionInformationService>().As<IReleaseVersionInformationService>();
            containerBuilder.RegisterType<ReferenceDataVersionInformationService>().As<IReferenceDataVersionInformationService>();
            containerBuilder.RegisterType<FeatureSwitchService>().As<IFeatureSwitchService>();
        }
    }
}
