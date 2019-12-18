using Autofac;
using ESFA.DC.ILR.Desktop.Utils.Modules;

namespace ESFA.DC.ILR.Desktop.Modules
{
    public class BaseModule : Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterModule<IlrServicesModule>();
            containerBuilder.RegisterModule<IOModule>();
            containerBuilder.RegisterModule<LoggingModule>();
            containerBuilder.RegisterModule<SerializationModule>();
            containerBuilder.RegisterModule<PollyModule>();
        }
    }
}
