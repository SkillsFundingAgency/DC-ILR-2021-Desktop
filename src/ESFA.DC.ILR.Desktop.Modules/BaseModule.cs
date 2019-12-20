using Autofac;
using ESFA.DC.ILR.Desktop.Utils.Modules;
using ESFA.DC.Telemetry;
using ESFA.DC.Telemetry.Interfaces;

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

            containerBuilder.RegisterType<NullTelemetry>()
              .As<ITelemetry>()
              .InstancePerLifetimeScope();
        }
    }
}
