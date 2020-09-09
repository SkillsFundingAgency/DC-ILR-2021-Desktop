using System;
using System.Collections.Immutable;
using System.Linq;
using Autofac;
using Autofac.Features.Indexed;
using ESFA.DC.DateTimeProvider.Interface;
using ESFA.DC.FileService;
using ESFA.DC.FileService.Interface;
using ESFA.DC.ILR.DataStore.Desktop;
using ESFA.DC.ILR.DataStore.Desktop.Modules;
using ESFA.DC.ILR.Desktop.ExportDatabase.Console.Tasks;
using ESFA.DC.ILR.Desktop.Interface;
using ESFA.DC.ILR.Desktop.Messaging;
using ESFA.DC.ILR.Desktop.Service.Pipeline;
using ESFA.DC.ILR.Desktop.Service.Pipeline.Interface;
using ESFA.DC.IO.FileSystem;
using ESFA.DC.IO.FileSystem.Config.Interfaces;
using ESFA.DC.IO.Interfaces;
using ESFA.DC.Serialization.Interfaces;
using ESFA.DC.Serialization.Json;
using ESFA.DC.Serialization.Xml;

namespace ESFA.DC.ILR.Desktop.ExportDatabase.Console.Modules
{
    public class ServicesModule : Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<DateTimeProvider.DateTimeProvider>().As<IDateTimeProvider>();
            containerBuilder.RegisterType<ExportJobPipelineProvider>().As<IIlrPipelineProvider>();
            containerBuilder.RegisterType<DesktopTaskExecutionService>().As<IDesktopTaskExecutionService>();
            containerBuilder.RegisterType<IlrDesktopService>().As<IIlrDesktopService>();

            containerBuilder.RegisterType<BuildDataStoreDesktopTask>().Keyed<IDesktopTask>(IlrDesktopTaskKeys.DatabaseCreate);
            containerBuilder.RegisterModule<BuildDataStoreModule>();

            containerBuilder.RegisterType<DataStoreDesktopTask>().Keyed<IDesktopTask>(IlrDesktopTaskKeys.DataStore);
            containerBuilder.RegisterModule<DataStoreModule>();

            containerBuilder.RegisterType<BuildMdbDesktopTask>().Keyed<IDesktopTask>(IlrDesktopTaskKeys.MdbCreate);
            containerBuilder.RegisterModule<BuildMdbModule>();

            containerBuilder.RegisterType<MdbDesktopTask>().Keyed<IDesktopTask>(IlrDesktopTaskKeys.MdbExport);
            containerBuilder.RegisterModule<MdbModule>();

            containerBuilder.RegisterType<PublishMdbDesktopTask>().Keyed<IDesktopTask>(IlrDesktopTaskKeys.MdbPublish);

            containerBuilder.RegisterType<JsonSerializationService>().As<IJsonSerializationService>().As<ISerializationService>();
            containerBuilder.RegisterType<XmlSerializationService>().As<IXmlSerializationService>();

            containerBuilder.RegisterType<ContextMutatorExecutor>().As<IContextMutatorExecutor>();
            containerBuilder.RegisterType<CommandLineMessengerService>().As<IMessengerService>();

            containerBuilder.RegisterType<FileSystemFileService>().As<IFileService>();
            containerBuilder.RegisterType<FileSystemKeyValuePersistenceService>()
                .As<IKeyValuePersistenceService>()
                .As<IStreamableKeyValuePersistenceService>();

            var fileSystemKeyValuePersistenceServiceConfiguration = new FileSystemKeyValuePersistenceServiceConfig()
            {
                Directory = "Sandbox"
            };

            containerBuilder.RegisterInstance(fileSystemKeyValuePersistenceServiceConfiguration).As<IFileSystemKeyValuePersistenceServiceConfig>();

            containerBuilder
                .RegisterAdapter<IIndex<IlrDesktopTaskKeys, Func<Module>>, IImmutableDictionary<IlrDesktopTaskKeys, Func<Module>>>(
                    idx =>
                    {
                        return Enum.GetValues(typeof(IlrDesktopTaskKeys)).Cast<IlrDesktopTaskKeys>()
                            .Where(x => idx.TryGetValue(x, out _))
                            .ToImmutableDictionary(i => i, v => idx[v]);
                    });
        }
    }
}
