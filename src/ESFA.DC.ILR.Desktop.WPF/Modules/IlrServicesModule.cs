using Autofac;
using ESFA.DC.DateTimeProvider.Interface;
using ESFA.DC.ILR.DataStore.Desktop;
using ESFA.DC.ILR.DataStore.Desktop.Modules;
using ESFA.DC.ILR.Desktop.Interface;
using ESFA.DC.ILR.Desktop.Service;
using ESFA.DC.ILR.Desktop.Service.Context;
using ESFA.DC.ILR.Desktop.Service.Interface;
using ESFA.DC.ILR.Desktop.Service.Tasks;
using ESFA.DC.ILR.FileValidationService.Desktop;
using ESFA.DC.ILR.FileValidationService.Desktop.Modules;
using ESFA.DC.ILR.ReferenceDataService.Desktop;
using ESFA.DC.ILR.ReferenceDataService.Desktop.Modules;
using ESFA.DC.ILR.ValidationService.Desktop;
using ESFA.DC.ILR.ValidationService.Desktop.Modules;

namespace ESFA.DC.ILR.Desktop.WPF.Modules
{
    public class IlrServicesModule : Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<DateTimeProvider.DateTimeProvider>().As<IDateTimeProvider>();
            containerBuilder.RegisterType<DesktopContextFactory>().As<IDesktopContextFactory>();
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

            containerBuilder.RegisterType<DataStoreDesktopTask>().Keyed<IDesktopTask>(IlrDesktopTaskKeys.DataStore);
            containerBuilder.RegisterModule<DataStoreModule>();

            containerBuilder.RegisterType<PostProcessingDesktopTask>().Keyed<IDesktopTask>(IlrDesktopTaskKeys.PostExecution);
        }
    }
}
