using Autofac;
using ESFA.DC.DateTimeProvider.Interface;
using ESFA.DC.ILR.Desktop.Interface;
using ESFA.DC.ILR.Desktop.Service.Context;
using ESFA.DC.ILR.Desktop.Service.Interface;
using ESFA.DC.ILR.Desktop.Service.Tasks;
using ESFA.DC.ILR.Desktop.Stubs;
using ESFA.DC.ILR.Desktop.Stubs.Tasks;
using ESFA.DC.ILR.FileValidationService.Desktop.Modules;

namespace ESFA.DC.ILR.Desktop.WPF.Modules
{
    public class IlrServicesModule : Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<DateTimeProvider.DateTimeProvider>().As<IDateTimeProvider>();
            containerBuilder.RegisterType<DesktopContextFactory>().As<IDesktopContextFactory>();
            containerBuilder.RegisterType<IlrDesktopServiceStub>().As<IIlrDesktopService>();

            containerBuilder.RegisterType<DesktopTaskStub>()
                .Keyed<IDesktopTask>(IlrDesktopTaskKeys.DatabaseCreate)
                .Keyed<IDesktopTask>(IlrDesktopTaskKeys.ValidationService)
                .Keyed<IDesktopTask>(IlrDesktopTaskKeys.FundingService)
                .Keyed<IDesktopTask>(IlrDesktopTaskKeys.DataStore)
                .Keyed<IDesktopTask>(IlrDesktopTaskKeys.ReportService)
                .Keyed<IDesktopTask>(IlrDesktopTaskKeys.PostExecution);

            containerBuilder.RegisterType<PreProcessingDesktopTask>().Keyed<IDesktopTask>(IlrDesktopTaskKeys.PreExecution);

            containerBuilder.RegisterType<FileValidationService.Desktop.DesktopTask>().Keyed<IDesktopTask>(IlrDesktopTaskKeys.FileValidationService);
            containerBuilder.RegisterModule<FileValidationServiceDesktopModule>();
        }
    }
}
