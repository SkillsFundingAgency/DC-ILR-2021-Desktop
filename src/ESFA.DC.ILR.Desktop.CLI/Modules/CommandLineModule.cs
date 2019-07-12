using System.Security.Cryptography;
using System.Threading;
using Autofac;
using ESFA.DC.ILR.Desktop.CLI.Interface;
using ESFA.DC.ILR.Desktop.CLI.Service;
using ESFA.DC.ILR.Desktop.Service.Interface;

namespace ESFA.DC.ILR.Desktop.CLI.Modules
{
    public class CommandLineModule : Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<CliEntryPoint>().As<ICliEntryPoint>();
            containerBuilder.RegisterType<DesktopContextFactory>().As<IDesktopContextFactory>();
            containerBuilder.RegisterType<CommandLineMessengerService>().As<IMessengerService>().SingleInstance();
        }
    }
}
