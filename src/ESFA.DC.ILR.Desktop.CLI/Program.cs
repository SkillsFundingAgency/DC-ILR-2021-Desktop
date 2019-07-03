using System.Threading;
using Autofac;
using CommandLine;
using ESFA.DC.ILR.Desktop.CLI.Interface;
using ESFA.DC.ILR.Desktop.CLI.Modules;
using ESFA.DC.ILR.Desktop.Modules;

namespace ESFA.DC.ILR.Desktop.CLI
{
    public class Program
    {
        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<CommandLineArguments>(args)
                .WithParsed(a =>
                    {
                        using (var container = BuildContainerBuilder().Build())
                        {
                            container.Resolve<ICliEntryPoint>().Execute(a, CancellationToken.None).Wait();
                        }
                    });
        }

        private static ContainerBuilder BuildContainerBuilder()
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterModule<IlrServicesModule>();
            containerBuilder.RegisterModule<LoggingModule>();
            containerBuilder.RegisterModule<SerializationModule>();
            containerBuilder.RegisterModule<IOModule>();
            containerBuilder.RegisterModule<CommandLineModule>();

            return containerBuilder;
        }
    }
}
