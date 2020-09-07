﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using CommandLine;
using ESFA.DC.ILR.Desktop.ExportDatabase.Console.Interfaces;
using ESFA.DC.ILR.Desktop.ExportDatabase.Console.Modules;
using ESFA.DC.Logging.Interfaces;

namespace ESFA.DC.ILR.Desktop.ExportDatabase.Console
{
    class Program
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
                            container.Resolve<IEntryPoint>().ExecuteAsync(a, CancellationToken.None).Wait();
                        }
                        catch (Exception e)
                        {
                            var logger = container.Resolve<ILogger>();
                            logger.LogError("Exception occured in the export access console app", e);
                            throw;
                        }
                    }
                });
        }

        private static ContainerBuilder BuildContainerBuilder()
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterModule<CommandLineModule>();
            containerBuilder.RegisterModule<ServicesModule>();
            containerBuilder.RegisterModule<LoggingModule>();

            return containerBuilder;
        }
    }
}
