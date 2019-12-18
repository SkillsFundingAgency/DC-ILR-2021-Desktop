using System;
using System.Threading;
using Autofac;
using CommandLine;
using ESFA.DC.ILR.Desktop.CLI.Interface;
using ESFA.DC.ILR.Desktop.CLI.Modules;
using ESFA.DC.ILR.Desktop.CLI.Service;
using ESFA.DC.ILR.Desktop.Modules;
using ESFA.DC.ILR.Desktop.Service.Interface;

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
                            try
                            {
                                container.Resolve<ICliEntryPoint>().ExecuteAsync(a, CancellationToken.None).Wait();
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                        }
                    });
        }

        private static ContainerBuilder BuildContainerBuilder()
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterModule<CommandLineModule>();
            containerBuilder.RegisterModule<BaseModule>();

            var desktopServiceSettings = new DesktopServiceSettings();
            desktopServiceSettings.LoadAsync(CancellationToken.None).Wait();

            containerBuilder.RegisterInstance<DesktopServiceSettings>(desktopServiceSettings).As<IDesktopServiceSettings>();

            return containerBuilder;
        }
    }
}
