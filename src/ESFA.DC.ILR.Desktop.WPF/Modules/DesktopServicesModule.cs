using System.Threading;
using Autofac;
using ESFA.DC.ILR.Desktop.Service;
using ESFA.DC.ILR.Desktop.Service.Interface;
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
            containerBuilder.RegisterType<WindowsProcessService>().As<IWindowsProcessService>();
            containerBuilder.RegisterType<ReleaseInformationService>().As<IReleaseInformationService>();

            containerBuilder.Register(c =>
            {
                var settings = new DesktopServiceSettings();
                settings.LoadAsync(CancellationToken.None).Wait();
                return settings;
            }).As<IDesktopServiceSettings>().SingleInstance();
        }
    }
}
