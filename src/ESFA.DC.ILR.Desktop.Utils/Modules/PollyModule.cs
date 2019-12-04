using Autofac;
using ESFA.DC.ILR.Desktop.Utils.Polly;
using ESFA.DC.ILR.Desktop.Utils.Polly.Interface;

namespace ESFA.DC.ILR.Desktop.Utils.Modules
{
    public class PollyModule : Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<PollyPolicies>().As<IPollyPolicies>();
        }
    }
}
