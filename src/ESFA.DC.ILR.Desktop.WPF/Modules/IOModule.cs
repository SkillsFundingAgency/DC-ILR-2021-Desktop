﻿using Autofac;
using ESFA.DC.FileService;
using ESFA.DC.FileService.Interface;
using ESFA.DC.ILR.Desktop.Stubs;
using ESFA.DC.IO.FileSystem;
using ESFA.DC.IO.FileSystem.Config.Interfaces;
using ESFA.DC.IO.Interfaces;

namespace ESFA.DC.ILR.Desktop.WPF.Modules
{
    public class IOModule : Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<FileSystemFileService>().As<IFileService>();
            containerBuilder.RegisterType<FileSystemKeyValuePersistenceService>()
                .As<IKeyValuePersistenceService>()
                .As<IStreamableKeyValuePersistenceService>();

            var fileSystemKeyValuePersistenceServiceConfiguration = new FileSystemKeyValuePersistenceServiceConfigStub()
            {
                Directory = "Sandbox"
            };


            containerBuilder.RegisterInstance(fileSystemKeyValuePersistenceServiceConfiguration).As<IFileSystemKeyValuePersistenceServiceConfig>();
        }
    }
}