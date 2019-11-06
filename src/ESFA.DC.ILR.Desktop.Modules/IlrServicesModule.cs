using Autofac;
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
            containerBuilder.RegisterModule<BuildDataStoreModule>();

            containerBuilder.RegisterType<FileValidationServiceDesktopTask>().Keyed<IDesktopTask>(IlrDesktopTaskKeys.FileValidationService);
            containerBuilder.RegisterModule<FileValidationServiceDesktopModule>();

            containerBuilder.RegisterType<ReferenceDataServiceDesktopTask>().Keyed<IDesktopTask>(IlrDesktopTaskKeys.ReferenceDataService);
            containerBuilder.RegisterModule<ReferenceDataServiceDesktopTaskModule>();

            containerBuilder.RegisterType<ValidationServiceDesktopTask>().Keyed<IDesktopTask>(IlrDesktopTaskKeys.ValidationService);
            containerBuilder.RegisterModule<ValidationServiceDesktopModule>();

            containerBuilder.RegisterType<FundingServiceDesktopTask>().Keyed<IDesktopTask>(IlrDesktopTaskKeys.FundingService);
            containerBuilder.RegisterModule<FundingServiceDesktopModule>();

            containerBuilder.RegisterType<DataStoreDesktopTask>().Keyed<IDesktopTask>(IlrDesktopTaskKeys.DataStore);
            containerBuilder.RegisterModule<DataStoreModule>();

            containerBuilder.RegisterType<ReportServiceDesktopTask>().Keyed<IDesktopTask>(IlrDesktopTaskKeys.ReportService);
            containerBuilder.RegisterModule<ReportServiceDesktopModule>();

            containerBuilder.RegisterType<PostProcessingDesktopTask>().Keyed<IDesktopTask>(IlrDesktopTaskKeys.PostExecution);

            containerBuilder.RegisterType<BuildMdbDesktopTask>().Keyed<IDesktopTask>(IlrDesktopTaskKeys.MdbCreate);
            containerBuilder.RegisterModule<BuildMdbModule>();

            containerBuilder.RegisterType<MdbDesktopTask>().Keyed<IDesktopTask>(IlrDesktopTaskKeys.MdbExport);
            containerBuilder.RegisterModule<MdbModule>();

            containerBuilder.RegisterType<PublishMdbDesktopTask>().Keyed<IDesktopTask>(IlrDesktopTaskKeys.MdbPublish);

            containerBuilder.RegisterType<ContextMutatorExecutor>().As<IContextMutatorExecutor>();
            containerBuilder.RegisterType<SchemaErrorContextMutator>().Keyed<IContextMutator>(ContextMutatorKeys.SchemaError);
        }
    }
}
