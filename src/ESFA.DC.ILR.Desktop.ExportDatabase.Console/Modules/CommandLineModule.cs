using Autofac;
using ESFA.DC.ILR.Desktop.ExportDatabase.Console.Interfaces;
using ESFA.DC.ILR.Desktop.ExportDatabase.Console.Services;

namespace ESFA.DC.ILR.Desktop.ExportDatabase.Console.Modules
{
    public class CommandLineModule : Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<EntryPoint>().As<IEntryPoint>();
            containerBuilder.RegisterType<DesktopContextFactory>().As<IDesktopContextFactory>();
        }
    }
}
