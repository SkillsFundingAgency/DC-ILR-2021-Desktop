using Autofac;
using ESFA.DC.DateTimeProvider.Interface;
using ESFA.DC.ILR.Desktop.Interface;
using ESFA.DC.ILR.Desktop.Service.Context;
using ESFA.DC.ILR.Desktop.Service.Interface;
using ESFA.DC.ILR.Desktop.Stubs;
using ESFA.DC.ILR.Desktop.Stubs.Tasks;

namespace ESFA.DC.ILR.Desktop.WPF.Modules
{
    public class IlrServicesModule : Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<DateTimeProvider.DateTimeProvider>().As<IDateTimeProvider>();
            containerBuilder.RegisterType<DesktopContextFactory>().As<IDesktopContextFactory>();
            containerBuilder.RegisterType<IlrDesktopServiceStub>().As<IIlrDesktopService>();
            containerBuilder.RegisterType<DesktopTaskStub>().As<IDesktopTask>();
        }
    }
}
