using Autofac;
using ESFA.DC.ILR.Desktop.Stubs;
using ESFA.DC.Logging.Interfaces;

namespace ESFA.DC.ILR.Desktop.WPF.Modules
{
    public class LoggingModule : Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<LoggerStub>().As<ILogger>().InstancePerLifetimeScope();
        }
    }
}
