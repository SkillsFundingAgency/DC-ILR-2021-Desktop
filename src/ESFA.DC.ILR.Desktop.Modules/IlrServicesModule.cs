using System;
using System.Collections.Immutable;
using System.Linq;
using Autofac;
using Autofac.Features.Indexed;
using ESFA.DC.DateTimeProvider.Interface;
using ESFA.DC.ILR.DataStore.Desktop;
using ESFA.DC.ILR.DataStore.Desktop.Modules;
using ESFA.DC.ILR.Desktop.Interface;
using ESFA.DC.ILR.Desktop.Service;
using ESFA.DC.ILR.Desktop.Service.Interface;
using ESFA.DC.ILR.Desktop.Service.Mutator;
using ESFA.DC.ILR.Desktop.Service.Tasks;
using ESFA.DC.ILR.FileValidationService.Desktop;
using ESFA.DC.ILR.FileValidationService.Desktop.Modules;
using ESFA.DC.ILR.FundingService.Desktop;
using ESFA.DC.ILR.FundingService.Desktop.Modules;
using ESFA.DC.ILR.ReferenceDataService.Desktop;
using ESFA.DC.ILR.ReferenceDataService.Desktop.Modules;
using ESFA.DC.ILR.ReportService.Desktop;
using ESFA.DC.ILR.ReportService.Desktop.Modules;
using ESFA.DC.ILR.ValidationService.Desktop;
using ESFA.DC.ILR.ValidationService.Desktop.Modules;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ESFA.DC.ILR.Desktop.Modules
{
    public class IlrServicesModule : Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<DateTimeProvider.DateTimeProvider>().As<IDateTimeProvider>();
            containerBuilder.RegisterType<IlrPipelineProvider>().As<IIlrPipelineProvider>();
            containerBuilder.RegisterType<DesktopTaskExecutionService>().As<IDesktopTaskExecutionService>();
            containerBuilder.RegisterType<IlrDesktopService>().As<IIlrDesktopService>();

            containerBuilder.RegisterType<PreProcessingDesktopTask>().Keyed<IDesktopTask>(IlrDesktopTaskKeys.PreExecution);

            containerBuilder.RegisterType<BuildDataStoreDesktopTask>().Keyed<IDesktopTask>(IlrDesktopTaskKeys.DatabaseCreate);
            containerBuilder.RegisterType<BuildDataStoreModule>().Keyed<Module>(IlrDesktopTaskKeys.DatabaseCreate);

//            containerBuilder.RegisterModule<BuildDataStoreModule>();

            containerBuilder.RegisterType<FileValidationServiceDesktopTask>().Keyed<IDesktopTask>(IlrDesktopTaskKeys.FileValidationService);
            containerBuilder.RegisterType<FileValidationServiceDesktopModule>().Keyed<Module>(IlrDesktopTaskKeys.FileValidationService);
//            containerBuilder.RegisterModule<FileValidationServiceDesktopModule>();

            containerBuilder.RegisterType<ReferenceDataServiceDesktopTask>().Keyed<IDesktopTask>(IlrDesktopTaskKeys.ReferenceDataService);
            containerBuilder.RegisterType<ReferenceDataServiceDesktopTaskModule>().Keyed<Module>(IlrDesktopTaskKeys.ReferenceDataService);
//            containerBuilder.RegisterModule<ReferenceDataServiceDesktopTaskModule>();

            containerBuilder.RegisterType<ValidationServiceDesktopTask>().Keyed<IDesktopTask>(IlrDesktopTaskKeys.ValidationService);
            containerBuilder.RegisterType<ValidationServiceDesktopModule>()
                .Keyed<Module>(IlrDesktopTaskKeys.ValidationService);
//            containerBuilder.RegisterModule<ValidationServiceDesktopModule>();

            containerBuilder.RegisterType<FundingServiceDesktopTask>().Keyed<IDesktopTask>(IlrDesktopTaskKeys.FundingService);
            containerBuilder.RegisterType<FundingServiceDesktopModule>()
                .Keyed<Module>(IlrDesktopTaskKeys.FundingService);
//            containerBuilder.RegisterModule<FundingServiceDesktopModule>();

            containerBuilder.RegisterType<DataStoreDesktopTask>().Keyed<IDesktopTask>(IlrDesktopTaskKeys.DataStore);
            containerBuilder.RegisterType<DataStoreModule>().Keyed<Module>(IlrDesktopTaskKeys.DataStore);
//            containerBuilder.RegisterModule<DataStoreModule>();

            containerBuilder.RegisterType<ReportServiceDesktopTask>().Keyed<IDesktopTask>(IlrDesktopTaskKeys.ReportService);
            containerBuilder.RegisterType<ReportServiceDesktopModule>().Keyed<Module>(IlrDesktopTaskKeys.ReportService);
//            containerBuilder.RegisterModule<ReportServiceDesktopModule>();

            containerBuilder.RegisterType<PostProcessingDesktopTask>().Keyed<IDesktopTask>(IlrDesktopTaskKeys.PostExecution);

            containerBuilder.RegisterType<BuildMdbDesktopTask>().Keyed<IDesktopTask>(IlrDesktopTaskKeys.MdbCreate);
            containerBuilder.RegisterType<BuildMdbModule>().Keyed<Module>(IlrDesktopTaskKeys.MdbCreate);
//            containerBuilder.RegisterModule<BuildMdbModule>();

            containerBuilder.RegisterType<MdbDesktopTask>().Keyed<IDesktopTask>(IlrDesktopTaskKeys.MdbExport);
            containerBuilder.RegisterType<MdbModule>().Keyed<Module>(IlrDesktopTaskKeys.MdbExport);

//            containerBuilder.RegisterModule<MdbModule>();

            containerBuilder.RegisterType<PublishMdbDesktopTask>().Keyed<IDesktopTask>(IlrDesktopTaskKeys.MdbPublish);

            containerBuilder.RegisterType<ContextMutatorExecutor>().As<IContextMutatorExecutor>();
            containerBuilder.RegisterType<SchemaErrorContextMutator>().Keyed<IContextMutator>(ContextMutatorKeys.FileFailure);

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
