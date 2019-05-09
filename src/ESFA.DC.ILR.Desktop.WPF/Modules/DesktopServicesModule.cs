using Autofac;
using ESFA.DC.ILR.Desktop.Service.Interface;
using ESFA.DC.ILR.Desktop.Stubs;
using ESFA.DC.ILR.Desktop.WPF.Service;
using ESFA.DC.ILR.Desktop.WPF.Service.Interface;

namespace ESFA.DC.ILR.Desktop.WPF.Modules
{
    public class DesktopServicesModule : Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {          
            containerBuilder.RegisterType<MessengerService>().As<IMessengerService>().SingleInstance();
            containerBuilder.RegisterType<WindowService>().As<IWindowService>();
            containerBuilder.RegisterType<DialogInteractionService>().As<IDialogInteractionService>();
            containerBuilder.RegisterType<ConfigServiceStub>().As<IConfigService>().SingleInstance();
            containerBuilder.RegisterType<SettingsServiceStub>().As<ISettingsService>();
        }
    }
}
